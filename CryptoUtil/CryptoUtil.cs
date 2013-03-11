using System;
using System.IO;
using OrganicBit.Zip;

namespace CryptoUtil
{
    public class CryptoUtil
    {        
        

        public static bool DosyaGonder(string yereldosya, string uzakdosya)
        {            
            try
            {                
                var sr = new FileStream(yereldosya, FileMode.Open);                
                
                var zipWriter = new ZipWriter(uzakdosya);
                var dosyaAdi = Path.GetFileName(yereldosya).Replace("İ", "I").Replace("ı", "i").Replace("Ç", "C").Replace("ç", "c").Replace("Ü", "U").Replace("ü", "u").Replace("Ğ", "G").Replace("ğ", "g").Replace("Ş", "S").Replace("ş", "s").Replace("Ö", "O").Replace("ö", "o");
                
                var target = new ZipEntry(dosyaAdi);
                zipWriter.AddEntry(target);
                var buf = new byte[1024];
                int count;
                while ((count = sr.Read(buf, 0, 1024)) > 0)
                {
                    zipWriter.Write(buf, 0, count);
                }
                zipWriter.Close();                                               
            }
            catch (Exception)
            {
                return false;                
            }
            return true;
        }
    }
}
