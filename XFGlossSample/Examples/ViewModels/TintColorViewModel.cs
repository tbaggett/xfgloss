﻿/*
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

namespace XFGlossSample.Examples.ViewModels
{
    public class TintColorViewModel : IExamplesViewModel
    {
        public string PropertyDefault
        {
            get { return "Color.Default"; }
        }

        public string[] PropertyDescription
        {
            get
            {
                string result;

                if (Device.RuntimePlatform == Device.iOS)
                {
                    result = "Specifies a numeric or named XF.Color value to apply to the cell's accessory view or " +
                            "the track/border portion of the Switch control when the control is in the \"Off\" " +
                            "position.";
                }
                else
                {
                    result = "Specifies a numeric or named XF.Color value to apply to the track/border portion of " +
                                "the Switch control when the control is in the \"Off\" position.";
                }

                return new string[] { result };
            }
        }

        public string PropertyType
        {
            get { return "Xamarin.Forms.Color"; }
        }

        public string TargetClasses
        {
            get
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    return "EntryCell, ImageCell, Switch, SwitchCell, TextCell, ViewCell";
                }
                else
                {
                    return "Switch, SwitchCell";
                }
            }
        }

        public bool isRunningiOS
        {
            get { return Device.RuntimePlatform == Device.iOS; }
        }
    }
}