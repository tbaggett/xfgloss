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

using Android.App;
using Android.Content.PM;
using Android.OS;

namespace XFGlossSample.Droid
{
	[Activity(Label = "XFGlossSample.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle savedInstanceState)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(savedInstanceState);

			Xamarin.Forms.Forms.Init(this, savedInstanceState);

			var app = new App();
			LoadApplication(app);

			// Set this property to FALSE if you don't want XFGloss to render XF SwitchCell controls using the
			// material design styling on older Android APIs (16+) when the AppCompat library is being used.
			// NOTE: The SwitchGloss tinting properties WILL NOT WORK on older Android APIs if you set this
			// property to false.

			// XFGloss.Droid.Library.UsingSwitchCompatCell = false;

			// Set this property to TRUE if you want translucent slider and switch control tracks to be used
			// on APIs older than 21 when AppCompat is being used. This property is disabled by default due to
			// older devices' performance possibly being impacted by having to render partially transparent UI elements.

			// XFGloss.Droid.Library.UsingAppCompatAlpha = true;

			// IMPORTANT: Initialize XFGloss AFTER calling LoadApplication on the Android platform
			XFGloss.Droid.Library.Init(this, savedInstanceState);
		}
	}
}