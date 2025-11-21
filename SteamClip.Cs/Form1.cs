using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Diagnostics;
using System.Web.Script.Serialization;

namespace SteamClip
{
    public partial class Form1 : Form
    {
        private static readonly HttpClient client = new HttpClient();
        private const string CURRENT_VERSION = "v3.0";
        public readonly string CONFIG_DIR;
        private readonly string CONFIG_FILE;
        private readonly string GAME_IDS_FILE;
        private readonly string STEAM_APP_DETAILS_URL = "https://store.steampowered.com/api/appdetails";

        private bool _is_cancelled = false;
        private int clip_index = 0;
        private List<string> clip_folders = new List<string>();
        private List<string> original_clip_folders = new List<string>();
        public Dictionary<string, string> game_ids = new Dictionary<string, string>();
        private Dictionary<string, string> _custom_record_cache = new Dictionary<string, string>();
        private string default_dir;
        public string export_dir;
        private string prev_steamid;
        private string prev_media_type;

        private HashSet<string> selected_clips = new HashSet<string>();

        public Form1()
        {
            InitializeComponent();

            CONFIG_DIR = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SteamClip");
            CONFIG_FILE = Path.Combine(CONFIG_DIR, "SteamClip.conf");
            GAME_IDS_FILE = Path.Combine(CONFIG_DIR, "GameIDs.json");

            Directory.CreateDirectory(CONFIG_DIR);

            LoadConfig();

            if (string.IsNullOrEmpty(default_dir))
            {
                // In a real implementation, we would prompt the user to select the Steam userdata directory.
                // For now, we'll assume a default path for development purposes.
                default_dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Steam", "userdata");
            }

            SaveConfig();
            LoadGameIDs();

            media_type_combo.Items.AddRange(new object[] { "All Clips", "Manual Clips", "Background Clips" });
            media_type_combo.SelectedIndex = 0;

            steamid_combo.SelectedIndexChanged += on_steamid_selected;
            gameid_combo.SelectedIndexChanged += filter_clips_by_gameid;
            media_type_combo.SelectedIndexChanged += filter_media_type;
            settings_button.Click += open_settings;
            clear_selection_button.Click += clear_selection;
            export_all_button.Click += export_all;
            prev_button.Click += show_previous_clips;
            next_button.Click += show_next_clips;
            convert_button.Click += convert_clip;
            exit_button.Click += (sender, e) => Close();

            populate_steamid_dirs();
            // perform_update_check(); // This will be implemented later.
        }

