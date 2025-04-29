using PetProfileApp.Data;
using PetProfileApp.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PetProfileApp.Services
{
    public class BiometricRecognitionService
    {
        private readonly ApplicationDbContext _context;

        public BiometricRecognitionService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Compară imaginea încărcată cu cele din baza de date folosind un hash
        public PetProfile FindPetByImage(byte[] image)
        {
            using (var sha256 = SHA256.Create())
            {
                var imageHash = Convert.ToBase64String(sha256.ComputeHash(image));

                // Căutăm în baza de date animalul care are același hash
                return null;
                //return _context.PetProfile.FirstOrDefault(pet => pet.PetPhotoHash == imageHash);
            }
        }



        // Metoda pentru generarea unui hash MD5 al imaginii
        private string GetImageHash(byte[] image)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashBytes = md5.ComputeHash(image);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
