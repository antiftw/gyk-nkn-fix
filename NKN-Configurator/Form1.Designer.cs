namespace NKN_Configurator {
	partial class Form1 {
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent() {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btnLangRu = new System.Windows.Forms.Button();
			this.btnLangEn = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.versionLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(305, 179);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(335, 18);
			this.label1.TabIndex = 3;
			this.label1.Text = "Select the language of the configurator.";
			// 
			// label2
			// 
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(330, 337);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(292, 39);
			this.label2.TabIndex = 4;
			this.label2.Text = "It does not affect the language of your game.\r\nThe mod has absolutely no effect o" +
    "n game localization.\r\nThese options are for this single run of this config app o" +
    "nly.";
			this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// btnLangRu
			// 
			this.btnLangRu.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnLangRu.Cursor = System.Windows.Forms.Cursors.Default;
			this.btnLangRu.Image = global::NKN_Configurator.Properties.Resources.ru;
			this.btnLangRu.Location = new System.Drawing.Point(488, 224);
			this.btnLangRu.Name = "btnLangRu";
			this.btnLangRu.Size = new System.Drawing.Size(148, 90);
			this.btnLangRu.TabIndex = 1;
			this.btnLangRu.UseVisualStyleBackColor = true;
			this.btnLangRu.Click += new System.EventHandler(this.btnLangRu_Click);
			// 
			// btnLangEn
			// 
			this.btnLangEn.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.btnLangEn.Image = global::NKN_Configurator.Properties.Resources.en;
			this.btnLangEn.Location = new System.Drawing.Point(305, 224);
			this.btnLangEn.Name = "btnLangEn";
			this.btnLangEn.Size = new System.Drawing.Size(148, 90);
			this.btnLangEn.TabIndex = 0;
			this.btnLangEn.UseVisualStyleBackColor = true;
			this.btnLangEn.Click += new System.EventHandler(this.btnLangEn_Click);
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "txt";
			this.saveFileDialog1.FileName = "config.txt";
			this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Location = new System.Drawing.Point(848, 548);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(82, 13);
			this.versionLabel.TabIndex = 5;
			this.versionLabel.Text = "NKN Conf v.0.9";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(942, 570);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnLangRu);
			this.Controls.Add(this.btnLangEn);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(520, 420);
			this.Name = "Form1";
			this.Text = "NotKeepersNeeds Configurator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLangEn;
		private System.Windows.Forms.Button btnLangRu;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.Label versionLabel;
	}
}

