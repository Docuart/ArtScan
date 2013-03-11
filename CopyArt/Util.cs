using System;

namespace CopyArt
{
    public class Util
    {
        public static string GetName()
        {
            return Environment.UserName + "@" + Environment.MachineName;
        }
    }
}
