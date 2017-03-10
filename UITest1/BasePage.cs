using System;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;

using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using System.Linq;


namespace UITest1
{
    public class BasePage
    {
        protected readonly IApp iapp;
        protected readonly bool OnAndroid;
        protected readonly bool OniOS;

        protected Func<AppQuery, AppQuery> Trait;
        protected BasePage(string androidTrait, string iOSTrait)
            : this(x => x.Marked(androidTrait), x => x.Marked(iOSTrait))
        {

        }
        protected BasePage()
        {
            iapp = AppInitializer.App;

            OnAndroid = iapp.GetType() == typeof(AndroidApp);
            OniOS = iapp.GetType() == typeof(iOSApp);

            InitializeCommonQueries();
        }
        protected BasePage(Func<AppQuery, AppQuery> androidTrait, Func<AppQuery, AppQuery> iOSTrait)
           : this()
        {
            if (OnAndroid)
                Trait = androidTrait;
            if (OniOS)
                Trait = iOSTrait;

            AssertOnPage(TimeSpan.FromSeconds(30));

            iapp.Screenshot("On " + this.GetType().Name);
        }
        protected void AssertOnPage(TimeSpan? timeout = default(TimeSpan?))
        {
            if (Trait == null)
                throw new NullReferenceException("Trait not set");

            var message = "Unable to verify on page: " + this.GetType().Name;

            if (timeout == null)
                Assert.IsNotEmpty(iapp.Query(Trait), message);
            else
                Assert.DoesNotThrow(() => iapp.WaitForElement(Trait, timeout: timeout), message);
        }

        protected void WaitForPageToLeave(TimeSpan? timeout = default(TimeSpan?))
        {
            if (Trait == null)
                throw new NullReferenceException("Trait not set");

            timeout = timeout ?? TimeSpan.FromSeconds(2);
            var message = "Unable to verify *not* on page: " + this.GetType().Name;

            Assert.DoesNotThrow(() => iapp.WaitForNoElement(Trait, timeout: timeout), message);
        }


        Query Hamburger;
        Func<string, Query> Tab;

        void InitializeCommonQueries()
        {
            if (OnAndroid)
            {
                Hamburger = x => x.Class("ImageButton").Marked("OK");
                Tab = name => x => x.Id("design_menu_item_text").Text(name);
            }
            if (OniOS)
            {
                Tab = name => x => x.Class("UITabBarButtonLabel").Text(name);
            }
        }

        public void NavigateTo(string tabName)
        {
            if (OnAndroid)
            {
                if (iapp.Query(Hamburger).Any())
                    iapp.Tap(Hamburger);

                iapp.Screenshot("Navigation Menu Open");
                int count = 0;
                while (!iapp.Query(tabName).Any() && count < 3)
                {
                    iapp.ScrollDown(x => x.Class("NavigationMenuView"));
                    count++;
                }
            }
            iapp.Tap(Tab(tabName));
        }

        public void PullToRefresh()
        {
            var rect = iapp.Query().First().Rect;
            iapp.DragCoordinates(rect.CenterX, rect.CenterY, rect.CenterX, rect.Height);
        }

    }

}
