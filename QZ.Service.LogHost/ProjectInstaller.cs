using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace QZ.Service.LogHost
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(IDictionary savedState)
        {
            //try
            //{
            //    base.OnAfterInstall(savedState);

            //    // allowed to interact with desktop
            //    var service = new System.Management.ManagementObject($"WinService_Name='{this.LogServiceInstaller.ServiceName}'");
            //    var method = service.GetMethodParameters("Change");
            //    method["DesktopInteract"] = true;
            //    var value = service.InvokeMethod("Change", method, null);
            //}
            //catch
            //{

            //}
        }
    }
}
