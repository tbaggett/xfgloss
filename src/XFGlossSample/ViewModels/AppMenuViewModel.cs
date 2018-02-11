/*
 * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
 * http://github.com/tbaggett
 * http://twitter.com/tbaggett
 * http://tommyb.com
 * http://ansuria.com
 * 
 * The MIT License (MIT) see GitHub For more information
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
					new AppMenuItem("MaxTrackTintColor"),
					new AppMenuItem("MinTrackTintColor"),
					new AppMenuItem("OnTintColor"),
					new AppMenuItem("ThumbOnTintColor"),
					new AppMenuItem("ThumbTintColor"),
					new AppMenuItem("TintColor"),
				},
				new AppMenuViewModel("ListView Performance Tests")
				{
					new AppMenuItem("Recycle, BkgrndColor", "RecycleBackgroundColor"),
					new AppMenuItem("Retain, BkgrndColor", "RetainBackgroundColor"),
					new AppMenuItem("Recycle, BkgrndGradient", "RecycleBackgroundGradient"),
					new AppMenuItem("Retain, BkgrndGradient", "RetainBackgroundGradient"),
				}
			};

			// Add iOS only entry if we're running on iOS
			if (Device.RuntimePlatform == Device.iOS)
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