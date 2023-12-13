using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace KursovaTRPZ.Models;


public class Administrator : User
{
    //the one-to-many relationship
    public ICollection<EventLog> EventLogs { get; set; }
}