        private void LoadConfig()
        {
            export_dir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (File.Exists(CONFIG_FILE))
            {
                var lines = File.ReadAllLines(CONFIG_FILE);
                foreach (var line in lines)
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim();
                        if (key == "userdata_path")
                        {
                            default_dir = value;
                        }
                        else if (key == "export_path")
                        {
                            export_dir = value;
                        }
                    }
                }
            }
        }

        public void SaveConfig()
        {
            var lines = new List<string>
            {
                $"userdata_path={default_dir}",
                $"export_path={export_dir}"
            };
            File.WriteAllLines(CONFIG_FILE, lines);
        }

        private void LoadGameIDs()
        {
            if (File.Exists(GAME_IDS_FILE))
            {
                var json = File.ReadAllText(GAME_IDS_FILE);
                var serializer = new JavaScriptSerializer();
                game_ids = serializer.Deserialize<Dictionary<string, string>>(json);
            }
        }

        private void SaveGameIDs()
        {
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(game_ids);
            File.WriteAllText(GAME_IDS_FILE, json);
        }

        private void on_steamid_selected(object sender, EventArgs e)
        {
            var selected_steamid = steamid_combo.SelectedItem.ToString();
            if (selected_steamid != prev_steamid)
            {
                prev_steamid = selected_steamid;
                filter_media_type(sender, e);
            }
        }

        private void filter_clips_by_gameid(object sender, EventArgs e)
        {
            var selected_index = gameid_combo.SelectedIndex;
            if (selected_index == 0)
            {
                clip_folders = original_clip_folders.ToList();
            }
            else
            {
                var selected_game_id = ((KeyValuePair<string, string>)gameid_combo.SelectedItem).Key;
                clip_folders = original_clip_folders.Where(f => f.Contains($"_{selected_game_id}_")).ToList();
            }
            clip_index = 0;
            display_clips();
        }

        private void filter_media_type(object sender, EventArgs e)
        {
            var selected_media_type = media_type_combo.SelectedItem.ToString();
            if (selected_media_type != prev_media_type)
            {
                prev_media_type = selected_media_type;
            }

            var selected_steamid = steamid_combo.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selected_steamid)) return;

            var userdata_dir = Path.Combine(default_dir, selected_steamid);
            var clips_dir_default = Path.Combine(userdata_dir, "gamerecordings", "clips");
            var video_dir_default = Path.Combine(userdata_dir, "gamerecordings", "video");

            var clip_folders_list = new List<string>();
            var video_folders_list = new List<string>();

            if (Directory.Exists(clips_dir_default))
            {
                clip_folders_list.AddRange(Directory.GetDirectories(clips_dir_default, "*_*"));
            }
            if (Directory.Exists(video_dir_default))
            {
                video_folders_list.AddRange(Directory.GetDirectories(video_dir_default, "*_*"));
            }

            if (selected_media_type == "All Clips")
            {
                clip_folders = clip_folders_list.Concat(video_folders_list).ToList();
            }
            else if (selected_media_type == "Manual Clips")
            {
                clip_folders = clip_folders_list;
            }
            else if (selected_media_type == "Background Recordings")
            {
                clip_folders = video_folders_list;
            }

            clip_folders = clip_folders.OrderByDescending(f => extract_datetime_from_folder_name(f)).ToList();
            original_clip_folders = clip_folders.ToList();
            populate_gameid_combo();
            display_clips();
        }

        private void open_settings(object sender, EventArgs e)
        {
            using (var settingsWindow = new SettingsWindow(this))
            {
                settingsWindow.ShowDialog();
            }
        }

        private void clear_selection(object sender, EventArgs e)
        {
            selected_clips.Clear();
            foreach (Control control in clip_grid.Controls)
            {
                if (control is Button button)
                {
                    button.BackColor = SystemColors.Control;
                }
            }
            convert_button.Enabled = false;
            clear_selection_button.Enabled = false;
        }

        private void export_all(object sender, EventArgs e)
        {
            process_clips(clip_folders);
        }

        private void show_previous_clips(object sender, EventArgs e)
        {
            if (clip_index - 6 >= 0)
            {
                clip_index -= 6;
                display_clips();
            }
        }

        private void show_next_clips(object sender, EventArgs e)
        {
            if (clip_index + 6 < clip_folders.Count)
            {
                clip_index += 6;
                display_clips();
            }
        }

        private void convert_clip(object sender, EventArgs e)
        {
            process_clips(selected_clips.ToList());
        }

        private void process_clips(List<string> clip_list)
        {
            if (clip_list.Count == 0)
            {
                MessageBox.Show("No clips to process.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            progress_bar.Visible = true;
            progress_bar.Value = 0;
            progress_bar.Maximum = clip_list.Count;

            Task.Run(async () =>
            {
                try
                {
                    for (int i = 0; i < clip_list.Count; i++)
                    {
                        var folder = clip_list[i];
                        var video_path = Directory.GetFiles(folder, "*-stream0-*.m4s").FirstOrDefault();
                        var audio_path = Directory.GetFiles(folder, "*-stream1-*.m4s").FirstOrDefault();
                        if (video_path == null || audio_path == null)
                        {
                            this.Invoke((MethodInvoker)delegate {
                                MessageBox.Show($"Could not find video or audio file in {folder}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            });
                            continue;
                        }

                        var game_name = await get_game_name(folder.Split('_')[1]);
                        var output_filename = get_unique_filename(export_dir, $"{game_name}_{extract_datetime_from_folder_name(folder):yyyy-MM-dd_HH-mm-ss}.mp4");

                        FfmpegWrapper.ConvertToMp4(video_path, audio_path, output_filename);

                        this.Invoke((MethodInvoker)delegate {
                            progress_bar.Value = i + 1;
                        });
                    }

                    this.Invoke((MethodInvoker)delegate {
                        progress_bar.Visible = false;
                        MessageBox.Show("Conversion complete!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                catch (Exception ex)
                {
                    this.Invoke((MethodInvoker)delegate {
                        progress_bar.Visible = false;
                        MessageBox.Show($"An error occurred during conversion: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
            });
        }

        private string get_unique_filename(string directory, string filename)
        {
            var path = Path.Combine(directory, filename);
            if (!File.Exists(path))
            {
                return path;
            }

            var i = 1;
            var new_path = Path.Combine(directory, $"{Path.GetFileNameWithoutExtension(filename)}_{i}{Path.GetExtension(filename)}");
            while (File.Exists(new_path))
            {
                i++;
                new_path = Path.Combine(directory, $"{Path.GetFileNameWithoutExtension(filename)}_{i}{Path.GetExtension(filename)}");
            }
            return new_path;
        }

        private void populate_steamid_dirs()
        {
            if (!Directory.Exists(default_dir))
            {
                MessageBox.Show("Default Steam userdata directory not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            steamid_combo.Items.Clear();
            bool steamid_found = false;
            foreach (var entry in Directory.GetDirectories(default_dir))
            {
                var dirName = new DirectoryInfo(entry).Name;
                if (long.TryParse(dirName, out _))
                {
                    var clips_dir = Path.Combine(entry, "gamerecordings", "clips");
                    var video_dir = Path.Combine(entry, "gamerecordings", "video");
                    if (Directory.Exists(clips_dir) || Directory.Exists(video_dir))
                    {
                        steamid_combo.Items.Add(dirName);
                        steamid_found = true;
                    }
                }
            }

            if (!steamid_found)
            {
                MessageBox.Show("Clips folder is empty. Record at least one clip to use SteamClip.", "No Clips Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            else
            {
                steamid_combo.SelectedIndex = 0;
            }
        }

        private async void populate_gameid_combo()
        {
            var game_ids_in_clips = new HashSet<string>(original_clip_folders.Select(f => Path.GetFileName(f).Split('_')[1]));
            var sorted_game_ids = game_ids_in_clips.OrderBy(id => id).ToList();

            gameid_combo.Items.Clear();
            gameid_combo.Items.Add("All Games");
            foreach (var game_id in sorted_game_ids)
            {
                gameid_combo.Items.Add(new KeyValuePair<string, string>(game_id, await get_game_name(game_id)));
            }
            gameid_combo.DisplayMember = "Value";
            gameid_combo.ValueMember = "Key";
            gameid_combo.SelectedIndex = 0;
        }

        private async Task<string> get_game_name(string game_id)
        {
            if (game_ids.ContainsKey(game_id))
            {
                return game_ids[game_id];
            }

            var url = $"{STEAM_APP_DETAILS_URL}?appids={game_id}&filters=basic";
            try
            {
                var response = await client.GetStringAsync(url);
                var serializer = new JavaScriptSerializer();
                var data = serializer.Deserialize<Dictionary<string, dynamic>>(response);
                if (data.ContainsKey(game_id) && data[game_id]["success"])
                {
                    var name = data[game_id]["data"]["name"];
                    game_ids[game_id] = name;
                    SaveGameIDs();
                    return name;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching game name for ID {game_id}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return game_id;
        }

        private DateTime extract_datetime_from_folder_name(string folder_name)
        {
            var parts = Path.GetFileName(folder_name).Split('_');
            if (parts.Length >= 3)
            {
                if (DateTime.TryParseExact(parts[parts.Length - 2] + parts[parts.Length - 1], "yyyyMMddHHmmss", null, System.Globalization.DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }
            return DateTime.MinValue;
        }

        private async void display_clips()
        {
            foreach (Control control in clip_grid.Controls)
            {
                if (control.BackgroundImage != null)
                {
                    control.BackgroundImage.Dispose();
                }
                control.Dispose();
            }
            clip_grid.Controls.Clear();
            var clips_to_show = clip_folders.Skip(clip_index).Take(6).ToList();
            for (int i = 0; i < clips_to_show.Count; i++)
            {
                var folder = clips_to_show[i];
                var thumbnail_path = Path.Combine(folder, "thumbnail.jpg");
                if (!File.Exists(thumbnail_path))
                {
                    var video_path = Directory.GetFiles(folder, "*-stream0-*.m4s").FirstOrDefault();
                    if (video_path != null)
                    {
                        try
                        {
                            FfmpegWrapper.ExtractFirstFrame(video_path, thumbnail_path);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error generating thumbnail for {folder}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                var button = new Button
                {
                    BackgroundImage = File.Exists(thumbnail_path) ? Image.FromFile(thumbnail_path) : null,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    Dock = DockStyle.Fill,
                    Tag = folder
                };
                button.Click += select_clip;
                if (selected_clips.Contains(folder))
                {
                    button.BackColor = Color.LightBlue;
                }
                clip_grid.Controls.Add(button, i % 3, i / 3);
            }
            update_navigation_buttons();
        }

        private void select_clip(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var folder = (string)button.Tag;

            if (selected_clips.Contains(folder))
            {
                selected_clips.Remove(folder);
                button.BackColor = SystemColors.Control;
            }
            else
            {
                selected_clips.Add(folder);
                button.BackColor = Color.LightBlue;
            }

            convert_button.Enabled = selected_clips.Count > 0;
            clear_selection_button.Enabled = selected_clips.Count > 0;
        }

        private void update_navigation_buttons()
        {
            prev_button.Enabled = clip_index > 0;
            next_button.Enabled = clip_index + 6 < clip_folders.Count;
        }

        public async void update_game_ids()
        {
            var game_ids_in_clips = new HashSet<string>(original_clip_folders.Select(f => Path.GetFileName(f).Split('_')[1]));
            var updated = false;
            foreach (var game_id in game_ids_in_clips)
            {
                if (!game_ids.ContainsKey(game_id))
                {
                    var name = await get_game_name(game_id);
                    if (name != game_id)
                    {
                        updated = true;
                    }
                }
            }

            if (updated)
            {
                populate_gameid_combo();
                MessageBox.Show("Game ID database updated.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No updates needed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void check_for_updates()
        {
            // This is a placeholder for the update check logic.
            MessageBox.Show("Checking for updates...", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void delete_config_folder()
        {
            var result = MessageBox.Show($"Are you sure you want to delete the entire configuration folder?\n\n{CONFIG_DIR}\n\nThis action cannot be undone.", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Directory.Delete(CONFIG_DIR, true);
                    MessageBox.Show("Configuration folder has been deleted.\nThe application will now close.", "Deletion Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to delete configuration folder:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
