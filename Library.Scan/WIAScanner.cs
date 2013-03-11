using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using WIA;


namespace Library.Scan
{
    public class WIAScanner
    {
        const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        private static string scanner = "";

        class WIA_DPS_DOCUMENT_HANDLING_SELECT
        {
            public const uint FEEDER = 0x00000001;
            public const uint FLATBED = 0x00000002;
        }

        class WIA_DPS_DOCUMENT_HANDLING_STATUS
        {
            public const uint FEED_READY = 0x00000001;
        }

        class WIA_PROPERTIES
        {
            public const uint WIA_RESERVED_FOR_NEW_PROPS = 1024;
            public const uint WIA_DIP_FIRST = 2;
            public const uint WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const uint WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            //
            // Scanner only device properties (DPS)
            //
            public const uint WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const uint WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13;
            public const uint WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14;
        }

        /// <summary>
        /// Use scanner to scan an image (with user selecting the scanner from a dialog).
        /// </summary>
        /// <returns>Scanned images.</returns>
        public static List<Image> Scan()
        {
            if (String.IsNullOrEmpty(scanner))
            {
                WIA.ICommonDialog dialog = new WIA.CommonDialog();
                WIA.Device device = dialog.ShowSelectDevice(WIA.WiaDeviceType.UnspecifiedDeviceType, true, false);
                scanner = device.DeviceID;
            }
            return Scan(scanner);            
        }

        private const int WIA_IPS_CUR_INTENT = 6146;
        private static int sayi = 0;

        /// <summary>
        /// Use scanner to scan an image (scanner is selected by its unique id).
        /// </summary>
        /// <param name="scannerName"></param>
        /// <returns>Scanned images.</returns>
        public static List<Image> Scan(string scannerId)
        {
            List<Image> images = new List<Image>();

            bool hasMorePages = true;
            const int WIA_INTENT_IMAGE_TYPE_COLOR = 0x00000001;
            object intent = WIA_INTENT_IMAGE_TYPE_COLOR;
            
            while (hasMorePages)
            {
                // select the correct scanner using the provided scannerId parameter
                WIA.DeviceManager manager = new WIA.DeviceManager();
                WIA.Device device = null;
                foreach (WIA.DeviceInfo info in manager.DeviceInfos)
                {
                    if (info.DeviceID == scannerId)
                    {
                        // connect to scanner
                        device = info.Connect();
                        
                        foreach (Property property in device.Properties)
                        {
                            switch (property.PropertyID)
                            {
                                case WIA_IPS_CUR_INTENT:
                                    property.set_Value(ref intent);                                    
                                    break;
                            }
                        }
                        
                        break;
                    }
                }

                // device was not found
                if (device == null)
                {
                    // enumerate available devices
                    string availableDevices = "";
                    foreach (WIA.DeviceInfo info in manager.DeviceInfos)
                    {
                        availableDevices += info.DeviceID + "n";
                    }

                    // show error with available devices
                    throw new Exception("The device with provided ID could not be found. Available Devices:n" + availableDevices);
                }

                WIA.Item item = device.Items[1] as WIA.Item;

                try
                {
                    const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";

                    const string configFile = "C:\\scanner.config";
                    if (File.Exists(configFile))
                    {
                        var configs = File.ReadAllLines(configFile);
                        foreach (var config in configs)
                        {                            
                            if (config.StartsWith("Config:"))
                            {                                
                                var param = config.Replace("Config:", "").Split('-');                                
                                setItem(item, param[0].Replace("-", ""), Convert.ToInt32(param[1].Replace("-", "")));
                            }
                        }
                    }
                    /* Fujitsu fi-5220C
                    setItem(item, "6146", 1);
                    setItem(item, "6147", 150);
                    setItem(item, "6148", 150);
                    setItem(item, "6151", 150 * 8.5);
                    setItem(item, "6152", 150 * 11);
                    */

                    // scan image
                    WIA.ICommonDialog wiaCommonDialog = new WIA.CommonDialog();
                    WIA.ImageFile image = (WIA.ImageFile)wiaCommonDialog.ShowTransfer(item, wiaFormatJPEG, true);

                    // save to temp file
                    string fileName = Path.GetTempFileName();
                    File.Delete(fileName);
                    image.SaveFile(fileName);
                    image = null;
                    // add file to output list
                    images.Add(Image.FromFile(fileName));
                }
                catch (Exception exc)
                {
                    throw exc;
                }
                finally
                {
                    item = null;
                    //determine if there are any more pages waiting
                    WIA.Property documentHandlingSelect = null;
                    WIA.Property documentHandlingStatus = null;

                    foreach (WIA.Property prop in device.Properties)
                    {
                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT)
                            documentHandlingSelect = prop;

                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                            documentHandlingStatus = prop;
                    }
                    // assume there are no more pages
                    hasMorePages = false;

                    // may not exist on flatbed scanner but required for feeder
                    if (documentHandlingSelect != null)
                    {
                        try
                        {
                            // check for document feeder
                            if ((Convert.ToUInt32(documentHandlingSelect.get_Value()) && WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER) != 0)
                            {
                                hasMorePages = ((Convert.ToUInt32(documentHandlingStatus.get_Value()) && WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0);
                            }
                        } catch (Exception)
                        {
                            
                        }
                    }
                }
            }

            return images;
        }

        /// <summary>
        /// Gets the list of available WIA devices.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDevices()
        {
            List<string> devices = new List<string>();
            WIA.DeviceManager manager = new WIA.DeviceManager();

            foreach (WIA.DeviceInfo info in manager.DeviceInfos)
            {
                devices.Add(info.DeviceID);
            }

            return devices;
        }


        private static void setItem(WIA.IItem item, object property, object value)
        {
            try
            {
                /*
                var c = File.CreateText("C:\\epson.txt");
                c.AutoFlush = true;
                foreach (Property property1 in item.Properties)
                {
                    var s = "property1.PropertyID " + property1.PropertyID + "\n";
                    s += "propery1.Name " + property1.Name + "\n";
                    s += "property1.get_Value() " + property1.get_Value() + "\n";
                    s += "------\n";
                    c.Write(s);
                }
                c.Close();
                */
                WIA.Property aProperty = item.Properties.get_Item(ref property);
                aProperty.set_Value(ref value);
            }
            catch (Exception)
            {
                MessageBox.Show("Değer ayarlanamadı:" + property + " - " + value);
            }
        }
    }
}
