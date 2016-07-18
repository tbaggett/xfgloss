using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XFGlossSample.ViewModels
{
	// Simple data models used to group menu items into sections for menu ListView's data source
	public class AppMenuViewModel : List<AppMenuItem>
	{
		public string Title { get; set; }

		private AppMenuViewModel(string title)
		{
			Title = title;
		}

		public static IList<AppMenuViewModel> MenuItems { get; private set; }

		static AppMenuViewModel()
		{
			List<AppMenuViewModel> menuItems = new List<AppMenuViewModel>
			{
				new AppMenuViewModel("XFGloss Properties")
				{
					new AppMenuItem("BackgroundColor"),
					new AppMenuItem("BackgroundGradient"),
					new AppMenuItem("OnTintColor"),
					new AppMenuItem("ThumbOnTintColor"),
					new AppMenuItem("ThumbTintColor"),
					new AppMenuItem("TintColor"),
				}
			};

			// Add iOS only entry if we're running on iOS
			if (Device.OS == TargetPlatform.iOS)
			{
				menuItems[0].Insert(0, new AppMenuItem("AccessoryType (iOS only)", "AccessoryType"));
			}

			MenuItems = menuItems;
		}
	}

	public class AppMenuItem
	{
		public string Title { get; set; }
		public string PropertyName { get; set; }

		public AppMenuItem(string title, string propertyName = null)
		{
			Title = title;
			PropertyName = propertyName ?? title;
		}
	}
}