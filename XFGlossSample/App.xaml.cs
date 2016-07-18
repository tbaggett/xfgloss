using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * Had to disable XamlC due to https://bugzilla.xamarin.com/show_bug.cgi?id=37371
 * causing OnPlatform to not work when assigning values to attached properties.
 * See AppMenu.xaml's IsVisible assignment for the "AccessoryType (iOS only)"
 * TextCell entry for an example of the problem code. Enabling XamlC will cause
 * the OnPlatform node to be ignored.
 */
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XFGlossSample
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppMenu();
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

