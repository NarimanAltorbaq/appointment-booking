namespace BookingApp.Models;

public class Appointment
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Service { get; set; } = "";
    public DateTime AppointmentTime { get; set; }
    public string Status { get; set; } = "Pending";
}