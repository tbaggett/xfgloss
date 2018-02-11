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

namespace XFGlossSample.Examples.ViewModels
{
	public class OnTintColorViewModel : IExamplesViewModel
	{
		public string PropertyDefault
		{
			get { return "Color.Default"; }
		}

		public string[] PropertyDescription
		{
			get
			{
				return new string[]
				{
					"Specifies a numeric or named XF.Color value to apply to the track/border portion of the Switch control when the control is in the \"On\" position.",
				};
			}
		}

		public string PropertyType
		{
			get { return "Xamarin.Forms.Color"; }
		}

		public string TargetClasses
		{
			get { return "Switch, SwitchCell"; }
		}
	}
}