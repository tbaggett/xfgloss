using System;
using Xamarin.Forms;
using XFGloss.Models;
using XFGlossSample.ViewModels;

namespace XFGlossSample.Examples.ViewModels
{
	public class AccessoryTypeViewModel : IExamplesViewModel
	{
		public string PropertyDefault
		{
			get { return "CellAccessoryType.None"; }
		}

		public string[] PropertyDescription
		{
			get
			{
				return new string[] 
				{
					"Specifies an indicator type to display in the right side of a table or list view cell.  See the " +
					"XFGloss CellAccessoryType enumeration for the available types.",
					"This property is only implemented on the iOS platform. iOS is the only " +
					"platform that natively supports an accessory view."
				};
			}
		}

		public string PropertyType
		{
			get { return "CellAccessoryType"; }
		}

		public string TargetClasses
		{
			get { return "EntryCell, ImageCell, TextCell, ViewCell"; }
		}
	}
}