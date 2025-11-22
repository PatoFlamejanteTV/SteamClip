namespace SteamClip
{
    partial class EditGameIDWindow
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
            this.game_id_grid = new System.Windows.Forms.DataGridView();
            this.cancel_button = new System.Windows.Forms.Button();
            this.apply_button = new System.Windows.Forms.Button();
            this.bottom_layout = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.game_id_grid)).BeginInit();
            this.bottom_layout.SuspendLayout();
            this.SuspendLayout();
            //
            // game_id_grid
            //
            this.game_id_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.game_id_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.game_id_grid.Location = new System.Drawing.Point(0, 0);
            this.game_id_grid.Name = "game_id_grid";
            this.game_id_grid.Size = new System.Drawing.Size(400, 250);
            this.game_id_grid.TabIndex = 0;
            //
            // cancel_button
            //
            this.cancel_button.Location = new System.Drawing.Point(3, 3);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 1;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            //
            // apply_button
            //
            this.apply_button.Location = new System.Drawing.Point(84, 3);
            this.apply_button.Name = "apply_button";
            this.apply_button.Size = new System.Drawing.Size(75, 23);
            this.apply_button.TabIndex = 2;
            this.apply_button.Text = "Apply";
            this.apply_button.UseVisualStyleBackColor = true;
            //
            // bottom_layout
            //
            this.bottom_layout.Controls.Add(this.cancel_button);
            this.bottom_layout.Controls.Add(this.apply_button);
            this.bottom_layout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottom_layout.Location = new System.Drawing.Point(0, 250);
            this.bottom_layout.Name = "bottom_layout";
            this.bottom_layout.Size = new System.Drawing.Size(400, 50);
            this.bottom_layout.TabIndex = 3;
            //
            // EditGameIDWindow
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.bottom_layout);
            this.Controls.Add(this.game_id_grid);
            this.Name = "EditGameIDWindow";
            this.Text = "Edit Game Names";
            ((System.ComponentModel.ISupportInitialize)(this.game_id_grid)).EndInit();
            this.bottom_layout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView game_id_grid;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button apply_button;
        private System.Windows.Forms.FlowLayoutPanel bottom_layout;
    }
}
