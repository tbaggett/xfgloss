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
using Xamarin.Forms;

namespace XFGlossSample.Examples
{
    public class DeviceExtensions
    {
        public static T OnPlatform<T>(T iOS, T android, T wp)
		{

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					return iOS;
				case Device.Android:
					return android;
				case Device.WinPhone:
					return wp;
			}
			return default(T);
		}
    }
}
