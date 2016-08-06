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

using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Extensions;
using XFGloss.Droid.Drawables;
using AView = Android.Views.View;
using AViewGroup = Android.Views.ViewGroup;
using Android.Content;

[assembly: ExportRenderer(typeof(EntryCell), typeof(XFGloss.Droid.Renderers.XFGlossEntryCellRenderer))]
[assembly: ExportRenderer(typeof(SwitchCell), typeof(XFGloss.Droid.Renderers.XFGlossSwitchCellRenderer))]
[assembly: ExportRenderer(typeof(TextCell), typeof(XFGloss.Droid.Renderers.XFGlossTextCellRenderer))]
[assembly: ExportRenderer(typeof(ImageCell), typeof(XFGloss.Droid.Renderers.XFGlossImageCellRenderer))]
[assembly: ExportRenderer(typeof(ViewCell), typeof(XFGloss.Droid.Renderers.XFGlossViewCellRenderer))]

namespace XFGloss.Droid.Renderers
{
	/// <summary>
	/// The Android platform-specific XFGlossRenderer base class used for all <see cref="T:Xamarin.Forms.Cell"/> types.
	/// </summary>
	internal class DroidXFGlossCellRenderer : XFGlossCellRenderer<AView>, IGradientRenderer
	{
		#region IGradientRenderer implementation

		/// <summary>
		/// Implementation of method required by the <see cref="T:XFGloss.IXFGlossRenderer"/> interface that the
		/// <see cref="T:XFGloss.IGradientRenderer"/> interface extends. Applies the passed 
		/// <see cref="T:XFGloss.XFGlossElement"/> properties to the Android cell controls.
		/// </summary>
		/// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
		/// <param name="element">The <see cref="T:XFGloss.XFGlossElement"/> instance that changed</param>
		/// <typeparam name="TElement">The type <see cref="T:XFGloss.XFGlossElement"/> that changed</typeparam>
		public void CreateNativeElement<TElement>(string propertyName, TElement element) where TElement : XFGlossElement
		{
			// No need to check property name yet. BackgroundGradient is the only property currently supported.
			//if (propertyName == CellGloss.BackgroundGradientProperty.PropertyName && element is Gradient)
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				RemoveBackgroundGradientDrawable(nativeCell);
				nativeCell.Background = new XFGlossPaintDrawable(element as Gradient);
			}
		}

