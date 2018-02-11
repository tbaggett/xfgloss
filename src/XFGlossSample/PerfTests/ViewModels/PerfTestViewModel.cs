// /*
//  * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
//  * http://github.com/tbaggett
//  * http://twitter.com/tbaggett
//  * http://tommyb.com
//  * http://ansuria.com
//  * 
//  * The MIT License (MIT) see GitHub For more information
//  *
//  * Unless required by applicable law or agreed to in writing, software
//  * distributed under the License is distributed on an "AS IS" BASIS,
//  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  * See the License for the specific language governing permissions and
//  * limitations under the License.
//  */
//
using System;
using System.Collections.Generic;

namespace XFGlossSample.PerfTests.ViewModels
{
	public class PerfTestViewModel : List<SectionItem>
	{
		public string Title { get; private set; }

		private PerfTestViewModel(string title)
		{
			Title = title;
		}

		public static IList<PerfTestViewModel> ListItems { get; private set; }

		static PerfTestViewModel()
		{
			List<PerfTestViewModel> listItems = new List<PerfTestViewModel>();
			for (int section = 0; section < 10; section++)
			{
				var sectionItem = new PerfTestViewModel($"Section {section}");
				for (int item = 1; item < 10; item++)
				{
					sectionItem.Add(new SectionItem($"Item {item}"));
				}

				listItems.Add(sectionItem);
			}

			ListItems = listItems;
		}
	}

	public class SectionItem
	{
		public string Title { get; private set; }

		public SectionItem(string title)
		{
			Title = title;
		}
	}
}