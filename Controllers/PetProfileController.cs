using Microsoft.AspNetCore.Mvc;
using PetProfileApp.Data;
using PetProfileApp.Models;
using PetProfileApp.ViewModels;
using System.Security.Cryptography;

namespace PetProfileApp.Controllers
{
    public class PetProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<PetProfileController> _logger;

        public PetProfileController(ApplicationDbContext context, IWebHostEnvironment environment, ILogger<PetProfileController> logger)
        {
            _context = context;
            _environment = environment;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PetProfileViewModel vm)
        {

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid.");
                return View(vm);
            }

            
            var petsFolder = Path.Combine(_environment.ContentRootPath, "PetNoseAiLocal", "pets");
            if (!Directory.Exists(petsFolder))
            {
                Directory.CreateDirectory(petsFolder);
            }

            // Generate a unique filename to avoid conflicts
            byte[] photoBytes;
            using (var ms = new MemoryStream())
            {
                await vm.PetPhoto.CopyToAsync(ms);
                photoBytes = ms.ToArray();
            }

            // Generate SHA256 hash
            using var sha = SHA256.Create();
            var hashBytes = sha.ComputeHash(photoBytes);
            var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            // Use the hash as the file name
            var extension = Path.GetExtension(vm.PetPhoto.FileName);
            var fileName = hash + extension;
            var filePath = Path.Combine(petsFolder, fileName);

            // Save file to disk
            await System.IO.File.WriteAllBytesAsync(filePath, photoBytes);

            // Map to entity
            var pet = new PetProfile
            {
                PetName = vm.PetName,
                PetBreed = vm.PetBreed,
                PetAge = vm.PetAge,
                MedicalInfo = vm.MedicalInfo,
                OwnerName = vm.OwnerName,
                TelNumber = vm.TelNumber,
                PetPhoto = photoBytes,
                PetPhotoHash = hash
            };

            try
            {
                _context.PetProfile.Add(pet);
                await _context.SaveChangesAsync();

               TempData["SuccessMessage"] = "Pet registered successfully!";
                return RedirectToAction("ViewPets");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the pet profile. Please try again.");
                return View(vm);
            }
        }


        public IActionResult ViewPets()
        {
            var pets = _context.PetProfile.ToList();
            return View(pets);
        }
    }
}
