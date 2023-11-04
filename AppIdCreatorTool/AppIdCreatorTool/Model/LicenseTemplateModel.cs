using System.ComponentModel.DataAnnotations;

namespace AppIdCreatorTool.Model
{
    public class LicenseTemplateModel
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string LicenseType { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
