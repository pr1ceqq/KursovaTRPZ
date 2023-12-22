using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace KursovaTRPZ.Models;


public class Administrator : User
{
    public ICollection<Engineer> Engineers { get; set; }
    public ICollection<EventLog> EventLogs { get; set; }
}
