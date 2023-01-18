namespace Wavefront.Tests
{
    [TestFixture]
    internal class MainWindowTests
    {
        /// <summary>
        /// Some may think this test is not that usefull and the chances are low
        /// For the moment this proves that things are working
        /// </summary>
        [Test]
        public void MainWindow_ctor_ThrowsInvalidArgumentExceptionWhenNull()
        {
            Assert.That(() => new MainWindow(null),
                Throws.ArgumentNullException);
        }

        [Test]
        public void MainWindow_ctor_SetsDataContextFromConstructor()
        {
            var data = new SensorsVM(() => Array.Empty<IAUVSensor>());
            var itemUnderTest = new MainWindow(data);

            Assert.That(itemUnderTest.DataContext, Is.SameAs(data));
        }
    }
}
