using System;
using System.Collections.Generic;
using SAPbouiCOM.Framework;
using Sap.Addon.Helpers;
using SAPbobsCOM;
using System.Configuration;
using Core.Utilities;

namespace Sap.Addon
{
    class Program
    {
        #region Variables

        public static SAPbobsCOM.Company oCompany;
        public static SAPbouiCOM.Application SBO_Application;
        public static SAPbouiCOM.Form oForm { get; set; }

        #endregion

        /// <summary_>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                //Application.SBO_Application.StatusBar.SetSystemMessage("Connecting to the Add-on", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                Application oApp = null;
                if (args.Length < 1)
                    oApp = new Application();
                else
                    oApp = new Application(args[0]);

                SBO_Application = SAPbouiCOM.Framework.Application.SBO_Application;
                oCompany = (SAPbobsCOM.Company)SBO_Application.Company.GetDICompany();
                Menu MyMenu = new Menu();
                MyMenu.AddMenuItems();

                GlobalMethods_Variables.SetsCreateUDF(ConfigurationManager.AppSettings["UDF"].ToString());
                if (GlobalMethods_Variables.CreateUDF == "N")
                {
                    AddonInfoInfo.InstallUDOs();
                }
                //AddonInfoInfo.GetCommonSettings();
                var applicationHandler = new ApplicationHandlers();
                Application.SBO_Application.StatusBar.SetSystemMessage("Add-on installed successfully.", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                oApp.Run();

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }


    }

}
