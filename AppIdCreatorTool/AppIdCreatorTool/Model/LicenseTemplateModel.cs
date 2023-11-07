using System.ComponentModel.DataAnnotations;
namespace AppIdCreatorTool.Model
{
    public class LicenseTemplateModel
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please enter Full name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Please enter ShortName")]
        public string ShortName { get; set; }
        public string LicenseType { get; set; }
        public DateTime DatePublished { get; set; }
    }
}
