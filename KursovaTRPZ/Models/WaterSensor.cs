using System.ComponentModel.DataAnnotations;
namespace KursovaTRPZ.Models;

public class WaterSensor : Sensor
{
    public float Ph_Value { get; set; }
}
