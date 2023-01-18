using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wavefront.Tests
{
    internal class SensorsVMTests
    {
        [Test]
        public void SensorsVM_ctor_throwsExceptionWhenNoFactoryPassed()
        {
            Assert.Throws<ArgumentNullException>(() => new SensorsVM(null));

        }

        [Test]
        public void SensorsVM_ctor_ConvertsIAUVSensorsToViewModels()
        {
            var sensors = Enumerable.Range(0, 4).Select(id => {
                var fake = A.Fake<IAUVSensor>();
                A.CallTo(() => fake.SensorId).Returns(id);
                return fake;
            });

            var itemUnderTest = new SensorsVM(sensors.ToArray);

            Assert.That(itemUnderTest.Sensors.Count, Is.EqualTo(4));

            Assert.That(itemUnderTest.Sensors[0].SensorId, Is.EqualTo(0)); ;
            Assert.That(itemUnderTest.Sensors[1].SensorId, Is.EqualTo(1)); ;
            Assert.That(itemUnderTest.Sensors[2].SensorId, Is.EqualTo(2)); ;
            Assert.That(itemUnderTest.Sensors[3].SensorId, Is.EqualTo(3)); ;
        }
    }
}
