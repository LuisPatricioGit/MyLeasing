using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Document*")]
        public int Document { get; set; }

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
    }
}
