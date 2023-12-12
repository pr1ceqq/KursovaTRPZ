using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace KursovaTRPZ.Models;


public class Administrator : User
{
    // Additional properties specific to Administrator if needed
    // Navigation property for the one-to-many relationship
    public ICollection<EventLog> EventLogs { get; set; }
}
