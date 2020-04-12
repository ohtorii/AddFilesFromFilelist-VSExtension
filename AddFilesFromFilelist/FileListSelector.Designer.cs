namespace AddFilesFromFilelist.Resources
{
	partial class FileListSelector
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
			this.FileName = new System.Windows.Forms.TextBox();
			this.OpenFileSelector = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.OK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// FileName
			// 
			this.FileName.Location = new System.Drawing.Point(80, 56);
			this.FileName.Name = "FileName";
			this.FileName.Size = new System.Drawing.Size(442, 25);
			this.FileName.TabIndex = 0;
			// 
			// OpenFileSelector
			// 
			this.OpenFileSelector.Location = new System.Drawing.Point(528, 56);
			this.OpenFileSelector.Name = "OpenFileSelector";
			this.OpenFileSelector.Size = new System.Drawing.Size(75, 23);
			this.OpenFileSelector.TabIndex = 1;
			this.OpenFileSelector.Text = "･･･";
			this.OpenFileSelector.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 63);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "File list";
			// 
			// OK
			// 
			this.OK.Location = new System.Drawing.Point(246, 151);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(75, 23);
			this.OK.TabIndex = 3;
			this.OK.Text = "OK";
			this.OK.UseVisualStyleBackColor = true;
			// 
			// FileListSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(626, 210);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.OpenFileSelector);
			this.Controls.Add(this.FileName);
			this.Name = "FileListSelector";
			this.Text = "Option.";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox FileName;
		private System.Windows.Forms.Button OpenFileSelector;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button OK;
	}
}