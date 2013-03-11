using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Library.Scan;

namespace ArtCreate
{
    partial class PdfService : ServiceBase
    {
        public PdfService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        protected override void OnContinue()
        {
            base.OnContinue();
        }

        protected override void OnPause()
        {
            base.OnPause();
        }
        private void CreatePdf()
        {
            var dbo = new DbObject();
            dbo.DataSet("SELECT * FROM SCAN_CILT ")
            dbo.Dispose();
        }
    }
}
