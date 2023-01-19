using Wavefront.AUV.API.Enums;

namespace Wavefront
{
    /// <summary>
    /// Class Deals with the Unit Conversions, Whilst this has two responsibilities i.e. it can 
    /// Do Temp and Pressure and could be refactored to two classes with the same interface which the Reading based VM's
    /// Cound Have rather than having overridden function
    /// 
    /// This is ok
    /// </summary>
    public static class UnitConversions
    {
        private static double ConvertOFtoOC(double value)
        {
            return (value - 32) / 1.8;
        }

        private static double ConvertOCtoOF(double value)
        {
            return value * 1.8 + 32;
        }

        private static double ConvertKPAtoPSI(double value)
        {
            return value / 6.895d;
        }

        private static double ConvertPSItoKPA(double value)
        {
            return value * 6.895d;
        }

        public static double ConvertTemprature((double value, eTemperature unit) value, eTemperature outputUnit)
        {
            var (inputValue, inputUnit) = value;

            if (inputUnit == outputUnit)
                return inputValue;

            return inputUnit switch
            {
                eTemperature.Celsius when outputUnit == eTemperature.Fahrenheit => ConvertOCtoOF(inputValue),
                eTemperature.Fahrenheit when outputUnit == eTemperature.Celsius => ConvertOFtoOC(inputValue),
                _ => throw new InvalidOperationException()
            };
        }

        public static double ConvertPressure((double value, ePressure unit) value, ePressure outputUnit)
        {
            var (inputValue, inputUnit) = value;

            if (inputUnit == outputUnit)
                return inputValue;

            return inputUnit switch
            {
                ePressure.kPa when outputUnit == ePressure.PSI => ConvertKPAtoPSI(inputValue),
                ePressure.PSI when outputUnit == ePressure.kPa => ConvertPSItoKPA(inputValue),
                _ => throw new InvalidOperationException()
            };
        }
    }
}
