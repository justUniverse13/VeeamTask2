using System.Collections.Generic;

namespace TestProjectSelenium.Code.Dto.Permissions
{
    class PermissionSettings
    {
        #region Case Info

        public List<KeyValuePair<string, bool>> CaseInfo = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Relativity Id", true),
            new KeyValuePair<string, bool>("Status", true),
            new KeyValuePair<string, bool>("Matter #", true),
            new KeyValuePair<string, bool>("Sales Rep", true),
            new KeyValuePair<string, bool>("Case PM", true),
            new KeyValuePair<string, bool>("PM Team", true),
            new KeyValuePair<string, bool>("Hosting Status", true),
            new KeyValuePair<string, bool>("Review Status", true),
            new KeyValuePair<string, bool>("Deposition Status", true),
            new KeyValuePair<string, bool>("Digital Reef Id", true)
        };

        #endregion

        #region Case Stats

        public KeyValuePair<string, bool> ShowCaseStats => new KeyValuePair<string, bool>("Show case stats", true);

        #endregion

        #region Details

        public KeyValuePair<string, bool> Details => new KeyValuePair<string, bool>("Details", true);

        public List<KeyValuePair<string, bool>> DetailsSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Collections", true),
            new KeyValuePair<string, bool>("Processing", true),
            new KeyValuePair<string, bool>("Review Batches", true),
            new KeyValuePair<string, bool>("Staffing", true),
            new KeyValuePair<string, bool>("Productions", true),
            new KeyValuePair<string, bool>("Deposition Services", true),
            new KeyValuePair<string, bool>("Reprographics", true)
        };

        #region Details Tables

        public List<KeyValuePair<string, bool>> CollectionsSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Data Source", true),
            new KeyValuePair<string, bool>("Custodian", true),
            new KeyValuePair<string, bool>("Status", true),
            new KeyValuePair<string, bool>("Date of Collection", true),
            new KeyValuePair<string, bool>("Item Count Collected", true),
            new KeyValuePair<string, bool>("Size Collected (GB)", true),
            new KeyValuePair<string, bool>("Item Count Extracted", true),
            new KeyValuePair<string, bool>("Size Extracted (GB)", true),
            new KeyValuePair<string, bool>("Evidence number", true)
        };

        public List<KeyValuePair<string, bool>> ProcessingSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Sources Size", true),
            new KeyValuePair<string, bool>("Export Table", true)
        };

        #region ProcessingTables

        public List<KeyValuePair<string, bool>> SourceSizeSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Date", true),
            new KeyValuePair<string, bool>("Source Size (GB)", true)
        };

        public List<KeyValuePair<string, bool>> ExportTableSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Date", true),
            new KeyValuePair<string, bool>("Volume", true),
            new KeyValuePair<string, bool>("Total Exported (GB)", true),
            new KeyValuePair<string, bool>("Total Exported (Docs)", true)
        };

        #endregion

        public List<KeyValuePair<string, bool>> ReviewBatchesSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Name", true),
            new KeyValuePair<string, bool>("Reviewed field Name", true),
            new KeyValuePair<string, bool>("Total Documents Batched", true),
            new KeyValuePair<string, bool>("Total Documents Reviewed", true),
            new KeyValuePair<string, bool>("Total Documents Remaining", true)
        };

        public List<KeyValuePair<string, bool>> StaffingSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Position", true),
            new KeyValuePair<string, bool>("Name", true),
            new KeyValuePair<string, bool>("Saturday", true),
            new KeyValuePair<string, bool>("Sunday", true),
            new KeyValuePair<string, bool>("Monday", true),
            new KeyValuePair<string, bool>("Tuesday", true),
            new KeyValuePair<string, bool>("Wednesday", true),
            new KeyValuePair<string, bool>("Thursday", true),
            new KeyValuePair<string, bool>("Friday", true),
            new KeyValuePair<string, bool>("Total Hours", true),
            new KeyValuePair<string, bool>("Regular Hours", true),
            new KeyValuePair<string, bool>("OT Hours", true),
            new KeyValuePair<string, bool>("Bill Rate (Reg)", true),
            new KeyValuePair<string, bool>("Bill Rate (OT)", true),
            new KeyValuePair<string, bool>("Bill (Reg)", true),
            new KeyValuePair<string, bool>("Bill(OT)", true),
            new KeyValuePair<string, bool>("Total Bill", true)
        };

        public List<KeyValuePair<string, bool>> ProductionsSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Production Volume", true),
            new KeyValuePair<string, bool>("Beg. Bates No.", true),
            new KeyValuePair<string, bool>("Beg. Bates No.", true),
            new KeyValuePair<string, bool>("# of Records ", true),
            new KeyValuePair<string, bool>("# of Images ", true),
            new KeyValuePair<string, bool>("Total Natives Produced", true)
        };

        public List<KeyValuePair<string, bool>> DepositionServicesSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Witness name", true),
            new KeyValuePair<string, bool>("Deposition Status", true),
            new KeyValuePair<string, bool>("Deposition Date", true),
            new KeyValuePair<string, bool>("Deposition Location", true)
        };

        public List<KeyValuePair<string, bool>> ReprographicsSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Project Number", true),
            new KeyValuePair<string, bool>("Description", true),
            new KeyValuePair<string, bool>("Total Pages", true),
            new KeyValuePair<string, bool>("Production Center", true),
            new KeyValuePair<string, bool>("Delivery Date", true)
        };

        #endregion

        #endregion

        #region Invoices

        public KeyValuePair<string, bool> Invoice => new KeyValuePair<string, bool>("Invoice", true);

        public List<KeyValuePair<string, bool>> InvoiceSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Invoice Date", true),
            new KeyValuePair<string, bool>("Invoice Due Date", true),
            new KeyValuePair<string, bool>("Invoice Amount", true),
            new KeyValuePair<string, bool>("Discount Amount", true),
            new KeyValuePair<string, bool>("Amount paid", true),
            new KeyValuePair<string, bool>("Combined", true)
        };

        #region InvoiceDetails

        public List<KeyValuePair<string, bool>> InvoiceDetailsSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Invoice Date", true),
            new KeyValuePair<string, bool>("Invoice Due Date", true),
            new KeyValuePair<string, bool>("Sales Amount", true),
            new KeyValuePair<string, bool>("Invoice Amount", true),
            new KeyValuePair<string, bool>("Discount Amount", true),
            new KeyValuePair<string, bool>("Amount paid", true),
            new KeyValuePair<string, bool>("Tax Amount", true),
            new KeyValuePair<string, bool>("Invoice Description", true),
        };

        public KeyValuePair<string, bool> ServiceDetails => new KeyValuePair<string, bool>("Service Details", true);

        #region ServiceDetails

        public List<KeyValuePair<string, bool>> ServiceDetailsSettings = new List<KeyValuePair<string, bool>>()
        {
            new KeyValuePair<string, bool>("Description", true),
            new KeyValuePair<string, bool>("Language Pair", true),
            new KeyValuePair<string, bool>("Service Code", true),
            new KeyValuePair<string, bool>("Service Description", true),
            new KeyValuePair<string, bool>("Quantity", true),
            new KeyValuePair<string, bool>("Billing rate", true),
            new KeyValuePair<string, bool>("Revenue Amount", true),
            new KeyValuePair<string, bool>("Unit of Measure", true)
        };

        #endregion

        #endregion

        #endregion

        public KeyValuePair<string, bool> Reports => new KeyValuePair<string, bool>("Reports", true);
    }
}