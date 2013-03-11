using System;
using System.Drawing.Imaging;

namespace Library.Scan
{
    public class Util
    {
        public static string GetName()
        {
            return Environment.UserName + "@" + Environment.MachineName;
        }

        /// <summary>
        /// Obtain an image encoder suitable for a specific graphics format.
        /// </summary>
        /// <param name="imageType">One of: BMP, JPEG, GIF, TIFF, PNG.</param>
        /// <returns>An ImageCodecInfo corresponding with the type requested,
        /// or null if the type was not found.</returns>
        public static ImageCodecInfo GetImageEncoder()
        {
            return GetImageEncoder("JPEG");
        }

        /// <summary>
        /// Obtain an image encoder suitable for a specific graphics format.
        /// </summary>
        /// <param name="imageType">One of: BMP, JPEG, GIF, TIFF, PNG.</param>
        /// <returns>An ImageCodecInfo corresponding with the type requested,
        /// or null if the type was not found.</returns>
        public static ImageCodecInfo GetImageEncoder(string imageType)
        {
            imageType = imageType.ToUpperInvariant();
            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.FormatDescription == imageType)
                {
                    return info;
                }
            }
            return null;
        }

        public static EncoderParameters GetEncoderParameters()
        {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Compression, 40);
            return encoderParameters;
        }


    }
}
