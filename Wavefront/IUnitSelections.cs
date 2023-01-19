using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    /// <summary>
    /// Interface represents all of the selected amd Selectable Units
    /// </summary>
    public interface IUnitSelections
    {
        UnitSelection<ePressure> PressureUnit { get; }
        UnitSelection<eTemperature> TempratureUnit { get; }
    }
}