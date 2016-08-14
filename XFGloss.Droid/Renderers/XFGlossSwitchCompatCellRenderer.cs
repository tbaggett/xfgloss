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
using System.ComponentModel;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Extensions;
using AView = Android.Views.View;
using AViewGroup = Android.Views.ViewGroup;

namespace XFGloss.Droid.Renderers
{
	/// <summary>
	/// The Android platform-specific XFGloss cell renderer class used for the <see cref="T:Xamarin.Forms.SwitchCell"/>
	/// class IF the Android AppCompat library is being used and the XFGloss.Droid.Library.UsingSwitchCompatCell 
	/// property is true when the XFGloss.Droid.Library.Init(...) method is executed by the app's main activity.
	/// </summary>
	internal class DroidXFGlossSwitchCompatCellRenderer : DroidXFGlossCellRenderer
	{
		SwitchCellGloss _properties;

		public DroidXFGlossSwitchCompatCellRenderer(BindableObject bindable)
		{
			_properties = new SwitchCellGloss(bindable);
		}

		/// <summary>
		/// Static method called by the custom <see cref="T:Xamarin.Forms.SwitchCell"/> renderer, used to direct the 
		/// call to the cross-platform base class and provide the required 
		/// <see cref="T:XFGloss.XFGlossCellRenderer"/> factory method. Hides the
		/// <see cref="T:XFGloss.Droid.Renderers.DroidXFGlossCellRenderer"/> base class's implementation of this method.
		/// </summary>
		/// <param name="cell">The associated <see cref="T:Xamarin.Forms.Cell"/> instance</param>
		/// <param name="nativeCell">The native Android view used to display the cell contents</param>
		new public static void UpdateProperties(Cell cell, AView nativeCell)
		{
			UpdateProperties(cell, nativeCell, () => new DroidXFGlossSwitchCompatCellRenderer(cell));
		}

		/// <summary>
		/// Override of the <see cref="T:XFGloss.Droid.Renderers.DroidXFGlossCellRenderer"/> base class's implementation
		/// of the ElementPropertyChanged method, checks the XFGloss properties that are unique to the 
		/// <see cref="T:Xamarin.Forms.SwitchCell"/> class.
		/// </summary>
		/// <param name="sender">The object instance the notification was received from</param>
		/// <param name="args">The PropertyChanged event arguments</param>
		protected override void ElementPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == CellGloss.TintColorProperty.PropertyName ||
				args.PropertyName == SwitchCellGloss.OnTintColorProperty.PropertyName ||
				args.PropertyName == SwitchCellGloss.ThumbTintColorProperty.PropertyName ||
				args.PropertyName == SwitchCellGloss.ThumbOnTintColorProperty.PropertyName)
			{
				UpdateProperties(args.PropertyName);
			}

			base.ElementPropertyChanged(sender, args);
		}

		/// <summary>
		/// Override of the <see cref="T:XFGloss.Droid.Renderers.DroidXFGlossCellRenderer"/> base class's implementation
		/// of the UpdateProperties method, applies XFGloss property changes that are unique to the 
		/// <see cref="T:Xamarin.Forms.SwitchCell"/> class.
		/// </summary>
		/// <param name="cell">Cell.</param>
		/// <param name="nativeCell">Native cell.</param>
		/// <param name="propertyName">Property name.</param>
		protected override void UpdateProperties(Cell cell, AView nativeCell, string propertyName)
		{
			if (nativeCell is SwitchCompatCellView && 
			    (nativeCell as SwitchCompatCellView).AccessoryView is SwitchCompat)
			{
				var aSwitch = (nativeCell as SwitchCompatCellView).AccessoryView as SwitchCompat;
				aSwitch.UpdateColorProperty(_properties, propertyName);
			}

			base.UpdateProperties(cell, nativeCell, propertyName);
		}
	}

	public class SwitchCompatCellView : BaseCellView, CompoundButton.IOnCheckedChangeListener
	{
		public SwitchCompatCellView(Context context, Cell cell) : base(context, cell)
		{
			var sw = new SwitchCompat(context);
			sw.SetOnCheckedChangeListener(this);

			SetAccessoryView(sw);

			SetImageVisible(false);
		}

		public SwitchCell Cell { get; set; }

		public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
		{
			Cell.On = isChecked;
		}
	}

	/// <summary>
	/// Custom <see cref="T:Xamarin.Forms.CellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation. This implementation supercedes the
	/// <see cref="T:XFGloss.Droid.Renderers.XFGlossSwitchCellRenderer"/> implementation if the Android AppCompat
	/// library is being used, the app is being run on an older API, and the XFGloss.Droid.Library.UsingSwitchCompatCell
	/// boolean property is set to true when the XFGloss.Droid.Library.Init(...) method is executed by the app's main
	/// activity. 
	/// </summary>
	public class XFGlossSwitchCompatCellRenderer : CellRenderer
	{
		const double DefaultHeight = 30;
		SwitchCompatCellView _view;

		/// <summary>
		/// Override of the <see cref="CellRenderer"/> GetCellCore method, used to apply any custom 
		/// settings to the Android platform-specific cell display element.
		/// </summary>
		/// <returns>The Android platform-specific cell display element after applying any custom settings to it</returns>
		/// <param name="item">The <see cref="T:Xamarin.Forms.Cell"/> instance whose properties need to be transferred 
		/// from</param>
		/// <param name="convertView">A previously-created Android platform-specific display element if this cell has
		/// been rendered before</param>
		/// <param name="parent">The parent Android view container for the cell</param>
		/// <param name="context">The Android context that should be used for retrieving assets</param>
		protected override AView GetCellCore(Cell item, AView convertView, AViewGroup parent, Context context)
		{
			var cell = (SwitchCell)Cell;

			if ((_view = convertView as SwitchCompatCellView) == null)
			{
				_view = new SwitchCompatCellView(context, item);
			}

			_view.Cell = cell;

			UpdateText();
			UpdateChecked();
			UpdateHeight();
			UpdateIsEnabled(_view, cell);

			DroidXFGlossSwitchCompatCellRenderer.UpdateProperties(item, _view);
			return _view;
		}

		protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == SwitchCell.TextProperty.PropertyName)
				UpdateText();
			else if (e.PropertyName == SwitchCell.OnProperty.PropertyName)
				UpdateChecked();
			else if (e.PropertyName == "RenderHeight")
				UpdateHeight();
			else if (e.PropertyName == Cell.IsEnabledProperty.PropertyName)
				UpdateIsEnabled(_view, (SwitchCell)sender);
		}

		void UpdateChecked()
		{
			((SwitchCompat)_view.AccessoryView).Checked = ((SwitchCell)Cell).On;
		}

		void UpdateIsEnabled(SwitchCompatCellView cell, SwitchCell switchCell)
		{
			cell.Enabled = switchCell.IsEnabled;
			var aSwitch = cell.AccessoryView as SwitchCompat;
			if (aSwitch != null)
				aSwitch.Enabled = switchCell.IsEnabled;
		}

		void UpdateHeight()
		{
			_view.SetRenderHeight(Cell.RenderHeight);
		}

		void UpdateText()
		{
			_view.MainText = ((SwitchCell)Cell).Text;
		}
	}
}