namespace XFGlossSample.Examples.ViewModels
{
	public class MaxTrackTintColorViewModel : IExamplesViewModel
	{
		public string PropertyDefault
		{
			get
			{
				return "Color.Default";
			}
		}

		public string[] PropertyDescription
		{
			get
			{
				return new string[]
				{
					"Specifies a numeric or named XF.Color value to apply to the right side of the Slider control's " +
					"track, beginning at the thumb position.",
				};
			}
		}

		public string PropertyType
		{
			get
			{
				return "Xamarin.Forms.Color";
			}
		}

		public string TargetClasses
		{
			get
			{
				return "Slider";
			}
		}
	}
}

