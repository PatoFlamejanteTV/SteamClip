namespace SteamClip
{
    partial class SettingsWindow
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
            this.open_config_button = new System.Windows.Forms.Button();
            this.edit_game_ids_button = new System.Windows.Forms.Button();
            this.update_game_ids_button = new System.Windows.Forms.Button();
            this.check_for_updates_button = new System.Windows.Forms.Button();
            this.close_settings_button = new System.Windows.Forms.Button();
            this.select_export_button = new System.Windows.Forms.Button();
            this.delete_config_button = new System.Windows.Forms.Button();
            this.version_label = new System.Windows.Forms.Label();
            this.ffmpeg_path_box = new System.Windows.Forms.TextBox();
            this.select_ffmpeg_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // open_config_button
            //
            this.open_config_button.Location = new System.Drawing.Point(12, 12);
            this.open_config_button.Name = "open_config_button";
            this.open_config_button.Size = new System.Drawing.Size(200, 45);
            this.open_config_button.TabIndex = 0;
            this.open_config_button.Text = "Open Config Folder";
            this.open_config_button.UseVisualStyleBackColor = true;
            //
            // edit_game_ids_button
            //
            this.edit_game_ids_button.Location = new System.Drawing.Point(12, 114);
            this.edit_game_ids_button.Name = "edit_game_ids_button";
            this.edit_game_ids_button.Size = new System.Drawing.Size(200, 45);
            this.edit_game_ids_button.TabIndex = 2;
            this.edit_game_ids_button.Text = "Edit Game Name";
            this.edit_game_ids_button.UseVisualStyleBackColor = true;
            //
            // update_game_ids_button
            //
            this.update_game_ids_button.Location = new System.Drawing.Point(12, 165);
            this.update_game_ids_button.Name = "update_game_ids_button";
            this.update_game_ids_button.Size = new System.Drawing.Size(200, 45);
            this.update_game_ids_button.TabIndex = 3;
            this.update_game_ids_button.Text = "Update GameIDs";
            this.update_game_ids_button.UseVisualStyleBackColor = true;
            //
            // check_for_updates_button
            //
            this.check_for_updates_button.Location = new System.Drawing.Point(12, 216);
            this.check_for_updates_button.Name = "check_for_updates_button";
            this.check_for_updates_button.Size = new System.Drawing.Size(200, 45);
            this.check_for_updates_button.TabIndex = 4;
            this.check_for_updates_button.Text = "Check for Updates";
            this.check_for_updates_button.UseVisualStyleBackColor = true;
            //
            // close_settings_button
            //
            this.close_settings_button.Location = new System.Drawing.Point(12, 318);
            this.close_settings_button.Name = "close_settings_button";
            this.close_settings_button.Size = new System.Drawing.Size(200, 45);
            this.close_settings_button.TabIndex = 6;
            this.close_settings_button.Text = "Close Settings";
            this.close_settings_button.UseVisualStyleBackColor = true;
            //
            // select_export_button
            //
            this.select_export_button.Location = new System.Drawing.Point(12, 63);
            this.select_export_button.Name = "select_export_button";
            this.select_export_button.Size = new System.Drawing.Size(200, 45);
            this.select_export_button.TabIndex = 1;
            this.select_export_button.Text = "Set Export Path";
            this.select_export_button.UseVisualStyleBackColor = true;
            //
            // delete_config_button
            //
            this.delete_config_button.Location = new System.Drawing.Point(12, 267);
            this.delete_config_button.Name = "delete_config_button";
            this.delete_config_button.Size = new System.Drawing.Size(200, 45);
            this.delete_config_button.TabIndex = 5;
            this.delete_config_button.Text = "Delete Config Folder";
            this.delete_config_button.UseVisualStyleBackColor = true;
            //
            // version_label
            //
            this.version_label.AutoSize = true;
            this.version_label.Location = new System.Drawing.Point(12, 420);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(42, 13);
            this.version_label.TabIndex = 7;
            this.version_label.Text = "Version";
            //
            // ffmpeg_path_box
            //
            this.ffmpeg_path_box.Location = new System.Drawing.Point(12, 370);
            this.ffmpeg_path_box.Name = "ffmpeg_path_box";
            this.ffmpeg_path_box.Size = new System.Drawing.Size(200, 20);
            this.ffmpeg_path_box.TabIndex = 8;
            //
            // select_ffmpeg_button
            //
            this.select_ffmpeg_button.Location = new System.Drawing.Point(12, 396);
            this.select_ffmpeg_button.Name = "select_ffmpeg_button";
            this.select_ffmpeg_button.Size = new System.Drawing.Size(200, 23);
            this.select_ffmpeg_button.TabIndex = 9;
            this.select_ffmpeg_button.Text = "Set FFmpeg Path";
            this.select_ffmpeg_button.UseVisualStyleBackColor = true;
            //
            // SettingsWindow
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(224, 450);
            this.Controls.Add(this.select_ffmpeg_button);
            this.Controls.Add(this.ffmpeg_path_box);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.delete_config_button);
            this.Controls.Add(this.select_export_button);
            this.Controls.Add(this.close_settings_button);
            this.Controls.Add(this.check_for_updates_button);
            this.Controls.Add(this.update_game_ids_button);
            this.Controls.Add(this.edit_game_ids_button);
            this.Controls.Add(this.open_config_button);
            this.Name = "SettingsWindow";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button open_config_button;
        private System.Windows.Forms.Button edit_game_ids_button;
        private System.Windows.Forms.Button update_game_ids_button;
        private System.Windows.Forms.Button check_for_updates_button;
        private System.Windows.Forms.Button close_settings_button;
        private System.Windows.Forms.Button select_export_button;
        private System.Windows.Forms.Button delete_config_button;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.TextBox ffmpeg_path_box;
        private System.Windows.Forms.Button select_ffmpeg_button;
    }
}
