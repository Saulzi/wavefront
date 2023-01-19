using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    public interface ISelectedSensorUnits
    {
        UnitSelection<ePressure> PressureUnit { get; }
        UnitSelection<eTemperature> TempratureUnit { get; }
    }
}