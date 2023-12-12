using System.ComponentModel.DataAnnotations;
namespace KursovaTRPZ.Models;

public class RadiationSensor : Sensor
{
    public float Radiation_Value { get; set; }
}
