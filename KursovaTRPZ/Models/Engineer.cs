using System.ComponentModel.DataAnnotations;
namespace KursovaTRPZ.Models;

    
public class Engineer : User
{
    //one-to-many relationships with sensors
    public ICollection<Sensor> Sensors { get; set; }
    public Administrator Administrator { get; set; }
}
