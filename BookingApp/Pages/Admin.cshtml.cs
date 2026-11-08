using BookingApp.Data;
using BookingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Pages;

public class AdminModel : PageModel
{
    private readonly AppDbContext _db;
    public AdminModel(AppDbContext db) => _db = db;

    public List<Appointment> Appointments { get; set; } = new();

    public async Task OnGetAsync()
    {
        Appointments = await _db.Appointments
            .OrderBy(a => a.AppointmentTime)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostConfirmAsync(int id)
    {
        await SetStatus(id, "Confirmed");
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostCancelAsync(int id)
    {
        await SetStatus(id, "Cancelled");
        return RedirectToPage();
    }

    private async Task SetStatus(int id, string status)
    {
        var appt = await _db.Appointments.FindAsync(id);
        if (appt == null) return;
        appt.Status = status;
        await _db.SaveChangesAsync();
    }
}