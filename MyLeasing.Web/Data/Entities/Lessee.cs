using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MyLeasing.Web.Data.Entities
{
    public class Lessee : IEntity
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Document*")]
        public string Document { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name*")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name*")]
        public string LastName { get; set; }

        [Display(Name = "Fixed Phone")]
        public string FixedPhone { get; set; }

        [Display(Name = "Cell Phone")]
        public string CellPhone { get; set; }

        public string Address { get; set; }

        [Display(Name = "Owner Name")]
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }

        public User User { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImageUrl))
                {
                    return null;
                }
                return $"https://localhost:44355{ImageUrl.Substring(1)}";
            }
        }
    }
}
