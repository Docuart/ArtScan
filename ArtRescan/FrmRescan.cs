using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Library.Scan;

namespace ArtRescan
{
    public partial class FrmRescan : Form
    {
        public FrmRescan()
        {
            InitializeComponent();
        }

        private const string IndeksKlasoru = "I:\\";
        //private const string IndeksKlasoru = "D:\\Scanned";
        private List<Taranacak> _taranacakList;

        private static string TempPath()
        {
            return Path.GetTempPath() + "\\ArtRescan\\";
        }

        private void OnFormLoad(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(TempPath()))
                    Directory.Delete(TempPath(), true);
                Directory.CreateDirectory(TempPath());
                _secili.Text = "";
                _aciklama.Text = "";

                _taranacakList = new List<Taranacak>();
                var dbo = new DbObject();
                var ds = dbo.DataSet(
                    @"
SELECT D.*, C.CILT_PATH, C.CILT_ID
  FROM SCAN_DOSYA D, SCAN_CILT C
 WHERE D.DOSYA_DURUM = 'S' 
   AND D.CILT_ID = C.CILT_ID
 ORDER BY D.DOSYA_PATH
");
                dbo.Dispose();

                if (ds != null)
                {
                    for (var i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        var taranacak = new Taranacak();
                        taranacak.DosyaId = ds.Tables[0].Rows[i]["DOSYA_ID"].ToString();
                        taranacak.DosyaPath = ds.Tables[0].Rows[i]["DOSYA_PATH"].ToString();
                        taranacak.Indeks = ds.Tables[0].Rows[i]["INDEKS"].ToString();
                        taranacak.CiltPath = ds.Tables[0].Rows[i]["CILT_PATH"].ToString();
                        taranacak.CiltId = ds.Tables[0].Rows[i]["CILT_ID"].ToString();
                        taranacak.Aciklama = ds.Tables[0].Rows[i]["ACIKLAMA"].ToString();
                        _taranacakList.Add(taranacak);

                        _taranacaklar.Items.Add(taranacak.DosyaPath);
                    }
                    _toplam = ds.Tables[0].Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
            Text = "ArtRescan - Kalan:" + (_toplam - _kaydedilen);
        }

        private void OnFormSizeChanged(object sender, EventArgs e)
        {
            _taranacaklar.Height = Height - 50 - _taranacaklar.Top;
            _hataliResim.Height = Height - 50 - _hataliResim.Top;

            _hataliResim.Width = Width - 30 - _hataliResim.Left;
        }

        private void TaranacaklarSelectedIndexChanged(object sender, EventArgs e)
        {
            if (_taranacaklar.SelectedIndex == -1)
                return;

            var taranacak = _taranacakList[_taranacaklar.SelectedIndex];
            _secili.Text = taranacak.CiltPath;
            _aciklama.Text = taranacak.Durum + "-" + taranacak.Aciklama;

            _yevmiyeTarihi.Text = "";
            _yevmiyeNo.Text = "";

            if (!File.Exists(Path.Combine(TempPath(), taranacak.DosyaPath)))
                File.Copy(Path.Combine(IndeksKlasoru, _secili.Text, taranacak.DosyaPath), Path.Combine(TempPath(), taranacak.DosyaPath));
            
            if (File.Exists(Path.Combine(TempPath(), taranacak.DosyaPath)))
                _hataliResim.Image = Image.FromFile(Path.Combine(TempPath(), taranacak.DosyaPath));

            Text = "ArtRescan - Kalan:" + (_toplam - _kaydedilen);
        }

        private void TaraClick(object sender, EventArgs e)
        {
            var images = WIAScanner.Scan();
            _hataliResim.Image = images[0];
        }

        private int _kaydedilen = 0;
        private int _toplam = 0;
        private void KaydetClick(object sender, EventArgs e)
        {            
            var taranacak = _taranacakList[_taranacaklar.SelectedIndex];
            File.Delete(Path.Combine(IndeksKlasoru, _secili.Text, taranacak.DosyaPath));
            _hataliResim.Image.Save(Path.Combine(IndeksKlasoru, _secili.Text, taranacak.DosyaPath), ImageFormat.Jpeg);
                          
            DateTime dt;
            int yno;
            if (!DateTime.TryParse(_yevmiyeTarihi.Text, out dt))
            {
                MessageBox.Show("Tarih hatalı.");
                return;
            }                    
            if (!int.TryParse(_yevmiyeNo.Text, out yno) || yno < 0)
            {
                MessageBox.Show("Yevmiye No hatalı.");
                return;
            }                    

            var dbo = new DbObject();
            dbo.Execute("UPDATE SCAN_DOSYA SET DOSYA_DURUM = 'O', YTARIH = '" + dt.ToShortDateString() + "', YNO = '" + yno + "', YYIL = '" + dt.Year + "', ACIKLAMA = '' WHERE DOSYA_ID = " + taranacak.DosyaId, null);
            dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = DECODE(NVL((SELECT TO_CHAR(COUNT(1)) FROM SCAN_DOSYA WHERE DOSYA_DURUM = 'S' AND CILT_ID = " + taranacak.CiltId + "),'0'), '0', 'KNT', 'SCN') WHERE CILT_ID = " + taranacak.CiltId, null);
            dbo.Dispose();
                    
            _taranacakList[_taranacaklar.SelectedIndex].Durum = "KAYDEDİLDİ. " + dt.ToShortDateString() + " - " + yno;
            MessageBox.Show("Kayıt İşlemi Tamamlandı.", "İşlem Tamam", MessageBoxButtons.OK, MessageBoxIcon.Information);
            _kaydedilen++;
        }

        private void SilClick(object sender, EventArgs e)
        {
            var dr = MessageBox.Show("Dosya silinecek. Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
                return;

            var taranacak = _taranacakList[_taranacaklar.SelectedIndex];
            var dbo = new DbObject();
            dbo.Execute("DELETE FROM SCAN_DOSYA WHERE DOSYA_ID = " + taranacak.DosyaId, null);
            dbo.Dispose();

            if (File.Exists(Path.Combine(IndeksKlasoru, _secili.Text, taranacak.DosyaPath)))
                File.Delete(Path.Combine(IndeksKlasoru, _secili.Text, taranacak.DosyaPath));
            taranacak.Durum = "SİLİNDİ";
        }
    }

    internal class Taranacak
    {
        public string DosyaId { get; internal set; }
        public string CiltId { get; internal set; }
        public string DosyaPath { get; internal set; }
        public string Indeks { get; internal set; }
        public string CiltPath { get; internal set; }
        public string Aciklama { get; internal set; }
        public string Durum { get; internal set; }
    }
}