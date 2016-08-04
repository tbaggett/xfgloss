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
	internal class DroidXFGlossCellRenderer : XFGlossCellRenderer<AView>, IGradientRenderer
	{
		#region IGradientRenderer implementation

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

		public bool IsUpdating(string propertyName)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				return GetBackgroundGradientDrawable(nativeCell) != null;
			}

			return false;
		}

		public void RemoveNativeElement(string propertyName)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				RemoveBackgroundGradientDrawable(nativeCell);
			}
		}

		public void UpdateRotation(string propertyName, int rotation)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				GetBackgroundGradientDrawable(nativeCell)?.UpdateRotation(rotation);
			}
		}

		public void UpdateSteps(string propertyName, GradientStepCollection steps)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				GetBackgroundGradientDrawable(nativeCell)?.UpdateSteps(steps);
			}
		}

		XFGlossPaintDrawable GetBackgroundGradientDrawable(AView nativeCell)
		{
			if (nativeCell.Background is XFGlossPaintDrawable)
			{
				return nativeCell.Background as XFGlossPaintDrawable;
			}

			return null;
		}

		void RemoveBackgroundGradientDrawable(AView nativeCell)
		{
			if (nativeCell != null)
			{
				nativeCell.Background?.Dispose();
				nativeCell.Background = null;
			}
		}

		#endregion

		public static void UpdateProperties(Cell cell, AView nativeCell)
		{
			UpdateProperties(cell, nativeCell, () => new DroidXFGlossCellRenderer());
		}

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

	internal class DroidXFGlossSwitchCellRenderer : DroidXFGlossCellRenderer
	{
		SwitchCellGloss _properties;

		public DroidXFGlossSwitchCellRenderer(BindableObject bindable)
		{
			_properties = new SwitchCellGloss(bindable);
		}

		new public static void UpdateProperties(Cell cell, AView nativeCell)
		{
			UpdateProperties(cell, nativeCell, () => new DroidXFGlossSwitchCellRenderer(cell));
		}

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

	public class XFGlossEntryCellRenderer : EntryCellRenderer
	{
		protected override AView GetCellCore(Cell item, AView convertView, AViewGroup parent, Context context)
		{
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	public class XFGlossSwitchCellRenderer : SwitchCellRenderer
	{
		protected override AView GetCellCore(Cell item, AView convertView, AViewGroup parent, Context context)
		{
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossSwitchCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	public class XFGlossTextCellRenderer : TextCellRenderer
	{
		protected override AView GetCellCore(Cell item, AView convertView, AViewGroup parent, Context context)
		{
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	public class XFGlossImageCellRenderer : ImageCellRenderer
	{
		protected override AView GetCellCore(Cell item, AView convertView, AViewGroup parent, Context context)
		{
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	public class XFGlossViewCellRenderer : ViewCellRenderer
	{
		protected override AView GetCellCore(Cell item, AView convertView, AViewGroup parent, Context context)
		{
			var nativeCell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}
}