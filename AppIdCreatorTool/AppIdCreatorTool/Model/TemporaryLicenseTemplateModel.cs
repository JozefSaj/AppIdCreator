namespace AppIdCreatorTool.Model
{
    public class TemporaryLicenseTemplateModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string LicenseType { get; set; }
        public DateTime DatePublished { get; set; }
        public StatusType CurrentStatus { get; set; }
    }

    public enum StatusType
    {
        Pending,
        Accepted,
        Declined
    }
}