using NUnit.Framework;

namespace Wavefront.Tests
{

   
    [TestFixture]
    public class UnitConversionsTests
    {
        [TestCase(0d, 32d)]
        [TestCase(1d, 33.8d)]
        [TestCase(5d, 41d)]
        [TestCase(100, 212)]
        public void ConvertDegreesToFarenheight(double oc, double of)
        {
            // x*(9/5)+32 
            var result = UnitConversions.ConvertOCtoOF(oc);
            Assert.That(result, Is.EqualTo(of).Within(0.0001d));
        }

        public record ConversionTestCase<T>(double value, eTemperature valueUnit, eTemperature outputUnit, double expectedValue);

        private static IEnumerable<ConversionTestCase<eTemperature>> ConvertTempratureTestCases =>
            new ConversionTestCase<eTemperature>[]
            {
                new (0d, eTemperature.Celsius, eTemperature.Fahrenheit, 32d),
                new (0d, eTemperature.Celsius, eTemperature.Celsius, 0d),
                new (1d, eTemperature.Celsius, eTemperature.Fahrenheit, 33.8d),
                new (5d, eTemperature.Celsius, eTemperature.Fahrenheit, 41d),
                new (100d, eTemperature.Celsius, eTemperature.Fahrenheit, 212d),
                new (100d, eTemperature.Celsius, eTemperature.Celsius, 100d),
                new (100d, eTemperature.Fahrenheit, eTemperature.Fahrenheit, 100d),
                new (41d, eTemperature.Fahrenheit, eTemperature.Celsius, 5d),
                new (33.8d, eTemperature.Fahrenheit, eTemperature.Celsius, 1d),
                new (32d, eTemperature.Fahrenheit, eTemperature.Celsius, 0d)
             };


        [TestCaseSource(nameof(ConvertTempratureTestCases))]
        public void ConvertTemprature(ConversionTestCase<eTemperature> testCase)
        {
            var ( value, valueUnit, outputUnit, expectedValue ) = testCase;
            var result = UnitConversions.ConvertTemprature((value, valueUnit), outputUnit);

            Assert.That(result, Is.EqualTo(expectedValue).Within(0.0001d));
        }

        [TestCase(32d, 0d)]
        [TestCase(33.8d, 1d)]
        [TestCase(41d, 5d)]
        [TestCase(212d, 100d)]
        public void ConvertFarenheightToDegrees(double of, double oc)
        {

            var result = UnitConversions.ConvertOFtoOC(of);
            Assert.That(result, Is.EqualTo(oc).Within(0.0001d));
            // (x-32)*9/5
        }

        [TestCase(6.89476d, 1d)]
        [TestCase(68.9476d, 10d)]
        [TestCase(344.738, 50d)]

        public void ConvertkPaToPSI(double kpa, double psi)
        {
            // for an approximate result, divide the pressure value by 6.895

            var result = UnitConversions.ConvertKPAtoPSI(kpa);
            Assert.That(result, Is.EqualTo(psi).Within(0.02d));         // this could be tighter and these would pass but matching other val
        }

        [TestCase(1d, 6.89476d)]
        [TestCase(10d, 68.9476d)]
        [TestCase(50d, 344.738)]
        public void ConvertPSIToKpa(double psi, double kpa)
        {
            //for an approximate result, multiply the pressure value by 6.895

            var result = UnitConversions.ConvertPSItoKPA(psi);
            Assert.That(result, Is.EqualTo(kpa).Within(0.02d));        
        }
    }
}
