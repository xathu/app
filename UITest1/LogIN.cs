using System;
using System.Threading;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;


namespace UITest1
{
  public  class LogIN: BasePage 
    {
        readonly string NotNowButton = "NotNowButton";
        readonly string SignInButton = "SignInButton";

        public LogIN()
              : base("LoginPageIdentifier", "LoginPageIdentifier")
        {
        }

        public void TapLogin()
        {
            iapp.Tap(SignInButton);
        }

        public void TapNotNow()
        {
            iapp.Tap(NotNowButton);
        }
    }
}
