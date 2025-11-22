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
using System.Text.Json;

namespace SteamClip
{
    public partial class EditGameIDWindow : Form
    {
        private Form1 mainForm;
        private Dictionary<string, string> game_names = new Dictionary<string, string>();

        public EditGameIDWindow(Form1 mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            populate_table();
            apply_button.Click += save_changes;
            cancel_button.Click += (sender, e) => Close();
        }

        private void populate_table()
        {
            game_names = mainForm.game_ids.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            game_id_grid.DataSource = new BindingSource(game_names, null);
        }

        private void save_changes(object sender, EventArgs e)
        {
            game_id_grid.EndEdit();
            mainForm.game_ids = (Dictionary<string, string>)((BindingSource)game_id_grid.DataSource).DataSource;
            mainForm.SaveGameIDs();
            mainForm.populate_gameid_combo();
            Close();
        }
    }
}
