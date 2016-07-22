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
using XFGlossSample.Examples.ViewModels;

namespace XFGlossSample.Examples.Views
{
	public partial class InfoPage : ContentPage
	{
		public InfoPage()
		{
			InitializeComponent();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();

			// Reset/add children to our PropertyDescription stack layout
			PropertyDescription.Children.Clear();

			if (BindingContext is IExamplesViewModel)
			{
				foreach (string description in (BindingContext as IExamplesViewModel).PropertyDescription)
				{
					PropertyDescription.Children.Add(new Label() { Text = description });
				}
			}
		}
	}
}

