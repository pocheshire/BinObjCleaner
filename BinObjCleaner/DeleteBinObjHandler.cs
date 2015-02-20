using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Core;

namespace BinObjCleaner
{
    public class DeleteBinObjHandler : CommandHandler
    {
        protected override void Run ()
        {
            var projectOperation = IdeApp.ProjectOperations;
            var isBuild = projectOperation.IsBuilding (projectOperation.CurrentSelectedSolution);
            var isRun = projectOperation.IsRunning (projectOperation.CurrentSelectedSolution);

            if (!isBuild && !isRun 
                && IdeApp.ProjectOperations.CurrentBuildOperation.IsCompleted
                && IdeApp.ProjectOperations.CurrentRunOperation.IsCompleted)
            {
                IdeApp.Workbench.StatusBar.BeginProgress("Deleting /bin and /obj folders");

                var solutionItems = projectOperation.CurrentSelectedSolution.Items;

                foreach (var item in solutionItems) {
                    var binPath = item.BaseDirectory.FullPath + "/bin";
                    if (FileService.IsValidPath (binPath) && FileService.IsDirectory (binPath))
                        FileService.DeleteDirectory (binPath);

                    var objPath = item.BaseDirectory.FullPath + "/obj";
                    if (FileService.IsValidPath (objPath) && FileService.IsDirectory (objPath))
                        FileService.DeleteDirectory (objPath);
                }

                IdeApp.Workbench.StatusBar.EndProgress ();
                IdeApp.Workbench.StatusBar.ShowMessage ("Deleted success");
            }
        }

        protected override void Update (CommandInfo info)
        {
            info.Visible = IdeApp.Workspace.IsOpen;
        }
    }
}

