namespace HLSE
{
    partial class MainForm
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
            this.btn_choose_sav = new System.Windows.Forms.Button();
            this.tb_save_path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_level = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_talent = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_reset_talent = new System.Windows.Forms.Button();
            this.btn_save_sav = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_choose_sav
            // 
            this.btn_choose_sav.Location = new System.Drawing.Point(713, 4);
            this.btn_choose_sav.Name = "btn_choose_sav";
            this.btn_choose_sav.Size = new System.Drawing.Size(75, 26);
            this.btn_choose_sav.TabIndex = 0;
            this.btn_choose_sav.Text = "读取文件";
            this.btn_choose_sav.UseVisualStyleBackColor = true;
            this.btn_choose_sav.Click += new System.EventHandler(this.btn_choose_sav_Click);
            // 
            // tb_save_path
            // 
            this.tb_save_path.Location = new System.Drawing.Point(77, 6);
            this.tb_save_path.Name = "tb_save_path";
            this.tb_save_path.Size = new System.Drawing.Size(618, 21);
            this.tb_save_path.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "存档位置";
            // 
            // tb_level
            // 
            this.tb_level.Location = new System.Drawing.Point(77, 33);
            this.tb_level.Name = "tb_level";
            this.tb_level.Size = new System.Drawing.Size(100, 21);
            this.tb_level.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "等级";
            // 
            // tb_talent
            // 
            this.tb_talent.Location = new System.Drawing.Point(77, 60);
            this.tb_talent.Name = "tb_talent";
            this.tb_talent.Size = new System.Drawing.Size(100, 21);
            this.tb_talent.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "可用天赋点";
            // 
            // btn_reset_talent
            // 
            this.btn_reset_talent.Location = new System.Drawing.Point(183, 58);
            this.btn_reset_talent.Name = "btn_reset_talent";
            this.btn_reset_talent.Size = new System.Drawing.Size(120, 26);
            this.btn_reset_talent.TabIndex = 2;
            this.btn_reset_talent.Text = "重置天赋点";
            this.btn_reset_talent.UseVisualStyleBackColor = true;
            this.btn_reset_talent.Click += new System.EventHandler(this.btn_reset_talent_Click);
            // 
            // btn_save_sav
            // 
            this.btn_save_sav.Location = new System.Drawing.Point(713, 32);
            this.btn_save_sav.Name = "btn_save_sav";
            this.btn_save_sav.Size = new System.Drawing.Size(75, 26);
            this.btn_save_sav.TabIndex = 2;
            this.btn_save_sav.Text = "保存文件";
            this.btn_save_sav.UseVisualStyleBackColor = true;
            this.btn_save_sav.Click += new System.EventHandler(this.btn_save_sav_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(486, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "如果你不点保存文件，则改动不会生效";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_talent);
            this.Controls.Add(this.tb_level);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_save_path);
            this.Controls.Add(this.btn_reset_talent);
            this.Controls.Add(this.btn_save_sav);
            this.Controls.Add(this.btn_choose_sav);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "霍格沃茨之遗存档修改器";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_choose_sav;
        private System.Windows.Forms.TextBox tb_save_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_level;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_talent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_reset_talent;
        private System.Windows.Forms.Button btn_save_sav;
        private System.Windows.Forms.Label label4;
    }
}