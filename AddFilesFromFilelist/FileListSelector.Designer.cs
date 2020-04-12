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
			this.components = new System.ComponentModel.Container();
			this.TextBoxFileName = new System.Windows.Forms.TextBox();
			this.fileListSelectorBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.OpenFileSelector = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.fileListSelectorBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// TextBoxFileName
			// 
			this.TextBoxFileName.Location = new System.Drawing.Point(80, 56);
			this.TextBoxFileName.Name = "TextBoxFileName";
			this.TextBoxFileName.Size = new System.Drawing.Size(442, 25);
			this.TextBoxFileName.TabIndex = 0;
			// 
			// fileListSelectorBindingSource
			// 
			this.fileListSelectorBindingSource.DataSource = typeof(AddFilesFromFilelist.Resources.FileListSelector);
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
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Location = new System.Drawing.Point(246, 151);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(75, 23);
			this.OK.TabIndex = 3;
			this.OK.Text = "OK";
			this.OK.UseVisualStyleBackColor = true;
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(359, 150);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 4;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// FileListSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(626, 210);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.OpenFileSelector);
			this.Controls.Add(this.TextBoxFileName);
			this.Name = "FileListSelector";
			this.Text = "Option.";
			this.Load += new System.EventHandler(this.FileListSelector_Load);
			((System.ComponentModel.ISupportInitialize)(this.fileListSelectorBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TextBoxFileName;
		private System.Windows.Forms.Button OpenFileSelector;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.BindingSource fileListSelectorBindingSource;
	}
}