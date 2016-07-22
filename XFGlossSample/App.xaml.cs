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

