using System.ComponentModel.DataAnnotations;

namespace BookingApp.Models;

public class Appointment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "الاسم مطلوب")]
    public string CustomerName { get; set; } = "";

    [Required(ErrorMessage = "رقم الهاتف مطلوب")]
    [RegularExpression(@"^\+?[0-9]{9,15}$", ErrorMessage = "رقم الهاتف غير صحيح")]
    public string Phone { get; set; } = "";

    [Required(ErrorMessage = "اختار الخدمة")]
    public string Service { get; set; } = "";

    public DateTime AppointmentTime { get; set; }
    public string Status { get; set; } = "Pending";
}