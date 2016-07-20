using Xamarin.Forms;
using XFGloss;
using XFGlossSample.Utils;

namespace XFGlossSample.Examples.Views.Xaml
{
	public partial class BackgroundGradientPage : ContentPage
	{
		Timer updater;
		GlossGradient spareGradient;

		public BackgroundGradientPage()
		{
			InitializeComponent();

			// Update the rotating gradient
			spareGradient = new GlossGradient(rotatingGradient);
			UpdateGradient();
		}

		/******************************************
		 * 
		 * NOTE: This code is for gradient demonstration purposes only. I do NOT recommend you continuously update
		 * a gradient fill in a cell or page! 
		 * 
		 ******************************************/

		void UpdateGradient(object gradient = null)
		{
			Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
			{
				GlossGradient crntGradient = CellGloss.GetBackgroundGradient(rotatingCell);
				if (crntGradient.Angle >= 355)
				{
					crntGradient.Angle = 0;
				}
				else
				{
					crntGradient.Angle += 5;
				}

				// Swap gradients
				GlossGradient newGradient;
				if (crntGradient == rotatingGradient)
				{
					newGradient = spareGradient;
				}
				else
				{
					newGradient = rotatingGradient;
				}

				newGradient.ShallowCopy(crntGradient);

				CellGloss.SetBackgroundGradient(rotatingCell, newGradient);
			});

			updater?.Dispose();
			updater = new Timer(UpdateGradient, rotatingGradient, 100, -1);
		}
	}
}