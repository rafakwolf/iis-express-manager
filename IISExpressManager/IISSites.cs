namespace IISExpressManager
{
    public class IISSites
    {
        public IISSites(string siteName, string id, string portNumber, string folder)
        {
            SiteName = siteName;
            Id = id;
            Status = "Stopped";
            ProcessId = "Not Found";
            Port = portNumber;
            Folder = folder;
        }

        public string ProcessId { get; set; }
        public string Status { get; set; }
        public string Id { get; set; }
        public string SiteName { get; set; }
        public string Port { get; set; }
        public string Folder { get; set; }
    }
}