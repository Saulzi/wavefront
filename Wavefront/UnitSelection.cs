using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public abstract class UnitSelection
    {
        public static UnitSelection<eTemperature> CreateTemprature() => new()
        { 
            Name="Temperature", 
            Value = eTemperature.Celsius, 
            Values = { 
                new("°C", eTemperature.Celsius),
                new("°F", eTemperature.Fahrenheit)
            }
        };
        public static UnitSelection<ePressure> CreatePressure() => new()
        {
            Name = "Pressure",
            Value = ePressure.PSI,
            Values = {
                new("psi", ePressure.PSI),
                new("kPa", ePressure.kPa)                                                                                   
            }
        };

    }

    public sealed class UnitSelection<UnitEnum> : UnitSelection
    {
        public required UnitEnum Value { get; set; }

        public static implicit operator UnitEnum(UnitSelection<UnitEnum> o) => o.Value;

        public record Unit(string Name, UnitEnum Value);

        public string Name { get; init; } = String.Empty;

        public List<Unit> Values { get; } = new List<Unit>();
    }
}