		/// <summary>
		/// Implementation of method required by the <see cref="T:XFGloss.IXFGlossRenderer"/> interface that the
		/// <see cref="T:XFGloss.IGradientRenderer"/> interface extends. Indicates if there is an existing 
		/// implementation of the property specified by the propertyName parameter.
		/// </summary>
		/// <returns><c>true</c>, if an existing implementation is found, <c>false</c> otherwise.</returns>
		/// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
		public bool CanUpdate(string propertyName)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				return GetBackgroundGradientDrawable(nativeCell) != null;
			}

			return false;
		}

		/// <summary>
		/// Implementation of method required by the <see cref="T:XFGloss.IXFGlossRenderer"/> interface that the
		/// <see cref="T:XFGloss.IGradientRenderer"/> interface extends. Removes any existing implementation of
		/// the property specified by the propertyName parameter.
		/// </summary>
		/// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
		public void RemoveNativeElement(string propertyName)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				RemoveBackgroundGradientDrawable(nativeCell);
			}
		}

		/// <summary>
		/// Implementation of method required by the <see cref="T:XFGloss.IGradientRenderer"/> interface. Updates
		/// the rotation angle being used by any existing implementation of the property specified by the propertyName
		/// parameter.
		/// </summary>
		/// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
		/// <param name="rotation">The new rotation value, an integer number between 0 and 359</param>
		public void UpdateRotation(string propertyName, int rotation)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				GetBackgroundGradientDrawable(nativeCell)?.UpdateRotation(rotation);
			}
		}

		/// <summary>
		/// Implementation of method required by the <see cref="T:XFGloss.IGradientRenderer"/> interface. Updates
		/// the gradient fill steps being used by any existing implementation of the property specified by the 
		/// propertyName parameter.
		/// </summary>
		/// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
		/// <param name="steps">The new collection of <see cref="T:XFGloss.GradientStep"/> instances that specify the
		/// colors and positions of each step of the gradient fill</param>
		public void UpdateSteps(string propertyName, GradientStepCollection steps)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				GetBackgroundGradientDrawable(nativeCell)?.UpdateSteps(steps);
			}
		}

		/// <summary>
		/// Private helper method used to find and return a previously-created 
		/// <see cref="T:XFGloss.Droid.Drawables.XFGlossPaintDrawable"/> instance if found, null if not found.
		/// </summary>
		/// <returns>The background gradient drawable if found, null if not.</returns>
		/// <param name="nativeCell">The native Android view used to display the cell contents</param>
		XFGlossPaintDrawable GetBackgroundGradientDrawable(AView nativeCell)
		{
			if (nativeCell.Background is XFGlossPaintDrawable)
			{
				return nativeCell.Background as XFGlossPaintDrawable;
			}

			return null;
		}

		/// <summary>
		/// Private helper method used to remove any previously-created 
		/// <see cref="T:XFGloss.Droid.Drawables.XFGlossPaintDrawable"/> instance if found.
		/// </summary>
		/// <param name="nativeCell">The native Android view used to display the cell contents</param>
		void RemoveBackgroundGradientDrawable(AView nativeCell)
		{
			if (nativeCell != null)
			{
				nativeCell.Background?.Dispose();
				nativeCell.Background = null;
			}
		}

		#endregion

		/// <summary>
		/// Static method called by custom Xamarin.Forms renderers, used to direct the call to the cross-platform base 
		/// class and provide the required <see cref="T:XFGloss.XFGlossCellRenderer"/> factory method.
		/// </summary>
		/// <param name="cell">The associated <see cref="T:Xamarin.Forms.Cell"/> instance</param>
		/// <param name="nativeCell">The native Android view used to display the cell contents</param>
		public static void UpdateProperties(Cell cell, AView nativeCell)
		{
			UpdateProperties(cell, nativeCell, () => new DroidXFGlossCellRenderer());
		}

		/// <summary>
		/// Implementation of the cross-platform base class's abstract UpdateProperties method. Used to apply the
		/// XFGloss attached BindableProperty values for the property specified by the propertyName parameter.
		/// </summary>
		/// <param name="cell">The associated <see cref="T:Xamarin.Forms.Cell"/> instance</param>
		/// <param name="nativeCell">The native Android view used to display the cell contents</param>
		/// <param name="propertyName">The name of the XFGloss attached BindableProperty that changed</param>
		protected override void UpdateProperties(Cell cell, AView nativeCell, string propertyName)
		{
			// BackgroundColor and BackgroundGradient properties
			// We shouldn't apply BOTH a background gradient and solid color. The gradient takes preference.
			Gradient bkgrndGradient = (Gradient)cell.GetValue(CellGloss.BackgroundGradientProperty);
			if (bkgrndGradient != null && bkgrndGradient.UpdateProperties(CellGloss.BackgroundGradientProperty.PropertyName,
																		  this, propertyName))
			{
				// We don't need to handle BackgroundColor if a BackgroundGradient is assigned/updated
				return;
			}

			// We only process background color if a gradient wasn't applied by the base class.
			// BackgroundColor property
			if (propertyName == null || propertyName == CellGloss.BackgroundColorProperty.PropertyName)
			{
				Color bkgrndColor = (Color)cell.GetValue(CellGloss.BackgroundColorProperty);
				nativeCell.SetBackgroundColor((bkgrndColor != Color.Default)
											  ? bkgrndColor.ToAndroid() : Android.Graphics.Color.Transparent);
			}
		}
	}

	/// <summary>
	/// The Android platform-specific XFGloss cell renderer class used for the <see cref="T:Xamarin.Forms.SwitchCell"/>
	/// class.
	/// </summary>
	internal class DroidXFGlossSwitchCellRenderer : DroidXFGlossCellRenderer
	{
		SwitchCellGloss _properties;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Droid.Renderers.DroidXFGlossSwitchCellRenderer"/> 
		/// class.
		/// </summary>
		/// <param name="bindable">Bindable.</param>
		public DroidXFGlossSwitchCellRenderer(BindableObject bindable)
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
			UpdateProperties(cell, nativeCell, () => new DroidXFGlossSwitchCellRenderer(cell));
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
			if (nativeCell is SwitchCellView && (nativeCell as SwitchCellView).AccessoryView is Android.Widget.Switch)
			{
				var aSwitch = (nativeCell as SwitchCellView).AccessoryView as Android.Widget.Switch;
				aSwitch.UpdateColorProperty(_properties, propertyName);
			}

			base.UpdateProperties(cell, nativeCell, propertyName);
		}
	}

	/// <summary>
	/// Custom <see cref="T:Xamarin.Forms.EntryCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation
	/// </summary>
	public class XFGlossEntryCellRenderer : EntryCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="Xamarin.Forms.EntryCellRenderer"/> GetCellCore method, used to apply any custom 
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
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	/// <summary>
	/// Custom <see cref="T:Xamarin.Forms.SwitchCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation
	/// </summary>
	public class XFGlossSwitchCellRenderer : SwitchCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="Xamarin.Forms.SwitchCellRenderer"/> GetCellCore method, used to apply any custom 
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
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossSwitchCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	/// <summary>
	/// Custom <see cref="T:Xamarin.Forms.TextCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation
	/// </summary>
	public class XFGlossTextCellRenderer : TextCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="Xamarin.Forms.TextCellRenderer"/> GetCellCore method, used to apply any custom 
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
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	/// <summary>
	/// Custom <see cref="T:Xamarin.Forms.ImageCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation
	/// </summary>
	public class XFGlossImageCellRenderer : ImageCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="Xamarin.Forms.ImageCellRenderer"/> GetCellCore method, used to apply any custom 
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
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	/// <summary>
	/// Custom <see cref="T:Xamarin.Forms.ViewCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation
	/// </summary>
	public class XFGlossViewCellRenderer : ViewCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="Xamarin.Forms.ViewCellRenderer"/> GetCellCore method, used to apply any custom 
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
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}
}