using System.ComponentModel.DataAnnotations;
namespace KursovaTRPZ.Models;

    
public class Engineer : User
{
    // Additional properties specific to Engineer if needed
    // Navigation properties for the one-to-many relationships with sensors
    public ICollection<Sensor> Sensors { get; set; }
}
