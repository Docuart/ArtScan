using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Library.Scan
{
    public class CryptedChecksum
    {
        public CryptedChecksum(string checksum, string key)
        {
            Checksum = checksum;
            Key = key;
        }

        public string Checksum { get; set; }
        public string Key { get; set; }
    }

    public class CryptoUtil
    {
        [System.Runtime.InteropServices.DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(ref string destination, int length);

        private static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            var desCrypto = (DESCryptoServiceProvider)DES.Create();
            // Use the Automatically generated key for Encryption. 
            return Encoding.ASCII.GetString(desCrypto.Key).Replace("\\", "/");
        }

        public static CryptedChecksum EncryptFile(string inputFilename, string outputFilename)
        {
            var fsInput = new FileStream(inputFilename, FileMode.Open, FileAccess.Read);
            var fsEncrypted = new FileStream(outputFilename, FileMode.Create, FileAccess.ReadWrite);
            var des = new DESCryptoServiceProvider();
            var key = GenerateKey();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(key);

            var desencrypt = des.CreateEncryptor();
            var cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
            var bytearrayinput = new byte[fsInput.Length - 1];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);

            return new CryptedChecksum(FileChecksum(fsEncrypted), key);
        }

        public static void DecryptFile(string inputFilename, string outputFilename)
        {
            var key = GenerateKey();
            var des = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.
            //Set secret key For DES algorithm.
            des.Key = Encoding.ASCII.GetBytes(key);
            //Set initialization vector.
            des.IV = Encoding.ASCII.GetBytes(key);

            //Create a file stream to read the encrypted file back.
            var fsread = new FileStream(inputFilename, FileMode.Open, FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            var desdecrypt = des.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            var cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            var fsDecrypted = new StreamWriter(outputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }

        public static string FileChecksum(string fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Open);
            var checksum = FileChecksum(fileStream);
            fileStream.Close();
            return checksum;
        }

        public static string FileChecksum(FileStream fileStream)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var retVal = md5.ComputeHash(fileStream);            

            var sb = new StringBuilder();
            for (var i = 0; i < retVal.Length; i++)
                sb.Append(retVal[i].ToString("x2"));
            return sb.ToString();
        }
    }
}