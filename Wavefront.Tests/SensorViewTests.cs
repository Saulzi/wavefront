namespace Wavefront.Tests
{
    class SensorViewTests
    {
        [Test]
        public void SensorView_ctor_ThrowsNullArgumentExceptionWhenSensorIsNull()
        {
            Assert.That(() => new SensorView(null), Throws.ArgumentNullException);
        }

        [Test]
        public void SensorView_ctor_PopulatesTempValue()
        {
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetTemperature()).Returns(10d);
            var itemUnderTest = new SensorView(sensor);

            Assert.That(itemUnderTest.Temprature, Is.EqualTo(10d));
        }

        public void SensorView_ctor_PopulatesPressureValue()
        {
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetPressure()).Returns(15d);
            var itemUnderTest = new SensorView(sensor);

            Assert.That(itemUnderTest.Pressure, Is.EqualTo(15d));
        }

        public void SensorView_ctor_PopulatesErrorWhenTempThrows()
        {
            var sensor = A.Fake<IAUVSensor>();
            var sensorView = new SensorView(sensor);

        }

        public void SensorView_ctor_PopulatesErrorWhenPressureThrows()
        {
            var sensor = A.Fake<IAUVSensor>();
            var sensorView = new SensorView(sensor);

        }
    }
}
