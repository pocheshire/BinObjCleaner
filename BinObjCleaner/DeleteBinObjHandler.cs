using MonoDevelop.Components.Commands;
using MonoDevelop.Core;
using MonoDevelop.Ide;

namespace BinObjCleaner
{
    public class DeleteBinObjHandler : CommandHandler
    {
        protected override void Run()
        {
            var projectOperation = IdeApp.ProjectOperations;
            var isBuild = projectOperation.IsBuilding(projectOperation.CurrentSelectedSolution);
            var isRun = projectOperation.IsRunning(projectOperation.CurrentSelectedSolution);

            if (!isBuild && !isRun
                && IdeApp.ProjectOperations.CurrentBuildOperation.IsCompleted
                && IdeApp.ProjectOperations.CurrentRunOperation.IsCompleted)
            {
                Gtk.Application.Invoke(delegate
                {
                    IdeApp.Workbench.StatusBar.BeginProgress(MonoDevelop.Ide.Gui.Stock.StatusBuild, "Deleting /bin and /obj folders...");
                });

                var solutionItems = projectOperation.CurrentSelectedSolution.Items;

                for (int i = 0; i < solutionItems.Count; i++)
                {
                    var item = solutionItems[i];

                    var binPath = item.BaseDirectory.FullPath + "/bin";
                    if (FileService.IsValidPath(binPath) && FileService.IsDirectory(binPath))
                        FileService.DeleteDirectory(binPath);

                    var objPath = item.BaseDirectory.FullPath + "/obj";
                    if (FileService.IsValidPath(objPath) && FileService.IsDirectory(objPath))
                        FileService.DeleteDirectory(objPath);

                    Gtk.Application.Invoke(delegate
                    {
                        IdeApp.Workbench.StatusBar.SetProgressFraction((double)i / (double)solutionItems.Count);
                    });
                }

                Gtk.Application.Invoke(delegate
                {
                    IdeApp.Workbench.StatusBar.ShowMessage("Deleted successfully");
                    IdeApp.Workbench.StatusBar.EndProgress();
                });
            }
        }

        protected override void Update(CommandInfo info)
        {
            info.Visible = IdeApp.Workspace.IsOpen;
        }
    }
}

