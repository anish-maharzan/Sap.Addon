using Core.SAPB1;
using Core.Utilities;
using GlobalVariable;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using System;

namespace Sap.Addon.Helpers
{
    public class AddonInfoInfo
    {
        #region Members
        public int Index { get; set; }
        public bool isHana { get; set; }
        private static int RetCode = 0;
        private static string ErrMsg = null;
        #endregion

        #region Constructor
        public AddonInfoInfo()
        {
        }
        #endregion

        #region UDODEAFAUTFORMSFORLC
        public static void CreateUDOForms()
        {
            try
            {
                string[] ChildTable = new string[0];
                string[] FindColumn = new string[0];
                string[] FormColumn = new string[0];

                #region G/L Determination

                B1Helper.AddTable("ITN_OGDL", "G/L Determination", BoUTBTableType.bott_Document);
                B1Helper.AddField("ACCNAM", "Account Name", "ITN_OGDL", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("GLLDGR", "G/L Ledger", "ITN_OGDL", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                // Array.Resize(ref FindColumn, 1);
                Array.Resize(ref FormColumn, 3);
                FormColumn[0] = "DocEntry";
                FormColumn[1] = "U_ACCNAM";
                FormColumn[2] = "U_GLLDGR";
                B1Helper.CreateUdo("OGDL", "G/L Determination", "ITN_OGDL", "D", "Y", FormColumn, null);

                #endregion

                #region Terms of Payment
                B1Helper.AddTable("ITN_OLPT", "Terms of Payment", BoUTBTableType.bott_MasterData);
                B1Helper.AddField("PAYTERMS", "Payment Terms", "ITN_OLPT", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 2);
                FormColumn[0] = "Code";
                FormColumn[1] = "U_PAYTERMS";
                B1Helper.CreateUdo("OLPT", "Terms Of Payment", "ITN_OLPT", "M", "Y", FormColumn, null);
                #endregion

                #region Letter Of Credit Type
                B1Helper.AddTable("ITN_OSLT", "Letter Of Credit Type", BoUTBTableType.bott_MasterData);
                B1Helper.AddField("LOCTYPE", "Letter Of Credit Type", "ITN_OSLT", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 2);
                FormColumn[0] = "Code";
                FormColumn[1] = "U_LOCTYPE";
                B1Helper.CreateUdo("OSLT", "Letter Of Credit Type", "ITN_OSLT", "M", "Y", FormColumn, null);
                #endregion

                #region Customer Location
                B1Helper.AddTable("ITN_OSCL", "Customer Location", BoUTBTableType.bott_MasterData);
                B1Helper.AddField("CUSTLOC", "Customer Location", "ITN_OSCL", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 2);
                FormColumn[0] = "Code";
                FormColumn[1] = "U_CUSTLOC";
                B1Helper.CreateUdo("OSCL", "Customer Location", "ITN_OSCL", "M", "Y", FormColumn, null);
                #endregion

                #region Custom clearance
                B1Helper.AddTable("ITNCUSTOM", "Customs", BoUTBTableType.bott_Document);
                //FindColumns = new List<string> { "DISTCODE", "Code", "Name" };
                B1Helper.AddField("CODE", "CUSTOM CODE", "ITNCUSTOM", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("NAME", "CUSTOM NAME", "ITNCUSTOM", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 3);
                FormColumn[0] = "DocEntry";
                FormColumn[1] = "U_CODE";
                FormColumn[2] = "U_NAME";
                CreateUDO("ITNCUSTOM", "Customs", "ITNCUSTOM", FormColumn, BoUDOObjType.boud_Document, "F");
                #endregion
            }
            catch
            {
            }

        }

        #endregion

        #region AutoUDO
        public static void AutoUDO(string code, string parm)
        {
            try
            {
                string[] ChildTable = new string[0];
                string[] FindColumn = new string[0];
                string[] FormColumn = new string[0];
                B1Helper.AddTable("ITN_" + parm, parm, BoUTBTableType.bott_MasterData);
                Array.Resize(ref FindColumn, 1);
                Array.Resize(ref FormColumn, 2);

                FormColumn[0] = "Code";
                FormColumn[1] = "Name";
                FindColumn[0] = "DocEntry";
                B1Helper.CreateUdo("UDO" + code, parm, "ITN_" + parm, "M", "Y", FormColumn, null);
            }
            catch
            {
            }
        }

        #endregion

        #region AutoCreateUDF
        public static void AutoCreateUDF(string parm)
        {
            Application.SBO_Application.StatusBar.SetText("Database structure is modifying...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
            try
            {
                Core.SAPB1.B1Helper.AddField(parm, parm, "OITM", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                //  Core.SAPB1.B1Helper.AddField(parm, parm, "OITM", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
            }
            catch
            {
            }
        }

        #endregion
        
        #region UDOFORITEMMASTER
        public static void UDOForItemMaster()
        {
            try
            {
                string[] ChildTable = new string[0];
                string[] FindColumn = new string[0];
                string[] FormColumn = new string[0];

                Application.SBO_Application.StatusBar.SetText("Database structure is modifying...", SAPbouiCOM.BoMessageTime.bmt_Medium, SAPbouiCOM.BoStatusBarMessageType.smt_Success);

                #region AutoItemMasterCodeSetup
                B1Helper.AddTable("ITN_OICS", "Auto Item Master Code Setup", BoUTBTableType.bott_Document);
                B1Helper.AddField("ITCS", "Item Master Code Status", "ITN_OICS", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ITDS", "Item Master Descrition Setup", "ITN_OICS", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ITCL", "Item Code Length", "ITN_OICS", BoFieldTypes.db_Numeric, 10, BoYesNoEnum.tNO, SAPbobsCOM.BoFldSubTypes.st_None, true);
                B1Helper.AddField("ITDL", "Item Code Length", "ITN_OICS", BoFieldTypes.db_Numeric, 10, BoYesNoEnum.tNO, SAPbobsCOM.BoFldSubTypes.st_None, true);
                B1Helper.AddField("ITPL", "Parameter Length", "ITN_OICS", BoFieldTypes.db_Numeric, 10, BoYesNoEnum.tNO, SAPbobsCOM.BoFldSubTypes.st_None, true);

                //child
                B1Helper.AddTable("ITN_ICS1", "Auto Item Master Code Setup CH", BoUTBTableType.bott_DocumentLines);
                B1Helper.AddField("Code", "Code", "ITN_ICS1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("Parameter", "Parameter Name", "ITN_ICS1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                Array.Resize(ref FormColumn, 0);
                Array.Resize(ref ChildTable, 1);
                ChildTable[0] = "ITN_ICS1";
                B1Helper.CreateUdo("OICS", "Auto Item Master Code Setup", "ITN_OICS", "D", "N", FormColumn, ChildTable);
                #endregion

                #region Item Code Logic Setup
                B1Helper.AddTable("ITN_OILC", "Item Code Logic Setup", BoUTBTableType.bott_Document);
                B1Helper.AddField("ITMSPTR", "Seperator", "ITN_OILC", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("DESCSPTR", "Descrition Seperator", "ITN_OILC", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ITMCOD", "Item Code", "ITN_OILC", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("DESCCOD", "Item Code", "ITN_OILC", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ITMCODLN", "Item Code Length", "ITN_OILC", BoFieldTypes.db_Numeric, 10, BoYesNoEnum.tNO, SAPbobsCOM.BoFldSubTypes.st_None, true);
                B1Helper.AddField("DESCLEN", "Item Code Length", "ITN_OILC", BoFieldTypes.db_Numeric, 10, BoYesNoEnum.tNO, SAPbobsCOM.BoFldSubTypes.st_None, true);

                //Child
                B1Helper.AddTable("ITN_ILC1", "Item Code Logic Setup CH", BoUTBTableType.bott_DocumentLines);
                B1Helper.AddField("Code", "Code", "ITN_ILC1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("PARM", "Parameter Name", "ITN_ILC1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("DGTS", "No of Digits", "ITN_ILC1", BoFieldTypes.db_Numeric, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                //B1Helper.AddField("DESCDGTS", "No of Digits(Desc)", "ITN_ILC1", BoFieldTypes.db_Numeric, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);

                //child
                B1Helper.AddTable("ITN_ILC2", "Item Code Logic Setup CH", BoUTBTableType.bott_DocumentLines);
                B1Helper.AddField("Code", "Code", "ITN_ILC2", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("PARM", "Parameter Name", "ITN_ILC2", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("DESCDGTS", "No of Digits", "ITN_ILC2", BoFieldTypes.db_Numeric, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                Array.Resize(ref FormColumn, 0);
                Array.Resize(ref ChildTable, 2);
                ChildTable[0] = "ITN_ILC1";
                ChildTable[1] = "ITN_ILC2";
                B1Helper.CreateUdo("OILC", "Item Code Logic Setup", "ITN_OILC", "D", "N", FormColumn, ChildTable);
                #endregion
            }
            catch
            {
            }
        }
        #endregion

        #region Methods
        public static bool InstallUDOs()
        {
            try
            {
                bool UDOAdded = true;

                string[] ChildTable = new string[0];
                string[] FindColumn = new string[0];
                string[] FormColumn = new string[0];
                B1Helper.AddTable("ITN_OGDL", "G/L Determination", BoUTBTableType.bott_Document);

                #region System Tables Fields

                //B1Helper.DiCompany.StartTransaction();
                #region OADM
                Core.SAPB1.B1Helper.AddField("LOCSTATS", "Letter Of Credit", "OADM", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ATOGEN", "Auto Item Code Generation", "OADM", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                #endregion

                #region OCRD
                B1Helper.AddField("LCMRGN", "LC MARGIN", "OCRD", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                #endregion

                #region Tracking

                //Tracking
                B1Helper.AddTable("ITNTRCK", "Tracking", BoUTBTableType.bott_Document);
                //FindColumns = new List<string> { "DISTCODE", "Code", "Name" };
                B1Helper.AddField("CODE", "Code", "ITNTRCK", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("NAME", "Name", "ITNTRCK", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 3);
                FormColumn[0] = "DocEntry";
                FormColumn[1] = "U_CODE";
                FormColumn[2] = "U_NAME";
                CreateUDO("ITNTRCK", "Tracking", "ITNTRCK", FormColumn, BoUDOObjType.boud_Document, "F");

                #endregion

                #region Shipment Tacking

                B1Helper.AddTable("ITN_OSPT", "Shipment tracking", BoUTBTableType.bott_Document);

                //Header Fields
                B1Helper.AddField("ODOCRBB", "DOCUMENT RECEIVED", "ITN_OSPT", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PODNUM", "PURCHASE ORDER NUMBER", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PODENT", "PURCHASE ORDER DOCENTRY", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PODT", "PO DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("VCODE", "VENDOR CODE", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PROINDE", "PROFORMA INVOICE DOCENTRY", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PROINNUM", "PROFORMA INVOICE DOCNUM", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PERDT", "PROFORMA DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("COMINDE", "COMMERCIAL INVOICE DOCENTRY", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("COMINNUM", "COMMERCIAL INVOICE DOCNUM", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("COMDT", "COMMERCIAL DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("GRPODE", "GRPO DOCENTRY", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("GRPONUM", "GRPO DOCNUM", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("GRPODT", "GRPO DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("BLDNUM", "BL DOC NUMBER", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BLDT", "BL DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("SHNDT", "NEPALI DATE", "ITN_OSPT", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("SHDELNDT", "DELIVERY DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("TRANME", "TRANSPORTER NAME", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("LRNO", "LR NO", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ETADTPA", "EST DT OF PORT ARR", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("ETAHRPA", "EST HOUR ON PORT ARR", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_Time, true);
                B1Helper.AddField("DOCRELDT", "DOC RELEASE DT", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("STATUS", "STATUS", "ITN_OSPT", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("LCDE", "LC DOCENTRY", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("LCDN", "LC DOCNUM", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("LCCODE", "LC CODE", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("LCDT", "LC DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("ORDOCRECBYBNK", "ORIGINAL DOC REC BY BANK", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("BNKCLDT", "BANK CLEAR DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("PAYDATE", "PAYMENT DATE", "ITN_OSPT", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("DOCTOTAL", "DOCUMENT TOTAL", "ITN_OSPT", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Price, true, "");
                B1Helper.AddField("PREPBY", "PREPARED BY", "ITN_OSPT", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("REMARKS", "REMARKS", "ITN_OSPT", BoFieldTypes.db_Alpha, 250, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ATCHMENT", "ATTACHMENT", "ITN_OSPT", BoFieldTypes.db_Memo, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Link, true, "");
                B1Helper.AddField("WHSCODE", "WARE HOUSE CODE", "ITN_OSPT", BoFieldTypes.db_Alpha, 25, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("WHSNAME", "WARE HOUSE NAME", "ITN_OSPT", BoFieldTypes.db_Alpha, 150, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");

                //Child table
                B1Helper.AddTable("ITN_SPT1", "Shipment tracking CH1", BoUTBTableType.bott_DocumentLines);

                //Child Fields.
                B1Helper.AddField("ITEMCODE", "ITEM CODE", "ITN_SPT1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("DSCRIPTION", "DESCRIPTION", "ITN_SPT1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("QTY", "QUANTITY", "ITN_SPT1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Quantity, true);
                B1Helper.AddField("RATE", "RATE", "ITN_SPT1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Quantity, true);
                B1Helper.AddField("CURR", "CURRENCY", "ITN_SPT1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("AMT", "AMOUNT", "ITN_SPT1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Price, true);

                //Child table
                B1Helper.AddTable("ITN_SPT2", "Shipment tracking CH2", BoUTBTableType.bott_DocumentLines);

                //Child Fields.
                B1Helper.AddField("TRACD", "TRACKING CODE", "ITN_SPT2", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("CLRNCEDT", "CLEARENCE DATE", "ITN_SPT2", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("REMARKS", "REMARKS", "ITN_SPT2", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("GRPODE", "GRPO DOCENTRY", "ITN_SPT2", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("GRPODN", "GRPO DOCNUM", "ITN_SPT2", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);

                //Child table
                B1Helper.AddTable("ITN_SPT3", "Shipment tracking CH3", BoUTBTableType.bott_DocumentLines);

                //Child Fields.
                B1Helper.AddField("ATCHMENT", "ATTACHMENT", "ITN_SPT3", BoFieldTypes.db_Memo, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Link, true);
                B1Helper.AddField("FILENAME", "FILE NAME", "ITN_SPT3", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("DATE", "DATE", "ITN_SPT3", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);

                Array.Resize(ref FormColumn, 0);
                Array.Resize(ref ChildTable, 3);
                ChildTable[0] = "ITN_SPT1";
                ChildTable[1] = "ITN_SPT2";
                ChildTable[2] = "ITN_SPT3";

                B1Helper.CreateUdo("OSPT", "Shipment Tracking", "ITN_OSPT", "D", "N", FormColumn, ChildTable);

                #endregion

                #region Gatepass

                //GatePass table 
                Core.SAPB1.B1Helper.AddTable("ITN_OGTP", "Gate entry", BoUTBTableType.bott_Document);

                //Header fields
                B1Helper.AddField("TRASTYPE", "TRANSACTION TYPE", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("REFDOCTY", "REF DOCUMENT", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("REFBASDO", "REF BASE DOCUMENT", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("REFBDOEN", "REF BASE DOCENTRY", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PARCODE", "PARTY CODE", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PARNAME", "PARTY NAME", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("WHSCODE", "WAREHOUSE CODE", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("WHSNAME", "WAREHOUSE NAME", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("EXITDATE", "EXIT DATE", "ITN_OGTP", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("EXITNPDT", "EXIT NEPALI DATE", "ITN_OGTP", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("EXITTIME", "EXITTIME", "ITN_OGTP", BoFieldTypes.db_Date, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Time, true, "");
                B1Helper.AddField("INDATE", "IN DATE", "ITN_OGTP", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("INNPDATE", "IN NEPALI DATE", "ITN_OGTP", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("INTIME", "IN TIME", "ITN_OGTP", BoFieldTypes.db_Date, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Time, true, "");
                B1Helper.AddField("GATENNUM", "GATE ENTRY NUMBER", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("TRASDETL", "TRANSPORT DETAILS", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("VEHNUM", "VEHICLE NUMBER", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("DRINAME", "DRIVER NAME", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("CONTNUM", "CONTACT NUMBER", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PREPBY", "PREPARED BY", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("REMARKS", "REMARKS", "ITN_OGTP", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");

                //Child table
                B1Helper.AddTable("ITN_GTP1", "Gate entry CH", BoUTBTableType.bott_DocumentLines);

                //Child fields
                B1Helper.AddField("SELECT", "SELECT", "ITN_GTP1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ITEMCODE", "ITEM CODE", "ITN_GTP1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("NAME", "NAME", "ITN_GTP1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("UOM", "UOM", "ITN_GTP1", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("QUANTITY", "QUANTITY", "ITN_GTP1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("INEXTQTY", "IN/EXIT QTY", "ITN_GTP1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");

                Array.Resize(ref ChildTable, 1);
                ChildTable[0] = "ITN_GTP1";

                B1Helper.CreateUdo("OGTP", "GATE PASS", "ITN_OGTP", "D", "N", FormColumn, ChildTable);

                #endregion

                #region OPOR
                B1Helper.AddField("PURORGN", "PURCHASE ORIGIN", "OPOR", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("LOC", "Letter of Credit", "OPOR", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ITN_PFI", "Proforma Invoice", "OPOR", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ITN_CI", "Commercial Invoice", "OPOR", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ITN_PINUM", "PROFORMA INVOICE NUMBER", "OPOR", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ITN_PIDT", "PROFORMA INVOICE DATE", "OPOR", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false);
                B1Helper.AddField("ITN_LCPUR", "LC PURCHASE", "OPOR", BoFieldTypes.db_Alpha, 1, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ITN_PROVCOST", "PROVISIONAL COST", "POR1", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                #endregion

                #region OPCH
                B1Helper.AddField("PPNO", "PP NUMBER", "OPCH", SAPbobsCOM.BoFieldTypes.db_Alpha, 50, SAPbobsCOM.BoYesNoEnum.tNO, false);
                B1Helper.AddField("PPDAT", "PP DATE", "OPCH", SAPbobsCOM.BoFieldTypes.db_Date, SAPbobsCOM.BoYesNoEnum.tNO, false);
                B1Helper.AddField("PPMITI", "PP MITI", "OPCH", SAPbobsCOM.BoFieldTypes.db_Date, SAPbobsCOM.BoYesNoEnum.tNO, false);
                B1Helper.AddField("CUSENPONT", "CUSTOM ENTRY POINT", "OPCH", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("CUSDUTY", "CUSTOMS DUTY", "OPOR", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("EXDUTY", "EXCISE DUTY", "OPOR", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("VAT", "VAT", "OPOR", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("CSFCHRGE", "CSF CHARGES", "OPOR", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, ""); B1Helper.AddField("LCMARREF", "LC MARGIN REFUND", "OPOR", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("AGCLRACCT", "AGENT CLEARING ACCOUNT CODE", "OPOR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("AGCLRACTNME", "AGENT CLEARING ACCOUNT NAME", "OPOR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("REALSNLC", "REALIZATION LC", "OPOR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("CONREALSNJV", "CONSOLIDATED REALIZATION JV", "OPCH", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("PAYMENTLC", "PAYMENT LC", "OPOR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("PPJOURNAL", "PP JOURNAL ENTRY", "OPOR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("CONJOURNAL", "CONSOLIDATION JOURNAL ENTRY", "OPOR", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("ITNLC", "Landed Cost", "POR1", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");

                #endregion

                #region OIPF
                B1Helper.AddField("CUSDUTY", "CUSTOMS DUTY", "OIPF", BoFieldTypes.db_Float, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("EXDUTY", "EXCISE DUTY", "OIPF", BoFieldTypes.db_Float, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("CSFCHRGE", "CSF CHARGES", "OIPF", BoFieldTypes.db_Float, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("LCFEE", "LC FEE", "OIPF", BoFieldTypes.db_Float, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("LCNUM", "LC NUMBER", "OIPF", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");

                //IPF1
                B1Helper.AddField("ITMCUSDUTY", "ITEM WISE CUSTOMS DUTY", "IPF1", BoFieldTypes.db_Float, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("ITEMEXDUTY", "ITEM WISE EXCISE DUTY", "IPF1", BoFieldTypes.db_Float, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                //IPF2
                // B1Helper.AddField("STDCOST", "STANDARD COST", "IPF2", BoFieldTypes.db_Float, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");
                B1Helper.AddField("SERINV", "SERVICE INVOICE", "IPF2", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, false, "");
                B1Helper.AddField("PCost", "PROVISIONAL COST", "IPF2", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, false, "");

                #endregion

                #region Letter of Credit

                //LOC HEADER TABLE
                B1Helper.AddTable("ITN_OLOC", "Letter of Credit", BoUTBTableType.bott_Document);

                //HEADER FIELDS
                //B1Helper.AddField("DOCNUM", "DOCUMENT NUMBER", "ITN_OLOC", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("Vcode", "Vendor Code", "ITN_OLOC", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("LCNUM", "LC NUMBER", "ITN_OLOC", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("LOCTYPE", "LOCTYPE", "ITN_OLOC", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("LOCFEE", "LOC FEE", "ITN_OLOC", BoFieldTypes.db_Float, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("CURRENCY", "CURRENCY", "ITN_OLOC", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("TOLERANCE", "TOLERANCE PERCENTAGE", "ITN_OLOC", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("TOLVAL", "TOLERANCE AMOUNT", "ITN_OLOC", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("BUSPART", "BUSSINESS PARTNER CODE", "ITN_OLOC", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BUSPARTNAME", "BUSSINESS PARTNER NAME", "ITN_OLOC", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("STATUS", "STATUS", "ITN_OLOC", BoFieldTypes.db_Alpha, 15, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PROINVDT", "PROFORMA INVOICE DATE", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("DOCRECDT", "DOCUMENT RECEIPT DATE", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("GDRECDT", "GOODS RECEIVED DATE", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("LCVALUE", "LC VALUE", "ITN_OLOC", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("LCAMDVAL", "LC AMENDED VALUE", "ITN_OLOC", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("LCAMDDT", "LC AMENDED DATE", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                //B1Helper.AddField("LCDATE", "LC DATES", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("DTMATDIS", "DATE ON MATERIAL DISPATCHED", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("LOCREQDT", "LOC REQUESTED DATE", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("LOCOPENDT", "LOC OPENING DATE", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("FINDELDT", "FINAL DELIVERY DATE LOC", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("EXPDATE", "LC EXPIRY DATE", "ITN_OLOC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("LCMARGIN", "LC MARGIN", "ITN_OLOC", BoFieldTypes.db_Float, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("BANKCODE", "BANK CODE", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BANKNAME", "BANK NAME", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BANKBRANCH", "BANK BRANCH CODE", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BNKBRNCHNME", "BANK BRANCH NAME", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ISUBNKREF", "ISSUING BANK REFERENCE", "ITN_OLOC", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("SENDREF", "SENDERS REFERENCE", "ITN_OLOC", BoFieldTypes.db_Alpha, 150, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PINUM", "PINUM", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("CUSAGENT", "CUSTOM AGENT", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("CUSAGNME", "CUSTOM AGENT NAME", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("CUSLOC", "CUSTOM LOCATION CODE", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("CUSLOCNAME", "CUSTOM LOCATION NAME", "ITN_OLOC", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("TENOR", "TENOR", "ITN_OLOC", BoFieldTypes.db_Numeric, 11, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("TERMPAY", "TERMS OF PAYMENT CODE", "ITN_OLOC", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("TERMPAYNAME", "TERMS OF PAYMENT NAME", "ITN_OLOC", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ENTERBY", "ENTER BY", "ITN_OLOC", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("REMARKS", "REMARKS", "ITN_OLOC", BoFieldTypes.db_Alpha, 200, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("LEDGEPOS", "LC LEDGER POSING", "ITN_OLOC", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("BRANCH", "BRANCH", "ITN_OLOC", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("PODOCENTRY", "PURCHASE ORDER DOCENTRY", "ITN_OLOC", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");

                //LOC CHILD1 TABLE
                Core.SAPB1.B1Helper.AddTable("ITN_LOC1", "Letter of Credit CH1", BoUTBTableType.bott_DocumentLines);

                //LOC CHILD1
                B1Helper.AddField("DOCNUM", "DOCUMENT NUMBER", "ITN_LOC1", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("DOCNO", "DOCUMENT ENTRY", "ITN_LOC1", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("TRANSACTN", "TRANSACTION", "ITN_LOC1", BoFieldTypes.db_Alpha, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("DOCDATE", "DOCUMENT DATE", "ITN_LOC1", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("DUEDATE", "DUE DATE", "ITN_LOC1", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("TOTALAMT", "TOTAL AMOUNT", "ITN_LOC1", BoFieldTypes.db_Float, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");

                //LOC CHILD2 TABLE
                Core.SAPB1.B1Helper.AddTable("ITN_LOC2", "Letter of Credit CH2", BoUTBTableType.bott_DocumentLines);

                //LOC CHILD2
                B1Helper.AddField("DOCNUM", "DOCUMENT NUMBER", "ITN_LOC2", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("TRANSACTN", "TRANSACTION", "ITN_LOC2", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("DOCDATE", "DOCUMENT DATE", "ITN_LOC2", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("DUEDATE", "DUE DATE", "ITN_LOC2", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("TOTALAMT", "TOTAL AMOUNT", "ITN_LOC2", BoFieldTypes.db_Float, 30, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");

                //LOC CHILD3 TABLE
                Core.SAPB1.B1Helper.AddTable("ITN_LOC3", "Letter of Credit CH3", BoUTBTableType.bott_DocumentLines);

                //LOC CHILD3
                B1Helper.AddField("DATE", "DATE", "ITN_LOC3", BoFieldTypes.db_Date, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true);
                B1Helper.AddField("ATTACHMENT", "ATTACHMENT", "ITN_LOC3", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("FILENAME", "FILE NAME", "ITN_LOC3", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("FREETEXT", "FREE TEXT", "ITN_LOC3", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");

                Array.Resize(ref FormColumn, 0);
                Array.Resize(ref ChildTable, 3);
                ChildTable[0] = "ITN_LOC1";
                ChildTable[1] = "ITN_LOC2";
                ChildTable[2] = "ITN_LOC3";

                B1Helper.CreateUdo("OLOC", "Letter of Credit", "ITN_OLOC", "D", "", FormColumn, ChildTable);

                #endregion

                #region Provisional Cost

                //PROVISIONAL TABLE
                B1Helper.AddTable("ITN_OPRC", "Provisional Cost", BoUTBTableType.bott_Document);

                //HEADER FIELDS
                B1Helper.AddField("DOCNUM", "DOCUMENT NUMBER", "ITN_OPRC", BoFieldTypes.db_Alpha, 5, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("MITI", "NEPALI DATE", "ITN_OPRC", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);
                B1Helper.AddField("PREPBY", "PREPARED BY", "ITN_OPRC", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("REMARKS", "REMARKS", "ITN_OPRC", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");

                B1Helper.AddField("IGNAME", "ITEM GROUP NAME", "ITN_OPRC", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("IGROUP", "ITEM GROUP", "ITN_OPRC", BoFieldTypes.db_Alpha, 10, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");


                //CHILD1 TABLE
                B1Helper.AddTable("ITN_PRC1", "Provisional Cost CH1", BoUTBTableType.bott_DocumentLines);

                //CHILD1 FIELDS 
                B1Helper.AddField("ITEMCODE", "ITEM CODE", "ITN_PRC1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("ITEMNAME", "ITEM NAME", "ITN_PRC1", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("TRANINSU", "TRANSIT INSURANCE", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("FRSHLINE", "FREIGHT/SHIPPING LINE", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("SHLNDETCST", "SHIPPING LINE DETENTION COST", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("KOLCLRCST", "KOLKATA PORT CLEARING COST", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("FRKOLPORT", "FREIGHT FROM KOLKATA PORT", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("INDCUSCST", "INDIA CUSTOM CLEARING COST", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("CUSTCOST", "CUSTOM DUTY", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("EXCISEDUTY", "EXCISE DUTY", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("OTHADDDUTY", "OTHER ADDITIONAL DUTIES", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("CSFCHARGE", "CSF CHARGES", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("NPCUSCLRCST", "NEPAL CUSTOM CLEARING COST", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("DTNSNCST", "DETENTION COST", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("UNLOADEXPN", "UNLOADING EXPENSES", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");
                B1Helper.AddField("LCCOMMISN", "LC COMMISSION", "ITN_PRC1", BoFieldTypes.db_Float, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Rate, true, "");

                //CHILD2 TABLE
                B1Helper.AddTable("ITN_PRC2", "Provisional Cost CHL2", BoUTBTableType.bott_DocumentLines);

                //CHILD2 FIELDS
                B1Helper.AddField("ATCHMENT", "ATTACHMENT", "ITN_PRC2", BoFieldTypes.db_Memo, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_Link, true);
                B1Helper.AddField("FILENAME", "FILE NAME", "ITN_PRC2", BoFieldTypes.db_Alpha, 100, BoYesNoEnum.tNO, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("DATE", "DATE", "ITN_PRC2", BoFieldTypes.db_Date, BoYesNoEnum.tNO, true);

                Array.Resize(ref FormColumn, 0);
                Array.Resize(ref ChildTable, 2);
                ChildTable[0] = "ITN_PRC1";
                ChildTable[1] = "ITN_PRC2";
                B1Helper.CreateUdo("OPRC", "PROVISIONAL COST", "ITN_OPRC", "D", "N", FormColumn, ChildTable);

                #endregion

                #region G/L Determination

                B1Helper.AddTable("ITN_OGDL", "G/L Determination", BoUTBTableType.bott_Document);
                B1Helper.AddField("ACCNAM", "Account Name", "ITN_OGDL", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("GLLDGR", "G/L Ledger", "ITN_OGDL", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                B1Helper.AddField("SysAccount", "System Account", "ITN_OGDL", BoFieldTypes.db_Alpha, 20, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");

                // Array.Resize(ref FindColumn, 1);
                Array.Resize(ref FormColumn, 4);
                FormColumn[0] = "DocEntry";
                FormColumn[1] = "U_ACCNAM";
                FormColumn[2] = "U_GLLDGR";
                FormColumn[3] = "U_SysAccount";
                CreateUDO("OGDL", "G/L Determination", "ITN_OGDL", FormColumn, BoUDOObjType.boud_Document, "F");

                #endregion

                #region Terms of Payment

                B1Helper.AddTable("ITN_OLPT", "Terms of Payment", BoUTBTableType.bott_MasterData);
                B1Helper.AddField("PAYTERMS", "Payment Terms", "ITN_OLPT", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 2);
                Array.Resize(ref ChildTable, 0);
                FormColumn[0] = "Code";
                FormColumn[1] = "U_PAYTERMS";
                CreateUDO("OLPT", "Terms Of Payment", "ITN_OLPT", FormColumn, BoUDOObjType.boud_MasterData, "F");

                #endregion

                #region Letter Of Credit Type

                B1Helper.AddTable("ITN_OSLT", "Letter Of Credit Type", BoUTBTableType.bott_MasterData);
                B1Helper.AddField("LOCTYPE", "Letter Of Credit Type", "ITN_OSLT", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 2);
                FormColumn[0] = "Code";
                FormColumn[1] = "U_LOCTYPE";
                CreateUDO("OSLT", "Letter Of Credit Type", "ITN_OSLT", FormColumn, BoUDOObjType.boud_MasterData, "F");

                #endregion

                #region Customer Location
                B1Helper.AddTable("ITN_OSCL", "Customer Location", BoUTBTableType.bott_MasterData);
                B1Helper.AddField("CUSTLOC", "Customer Location", "ITN_OSCL", BoFieldTypes.db_Alpha, 50, BoYesNoEnum.tYES, BoFldSubTypes.st_None, true, "");
                Array.Resize(ref FormColumn, 2);
                FormColumn[0] = "Code";
                FormColumn[1] = "U_CUSTLOC";
                CreateUDO("OSCL", "Customer Location", "ITN_OSCL", FormColumn, BoUDOObjType.boud_MasterData, "F");

                #endregion

                #region ODLN
                B1Helper.AddField("ITN_MQTY", "Measured Quantity", "OPCH", SAPbobsCOM.BoFieldTypes.db_Float, 15, SAPbobsCOM.BoYesNoEnum.tNO, BoFldSubTypes.st_Quantity, false);
                B1Helper.AddField("ITN_RQTY", "Ratio of QTY", "PCH1", SAPbobsCOM.BoFieldTypes.db_Float, 15, SAPbobsCOM.BoYesNoEnum.tNO, BoFldSubTypes.st_Quantity, false);
                B1Helper.AddField("ITN_AQTY", "Actual Quantity", "PCH1", SAPbobsCOM.BoFieldTypes.db_Float, 15, SAPbobsCOM.BoYesNoEnum.tNO, BoFldSubTypes.st_Quantity, false);
                #endregion

                //B1Helper.DiCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                #endregion

                //Utility.LogException("Ending Transaction: UDOs Creation Process");
                //B1Helper.DiCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_Commit);
                //#endregion

                return UDOAdded;
            }
            catch (Exception ex)
            {
                //Utility.LogException(ex);
                //B1Helper.DiCompany.EndTransaction(SAPbobsCOM.BoWfTransOpt.wf_RollBack);
                return false;
            }
        }

        private static bool CreateUDO(string CodeID, string Name, string TableName, string[] FormColoums, SAPbobsCOM.BoUDOObjType ObjectType, string ManageSeries)
        {
            SAPbobsCOM.UserObjectsMD oUserObjectMD = default(SAPbobsCOM.UserObjectsMD);
            try
            {
                oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(Program.oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
                if (oUserObjectMD.GetByKey(CodeID) == true)
                {
                    return true;
                }
                oUserObjectMD.CanLog = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanFind = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanClose = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanCancel = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.CanDelete = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.ManageSeries = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.CanYearTransfer = SAPbobsCOM.BoYesNoEnum.tNO;

                oUserObjectMD.Code = CodeID;
                oUserObjectMD.Name = Name;
                oUserObjectMD.TableName = TableName;
                oUserObjectMD.ObjectType = ObjectType;


                oUserObjectMD.CanCreateDefaultForm = SAPbobsCOM.BoYesNoEnum.tYES;
                oUserObjectMD.EnableEnhancedForm = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.MenuItem = SAPbobsCOM.BoYesNoEnum.tNO;
                oUserObjectMD.MenuCaption = Name;
                oUserObjectMD.FatherMenuID = 47616;
                oUserObjectMD.Position = 0;
                oUserObjectMD.MenuUID = CodeID;

                if (FormColoums != null)
                {
                    for (int i = 0; i <= FormColoums.Length - 1; i++)
                    {
                        if (FormColoums[i].Trim() != "U_RUNDB")
                        {
                            oUserObjectMD.FormColumns.FormColumnAlias = FormColoums[i];
                            oUserObjectMD.FormColumns.Editable = SAPbobsCOM.BoYesNoEnum.tNO;
                            oUserObjectMD.FormColumns.Add();
                        }
                        else
                        {
                            oUserObjectMD.FormColumns.FormColumnAlias = FormColoums[i];
                            oUserObjectMD.FormColumns.Editable = SAPbobsCOM.BoYesNoEnum.tYES;
                            oUserObjectMD.FormColumns.Add();
                        }
                    }
                }
                // check for errors in the process
                RetCode = oUserObjectMD.Add();

                if (RetCode != 0)
                {
                    if (RetCode != -1)
                    {
                        Program.oCompany.GetLastError(out RetCode, out ErrMsg);
                        Program.SBO_Application.StatusBar.SetText("Object Failed : " + ErrMsg + "", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Error);
                    }
                }
                else
                {
                    Program.SBO_Application.StatusBar.SetText("Object Registered : " + Name + "", SAPbouiCOM.BoMessageTime.bmt_Short, SAPbouiCOM.BoStatusBarMessageType.smt_Success);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oUserObjectMD);
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        public static bool GetCommonSettings()
        {
            string query = "SELECT T0.\"U_A_Email\", T0.\"U_S_Email\", T0.\"U_J_Email\" , \"U_ExcessDay\" , \"U_N_Email\" FROM OADM T0";
            SAPbobsCOM.Recordset rsQry = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            rsQry.DoQuery(query);
            if (rsQry.RecordCount > 0)
            {
                Globals.SetsAEmail(rsQry.Fields.Item(0).Value.ToString());
                Globals.SetsSEmail(rsQry.Fields.Item(1).Value.ToString());
                Globals.SetsJournal(rsQry.Fields.Item(2).Value.ToString());
                Globals.SetsExcessDay(Convert.ToDouble(rsQry.Fields.Item(3).Value.ToString()));
                Globals.SetsNEmail(rsQry.Fields.Item(4).Value.ToString());
            }

            query = "SELECT T0.\"U_BillProcees\", T0.\"U_Account\" FROM \"@Z_SCGL\"  T0";
            rsQry = (SAPbobsCOM.Recordset)B1Helper.DiCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            rsQry.DoQuery(query);
            if (rsQry.RecordCount > 0)
            {
                while (rsQry.EoF == false)
                {
                    if (rsQry.Fields.Item(0).Value.ToString() == "A")
                    { Globals.SetsSAdvance(rsQry.Fields.Item(1).Value.ToString()); }
                    else if (rsQry.Fields.Item(0).Value.ToString() == "C") { Globals.SetsSCredit(rsQry.Fields.Item(1).Value.ToString()); }
                    rsQry.MoveNext();
                }
            }
            rsQry = null;
            return true;

        }
        public static void SetFormFilter()
        {
            try
            {
                //SAPbouiCOM.EventFilters objFilters = new SAPbouiCOM.EventFilters();
                //SAPbouiCOM.EventFilter objFilter;

                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_LOAD);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_CLOSE);
                //objFilter.AddEx("frm_TransferItems");



                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_MENU_CLICK);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_ITEM_PRESSED);
                //objFilter.AddEx("frm_TransferItems");



                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_COMBO_SELECT);
                //objFilter.AddEx("frm_TransferItems");

                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_CHOOSE_FROM_LIST);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_KEY_DOWN);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_LOST_FOCUS);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_VALIDATE);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_FORM_DATA_LOAD);
                //objFilter.AddEx("frm_TransferItems");



                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_CLICK);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_RIGHT_CLICK);
                //objFilter.AddEx("frm_TransferItems");


                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_DOUBLE_CLICK);
                //objFilter.AddEx("frm_TransferItems");

                //objFilter = objFilters.Add(SAPbouiCOM.BoEventTypes.et_PICKER_CLICKED);
                //objFilter.AddEx("frm_TransferItems");


                //SetFilter(objFilters);
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                // Log.LogException(LogLevel.Error, ex);
            }
        }
        public static void RemoveMenu(string menuId)
        {
            Application.SBO_Application.Menus.RemoveEx(menuId);
        }
        public static string GetNextEntryIndex(string tableName)
        {
            try
            {
                var result = B1Helper.GetNextEntryIndex(tableName);
                if (result.Equals(string.Empty))
                    result = "0";
                else
                    if (result.Equals("0"))
                    {
                        result = "1";
                    }

                return result;
            }
            catch (Exception ex)
            {
                Utility.LogException(ex);
                // Log.LogException(LogLevel.Error, ex);
                return null;
            }

        }
        protected static void SetFilter(SAPbouiCOM.EventFilters Filters)
        {
            Application.SBO_Application.SetFilter(Filters);
        }
        #endregion
    }
}

