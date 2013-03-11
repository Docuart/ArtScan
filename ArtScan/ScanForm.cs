using System;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using Library.Scan;

namespace ArtScan
{
    public partial class TarayiciForm : Form
    {
        private const string ScannedPath = "S:\\";
        public TarayiciForm()
        {
            InitializeComponent();
        }

        private int _saniye = 3;
        private bool _isScanning;
        private void BtnScanClick(object sender, EventArgs e)
        {
            if (!int.TryParse(_sn.Text, out _saniye) || _saniye > 10 || _saniye <= 0)
            {
                MessageBox.Show(@"Otomatik tarama süresi 1-10 arasında olmalıdır.", @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(_cilt.Text.Trim()))
            {
                MessageBox.Show(@"Cilt bilgisi giriniz.", @"Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _timer.Enabled = !_timer.Enabled;
            if (!_timer.Enabled)
            {
                btnScan.Text = @"Taramaya Devam Et";
                _logs.Text += Environment.NewLine + DateTime.Now.ToString("HH:mm:ss") + @" İşlem durduruldu.";
            }
            else
                _logs.Text += Environment.NewLine + DateTime.Now.ToString("HH:mm:ss") + @" İşlem devam ediyor.";            
        }

        private void Scan()
        {            
            if (_isScanning)
                return;
            //this.Visible = false;
            _timer.Enabled = false;

            var text = btnScan.Text;
            try
            {
                _isScanning = true;
                btnScan.Enabled = false;
                btnScan.Text = @"Taranıyor...";
                if (!_folder.Text.EndsWith("\\"))
                    _folder.Text = _folder.Text + "\\";
                _cilt.Text = _cilt.Text.Trim();

                if (!Directory.Exists(_folder.Text + _cilt.Text + "\\"))
                    Directory.CreateDirectory(_folder.Text + _cilt.Text + "\\");

                if (String.IsNullOrEmpty(_sayfa.Text.Trim()))
                    _sayfa.Text = "0";
                string fileName = _cilt.Text + "#" + _sayfa.Text.PadLeft(4, '0') + ".jpg";
                if (File.Exists(_folder.Text + _cilt.Text + "\\" + fileName))
                {
                    MessageBox.Show(fileName + " Dosyası mevcut.\nİşlem gerçekleştirilemez.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _logs.Text += Environment.NewLine + DateTime.Now.ToString("HH:mm:ss") + @" İşlem başlıyor.";
                var images = WIAScanner.Scan();
                
                _logs.Text += Environment.NewLine + DateTime.Now.ToString("HH:mm:ss") + @" Tarandı:" + fileName;                
                images[0].Save(_folder.Text + _cilt.Text + "\\" + fileName, ImageFormat.Jpeg);
                
                _logs.Text += Environment.NewLine + DateTime.Now.ToString("HH:mm:ss") + @" Kaydedildi:" + fileName;
                _sayfa.Text = (Convert.ToInt32(_sayfa.Text) + 1).ToString();

                _sonTaranan.Image = images[0];
            }
            catch (Exception ex)
            {
                _logs.Text += Environment.NewLine + ex.Message;
                _logs.Text += Environment.NewLine + ex.StackTrace;
            }
            finally
            {
                btnScan.Text = text;
                btnScan.Enabled = true;
                btnScan.Focus();
                _logs.SelectionStart = _logs.Text.Length;
                _logs.ScrollToCaret();
                _logs.Refresh();
                _isScanning = false;
                //this.Visible = true;
                _timer.Enabled = true;
                _sec = 0;
            }
        }

        private void CiltLeave(object sender, EventArgs e)
        {
            try
            {
                if (!_folder.Text.EndsWith("\\"))
                    _folder.Text = _folder.Text + "\\";
                _cilt.Text = _cilt.Text.Trim();
                if (!Directory.Exists(_folder.Text + _cilt.Text + "\\"))
                    Directory.CreateDirectory(_folder.Text + _cilt.Text + "\\");
                var files = Directory.GetFiles(_folder.Text + _cilt.Text, "*.jpg");
                if (files.Length == 0)
                {
                    _sayfa.Text = @"0";
                    return;
                }
                else
                {
                    var dr = MessageBox.Show(_cilt.Text + " sistemde kayıtlıdır.\nBu cilde devam etmek istiyor musunuz?", "Uyarı",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                        return;
                }
                Array.Sort(files);
                var fileName = files[files.Length - 1].Substring(files[files.Length - 1].LastIndexOf("\\") + 1);
                fileName = (Convert.ToInt32(fileName.Replace(_cilt.Text + "#", "").Replace(".jpg", "")) + 1).ToString();
                _sayfa.Text = fileName;

                if (!File.Exists(_folder.Text + _cilt.Text + "\\user.info"))
                    File.WriteAllText(_folder.Text + _cilt.Text + "\\user.info", Util.GetName());
            }
            catch (Exception ex)
            {
                _logs.Text += Environment.NewLine + ex.Message;
                _logs.Text += Environment.NewLine + ex.StackTrace;
            }
        }

        private string _guid;
        private void LogsTextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_guid))
                _guid = Guid.NewGuid().ToString();
            File.WriteAllText(_folder.Text + _cilt.Text + "\\" + _cilt.Text + _guid + ".zlog", _logs.Text);
        }

        private int _sec = 0;
        private void TimerTick(object sender, EventArgs e)
        {
            try
            {                                
                _sec = _sec + 1;
                btnScan.Text = (_saniye - _sec) + " sn sonra taranacak.\nDurdurmak için tıklayınız.";
                if (_saniye - _sec == 0)
                    Scan();
            }
            catch (Exception ex)
            {
                _logs.Text += Environment.NewLine + ex.Message;
                _logs.Text += Environment.NewLine + ex.StackTrace;
            }
        }

        private void CiltTamamClick(object sender, EventArgs e)
        {            
            if (String.IsNullOrEmpty(_cilt.Text))
                return;

            _logs.Text += Environment.NewLine + DateTime.Now.ToString("HH:mm:ss") + @" Cilt tamamlanıyor.";
            var text = _ciltTamam.Text;
            try
            {
                _timer.Enabled = false;
                var dr = MessageBox.Show(@"Bu cildin işlemini tamamlanmak istediğinize emin misiniz?", @"Uyarı",
                                         MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    if (Directory.Exists(ScannedPath + _cilt.Text))
                    {
                        MessageBox.Show(
                            @"Sunucuda seçtiğiniz cilt isimli bir klasör bulunuyor. İşlem gerçekleştirilemez." + '\n' + @"Lütfen bu konu hakkında sistem yöneticinize başvurunuz.", @"Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    Directory.CreateDirectory(ScannedPath + _cilt.Text);
                    var files = Directory.GetFiles(_folder.Text + _cilt.Text);
                    for (var i = 0; i < files.Length; i++)
                    {
                        _ciltTamam.Text = (i + 1) + "/" + files.Length;
                        File.Copy(files[i], ScannedPath + _cilt.Text + "\\" + Path.GetFileName(files[i]), true);
                    }
                    File.Create(ScannedPath + _cilt.Text + "\\cilt.end");
                    _cilt.Text = "";
                    MessageBox.Show(@"Cilt başarılı bir şekilde kaydedildi.", @"İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + '\n' + ex.StackTrace);
            }
            finally
            {
                _ciltTamam.Text = text;
            }
        }

        private void SnTextChanged(object sender, EventArgs e)
        {
            _sec = 0;
        }

       
    }
}