using Microsoft.AspNetCore.Identity;

namespace Delivery_Report_System.Models;

public class ApplicationUser : IdentityUser
{
    public string Role { get; set; }
}