using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddFilesFromFilelist.Resources
{
	public partial class FileListSelector : Form
	{
		public class Model
		{
			public string FileListName { get; set; } = "123";
		}
		public Model Data { get; set; } = new Model();

		public FileListSelector()
		{
			InitializeComponent();
			TextBoxFileName.DataBindings.Add("Text", Data, "FileListName");
		}

		private void FileListSelector_Load(object sender, EventArgs e)
		{
			
		}
	}
}
