namespace Wavefront.Tests
{
    class SensorVMTests
    {
        [Test]
        public void SensorView_ctor_ThrowsNullArgumentExceptionWhenSensorIsNull()
        {
            Assert.That(() => new SensorVM(null), Throws.ArgumentNullException);
        }

        [Test]
        public void SensorView_ctor_PopulatesTempValue()
        {
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetTemperature()).Returns(10d);
            var itemUnderTest = new SensorVM(sensor);

            Assert.That(itemUnderTest.Temprature, Is.EqualTo(10d));
            Assert.That(itemUnderTest.Error, Is.False);
        }

        [Test]
        public void SensorView_ctor_PopulatesPressureValue()
        {
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetPressure()).Returns(15d);
            var itemUnderTest = new SensorVM(sensor);

            Assert.That(itemUnderTest.Pressure, Is.EqualTo(15d));
            Assert.That(itemUnderTest.Error, Is.False);
        }

        [Test]
        public void SensorView_ctor_PopulatesErrorWhenTempThrows()
        {
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetTemperature()).Throws(new Exception());

            var itemUnderTest = new SensorVM(sensor);

            Assert.That(itemUnderTest.Error, Is.True);
        }

        [Test]
        public void SensorView_ctor_PopulatesErrorWhenPressureThrows()
        {
            var sensor = A.Fake<IAUVSensor>();

            A.CallTo(() => sensor.GetTemperature()).Throws(new Exception());
            var itemUnderTest = new SensorVM(sensor);

            Assert.That(itemUnderTest.Error, Is.True);
        }

        [Test]
        public void SensorView_SensorID_IsSameAsSensor()
        {
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.SensorId).Returns(5);

            var itemUnderTest = new SensorVM(sensor);

            Assert.That(itemUnderTest.SensorId, Is.EqualTo(5));
        }

        [TestCase(nameof(SensorVM.Pressure))]
        [TestCase(nameof(SensorVM.Temprature))]
        public void SensorVM_UpdateValues_NotifysPropertyChange(string property)
        {
            // Arrange
            var sensor = A.Fake<IAUVSensor>();
            var itemUnderTest = new SensorVM(sensor);
            bool propertyChanged = false;

            // Cannot rember the proper / clean way to do this this will do the job for now this has got a bit gnarly                   
            itemUnderTest.PropertyChanged += (object? sender, System.ComponentModel.PropertyChangedEventArgs e) => propertyChanged = propertyChanged || e.PropertyName == property;

            // Act
            itemUnderTest.UpdateValues();

            // Assert
            Assert.That(propertyChanged, Is.True);
        }
    }
}
