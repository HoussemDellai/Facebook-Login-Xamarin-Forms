using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookLogin.Models;
using FacebookLogin.ViewModels;
using Xamarin.Forms;
using Device = Xamarin.Forms.Device;

namespace FacebookLogin.Views
{
    public class FacebookProfileCsPage : ContentPage
    {

        /// <summary>
        /// Make sure to get a new ClientId from:
        /// https://developers.facebook.com/apps/
        /// </summary>
        private string ClientId = "165942640479284";//"1570192756563503";

        public FacebookProfileCsPage()
        {

            BindingContext = new FacebookViewModel();

            Title = "Facebook Profile";
            BackgroundColor = Color.White;

            var apiRequest =
                "https://www.facebook.com/dialog/oauth?client_id="
                + ClientId
                + "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

            var webView = new WebView
            {
                Source = apiRequest,
                HeightRequest = 1
            };

            webView.Navigated += WebViewOnNavigated;

            Content = webView;
        }

        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
        {

            var accessToken = ExtractAccessTokenFromUrl(e.Url);

            if (accessToken != "")
            {
                var vm = BindingContext as FacebookViewModel;

                await vm.SetFacebookUserProfileAsync(accessToken);

                SetPageContent(vm.FacebookProfile);
            }
        }

        private void SetPageContent(FacebookProfile facebookProfile)
        {
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(8, 30),
                Children =
                {
                    new Label
                    {
                        Text = facebookProfile.Name,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Id,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.IsVerified.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Devices.FirstOrDefault().Os,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Gender,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.AgeRange.Min.ToString(),
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Picture.Data.Url,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                    new Label
                    {
                        Text = facebookProfile.Cover.Source,
                        TextColor = Color.Black,
                        FontSize = 22,
                    },
                }
            };
        }

        private string ExtractAccessTokenFromUrl(string url)
        {
            if (url.Contains("access_token") && url.Contains("&expires_in="))
            {
                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
                {
                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
                }

                var accessToken = at.Remove(at.IndexOf("&expires_in="));

                return accessToken;
            }

            return string.Empty;
        }
    }
}

// To use XAML pages, use the following code:
//
//<? xml version="1.0" encoding="utf-8" ?>
//<ContentPage xmlns = "http://xamarin.com/schemas/2014/forms"
//             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
//             xmlns:viewModels="clr-namespace:FacebookLogin.ViewModels;assembly=FacebookLogin"
//             x:Class="FacebookLogin.FacebookProfilePage"
//             Title="Facebook Profile"
//             BackgroundColor="White">

//  <ContentPage.BindingContext>
//    <viewModels:FacebookViewModel/>
//  </ContentPage.BindingContext>

//  <StackLayout x:Name="MainStackLayout"
//               Padding="8,30">

//    <StackLayout Orientation = "Horizontal"
//                 Padding="0,20,0,50">

//      <Image Source = "{Binding FacebookProfile.Picture.Data.Url}"
//             HeightRequest="100"
//             WidthRequest="100"
//             VerticalOptions="Start"/>

//      <StackLayout Orientation = "Vertical" >

//        <Label Text="{Binding FacebookProfile.Name, StringFormat='Name: {0:N}'}"
//               TextColor="Black"
//               Font="Bold"
//               FontSize="22"/>

//        <Label Text = "{Binding FacebookProfile.Id, StringFormat='Id: {0:N}'}"
//               TextColor="Black"
//               FontSize="20"/>

//        <Label Text = "{Binding FacebookProfile.IsVerified, StringFormat='IsVerified: {0:N}'}"
//                  TextColor="Black"
//                  FontSize="22"/>

//      </StackLayout>

//    </StackLayout>

//    <Label Text = "{Binding FacebookProfile.AgeRange.Min, StringFormat='AgeRange.Min: {0:N}'}"
//           TextColor="Black"
//           FontSize="22"/>

//    <Label Text = "{Binding FacebookProfile.Devices[0].Os, StringFormat='Devices: {0:N}'}"
//           TextColor="Black"
//           FontSize="22"/>

//    <Image Source = "{Binding FacebookProfile.Cover.Source}"
//           HeightRequest="200" />

//  </StackLayout>

//</ContentPage>
/////////////////////////////////////////////////////////////////////////////////////////////////
// and the following for xaml.cs
//
//using FacebookLogin.ViewModels;
//using Xamarin.Forms;

//namespace FacebookLogin
//{
//    public partial class FacebookProfilePage : ContentPage
//    {

//        /// <summary>
//        /// Make sure to get a new ClientId from:
//        /// https://developers.facebook.com/apps/
//        /// </summary>
//        private string ClientId = "165942640479284";//"1570192756563503";

//        public FacebookProfilePage()
//        {
//            InitializeComponent();

//            var apiRequest =
//                "https://www.facebook.com/dialog/oauth?client_id="
//                + ClientId
//                + "&display=popup&response_type=token&redirect_uri=http://www.facebook.com/connect/login_success.html";

//            var webView = new WebView
//            {
//                Source = apiRequest,
//                HeightRequest = 1
//            };

//            webView.Navigated += WebViewOnNavigated;

//            Content = webView;
//        }

//        private async void WebViewOnNavigated(object sender, WebNavigatedEventArgs e)
//        {

//            var accessToken = ExtractAccessTokenFromUrl(e.Url);

//            if (accessToken != "")
//            {
//                var vm = BindingContext as FacebookViewModel;

//                await vm.SetFacebookUserProfileAsync(accessToken);

//                Content = MainStackLayout;
//            }
//        }

//        private string ExtractAccessTokenFromUrl(string url)
//        {
//            if (url.Contains("access_token") && url.Contains("&expires_in="))
//            {
//                var at = url.Replace("https://www.facebook.com/connect/login_success.html#access_token=", "");

//                if (Device.OS == TargetPlatform.WinPhone || Device.OS == TargetPlatform.Windows)
//                {
//                    at = url.Replace("http://www.facebook.com/connect/login_success.html#access_token=", "");
//                }

//                var accessToken = at.Remove(at.IndexOf("&expires_in="));

//                return accessToken;
//            }

//            return string.Empty;
//        }
//    }
//}
