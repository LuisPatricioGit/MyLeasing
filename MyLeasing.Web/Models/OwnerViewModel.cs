using Microsoft.AspNetCore.Http;
using MyLeasing.Common.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Web.Models
{
    public class OwnerViewModel : Owner
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
