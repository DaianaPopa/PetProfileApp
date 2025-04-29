using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PetProfileApp.ViewModels
{
    public class PetProfileViewModel
    {
        [Required]
        public string PetName { get; set; }

        [Required]
        public string PetBreed { get; set; }

        [Required]
        public int PetAge { get; set; }

        public string MedicalInfo { get; set; }

        [Required]
        public string OwnerName { get; set; }

        [Required]
        public string TelNumber { get; set; }

        [Required]
        public IFormFile PetPhoto { get; set; }
    }
}
