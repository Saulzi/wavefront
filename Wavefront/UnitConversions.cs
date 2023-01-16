namespace Wavefront
{
    public class UnitConversions
    {
        public static double ConvertOFtoOC(double value)
        {
            return (value - 32) / 1.8;
        }

        public static double ConvertOCtoOF(double value)
        {
            return value * 1.8 + 32;
        }

        public static double ConvertKPAtoPSI(double value)
        {
            return value / 6.895d;
        }

        public static double ConvertPSItoKPA(double value)
        {
            return value * 6.895d;
        }
    }
}
