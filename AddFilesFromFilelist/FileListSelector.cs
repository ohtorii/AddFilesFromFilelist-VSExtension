using System;
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
	}
}
