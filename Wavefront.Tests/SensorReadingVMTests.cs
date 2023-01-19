using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wavefront.Tests
{
    internal class SensorReadingVMTests
    {
        [Test]
        public void SensorReadingVM_ofTemprature_ValueReturnsTempratureTo3dp()
        {
            // Arrange
            var sensor = A.Fake<IAUVSensor>();
            A.CallTo(() => sensor.GetTemperature()).Returns(0.1234d);
            var itemUnderTest = new SensorReadingVm<eTemperature>(sensor);

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
            var itemUnderTest = new SensorReadingVm<ePressure>(sensor);

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
            var itemUnderTest = new SensorReadingVm<eTemperature>(sensor);
            bool propertyChanged = false;

            // Cannot rember the proper / clean way to do this, and I have copy pasted it into a few places for speed             
            itemUnderTest.PropertyChanged += (object? sender, System.ComponentModel.PropertyChangedEventArgs e) => propertyChanged = e.PropertyName == nameof(SensorReadingVm<eTemperature>.Value);

            // Act
            itemUnderTest.ReadValue();

            // Assert
            Assert.That(propertyChanged, Is.True);
        }
    }
}
