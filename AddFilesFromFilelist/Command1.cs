using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;


using Task = System.Threading.Tasks.Task;
using System.Windows.Forms;

namespace AddFilesFromFilelist
{
	/// <summary>
	/// Command handler
	/// </summary>
	internal sealed class Command1
	{
		/// <summary>
		/// Command ID.
		/// </summary>
		public const int CommandId = 0x0100;

		/// <summary>
		/// Command menu group (command set GUID).
		/// </summary>
		public static readonly Guid CommandSet = new Guid("27e5c22d-9dfb-45a8-8374-0984b9ea9eaa");

		/// <summary>
		/// VS Package that provides this command, not null.
		/// </summary>
		private readonly AsyncPackage package;

		/// <summary>
		/// Initializes a new instance of the <see cref="Command1"/> class.
		/// Adds our command handlers for menu (commands must exist in the command table file)
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		/// <param name="commandService">Command service to add command to, not null.</param>
		private Command1(AsyncPackage package, OleMenuCommandService commandService)
		{
			this.package = package ?? throw new ArgumentNullException(nameof(package));
			commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

			var menuCommandID = new CommandID(CommandSet, CommandId);
			var menuItem = new MenuCommand(this.Execute, menuCommandID);
			commandService.AddCommand(menuItem);
		}

		/// <summary>
		/// Gets the instance of the command.
		/// </summary>
		public static Command1 Instance
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the service provider from the owner package.
		/// </summary>
		private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
		{
			get
			{
				return this.package;
			}
		}

		/// <summary>
		/// Initializes the singleton instance of the command.
		/// </summary>
		/// <param name="package">Owner package, not null.</param>
		public static async Task InitializeAsync(AsyncPackage package)
		{
			// Switch to the main thread - the call to AddCommand in Command1's constructor requires
			// the UI thread.
			await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

			OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
			Instance = new Command1(package, commandService);
		}

		/// <summary>
		/// This function is the callback used to execute the command when the menu item is clicked.
		/// See the constructor to see how the menu item is associated with this function using
		/// OleMenuCommandService service and MenuCommand class.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event args.</param>
		private void Execute(object sender, EventArgs e)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			/*フォームからファイルリストのファイル名を得る
			 */
			var form = new Resources.FileListSelector();
			form.ShowDialog();
			if(form.DialogResult != DialogResult.OK)
			{
				VsShellUtilities.ShowMessageBox(
				this.package,
				"! OK",
				"title",
				OLEMSGICON.OLEMSGICON_INFO,
				OLEMSGBUTTON.OLEMSGBUTTON_OK,
				OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
				return;
			}
			var filename = form.Data.FileListName;
			if ((filename==null)||(filename==""))
			{
				VsShellUtilities.ShowMessageBox(
				this.package,
				string.Format("filename={0}", filename),
				"title",
				OLEMSGICON.OLEMSGICON_INFO,
				OLEMSGBUTTON.OLEMSGBUTTON_OK,
				OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
				return;
			}


			object item = ProjectHelpers.GetSelectedItem();
			var selectedItem = item as ProjectItem;
			var selectedProject = item as Project;
			Project project = selectedItem?.ContainingProject ?? selectedProject ?? ProjectHelpers.GetActiveProject();
			if (project == null) {
				return;
			}
			using (StreamReader reader = new StreamReader(filename, true))
			{
				while (! reader.EndOfStream)
				{
					var line = reader.ReadLine().TrimEnd(new char[] { '\n', '\r' });
					if (line == "")
					{
						continue;
					}
					project.AddFileToProject(line);
				}
			}
			/*
			VsShellUtilities.ShowMessageBox(
				this.package,
				"kita----",
				"title",
				OLEMSGICON.OLEMSGICON_INFO,
				OLEMSGBUTTON.OLEMSGBUTTON_OK,
				OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
				*/
		
	}
	}
}
