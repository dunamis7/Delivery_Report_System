using System.ComponentModel.DataAnnotations;

namespace Delivery_Report_System.Models.Authentication;

public class Request
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } 
}