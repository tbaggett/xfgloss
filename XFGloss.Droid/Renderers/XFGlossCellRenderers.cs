using System;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XFGloss.Droid.Extensions;
using XFGloss.Droid.Drawables;
using XFGloss.Models;
using XFGloss.Trackers;
using AGraphics = Android.Graphics;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(EntryCell), typeof(XFGloss.Droid.Renderers.XFGlossEntryCellRenderer))]
[assembly: ExportRenderer(typeof(Xamarin.Forms.SwitchCell), typeof(XFGloss.Droid.Renderers.XFGlossSwitchCellRenderer))]
[assembly: ExportRenderer(typeof(TextCell), typeof(XFGloss.Droid.Renderers.XFGlossTextCellRenderer))]
[assembly: ExportRenderer(typeof(ImageCell), typeof(XFGloss.Droid.Renderers.XFGlossImageCellRenderer))]
[assembly: ExportRenderer(typeof(ViewCell), typeof(XFGloss.Droid.Renderers.XFGlossViewCellRenderer))]

namespace XFGloss.Droid.Renderers
{
	internal class DroidXFGlossCellTracker : XFGlossCellTracker<AView>
	{
		public static void Apply(Xamarin.Forms.Cell cell, AView nativeCell)
		{
			Apply(cell, nativeCell, () => new DroidXFGlossCellTracker());
		}

		protected override void CellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == XFGlossPropertyNames.BackgroundColor ||
			    args.PropertyName == XFGlossPropertyNames.BackgroundGradient)
			{
				Reapply(args.PropertyName);
			}
		}

		protected override void Reapply(string propertyName = null)
		{
			Xamarin.Forms.Cell cell;
			AView nativeCell;

			if (Reapply(out cell, out nativeCell))
			{
				ReapplyProperties(cell, nativeCell, propertyName);
			}
		}

		protected virtual void ReapplyProperties(Xamarin.Forms.Cell cell, AView nativeCell, string propertyName)
		{
			if (propertyName == null || propertyName == XFGlossPropertyNames.BackgroundGradient)
			{
				Gradient bkgrndGradient = (Gradient)cell.GetValue(Views.Cell.BackgroundGradientProperty);
				// Initialize/update the painter and shader as needed if a gradient is assigned
				if (bkgrndGradient != null)
				{
					if (nativeCell.Background is XFGlossPaintDrawable)
					{
						(nativeCell.Background as XFGlossPaintDrawable).UpdateXFGlossGradient(bkgrndGradient);
						nativeCell.Invalidate();
					}
					else
					{
						nativeCell.Background = new XFGlossPaintDrawable(bkgrndGradient);
					}

					// We don't need to handle BackgroundColor if a BackgroundGradient is assigned
					return;
				}
				else
				{
					nativeCell.Background?.Dispose();
					nativeCell.Background = null;
				}
			}

			// BackgroundColor property
			if (propertyName == null || propertyName == XFGlossPropertyNames.BackgroundColor)
			{
				Color bkgrndColor = (Color)cell.GetValue(Views.Cell.BackgroundColorProperty);
				nativeCell.SetBackgroundColor((bkgrndColor != Color.Default)
											  ? bkgrndColor.ToAndroid() : Android.Graphics.Color.Transparent);
			}
		}
	}

	internal class DroidXFGlossSwitchCellTracker : DroidXFGlossCellTracker
	{
		Views.SwitchCell _properties;

		public DroidXFGlossSwitchCellTracker(BindableObject bindable)
		{
			_properties = new Views.SwitchCell(bindable);
		}

		new public static void Apply(Xamarin.Forms.Cell cell, AView nativeCell)
		{
			Apply(cell, nativeCell, () => new DroidXFGlossSwitchCellTracker(cell));
		}

		protected override void CellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == XFGlossPropertyNames.TintColor ||
			    args.PropertyName == XFGlossPropertyNames.OnTintColor ||
			    args.PropertyName == XFGlossPropertyNames.ThumbTintColor ||
			    args.PropertyName == XFGlossPropertyNames.ThumbOnTintColor)
			{
				Reapply(args.PropertyName);
			}

			base.CellPropertyChanged(sender, args);
		}

		protected override void ReapplyProperties(Xamarin.Forms.Cell cell, AView nativeCell, string propertyName)
		{
			if (nativeCell is SwitchCellView && (nativeCell as SwitchCellView).AccessoryView is global::Android.Widget.Switch)
			{
				var aSwitch = (nativeCell as SwitchCellView).AccessoryView as global::Android.Widget.Switch;
				aSwitch.UpdateColorProperty(_properties, propertyName);
			}

			base.ReapplyProperties(cell, nativeCell, propertyName);
		}
	}

	public class XFGlossEntryCellRenderer : EntryCellRenderer
	{
		protected override Android.Views.View GetCellCore(Xamarin.Forms.Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
		{
			var cell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellTracker.Apply(item, cell);
			return cell;
		}
	}

	public class XFGlossSwitchCellRenderer : SwitchCellRenderer
	{
		protected override Android.Views.View GetCellCore(Xamarin.Forms.Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
		{
			var cell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossSwitchCellTracker.Apply(item, cell);
			return cell;
		}
	}

	public class XFGlossTextCellRenderer : TextCellRenderer
	{
		protected override Android.Views.View GetCellCore(Xamarin.Forms.Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
		{
			var cell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellTracker.Apply(item, cell);
			return cell;
		}
	}

	public class XFGlossImageCellRenderer : ImageCellRenderer
	{
		protected override Android.Views.View GetCellCore(Xamarin.Forms.Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
		{
			var cell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellTracker.Apply(item, cell);
			return cell;
		}
	}

	public class XFGlossViewCellRenderer : ViewCellRenderer
	{
		protected override Android.Views.View GetCellCore(Xamarin.Forms.Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
		{
			var cell = base.GetCellCore(item, convertView, parent, context);
			DroidXFGlossCellTracker.Apply(item, cell);
			return cell;
		}
	}
}