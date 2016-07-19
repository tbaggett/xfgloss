namespace XFGlossSample.Examples.ViewModels
{
	public class BackgroundGradientViewModel : IExamplesViewModel
	{
		public string PropertyDefault
		{
			get { return "null"; }
		}

		public string[] PropertyDescription
		{
			get
			{
				return new string[]
				{
					"Specifies a multi-color gradient to fill the background of the associated control instance with." +
					" You can specify as many colors as you like and control their distribution across the fill at " +
					"any angle. Convenience properties and constructors also make it easy to create two-color " +
					"horizontal or vertical fills."
				};
			}
		}

		public string PropertyType
		{
			get { return "XFGloss.GlossGradient"; }
		}

		public string TargetClasses
		{
			get { return "ContentPage, EntryCell, ImageCell, SwitchCell, TextCell, ViewCell"; }
		}
	}
}