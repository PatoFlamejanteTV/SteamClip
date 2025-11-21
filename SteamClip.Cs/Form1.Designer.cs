namespace SteamClip
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.steamid_combo = new System.Windows.Forms.ComboBox();
            this.gameid_combo = new System.Windows.Forms.ComboBox();
            this.media_type_combo = new System.Windows.Forms.ComboBox();
            this.clip_grid = new System.Windows.Forms.TableLayoutPanel();
            this.clear_selection_button = new System.Windows.Forms.Button();
            this.export_all_button = new System.Windows.Forms.Button();
            this.progress_bar = new System.Windows.Forms.ProgressBar();
            this.settings_button = new System.Windows.Forms.Button();
            this.prev_button = new System.Windows.Forms.Button();
            this.next_button = new System.Windows.Forms.Button();
            this.convert_button = new System.Windows.Forms.Button();
            this.exit_button = new System.Windows.Forms.Button();
            this.top_layout = new System.Windows.Forms.FlowLayoutPanel();
            this.bottom_layout = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            //
            // steamid_combo
            //
            this.steamid_combo.FormattingEnabled = true;
            this.steamid_combo.Location = new System.Drawing.Point(50, 12);
            this.steamid_combo.Name = "steamid_combo";
            this.steamid_combo.Size = new System.Drawing.Size(200, 21);
            this.steamid_combo.TabIndex = 0;
            //
            // gameid_combo
            //
            this.gameid_combo.FormattingEnabled = true;
            this.gameid_combo.Location = new System.Drawing.Point(260, 12);
            this.gameid_combo.Name = "gameid_combo";
            this.gameid_combo.Size = new System.Drawing.Size(200, 21);
            this.gameid_combo.TabIndex = 1;
            //
            // media_type_combo
            //
            this.media_type_combo.FormattingEnabled = true;
            this.media_type_combo.Location = new System.Drawing.Point(470, 12);
            this.media_type_combo.Name = "media_type_combo";
            this.media_type_combo.Size = new System.Drawing.Size(200, 21);
            this.media_type_combo.TabIndex = 2;
            //
            // clip_grid
            //
            this.clip_grid.ColumnCount = 3;
            this.clip_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.clip_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.clip_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.clip_grid.Location = new System.Drawing.Point(12, 50);
            this.clip_grid.Name = "clip_grid";
            this.clip_grid.RowCount = 2;
            this.clip_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.clip_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.clip_grid.Size = new System.Drawing.Size(776, 350);
            this.clip_grid.TabIndex = 3;
            //
            // clear_selection_button
            //
            this.clear_selection_button.Location = new System.Drawing.Point(250, 415);
            this.clear_selection_button.Name = "clear_selection_button";
            this.clear_selection_button.Size = new System.Drawing.Size(100, 23);
            this.clear_selection_button.TabIndex = 4;
            this.clear_selection_button.Text = "Clear Selection";
            this.clear_selection_button.UseVisualStyleBackColor = true;
            //
            // export_all_button
            //
            this.export_all_button.Location = new System.Drawing.Point(360, 415);
            this.export_all_button.Name = "export_all_button";
            this.export_all_button.Size = new System.Drawing.Size(100, 23);
            this.export_all_button.TabIndex = 5;
            this.export_all_button.Text = "Export All";
            this.export_all_button.UseVisualStyleBackColor = true;
            //
            // progress_bar
            //
            this.progress_bar.Location = new System.Drawing.Point(12, 450);
            this.progress_bar.Name = "progress_bar";
            this.progress_bar.Size = new System.Drawing.Size(776, 23);
            this.progress_bar.TabIndex = 6;
            //
            // settings_button
            //
            this.settings_button.Location = new System.Drawing.Point(12, 12);
            this.settings_button.Name = "settings_button";
            this.settings_button.Size = new System.Drawing.Size(30, 21);
            this.settings_button.TabIndex = 7;
            this.settings_button.Text = "S";
            this.settings_button.UseVisualStyleBackColor = true;
            //
            // prev_button
            //
            this.prev_button.Location = new System.Drawing.Point(12, 480);
            this.prev_button.Name = "prev_button";
            this.prev_button.Size = new System.Drawing.Size(75, 23);
            this.prev_button.TabIndex = 8;
            this.prev_button.Text = "<< Previous";
            this.prev_button.UseVisualStyleBackColor = true;
            //
            // next_button
            //
            this.next_button.Location = new System.Drawing.Point(93, 480);
            this.next_button.Name = "next_button";
            this.next_button.Size = new System.Drawing.Size(75, 23);
            this.next_button.TabIndex = 9;
            this.next_button.Text = "Next >>";
            this.next_button.UseVisualStyleBackColor = true;
            //
            // convert_button
            //
            this.convert_button.Location = new System.Drawing.Point(632, 480);
            this.convert_button.Name = "convert_button";
            this.convert_button.Size = new System.Drawing.Size(75, 23);
            this.convert_button.TabIndex = 10;
            this.convert_button.Text = "Convert";
            this.convert_button.UseVisualStyleBackColor = true;
            //
            // exit_button
            //
            this.exit_button.Location = new System.Drawing.Point(713, 480);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(75, 23);
            this.exit_button.TabIndex = 11;
            this.exit_button.Text = "Exit";
            this.exit_button.UseVisualStyleBackColor = true;
            //
            // top_layout
            //
            this.top_layout.Controls.Add(this.settings_button);
            this.top_layout.Controls.Add(this.steamid_combo);
            this.top_layout.Controls.Add(this.gameid_combo);
            this.top_layout.Controls.Add(this.media_type_combo);
            this.top_layout.Dock = System.Windows.Forms.DockStyle.Top;
            this.top_layout.Location = new System.Drawing.Point(0, 0);
            this.top_layout.Name = "top_layout";
            this.top_layout.Size = new System.Drawing.Size(800, 45);
            this.top_layout.TabIndex = 12;
            //
            // bottom_layout
            //
            this.bottom_layout.Controls.Add(this.prev_button);
            this.bottom_layout.Controls.Add(this.next_button);
            this.bottom_layout.Controls.Add(this.convert_button);
            this.bottom_layout.Controls.Add(this.exit_button);
            this.bottom_layout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_layout.Location = new System.Drawing.Point(0, 515);
            this.bottom_layout.Name = "bottom_layout";
            this.bottom_layout.Size = new System.Drawing.Size(800, 45);
            this.bottom_layout.TabIndex = 13;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 560);
            this.Controls.Add(this.bottom_layout);
            this.Controls.Add(this.top_layout);
            this.Controls.Add(this.progress_bar);
            this.Controls.Add(this.export_all_button);
            this.Controls.Add(this.clear_selection_button);
            this.Controls.Add(this.clip_grid);
            this.Name = "Form1";
            this.Text = "SteamClip";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox steamid_combo;
        private System.Windows.Forms.ComboBox gameid_combo;
        private System.Windows.Forms.ComboBox media_type_combo;
        private System.Windows.Forms.TableLayoutPanel clip_grid;
        private System.Windows.Forms.Button clear_selection_button;
        private System.Windows.Forms.Button export_all_button;
        private System.Windows.Forms.ProgressBar progress_bar;
        private System.Windows.Forms.Button settings_button;
        private System.Windows.Forms.Button prev_button;
        private System.Windows.Forms.Button next_button;
        private System.Windows.Forms.Button convert_button;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.FlowLayoutPanel top_layout;
        private System.Windows.Forms.FlowLayoutPanel bottom_layout;
    }
}
