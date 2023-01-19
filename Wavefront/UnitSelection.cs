using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public abstract class UnitSelection
    {
        public static UnitSelection<eTemperature> CreateTemprature() => new UnitSelection<eTemperature> { Value = eTemperature.Celsius };
        public static UnitSelection<ePressure> CreatePressure() => new UnitSelection<ePressure> { Value = ePressure.PSI };

    }
    public sealed class UnitSelection<UnitEnum> : UnitSelection
    {
        public required UnitEnum Value { get; set; }

        public static implicit operator UnitEnum(UnitSelection<UnitEnum> o) => o.Value;

    }
}
