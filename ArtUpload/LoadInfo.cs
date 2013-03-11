namespace ArtUpload
{
    public sealed class LoadInfo
    {
        static LoadInfo _instance;        
        static readonly object Lock = new object();
        LoadInfo()
        {
        }

        public static LoadInfo Instance
        {
            get
            {
                lock (Lock)
                {
                    return _instance ?? (_instance = new LoadInfo());
                }
            }
        }

        public string Info { get; private set; }                        

        public void AddInfo(string infoMessage)
        {
            if (!string.IsNullOrEmpty(Info))
                Info += "\n";
            Info += infoMessage;         
        }         
    }
}
