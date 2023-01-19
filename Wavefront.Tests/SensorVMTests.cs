﻿namespace Wavefront.Tests
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

            Assert.That((double)itemUnderTest.Temprature, Is.EqualTo(10d));
            Assert.That(itemUnderTest.Error, Is.False);
        }

        [Test]
        public void SensorView_ctor_PopulatesPressureValue()
        {
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetPressure()).Returns(15d);
            var itemUnderTest = new SensorVM(sensor);

            Assert.That((double)itemUnderTest.Pressure, Is.EqualTo(15d));
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

        [Test]
        public void SensorVM_UpdateValues_NotifysErrorPropertyChange()
        {
            // Arrange
            var sensor = A.Fake<IAUVSensor>();
            var itemUnderTest = new SensorVM(sensor);
            bool propertyChanged = false;

            // Cannot rember the proper / clean way to do this                
            itemUnderTest.PropertyChanged += (object? sender, System.ComponentModel.PropertyChangedEventArgs e) => propertyChanged = e.PropertyName == nameof(SensorVM.Error);

            // Act
            itemUnderTest.UpdateValues();

            // Assert
            Assert.That(propertyChanged, Is.True);
        }
    }
}
