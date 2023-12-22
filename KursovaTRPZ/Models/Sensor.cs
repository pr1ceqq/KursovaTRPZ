using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 
namespace KursovaTRPZ.Models;

public class Sensor
{
    [Key]
    public int Sensor_Id { get; set; }
    public Engineer Engineer { get; set; }
    public string Sensor_Location { get; set; }
    [NotMapped]
    public virtual string SensorType => GetType().Name;

    // Navigation for one-to-many relationship
    public virtual ICollection<EventLog> EventLogs { get; set; }
}
