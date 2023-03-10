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

            Assert.That(itemUnderTest.Sensors, Has.Count.EqualTo(4));
            Assert.Multiple(() =>
            {
                Assert.That(itemUnderTest.Sensors[0].SensorId, Is.EqualTo(0));
                Assert.That(itemUnderTest.Sensors[1].SensorId, Is.EqualTo(1));
                Assert.That(itemUnderTest.Sensors[2].SensorId, Is.EqualTo(2));
                Assert.That(itemUnderTest.Sensors[3].SensorId, Is.EqualTo(3));
            });
        }

        [Test]
        public void SensorsVM_Update_RetrievesSensorReadingsFromIAuvSensor()
        {
            var sensors = Enumerable.Range(0, 2)
                                    .Select(f => A.Fake<IAUVSensor>())
                                    .ToArray();

            var itemUnderTest = new SensorsVM(() => sensors);

            itemUnderTest.UpdateSensors();

            A.CallTo(() => sensors[0].GetPressure()).MustHaveHappened();
            A.CallTo(() => sensors[0].GetTemperature()).MustHaveHappened();

            A.CallTo(() => sensors[1].GetPressure()).MustHaveHappened();
            A.CallTo(() => sensors[1].GetTemperature()).MustHaveHappened();
        }


    }
}
