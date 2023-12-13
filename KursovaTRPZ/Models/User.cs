using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KursovaTRPZ.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //one-to-one relationship with Auth
        public Auth Auth { get; set; }
    }
}
