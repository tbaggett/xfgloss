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