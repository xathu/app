using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;
using app;

namespace UITest1
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp iapp;
        Platform platform;
        protected bool OnAndroid;
        protected bool OniOS;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            iapp = AppInitializer.StartApp(platform);
            OnAndroid = iapp.GetType() == typeof(AndroidApp);
            OniOS = iapp.GetType() == typeof(iOSApp);

            if (iapp.Query("LoginPageIdentifier").Any())
            {
                new LogIN().TapNotNow();
                System.Threading.Thread.Sleep(2000);
            }

            if (iapp.Query("Push Notifications").Any())
                iapp.Tap("Maybe Later");
        }

    }
}

