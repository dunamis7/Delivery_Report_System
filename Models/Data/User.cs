using System.ComponentModel.DataAnnotations;
using Delivery_Report_System.Models.enums;
using Microsoft.AspNetCore.Identity;

namespace Delivery_Report_System.Models.Data;

public class User
{
    [Required]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "Password is required")]
    [MinLength(10,ErrorMessage = "Password should contain at least 10 characters")]
    public string Password { get; set; }
    
    [Compare("Password", ErrorMessage = "Password should be the same as password")]
    [Required]
    public string ConfirmPassword { get; set; }
    
    [EmailAddress]
    [Required]
    public string Email { get; set; }

   [EnumDataType(typeof(Role))]
    public string Role { get; set; }
}