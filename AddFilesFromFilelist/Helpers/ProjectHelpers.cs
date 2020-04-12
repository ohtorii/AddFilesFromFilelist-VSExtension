﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;

namespace AddFilesFromFilelist
{
    public static class ProjectHelpers
    {
        static DTE2 _dte; //AddAnyFilePackage._dte;

        public static string GetRootNamespace(this Project project)
        {
            if (project == null)
                return null;

            string ns = project.Name ?? string.Empty;

            try
            {
                Property prop = project.Properties.Item("RootNamespace");

                if (prop != null && prop.Value != null && !string.IsNullOrEmpty(prop.Value.ToString()))
                    ns = prop.Value.ToString();
            }
            catch { /* Project doesn't have a root namespace */ }

            return CleanNameSpace(ns, stripPeriods: false);
        }

        public static string CleanNameSpace(string ns, bool stripPeriods = true)
        {
            if (stripPeriods)
            {
                ns = ns.Replace(".", "");
            }

            ns = ns.Replace(" ", "")
                     .Replace("-", "")
                     .Replace("\\", ".");

            return ns;
        }

        public static string GetRootFolder(this Project project)
        {
            if (project == null)
                return null;

            if (project.IsKind("{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")) //ProjectKinds.vsProjectKindSolutionFolder
                return Path.GetDirectoryName(_dte.Solution.FullName);

            if (string.IsNullOrEmpty(project.FullName))
                return null;

            string fullPath;

            try
            {
                fullPath = project.Properties.Item("FullPath").Value as string;
            }
            catch (ArgumentException)
            {
                try
                {
                    // MFC projects don't have FullPath, and there seems to be no way to query existence
                    fullPath = project.Properties.Item("ProjectDirectory").Value as string;
                }
                catch (ArgumentException)
                {
                    // Installer projects have a ProjectPath.
                    fullPath = project.Properties.Item("ProjectPath").Value as string;
                }
            }

            if (string.IsNullOrEmpty(fullPath))
                return File.Exists(project.FullName) ? Path.GetDirectoryName(project.FullName) : null;

            if (Directory.Exists(fullPath))
                return fullPath;

            if (File.Exists(fullPath))
                return Path.GetDirectoryName(fullPath);

            return null;
        }

        public static ProjectItem AddFileToProject(this Project project, string filename /*, string itemType = null*/)
        {
            if (project.IsKind(ProjectTypes.ASPNET_5, ProjectTypes.SSDT))
                return _dte.Solution.FindProjectItem(filename);

            string root = project.GetRootFolder();
            if (string.IsNullOrEmpty(root))
            {
                return null;
            }
            /*if (string.IsNullOrEmpty(root) || !filename.StartsWith(root, StringComparison.OrdinalIgnoreCase))
                return null;
            */
            ProjectItem item = project.ProjectItems.AddFromFile(filename);
            //item.SetItemType(itemType);
            return item;
        }

        public static void SetItemType(this ProjectItem item, string itemType)
        {
            try
            {
                if (item == null || item.ContainingProject == null)
                    return;

                if (string.IsNullOrEmpty(itemType)
                    || item.ContainingProject.IsKind(ProjectTypes.WEBSITE_PROJECT)
                    || item.ContainingProject.IsKind(ProjectTypes.UNIVERSAL_APP))
                    return;

                item.Properties.Item("ItemType").Value = itemType;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        public static bool IsKind(this Project project, params string[] kindGuids)
        {
            foreach (string guid in kindGuids)
            {
                if (project.Kind.Equals(guid, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }

        private static IEnumerable<Project> GetChildProjects(Project parent)
        {
            try
            {
                if (!parent.IsKind("{66A26720-8FB5-11D2-AA7E-00C04F688DDE}") && parent.Collection == null)  // Unloaded
                    return Enumerable.Empty<Project>();

                if (!string.IsNullOrEmpty(parent.FullName))
                    return new[] { parent };
            }
            catch (COMException)
            {
                return Enumerable.Empty<Project>();
            }

            return parent.ProjectItems
                    .Cast<ProjectItem>()
                    .Where(p => p.SubProject != null)
                    .SelectMany(p => GetChildProjects(p.SubProject));
        }

        public static Project GetActiveProject()
        {
            try
            {

                if (_dte.ActiveSolutionProjects is Array activeSolutionProjects && activeSolutionProjects.Length > 0)
                    return activeSolutionProjects.GetValue(0) as Project;

                Document doc = _dte.ActiveDocument;

                if (doc != null && !string.IsNullOrEmpty(doc.FullName))
                {
                    ProjectItem item = _dte.Solution?.FindProjectItem(doc.FullName);

                    if (item != null)
                        return item.ContainingProject;
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error getting the active project" + ex);
            }

            return null;
        }

        /*public static IWpfTextView GetCurentTextView()
        {
            IComponentModel componentModel = GetComponentModel();
            if (componentModel == null) return null;
            IVsEditorAdaptersFactoryService editorAdapter = componentModel.GetService<IVsEditorAdaptersFactoryService>();

            return editorAdapter.GetWpfTextView(GetCurrentNativeTextView());
        }*/

        public static IVsTextView GetCurrentNativeTextView()
        {
            var textManager = (IVsTextManager)ServiceProvider.GlobalProvider.GetService(typeof(SVsTextManager));

            ErrorHandler.ThrowOnFailure(textManager.GetActiveView(1, null, out IVsTextView activeView));
            return activeView;
        }

        /*public static IComponentModel GetComponentModel()
        {
            return (IComponentModel)Command1.GetGlobalService(typeof(SComponentModel));
        }*/

        public static object GetSelectedItem()
        {
            object selectedObject = null;

            var monitorSelection = (IVsMonitorSelection)Package.GetGlobalService(typeof(SVsShellMonitorSelection));

            try
            {
                monitorSelection.GetCurrentSelection(out IntPtr hierarchyPointer,
                                                 out uint itemId,
                                                 out IVsMultiItemSelect multiItemSelect,
                                                 out IntPtr selectionContainerPointer);


                if (Marshal.GetTypedObjectForIUnknown(
                                                     hierarchyPointer,
                                                     typeof(IVsHierarchy)) is IVsHierarchy selectedHierarchy)
                {
                    ErrorHandler.ThrowOnFailure(selectedHierarchy.GetProperty(itemId, (int)__VSHPROPID.VSHPROPID_ExtObject, out selectedObject));
                }

                Marshal.Release(hierarchyPointer);
                Marshal.Release(selectionContainerPointer);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }

            return selectedObject;
        }
    }

    public static class ProjectTypes
    {
        public const string ASPNET_5 = "{8BB2217D-0F2D-49D1-97BC-3654ED321F3B}";
        public const string DOTNET_Core = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";
        public const string WEBSITE_PROJECT = "{E24C65DC-7377-472B-9ABA-BC803B73C61A}";
        public const string UNIVERSAL_APP = "{262852C6-CD72-467D-83FE-5EEB1973A190}";
        public const string NODE_JS = "{9092AA53-FB77-4645-B42D-1CCCA6BD08BD}";
        public const string SSDT = "{00d1a9c2-b5f0-4af3-8072-f6c62b433612}";
    }
}
