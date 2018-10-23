using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace ProyectoPaises
{
	public partial class App : Application
	{
        public const string WebServiceUrl = "https://restcountries.eu/rest/v2/";

        public App ()
		{
			InitializeComponent();

			MainPage = new BusquedasTabPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
