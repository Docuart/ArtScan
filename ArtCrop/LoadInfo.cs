namespace ArtCrop
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
        public int Length { get; private set; }
        public int Status { get; private set; }

        public void SetLength(int lenght)
        {
            Length = lenght;
        }

        public void AddInfo(string infoMessage)
        {
            AddInfo(infoMessage, null);
        }

        public void AddInfo(string infoMessage, int? status)
        {
            if (!string.IsNullOrEmpty(Info))
                Info += "\n";
            Info += infoMessage;

            if (status != null)
                Status = status.Value;
        }         
    }
}
