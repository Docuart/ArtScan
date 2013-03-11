using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Library.Scan;

namespace ArtPdf
{
    public partial class Form1 : Form
    {
        private const string IndexKlasoru = "F:\\KESİLMİŞ YÜKLENMİŞLER\\2008 Taramalar\\";
        private const string PdfKlasoru = "C:\\PDFs\\";

        public Form1()
        {
            InitializeComponent();

            // Create a simple tray menu with only one item.
            _trayMenu = new ContextMenu();
            _trayMenu.MenuItems.Add("Çıkış", OnExit);

            // Create a tray icon. In this example we use a
            // standard system icon for simplicity, but you
            // can of course use your own custom icon too.
            _trayIcon = new NotifyIcon();
            _trayIcon.Text = "PDF Oluşturucu";
            _trayIcon.Icon = Icon;

            // Add menu to tray icon and show it.
            _trayIcon.ContextMenu = _trayMenu;
            _trayIcon.Visible = true;
        }

        private readonly NotifyIcon _trayIcon;
        private readonly ContextMenu _trayMenu;

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.

            base.OnLoad(e);

            var dbo = new DbObject();
            var ds = dbo.DataSet("SELECT CILT_ID FROM SCAN_CILT WHERE CILT_DURUM = 'OK' AND CILT_PATH LIKE '%2008%' ORDER BY CILT_PATH");
            dbo.Dispose();
            var exString = "";
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                try
                {
                    CreatePdfFile(ds.Tables[0].Rows[i]["CILT_ID"].ToString());
                }
                catch (Exception ex)
                {
                    exString += ex.Message + "\n" + ex.StackTrace + "\n\n";
                    File.WriteAllText(PdfKlasoru + "Exceptions.Log", exString);
                }
            }
            _trayIcon.ShowBalloonTip(1000, "PDF Oluşturucu", "Tamamlandı.", ToolTipIcon.Info);
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreatePdfFile(string ciltId)
        {
            var parameters = new Parameter[1];
            parameters[0] = new Parameter("ciltId", ciltId);
            var dbo = new DbObject();
            var ds = dbo.DataSet(
                @"
SELECT D.DOSYA_ID, D.DOSYA_PATH, D.INDEKS, C.CILT_PATH, C.CILT_ID, D.YYIL, D.YNO, D.YTARIH
FROM SCAN_DOSYA D, SCAN_CILT C
WHERE D.CILT_ID = :ciltId
AND D.CILT_ID = C.CILT_ID
ORDER BY D.YYIL, D.YNO
",
                parameters);
            dbo.Dispose();

            var yTarih = "";
            var yNo = "";
            PdfPTable table = null;
            Document pdfDosya = null;
            for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                var ciltPath = ds.Tables[0].Rows[i]["CILT_PATH"].ToString();
                ciltPath = ciltPath.Substring(ciltPath.LastIndexOf("\\") + 1);

                if (yTarih != ds.Tables[0].Rows[i]["YTARIH"].ToString() || yNo != ds.Tables[0].Rows[i]["YNO"].ToString())
                {
                    yTarih = ds.Tables[0].Rows[i]["YTARIH"].ToString();
                    yNo = ds.Tables[0].Rows[i]["YNO"].ToString();

                    if (!Directory.Exists(Path.Combine(PdfKlasoru, ciltPath)))
                        Directory.CreateDirectory(Path.Combine(PdfKlasoru, ciltPath));

                    pdfDosya = new Document();
                    PdfWriter.GetInstance(pdfDosya, new FileStream(Path.Combine(PdfKlasoru, ciltPath, yTarih + " - " + yNo + ".pdf"), FileMode.Create));
                    pdfDosya.Open();
                    table = new PdfPTable(1);

                    _trayIcon.ShowBalloonTip(1, "PDF Oluşturucu", yTarih + " - " + yNo + " yevmiyesi oluşturuluyor.", ToolTipIcon.Info);
                }

                var cell = new PdfPCell(new Jpeg(new Uri(Path.Combine(IndexKlasoru, ciltPath, ds.Tables[0].Rows[i]["DOSYA_PATH"].ToString()))), true);
                if (table != null)
                    table.Rows.Add(new PdfPRow(new[] { cell }));


                if (i + 1 == ds.Tables[0].Rows.Count || yTarih != ds.Tables[0].Rows[i + 1]["YTARIH"].ToString() || yNo != ds.Tables[0].Rows[i + 1]["YNO"].ToString())
                {
                    if (pdfDosya != null)
                    {
                        pdfDosya.Add(table);
                        pdfDosya.Close();
                    }
                }
            }            
        }
    }
}
