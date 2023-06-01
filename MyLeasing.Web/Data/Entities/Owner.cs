using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Data.Entities
{
    public class Owner
    {
        public int Id { get; set; }

        [Display(Name = "Document*")]
        public string Document { get; set; }

        [Display(Name = "First Name*")]
        public string FirstName { get; set; }

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
