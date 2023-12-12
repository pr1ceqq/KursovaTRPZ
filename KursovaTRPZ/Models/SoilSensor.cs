using System.ComponentModel.DataAnnotations;
namespace KursovaTRPZ.Models;

public class SoilSensor : Sensor
{
    public float Ph_Value { get; set; }
    public float Humidity_Value { get; set; }
}
