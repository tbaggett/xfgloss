/*
 * Copyright (C) 2016-2017 Ansuria Solutions LLC & Tommy Baggett: 
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
using AColor = Android.Graphics.Color;
using AView = Android.Views.View;
using AViewGroup = Android.Views.ViewGroup;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using System;
using Android.Views;

[assembly: ExportCell(typeof(EntryCell), typeof(XFGloss.Droid.Renderers.XFGlossEntryCellRenderer))]
[assembly: ExportCell(typeof(SwitchCell), typeof(XFGloss.Droid.Renderers.XFGlossSwitchCellRenderer))]
[assembly: ExportCell(typeof(TextCell), typeof(XFGloss.Droid.Renderers.XFGlossTextCellRenderer))]
[assembly: ExportCell(typeof(ImageCell), typeof(XFGloss.Droid.Renderers.XFGlossImageCellRenderer))]
[assembly: ExportCell(typeof(ViewCell), typeof(XFGloss.Droid.Renderers.XFGlossViewCellRenderer))]

namespace XFGloss.Droid.Renderers
{
	/// <summary>
	/// The Android platform-specific XFGlossRenderer base class used for all <see cref="T:Xamarin.Forms.Cell"/> types.
	/// </summary>
	[Android.Runtime.Preserve(AllMembers = true)]
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
				// The material design ripple effect was introduced in Lollipop. Use it if we're running on that or newer
				if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
				{
					var ripple = BackgroundRippleDrawable.Create(new XFGlossPaintDrawable(element as Gradient),
															 (element as Gradient).AverageColor.ToAndroid());
					nativeCell.SetOnTouchListener(ripple);
					nativeCell.Background = ripple;
				}
				else
				{
					// Otherwise we just darken/lighten the cell background depending on how dark the background is
					nativeCell.Background = 
						BackgroundStateListDrawable.Create(new XFGlossPaintDrawable(element as Gradient),
														   (element as Gradient).AverageColor.ToAndroid());
				}
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
			// We expect either a ripple or state list drawable depending on the version of OS we're running on
			if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
			{
				if (nativeCell.Background is BackgroundRippleDrawable)
				{
					return (nativeCell.Background as BackgroundRippleDrawable).GetBackgroundDrawable() 
						                                                      as XFGlossPaintDrawable;
				}
			}
			else
			{
				if (nativeCell.Background is BackgroundStateListDrawable)
				{
					return (nativeCell.Background as BackgroundStateListDrawable).GetBackgroundDrawable() 
						                                                         as XFGlossPaintDrawable;
				}
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
				var bk = nativeCell.Background;

				Color bkgrndColor = (Color)cell.GetValue(CellGloss.BackgroundColorProperty);
				// We don't want to assign a background color if the default color is specified 
				// and we're updating all properties.
				if (propertyName != null || bkgrndColor != Color.Default)
				{
					AColor aBkColor = (bkgrndColor != Color.Default) ? bkgrndColor.ToAndroid() : AColor.Transparent;

					// The material design ripple effect was introduced in Lollipop. Use it if we're running on that or newer
					if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
					{
						var ripple = BackgroundRippleDrawable.Create(aBkColor);
						nativeCell.SetOnTouchListener(ripple);
						nativeCell.Background = ripple;
					}
					else
					{
						// Pre-lollipop means no ripple available
						// See FAQ at bottom of http://android-developers.blogspot.com/2014/10/appcompat-v21-material-design-for-pre.html
						// Q: Why are there no ripples on pre-Lollipop?
						// A: A lot of what allows RippleDrawable to run smoothly is Android 5.0’s new RenderThread. 
						// To optimize for performance on previous versions of Android, we've left RippleDrawable out 
						// for now.

						nativeCell.Background = BackgroundStateListDrawable.Create(aBkColor);
					}
				}
			}
		}

		// Returns a dark or light colored ripple/shading color based on the provided background color value
		static AColor GetEffectColor(AColor backgroundColor)
		{
			// Determine if we should ripple/shade with black or white - black if max color level >= 50%, white if not
			var level = Math.Max(Math.Max(backgroundColor.R, backgroundColor.G), backgroundColor.B);
			// Different alpha levels needed depending on a ripple or shading being used
			var alpha = (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop) ? 128 : 64;

			return (level >= 128) ? new AColor(0, 0, 0, alpha) : new AColor(255, 255, 255, alpha);
		}

		// Helper class used to create a new ripple drawable on top of the desired background color or gradient fill
		[Android.Runtime.Preserve(AllMembers = true)]
		class BackgroundRippleDrawable : RippleDrawable, AView.IOnTouchListener
		{
			// Static method to create a ripple with a background color as its content layer
			public static BackgroundRippleDrawable Create(AColor backgroundColor)
			{
				return BackgroundRippleDrawable.Create(new ColorDrawable(backgroundColor), backgroundColor);
			}

			// Static method to create a ripple with a gradient drawable (or whatever kind is passed) as its content layer
			public static BackgroundRippleDrawable Create(Drawable contentDrawable, AColor averageColor)
			{
				return new BackgroundRippleDrawable(ColorStateList.ValueOf(GetEffectColor(averageColor)), contentDrawable);
			}

			// Convenience initializer to drop the unwanted mask param
			BackgroundRippleDrawable(ColorStateList csl, Drawable background) : base(csl, background, null)
			{
			}

			// Helper needed for accessing an existing BackgroundGradient gradient fill layer
			public Drawable GetBackgroundDrawable()
			{
				Drawable result = null;

				// We should have at least 1 layer. If so, the bottom-most layer will be the provided content layer
				if (NumberOfLayers > 0)
				{
					result = GetDrawable(0);
				}

				return result;
			}

			// Update the ripple's origin if we receive a touch event (doesn't matter what kind)
			public bool OnTouch(AView v, MotionEvent e)
			{
				SetHotspot(e.GetX(), e.GetY());
				return false;
			}
		}

		// Helper class used to create a darkening/lightening tint effect on top of the desired background color or
		// gradient fill on older (pre-Lollipop) versions of the OS
		[Android.Runtime.Preserve(AllMembers = true)]
		class BackgroundStateListDrawable : StateListDrawable
		{
			public static BackgroundStateListDrawable Create(AColor backgroundColor)
			{
				return BackgroundStateListDrawable.Create(new ColorDrawable(backgroundColor), backgroundColor);
			}

			public static BackgroundStateListDrawable Create(Drawable contentDrawable, AColor averageColor)
			{
				return new BackgroundStateListDrawable(contentDrawable, averageColor);
			}

			// Helper needed for accessing an existing BackgroundGradient gradient fill layer
			public Drawable GetBackgroundDrawable()
			{
				Drawable result = null;
				if (_contentDrawable != null && _contentDrawable.TryGetTarget(out result))
				{
					return result;
				}

				return result;
			}

			// Weak reference to the assigned background content drawable
			WeakReference<Drawable> _contentDrawable;

			BackgroundStateListDrawable(Drawable contentDrawable, AColor averageColor) : base()
			{
				// This is a bit tricky... since we are setting up one drawable to be displayed as the background
				// depending on the cell's current display state, we have to composite the partially transparent
				// tinting overlay on top of the provided content drawable into a LayerDrawable that we will assign
				// to the states that the tinting should appear to be applied to.

				// Get the needed tinting color
				AColor effectColor = GetEffectColor(averageColor);
				// Create a new ColorDrawable filled with the tinting color
				var tint = new ColorDrawable(effectColor);
				// Create a LayerDrawable that will composite the tint ColorDrawable on top of the provided content
				// drawable.
				var compositeContent = new LayerDrawable(new Drawable[] { contentDrawable, tint });

				// Assign the composite content to all the states that may be displayed when the user taps the cell
				AddState(new int[] { Android.Resource.Attribute.StatePressed }, compositeContent);
				AddState(new int[] { Android.Resource.Attribute.StateFocused }, compositeContent);
				AddState(new int[] { Android.Resource.Attribute.StateActivated }, compositeContent);

				// Assign just the passed content drawable for the normal (not tapped) state
				AddState(new int[] { }, contentDrawable);

				// Keep a weak reference to the passed content drawable around so we can return it if/when it is needed
				// later.
				_contentDrawable = new WeakReference<Drawable>(contentDrawable);
			}

			// Clean up our weak reference if we're disposing
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					_contentDrawable = null;
				}

				base.Dispose(disposing);
			}
		}
	}

	/// <summary>
	/// The Android platform-specific XFGloss cell renderer class used for the <see cref="T:Xamarin.Forms.SwitchCell"/>
	/// class.
	/// </summary>
	[Android.Runtime.Preserve(AllMembers = true)]
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
	[Android.Runtime.Preserve(AllMembers = true)]
	public class XFGlossEntryCellRenderer : EntryCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="EntryCellRenderer"/> GetCellCore method, used to apply any custom 
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
	/// Custom <see cref="T:SwitchCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation. This implementation may be superceded by the
	/// <see cref="T:XFGloss.Droid.Renderers.XFGlossSwitchCompatCellRenderer"/> implementation if the Android AppCompat
	/// library is being used, the app is being run on an older API, and the XFGloss.Droid.Library.UsingSwitchCompatCell
	/// boolean property is set to true when the XFGloss.Droid.Library.Init(...) method is executed by the app's main
	/// activity. 
	/// </summary>
	[Android.Runtime.Preserve(AllMembers = true)]
	public class XFGlossSwitchCellRenderer : SwitchCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="SwitchCellRenderer"/> GetCellCore method, used to apply any custom 
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
	/// Custom <see cref="T:TextCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation
	/// </summary>
	[Android.Runtime.Preserve(AllMembers = true)]
	public class XFGlossTextCellRenderer : TextCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="TextCellRenderer"/> GetCellCore method, used to apply any custom 
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
	/// Custom <see cref="T:ImageCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation
	/// </summary>
	[Android.Runtime.Preserve(AllMembers = true)]
	public class XFGlossImageCellRenderer : ImageCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="ImageCellRenderer"/> GetCellCore method, used to apply any custom 
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
	/// Custom <see cref="T:ViewCellRenderer"/>-based renderer class used to apply the custom XFGloss
	/// properties to the Android platform-specific implementation
	/// </summary>
	[Android.Runtime.Preserve(AllMembers = true)]
	public class XFGlossViewCellRenderer : ViewCellRenderer
	{
		/// <summary>
		/// Override of the <see cref="ViewCellRenderer"/> GetCellCore method, used to apply any custom 
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