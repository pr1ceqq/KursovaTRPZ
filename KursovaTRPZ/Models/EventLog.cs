using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KursovaTRPZ.Models;

public class EventLog
{
    [Key]
    public int Event_ID { get; set; }
    public string EventName { get; set; }
    public DateTime EventTime { get; set; }
    
    [ForeignKey("Sensor")]
    public int? Sensor_ID { get; set; }
    
    public virtual Sensor Sensor { get; set; }

    // Navigation for the many-to-one relationship
    public virtual Administrator AdminNavigation { get; set; }
    public string Event_Context { get; set; }
}