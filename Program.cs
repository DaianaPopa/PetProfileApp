using Microsoft.EntityFrameworkCore;
using PetProfileApp.Data;
using PetProfileApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurare serviciu pentru baza de date
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ad?ug?m MVC
builder.Services.AddControllersWithViews();

// Serviciul pentru recunoa?tere biometric?
builder.Services.AddScoped<BiometricRecognitionService>();

var app = builder.Build();

// Tratare erori în produc?ie
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // pentru CSS, JS, imagini

app.UseRouting();

app.UseAuthorization();

// Rut? default: Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
