using Core.SAPB1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAPbobsCOM;

namespace Sap.Addon.Helpers
{
    public class Menu
    {
        public static void addMenu()
        {
            B1Helper.addMenuItem("3072", "Auto Item Master Code Setup", "Auto Item Master Code Setup");
            B1Helper.addMenuItem("15872", "Item_Code_Logic_Setup", "Item_Code_Logic_Setup");
            //B1Helper.AddSubMenu(MenuID_UD.MODULE, "Gate Pass master", "Gate Pass", -1, string.Concat(System.Windows.Forms.Application.StartupPath, @"\Images\Icon.png"));
            //B1Helper.addMenuItem("Gate Pass master", "Gate Pass", "Gate Pass");
            //B1Helper.addMenuItem("2304", "Shipment Tracking", "Shipment Tracking");
            //B1Helper.addMenuItem("2304", "Provisional Cost", "Provisional Cost");
            //B1Helper.addMenuItem("43537", "Letter of Credit", "Letter of Credit");
            //B1Helper.addMenuItem("43534", "Landed Cost Details Report", "Landed Cost Details Report");
        }
        public static void RemoveMenu()
        {
            B1Helper.RemoveMenuItem("3072", "Auto Item Master Code Setup", "Auto Item Master Code Setup");
        }
        public void AddMenuItems()
        {
            try
            {
                B1Helper.AddSubMenu(MenuID_UD.MODULE, "Gate Pass master", "Gate Pass", -1, string.Concat(System.Windows.Forms.Application.StartupPath, @"\Images\Icon.png"));
                B1Helper.addMenuItem("Gate Pass master", "Gate Pass", "Gate Pass");
                B1Helper.addMenuItem("2304", "Shipment Tracking", "Shipment Tracking");
                string qry = "Select \"U_ATOGEN\" from OADM";
                SAPbobsCOM.Recordset rec = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(BoObjectTypes.BoRecordset);
                rec.DoQuery(qry);
                if (rec.RecordCount > 0)
                {
                    if (rec.Fields.Item("U_ATOGEN").Value.ToString() == "Y")
                    {
                        B1Helper.addMenuItem("3072", "Auto Item Master Code Setup", "Auto Item Master Code Setup");
                        B1Helper.addMenuItem("3072", "Item_Code_Logic_Setup", "Item Code Logic Setup");
                    }
                }
                B1Helper.addMenuItem("2304", "Provisional Cost", "Provisional Cost");

                qry = "Select \"U_LOCSTATS\" from OADM";
                rec.DoQuery(qry);
                if (rec.RecordCount > 0)
                {
                    if (rec.Fields.Item("U_LOCSTATS").Value.ToString() == "Y")
                    {
                        B1Helper.addMenuItem("43537", "Letter of Credit", "Letter of Credit");
                    }
                }

                B1Helper.addMenuItem("43534", "Landed Cost Details Report", "Landed Cost Details Report");
                //B1Helper.addMenuItem(MenuID_UD.ReportMID, MenuID_UD.ServiceEarningDataDistributorsMID, MenuID_UD.ServiceEarningDataDistributors);
                //B1Helper.addMenuItem(MenuID_UD.ReportMID, MenuID_UD.ServiceCallPendingForInvociesMID, MenuID_UD.ServiceCallPendingForInvocies);
                ////B1Helper.addMenuItem(MenuID_UD.SERVICEMODULEMASTERS, MenuID_UD.MACHINEPRICINGMASTER, MenuID_UD.MACHINEPRICINGMASTER);
                //B1Helper.addMenuItem(MenuID_UD.ReportMID, MenuID_UD.AdministrativeDataFieldServiceCallMID, MenuID_UD.AdministrativeDataFieldServiceCall);
            
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
