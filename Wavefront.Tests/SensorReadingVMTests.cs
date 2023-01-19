namespace Wavefront.Tests
{
    internal class SensorReadingVMTests
    {
        [Test]
        public void SensorReadingVM_ofTemprature_ValueReturnsTempratureTo3dp()
        {
            // Arrange
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.TemperatureUnit).Returns(eTemperature.Celsius);
            A.CallTo(() => sensor.GetTemperature()).Returns(0.1234d);
            var itemUnderTest = new TempratureReadingVm(sensor, UnitSelection.CreateTemprature());

            // Act
            itemUnderTest.ReadValue();

            // Assert
            Assert.That(itemUnderTest.Value, Does.Match("^0\\.123\\s"));
        }

        [Test]
        public void SensorReadingVM_ofPressure_ValueReturnsPressureTo3dp()
        {
            // Arrange
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetPressure()).Returns(0.1234d);
            var itemUnderTest = new PressureReadingVm(sensor, UnitSelection.CreatePressure());

            // Act
            itemUnderTest.ReadValue();

            // Assert
            Assert.That(itemUnderTest.Value, Does.Match("^0\\.123\\s"));
        }

        [Test]
        public void SensorReadingVM_NotifiesValuePropertyChanged()
        {
            // Arrange
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.TemperatureUnit).Returns(eTemperature.Celsius);

            var itemUnderTest = new TempratureReadingVm(sensor, UnitSelection.CreateTemprature());
            bool propertyChanged = false;

            // Cannot rember the proper / clean way to do this, and I have copy pasted it into a few places for speed             
            itemUnderTest.PropertyChanged += (object? sender, System.ComponentModel.PropertyChangedEventArgs e) => propertyChanged = e.PropertyName == nameof(SensorReadingVm<eTemperature>.Value);

            // Act
            itemUnderTest.ReadValue();

            // Assert
            Assert.That(propertyChanged, Is.True);
        }

        [TestCase(eTemperature.Celsius, "°C")]
        [TestCase(eTemperature.Fahrenheit, "°F")]
        [TestCase(eTemperature.Unknown, "??")]
        public void TempratureReadingVM_HasSelectedUnitSymbol(eTemperature temperatureUnit, string expectedSymbol)
        {
            var unitSelection = UnitSelection.CreateTemprature();
            unitSelection.Value = temperatureUnit;
            var itemUnderTest = new TempratureReadingVm(A.Fake<IAUVSensor>(), unitSelection);

            StringAssert.EndsWith(expectedSymbol, itemUnderTest.Value);
        }

        [TestCase(ePressure.Unknown, "??")]
        [TestCase(ePressure.PSI, "psi")]
        [TestCase(ePressure.kPa, "kPa")]
        public void PressureReadingVM_HasSelectedUnitSymbol(ePressure pressureUnit, string expectedSymbol)
        {
            var unitSelection = UnitSelection.CreatePressure();
            unitSelection.Value = pressureUnit;
            var itemUnderTest = new PressureReadingVm(A.Fake<IAUVSensor>(), unitSelection);

            StringAssert.EndsWith(expectedSymbol, itemUnderTest.Value);
        }

        [Test]
        public void TempratureReadingVM_ConvertsUnits()
        {
            var unitSelection = UnitSelection.CreateTemprature();
            unitSelection.Value = eTemperature.Fahrenheit;

            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetTemperature()).Returns(5);
            A.CallTo(() => sensor.TemperatureUnit).Returns(eTemperature.Celsius);

            var itemUnderTest = new TempratureReadingVm(sensor, unitSelection);
            
            itemUnderTest.ReadValue();

            Assert.That(itemUnderTest.Value, Is.EqualTo("41.000 °F"));
        }
        
    }
}
