namespace PetProfileApp.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

public class ContactFormModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
    public string VeterinarianNotes { get; set; }
}