namespace Wavefront.Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var result = AUVSensorsFactory.Build();

            Assert.True(result.Count > 0);
        }

        [Test]
        public void Test2()
        {
            var sensors = AUVSensorsFactory.Build();

            sensors[0].ToString();
        }


    }
}