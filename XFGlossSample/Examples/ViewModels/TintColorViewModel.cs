using System;
using Xamarin.Forms;
using XFGloss.Models;

namespace XFGlossSample.Examples.ViewModels
{
	public class TintColorViewModel : IExamplesViewModel
	{
		public string PropertyDefault
		{
			get { return "Color.Default"; }
		}

		public string[] PropertyDescription
		{
			get
			{
				string result;

				if (Device.OS == TargetPlatform.iOS)
				{
					result = "Specifies a numeric or named XF.Color value to apply to the cell's accessory view or " +
							"the track/border portion of the Switch control when the control is in the \"Off\" " +
							"position.";
				}
				else
				{
					result = "Specifies a numeric or named XF.Color value to apply to the track/border portion of " +
								"the Switch control when the control is in the \"Off\" position.";
				}

				return new string[] { result };
			}
		}

		public string PropertyType
		{
			get { return "Xamarin.Forms.Color"; }
		}

		public string TargetClasses
		{
			get 
			{
				if (Device.OS == TargetPlatform.iOS)
				{
					return "EntryCell, ImageCell, Switch, SwitchCell, TextCell, ViewCell";
				}
				else
				{
					return "Switch, SwitchCell";
				}
			}
		}

		public bool isRunningiOS
		{
			get { return Device.OS == TargetPlatform.iOS; }
		}
	}
}