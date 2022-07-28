using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using WeatherTest;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace WeatherTest.Tests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
           // app = AppInitializer.StartApp(platform);
        }

        [TestCase]
        public void TestCurrencyFormatter()
        {

            
            Assert.AreEqual(17.41, Helpers.KelvinToCelsius(290.56));
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
    }
}
