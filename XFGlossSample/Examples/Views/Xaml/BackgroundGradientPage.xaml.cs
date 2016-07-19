using Xamarin.Forms;
using XFGloss;
using XFGlossSample.Utils;

namespace XFGlossSample.Examples.Views.Xaml
{
	public partial class BackgroundGradientPage : ContentPage
	{
		Timer updater;

		public BackgroundGradientPage()
		{
			InitializeComponent();

			UpdateGradient();
		}

		/******************************************
		 * 
		 * NOTE: This code is for gradient demonstration purposes only. I do NOT recommend you continuously update
		 * a gradient fill in a cell or page! It requires a lot of instance creation to trigger updating and the GC
		 * runs on a pretty regular basis as a result. I only added it to make it obvious that you can create a gradient
		 * at any angle and with as many color steps as desired. You have been warned! :-)
		 * 
		 ******************************************/

		void UpdateGradient(object gradient = null)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				if (rotatingGradient.Angle >= 355)
				{
					rotatingGradient.Angle = 0;
				}
				else
				{
					rotatingGradient.Angle += 5;
				}

				var newGradient = new GlossGradient(rotatingGradient);
				rotatingGradient.Dispose();
				rotatingGradient = newGradient;

				CellGloss.SetBackgroundGradient(testCell, rotatingGradient);
			});

			updater?.Dispose();
			updater = new Timer(UpdateGradient, rotatingGradient, 100, -1);
		}
	}
}