using System.ComponentModel.DataAnnotations;

namespace PetProfileApp.Models
{
    public class PetProfile
    {
        public int Id { get; set; }

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
        public byte[] PetPhoto { get; set; }

        public string PetPhotoHash { get; set; } 
    }

}
