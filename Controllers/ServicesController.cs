using Microsoft.AspNetCore.Mvc;
using PetProfileApp.Controllers;

namespace PetProfileApp.Controllers
{
    public class ServicesController : Controller
    {

         //livechat
        public IActionResult Chat()
        {
            return View();
        }


        //Payment
        public IActionResult Payment()
        {
            // Example: These values can be set dynamically based on the prescription/subscription.
            TempData["ItemName"] = "Dog Vaccine Prescription";
            TempData["ItemDescription"] = "Prescription for a vaccine required for your dog.";
            TempData["Amount"] = 75.00; // Total price in USD

            return View();
        }


        //appointment

        public IActionResult Appointment()
        {
            return View();
        }

    }
}
