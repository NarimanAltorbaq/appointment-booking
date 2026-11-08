using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookingApp.Pages;

public class BookModel : PageModel
{
    private readonly AppDbContext _db;
    public BookModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Appointment Appointment { get; set; } = new();

    public void OnGet()
    {
        Appointment.AppointmentTime = DateTime.Now.AddDays(1);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        Appointment.Status = "Pending";
        _db.Appointments.Add(Appointment);
        await _db.SaveChangesAsync();

        TempData["Success"] = "تم تسجيل حجزك بنجاح!";
        return RedirectToPage();
    }
}