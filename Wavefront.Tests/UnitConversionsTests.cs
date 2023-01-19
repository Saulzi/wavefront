using NUnit.Framework;

namespace Wavefront.Tests
{

   
    [TestFixture]
    public class UnitConversionsTests
    {
        public record ConversionTestCase<T>(double value, T valueUnit, T outputUnit, double expectedValue);

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

        private static IEnumerable<ConversionTestCase<ePressure>> ConvertPressureTestCases =>
            new ConversionTestCase<ePressure>[]
            {
                new (6.89476d, ePressure.kPa, ePressure.PSI, 1d),
                new (6.89476d, ePressure.kPa, ePressure.kPa, 6.89476d),
                new (68.9476d, ePressure.kPa, ePressure.PSI, 10d),
                new (344.738, ePressure.kPa, ePressure.PSI, 50d),
                new (1d, ePressure.PSI, ePressure.kPa, 6.89476d),
                new (10d, ePressure.PSI, ePressure.kPa, 68.9476d),
                new (50d, ePressure.PSI, ePressure.kPa, 344.738),
                new (50d, ePressure.PSI, ePressure.PSI, 50d)
            };

        [TestCaseSource(nameof(ConvertPressureTestCases))]
        public void ConvertPressure(ConversionTestCase<ePressure> testCase)
        {
            var (value, valueUnit, outputUnit, expectedValue) = testCase;
            var result = UnitConversions.ConvertPressure((value, valueUnit), outputUnit);

            Assert.That(result, Is.EqualTo(expectedValue).Within(0.02d));
        }
    }
}
