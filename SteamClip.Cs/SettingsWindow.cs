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
            close_settings_button.Click += (sender, e) => Close();
        }

        private void open_config_folder(object sender, EventArgs e)
        {
            Process.Start(mainForm.CONFIG_DIR);
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
    }
}
