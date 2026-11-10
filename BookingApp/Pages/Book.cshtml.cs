using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Pages;

public class BookModel : PageModel
{
    private readonly AppDbContext _db;
    public BookModel(AppDbContext db) => _db = db;

    [BindProperty]
    public Appointment Appointment { get; set; } = new();

    public void OnGet()
    {
        var t = DateTime.Now.AddDays(1);
        Appointment.AppointmentTime = new DateTime(t.Year, t.Month, t.Day, t.Hour, 0, 0);
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var t = Appointment.AppointmentTime;
        Appointment.AppointmentTime = new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, 0);

        if (Appointment.AppointmentTime <= DateTime.Now)
            ModelState.AddModelError("Appointment.AppointmentTime", "اختار وقت بالمستقبل");

        var taken = await _db.Appointments.AnyAsync(a =>
            a.AppointmentTime == Appointment.AppointmentTime &&
            a.Status != "Cancelled");

        if (taken)
            ModelState.AddModelError("Appointment.AppointmentTime", "هالوقت محجوز، اختار وقت تاني");

        if (!ModelState.IsValid) return Page();

        Appointment.Status = "Pending";
        _db.Appointments.Add(Appointment);
        await _db.SaveChangesAsync();

        TempData["Success"] = "تم تسجيل حجزك بنجاح!";
        return RedirectToPage();
    }
}