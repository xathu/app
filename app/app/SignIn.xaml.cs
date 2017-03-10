using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Text.RegularExpressions;

namespace app
{
    public partial class SignIn : ContentPage
    {
        public SignIn()
        {
            
            InitializeComponent();
        }
        public SignIn(string mobielNumber)
        {

            InitializeComponent();
         //  MobileNumber.Text = mobielNumber;
        }
    }
}
