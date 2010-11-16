using System.Diagnostics.CodeAnalysis;
using System.IO;
using EnvDTE;
using NuGet.VisualStudio.Resources;

namespace NuGet.VisualStudio {
    internal class WebSiteProjectSystem : WebProjectSystem {
        public WebSiteProjectSystem(Project project)
            : base(project) {
        }

        public override string ProjectName {
            get {
                return Path.GetFileName(Path.GetDirectoryName(Project.FullName));
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "We want to catch all exceptions")]
        public override void AddReference(string referencePath, Stream stream) {
            try {
                string name = Path.GetFileNameWithoutExtension(referencePath);

                // Add a reference to the project
                Project.Object.References.AddFromFile(GetAbsolutePath(referencePath));

                Logger.Log(MessageLevel.Debug, VsResources.Debug_AddReference, name, ProjectName);
            }
            catch {

            }
        }

        protected override bool ExcludeFile(string path) {
            // Exclude nothing from website projects
            return false;
        }
    }
}
