using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KursovaTRPZ.Models;

public class EventLog
{
    [Key]
    public int Event_ID { get; set; }
    public string EventName { get; set; }
    public DateTime EventTime { get; set; }

    // Explicitly specify the foreign key property names
    [ForeignKey("Sensor")]
    public int? Sensor_ID { get; set; }

    [ForeignKey("Administrator")]
    public int Id { get; set; }

    // Navigation properties
    public virtual Sensor Sensor { get; set; }

    // Navigation property for the many-to-one relationship
    public virtual Administrator AdminNavigation { get; set; }
}