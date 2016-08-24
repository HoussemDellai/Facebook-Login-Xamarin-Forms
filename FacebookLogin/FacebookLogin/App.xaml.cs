using FacebookLogin.Views;
using Xamarin.Forms;

namespace FacebookLogin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage())
            {
                Title = "Facebook Login"
            };
            //MainPage = new NavigationPage(new MainCsPage())
            //{
            //    Title = "Facebook Login"
            //};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
