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
using Xamarin.Forms;
using XFGloss;
using XFGlossSample.ViewModels;

namespace XFGlossSample.Examples.ViewModels
{
	public class AccessoryTypeViewModel : IExamplesViewModel
	{
		public string PropertyDefault
		{
			get { return "CellGlossAccessoryType.None"; }
		}

		public string[] PropertyDescription
		{
			get
			{
				return new string[] 
				{
					"Specifies an indicator type to display in the right side of a table or list view cell.  See the " +
					"CellGlossAccessoryType enumeration for the available types.",
					"This property is only implemented on the iOS platform. iOS is the only " +
					"platform that natively supports an accessory view."
				};
			}
		}

		public string PropertyType
		{
			get { return "CellGlossAccessoryType"; }
		}

		public string TargetClasses
		{
			get { return "EntryCell, ImageCell, TextCell, ViewCell"; }
		}
	}
}