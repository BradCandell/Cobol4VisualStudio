using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Cobol4VisualStudio.Extension.Settings;
using Microsoft.VisualStudio.Shell;

namespace Cobol4VisualStudio.Extension {


    [PackageRegistration(UseManagedResourcesOnly = true)]
    [ProvideOptionPage(typeof(MyOptionsGrid), "Cobol", "Page Name", 0, 0, true, 0)]
    [Guid(Cobol4VisualStudioPackage.PackageGuidString)]
    public sealed class Cobol4VisualStudioPackage : Package {
        /// <summary>
        /// Cobol4VisualStudioPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "b95ce356-0d0c-4c09-b6c8-cd2746c748b4";

        /// <summary>
        /// Initializes a new instance of the <see cref="Cobol4VisualStudioPackage"/> class.
        /// </summary>
        public Cobol4VisualStudioPackage() {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();
        }

        #endregion
    }
}
