using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace KursovaTRPZ.Models;

public class Auth
{
    [Key]
    public int Auth_Id { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }

    // Navigation property for the one-to-one relationship
    public User User { get; set; }
}
