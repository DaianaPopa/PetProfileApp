using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetProfileApp.Data;
using PetProfileApp.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace PetProfileApp.Controllers
{
    public class PetBiometricController : Controller
    {
        private readonly ILogger<PetBiometricController> _logger;
        private readonly ApplicationDbContext _context;

        public PetBiometricController(ILogger<PetBiometricController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult Scan()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Scan(IFormFile petImage)
        {
            if (petImage == null || petImage.Length == 0)
            {
                ViewBag.PetFound = false;
                ViewBag.Message = "No image was uploaded.";
                return View();
            }

            using var memoryStream = new MemoryStream();
            await petImage.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(memoryStream), "image", petImage.FileName);

            using var httpClient = new HttpClient();

            try
            {
                var response = await httpClient.PostAsync("http://127.0.0.1:5000/identify", content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<FlaskResponse>();

                if (result != null && result.Match)
                {
                    var hash = Path.GetFileNameWithoutExtension(result.Pet)?.ToLower();
                    _logger.LogInformation($"Identificat: {hash}");

                    var matchedPet = await _context.PetProfile
                        .FirstOrDefaultAsync(p => p.PetPhotoHash.ToLower() == hash);

                    if (matchedPet != null)
                    {
                        ViewBag.PetFound = true;
                        ViewBag.PetProfile = matchedPet;
                        return View();
                    }
                    else
                    {
                        ViewBag.PetFound = false;
                        ViewBag.Message = "The image was recognized, but no pet profile was found in the database.";
                    }
                }
                else
                {
                    ViewBag.PetFound = false;
                    ViewBag.Message = "Pet not recognized.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.PetFound = false;
                ViewBag.Message = "An error occurred while connecting to the identification server.";
            }

            return View();
        }
    }

}
