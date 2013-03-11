using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Library.Scan;

namespace ArtUpload
{
    public partial class UploadForm : Form
    {
        private const string IndexKlasoru = "I:\\"; 
        private const string TempKlasoru = "ArtCheck"; 
        public UploadForm()
        {
            InitializeComponent();
        }

        private void KlasorSecClick(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowDialog();
            _ciltKlasoru.Text = folderBrowserDialog.SelectedPath;

            if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath))
                return;

            var files = Directory.GetFiles(_ciltKlasoru.Text, "*.zip");
            foreach (var file in files)            
                _ciltler.Items.Add(Path.GetFileName(file) ?? "", CheckState.Checked);
        }

        private void UploadFormLoad(object sender, EventArgs e)
        {
            var dbObject = new DbObject();
            var dsBolge = dbObject.DataSet("SELECT BOLGE FROM SCAN_TAPU GROUP BY BOLGE ORDER BY BOLGE");
            for (var i = 0; i < dsBolge.Tables[0].Rows.Count; i++)
                _bolge.Items.Add(dsBolge.Tables[0].Rows[i]["BOLGE"]);
            dbObject.Dispose();
        }

        private void BolgeSelectedIndexChanged(object sender, EventArgs e)
        {
            var dbObject = new DbObject();
            var parameters = new Parameter[1];
            parameters[0] = new Parameter("bolge", _bolge.SelectedItem);
            var dsIl = dbObject.DataSet("SELECT IL FROM SCAN_TAPU WHERE BOLGE = :bolge GROUP BY IL ORDER BY IL", parameters);
            _sehir.Items.Clear();            
            for (var i = 0; i < dsIl.Tables[0].Rows.Count; i++)
                _sehir.Items.Add(dsIl.Tables[0].Rows[i]["IL"]);
            dbObject.Dispose();
        }

        private void SehirSelectedIndexChanged(object sender, EventArgs e)
        {
            var dbObject = new DbObject();
            var parameters = new Parameter[1];
            parameters[0] = new Parameter("il", _sehir.SelectedItem);
            var dsIlce = dbObject.DataSet("SELECT TAPU_ID, ILCE FROM SCAN_TAPU WHERE IL = :il GROUP BY TAPU_ID, ILCE ORDER BY ILCE", parameters);
            _mudurluk.Items.Clear();            
            for (var i = 0; i < dsIlce.Tables[0].Rows.Count; i++)
                AddComboItem(_mudurluk, dsIlce.Tables[0].Rows[i]["ILCE"].ToString(), dsIlce.Tables[0].Rows[i]["TAPU_ID"].ToString());
                
            dbObject.Dispose();
        }

        private LoadingForm _f;
        private void CreateForm()
        {
            _f = new LoadingForm();
            _f.ShowDialog();
        }

        private void CiltKaydet()
        {
            var t = new System.Threading.Thread(CreateForm);
            t.Start();
            var folders = Directory.GetDirectories(IndexKlasoru + _bolge.SelectedItem + "\\" + _sehir.SelectedItem + "\\" + _mudurluk.SelectedItem.ToString().Split(' ')[0]);
            Array.Sort(folders);
            var dbo = new DbObject();
            foreach (var folder in folders)
            {
                if (!string.IsNullOrEmpty(_ciltKlasoru.Text) && !folder.Contains(_ciltKlasoru.Text))
                    continue;

                LoadInfo.Instance.AddInfo("Cilt okunuyor. " + folder);
                var paramPath = new Parameter[1];
                paramPath[0] = new Parameter("ciltPath", folder.Replace(IndexKlasoru, ""));
                
                var ds = dbo.DataSet("SELECT * FROM SCAN_CILT WHERE CILT_PATH = :ciltPath", paramPath);
                if (ds != null)
                {
                    LoadInfo.Instance.AddInfo("Cilt sistemde mevcut. Yeni cilde geçilecek." + folder);
                    continue;
                }

                LoadInfo.Instance.AddInfo("Cilt kaydediliyor. " + folder);
                var ciltFields = new ArrayList();
                ciltFields.Add("CILT_ID");
                ciltFields.Add("TAPU_ID");
                ciltFields.Add("CILT_PATH");
                ciltFields.Add("CILT_DURUM");
                ciltFields.Add("TARAYAN");

                var user = "Kullanıcı Bilgisi Yok";
                if (File.Exists(folder + "\\user.info"))
                    user = File.ReadAllText(folder + "\\user.info");

                var ciltValues = new ArrayList();
                var ciltId = dbo.GetSequence("SCAN");
                ciltValues.Add(ciltId);
                ciltValues.Add(GetComboItem(_mudurluk));
                ciltValues.Add(folder.Replace(IndexKlasoru, ""));
                ciltValues.Add("UPL");
                ciltValues.Add(user);
                dbo.InsertRecord(ciltFields, ciltValues, "SCAN_CILT");

                var tarananList = Directory.GetFiles(folder, "*.jpg");
                Array.Sort(tarananList);
                foreach (var s in tarananList)
                {
                    LoadInfo.Instance.AddInfo("Dosya kaydediliyor. " + Path.GetFileName(s));
                    var dosyaFields = new ArrayList();
                    dosyaFields.Add("DOSYA_ID");
                    dosyaFields.Add("CILT_ID");
                    dosyaFields.Add("DOSYA_PATH");
                    dosyaFields.Add("DOSYA_DURUM");

                    var dosyaValues = new ArrayList();
                    dosyaValues.Add(dbo.GetSequence("SCAN"));
                    dosyaValues.Add(ciltId);
                    dosyaValues.Add(Path.GetFileName(s) ?? "");
                    dosyaValues.Add("B");

                    dbo.InsertRecord(dosyaFields, dosyaValues, "SCAN_DOSYA");
                }
                
                LoadInfo.Instance.AddInfo("Cilt kapatılıyor. " + folder);
                var updateParams = new Parameter[1];
                updateParams[0] = new Parameter("ciltId", ciltId);
                dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'BEK' WHERE CILT_ID = :ciltId", updateParams);
            }
            dbo.Dispose();
            Invoke(new Action(() => _f.Close()));
        }

        private void YukleClick(object sender, EventArgs e)
        {
            CiltKaydet();
            return;
            foreach (var t in _ciltler.CheckedItems)
            {
                var uploadFolder = Guid.NewGuid().ToString();
                var folder = Path.GetTempPath() + TempKlasoru +  t;
                File.Copy(_ciltKlasoru.Text + t, IndexKlasoru + uploadFolder + ".zip");
                ZipUtil.Uncompress(_ciltKlasoru.Text + t, folder);
                var files = Directory.GetFiles(folder, "*.zlog");
                var log = files.Aggregate("", (current, file) => current + ("\n" + File.ReadAllText(file)));

                var dbo = new DbObject();

                var ciltFields = new ArrayList();
                ciltFields.Add("CILT_ID");
                ciltFields.Add("TAPU_ID");
                ciltFields.Add("CILT_PATH");
                ciltFields.Add("CILT_DURUM");
                ciltFields.Add("TARAYAN");
                ciltFields.Add("LOGS");
                ciltFields.Add("ZIP_NAME");

                var ciltValues = new ArrayList();
                var ciltId = dbo.GetSequence("SCAN");
                ciltValues.Add(ciltId);
                ciltValues.Add(GetComboItem(_mudurluk));
                ciltValues.Add(uploadFolder);
                ciltValues.Add("UPL");
                ciltValues.Add(File.ReadAllText(folder + "\\user.info"));
                ciltValues.Add(log);
                ciltValues.Add(t);
                dbo.InsertRecord(ciltFields, ciltValues, "SCAN_CILT");

                Directory.CreateDirectory(IndexKlasoru + uploadFolder);
                var tarananList = Directory.GetFiles(folder, "*.jpg");
                foreach (var s in tarananList)
                {
                    File.Copy(s, IndexKlasoru + uploadFolder + "\\" + Path.GetFileName(s));
                    
                    var dosyaFields = new ArrayList();
                    dosyaFields.Add("DOSYA_ID");
                    dosyaFields.Add("CILT_ID");
                    dosyaFields.Add("DOSYA_PATH");
                    dosyaFields.Add("DOSYA_DURUM");
                    
                    var dosyaValues = new ArrayList();
                    dosyaValues.Add(dbo.GetSequence("SCAN"));
                    dosyaValues.Add(ciltId);
                    dosyaValues.Add(Path.GetFileName(s) ?? "");
                    dosyaValues.Add("B");

                    dbo.InsertRecord(dosyaFields, dosyaValues, "SCAN_DOSYA");
                }

                var updateParams = new Parameter[1];
                updateParams[0] = new Parameter("ciltId", ciltId);
                dbo.Execute("UPDATE SCAN_CILT SET CILT_DURUM = 'BEK' WHERE CILT_ID = :ciltId", updateParams);

                dbo.Dispose();
            }
            MessageBox.Show(@"Ciltler sisteme yüklendi.", @"Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void AddComboItem(ComboBox comboBox, string item, string id)
        {
            comboBox.Items.Add(item + " [" + id + "]");
        }

        private static int GetComboItem(ComboBox comboBox)
        {
            return Convert.ToInt32(comboBox.SelectedItem.ToString().Split('[')[1].Replace("[", "").Replace("]", ""));
        }
    }
}
