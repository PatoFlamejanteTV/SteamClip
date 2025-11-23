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
using System.Diagnostics;

namespace SteamClip
{
    public partial class SettingsWindow : Form
    {
        private Form1 mainForm;

        public SettingsWindow(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            open_config_button.Click += open_config_folder;
            select_export_button.Click += select_export_path;
            edit_game_ids_button.Click += open_edit_game_ids;
            update_game_ids_button.Click += update_game_ids;
            check_for_updates_button.Click += check_for_updates;
            delete_config_button.Click += delete_config_folder;
            select_ffmpeg_button.Click += select_ffmpeg_path;
            close_settings_button.Click += (sender, e) => Close();

            ffmpeg_path_box.Text = mainForm.ffmpeg_path;
        }

        private void open_config_folder(object sender, EventArgs e)
        {
            if (Directory.Exists(mainForm.CONFIG_DIR))
            {
                try
                {
                    Process.Start(new ProcessStartInfo()
                    {
                        FileName = mainForm.CONFIG_DIR,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                catch (Exception ex)
                {
                    Logger.Error($"Failed to open config folder: {mainForm.CONFIG_DIR}", ex);
                    MessageBox.Show("Could not open the configuration folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Configuration directory does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void select_export_path(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    mainForm.export_dir = dialog.SelectedPath;
                    mainForm.SaveConfig();
                }
            }
        }

        private void open_edit_game_ids(object sender, EventArgs e)
        {
            using (var dialog = new EditGameIDWindow(mainForm))
            {
                dialog.ShowDialog();
            }
        }

        private void update_game_ids(object sender, EventArgs e)
        {
            mainForm.update_game_ids();
        }

        private void check_for_updates(object sender, EventArgs e)
        {
            mainForm.check_for_updates();
        }

        private void delete_config_folder(object sender, EventArgs e)
        {
            mainForm.delete_config_folder();
        }

        private void select_ffmpeg_path(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Filter = "ffmpeg.exe|ffmpeg.exe";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    mainForm.ffmpeg_path = dialog.FileName;
                    mainForm.SaveConfig();
                    ffmpeg_path_box.Text = mainForm.ffmpeg_path;
                    FfmpegWrapper.SetFfmpegPath(mainForm.ffmpeg_path);
                }
            }
        }
    }
}
