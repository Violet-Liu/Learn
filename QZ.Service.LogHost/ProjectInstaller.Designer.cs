namespace QZ.Service.LogHost
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.LogServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.LogServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // LogServiceProcessInstaller
            // 
            this.LogServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.LogServiceProcessInstaller.Password = null;
            this.LogServiceProcessInstaller.Username = null;
            // 
            // LogServiceInstaller
            // 
            this.LogServiceInstaller.Description = "Log Service";
            this.LogServiceInstaller.DisplayName = "LogService";
            this.LogServiceInstaller.ServiceName = "LogService";
            this.LogServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.LogServiceProcessInstaller,
            this.LogServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller LogServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller LogServiceInstaller;
    }
}