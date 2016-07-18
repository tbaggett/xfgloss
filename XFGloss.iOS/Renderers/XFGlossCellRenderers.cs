using System;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFGloss.iOS.Extensions;
using XFGloss.Models;
using XFGloss.Views;

[assembly: ExportRenderer(typeof(EntryCell), typeof(XFGloss.iOS.Renderers.XFGlossEntryCellRenderer))]
[assembly: ExportRenderer(typeof(SwitchCell), typeof(XFGloss.iOS.Renderers.XFGlossSwitchCellRenderer))]
[assembly: ExportRenderer(typeof(TextCell), typeof(XFGloss.iOS.Renderers.XFGlossTextCellRenderer))]
[assembly: ExportRenderer(typeof(ImageCell), typeof(XFGloss.iOS.Renderers.XFGlossImageCellRenderer))]
[assembly: ExportRenderer(typeof(ViewCell), typeof(XFGloss.iOS.Renderers.XFGlossViewCellRenderer))]

namespace XFGloss.iOS.Renderers
{
	#region iOSXFGlossCellTracker
	internal class iOSXFGlossCellTracker : XFGlossCellTracker<UITableViewCell>
	{
		WeakReference<UIView> _backgroundView;

		public static void Apply(Cell cell, UITableViewCell nativeCell)
		{
			Apply(cell, nativeCell, () => new iOSXFGlossCellTracker());
		}

		protected override void CellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == XFGlossPropertyNames.BackgroundColor ||
				args.PropertyName == XFGlossPropertyNames.TintColor)
			{
				Reapply(args.PropertyName);
			}
		}

		protected override void Reapply(string propertyName = null)
		{
			Cell cell;
			UITableViewCell nativeCell;

			if (Reapply(out cell, out nativeCell))
			{
				ReapplyProperties(cell, nativeCell, propertyName);
			}
		}

		protected virtual void ReapplyProperties(Cell cell, UITableViewCell nativeCell, string propertyName)
		{
			// TintColor property - to be passed to CreateEditIndicatorAccessoryView and possibly others in the future
			if (propertyName == null ||
				propertyName == XFGlossPropertyNames.TintColor)
			{
				Color tintColor = (Color)cell.GetValue(XFGlossCellProperties.TintColorProperty);
				if (tintColor != Color.Default)
				{
					nativeCell.TintColor = tintColor.ToUIColor();
					if (nativeCell.AccessoryView != null)
					{
						UIColor uiColor = tintColor.ToUIColor();
						nativeCell.AccessoryView.TintColor = uiColor;
					}
				}
			}

			// BackgroundColor property - can't check propertyName here due to needing to dispose of any
			// unneeded background view
			Color backgroundColor = (Color)cell.GetValue(XFGlossCellProperties.BackgroundColorProperty);
			if (backgroundColor != Color.Default)
			{
				UIColor uiColor = backgroundColor.ToUIColor();

				UIView backgroundView = new UIView(CGRect.Empty);
				backgroundView.BackgroundColor = uiColor;

				_backgroundView = new WeakReference<UIView>(backgroundView);

				nativeCell.BackgroundView = backgroundView;
			}
			else if (_backgroundView != null)
			{
				UIView backgroundView;
				if (_backgroundView.TryGetTarget(out backgroundView))
				{
					backgroundView.Dispose();
					_backgroundView = null;
				}
			}
		}
	}
	#endregion

	#region iOSXFGlossSwitchCellTracker
	internal class iOSXFGlossSwitchCellTracker : iOSXFGlossCellTracker
	{
		XFGlossSwitchCellProperties _properties;

		public iOSXFGlossSwitchCellTracker(BindableObject bindable)
		{
			_properties = new XFGlossSwitchCellProperties(bindable);
		}

		new public static void Apply(Cell cell, UITableViewCell nativeCell)
		{
			Apply(cell, nativeCell, () => new iOSXFGlossSwitchCellTracker(cell));
		}

		protected override void CellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == XFGlossPropertyNames.OnTintColor ||
				args.PropertyName == XFGlossPropertyNames.ThumbTintColor ||
				args.PropertyName == XFGlossPropertyNames.ThumbOnTintColor)
			{
				Reapply(args.PropertyName);
			}

			// Special handling of state change to make XF Switch and Switch property names consistent
			if (args.PropertyName == SwitchCell.OnProperty.PropertyName)
			{
				Reapply(XFGlossPropertyNames.ValueChanged);
			}

			base.CellPropertyChanged(sender, args);
		}

		protected override void ReapplyProperties(Cell cell, UITableViewCell nativeCell, string propertyName)
		{
			if (nativeCell.AccessoryView is UISwitch)
			{
				var uiSwitch = nativeCell.AccessoryView as UISwitch;
				uiSwitch.UpdateColorProperty(_properties, propertyName);
			}

			base.ReapplyProperties(cell, nativeCell, propertyName);
		}
	}
	#endregion

	#region iOSXFGlossAccessoryCellTracker
	internal class iOSXFGlossAccessoryCellTracker : iOSXFGlossCellTracker
	{
		WeakReference<UIView> _accessoryView;

		new public static void Apply(Cell cell, UITableViewCell nativeCell)
		{
			Apply(cell, nativeCell, () => new iOSXFGlossAccessoryCellTracker());
		}

		protected override void CellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == XFGlossPropertyNames.AccessoryType)
			{
				Reapply(args.PropertyName);
			}

			base.CellPropertyChanged(sender, args);
		}

		protected override void ReapplyProperties(Cell cell, UITableViewCell nativeCell, string propertyName)
		{
			// AccessoryType property
			if (propertyName == null ||
				propertyName == XFGlossPropertyNames.AccessoryType)
			{
				var accessoryType = (XFGlossCellAccessoryTypes)cell.GetValue(XFGlossAccessoryCellProperties.AccessoryTypeProperty);
				UIView accView;
				if (_accessoryView != null && _accessoryView.TryGetTarget(out accView))
				{
					accView.Dispose();
					_accessoryView = null;
					nativeCell.AccessoryView = null;
				}

				switch (accessoryType)
				{
					case XFGlossCellAccessoryTypes.None:
						nativeCell.Accessory = UITableViewCellAccessory.None;
						nativeCell.AccessoryView = new UIView(new CGRect(0, 0, 20, 40));
						_accessoryView = new WeakReference<UIView>(nativeCell.AccessoryView);
						break;

					case XFGlossCellAccessoryTypes.Checkmark:
						nativeCell.Accessory = UITableViewCellAccessory.Checkmark;
						break;

					case XFGlossCellAccessoryTypes.DetailButton:
						nativeCell.Accessory = UITableViewCellAccessory.DetailButton;
						break;

					case XFGlossCellAccessoryTypes.DetailDisclosureButton:
						nativeCell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
						break;

					case XFGlossCellAccessoryTypes.DisclosureIndicator:
						nativeCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
						break;

					case XFGlossCellAccessoryTypes.EditIndicator:
						nativeCell.Accessory = UITableViewCellAccessory.None;
						nativeCell.AccessoryView = CreateEditIndicatorAccessoryView((Color)cell.GetValue(XFGlossCellProperties.TintColorProperty));
						_accessoryView = new WeakReference<UIView>(nativeCell.AccessoryView);
						break;
				}
			}

			base.ReapplyProperties(cell, nativeCell, propertyName);
		}

		UIImageView CreateEditIndicatorAccessoryView(Color tintColor)
		{
			// Load our custom edit indicator image
			UIImage image = new UIImage("acc_edit_indicator.png");

			// Set custom tint color if one was passed to us
			if (tintColor != Color.Default)
			{
				image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			}

			UIImageView view = new UIImageView(image);
			view.TintColor = tintColor.ToUIColor();

			return view;
		}
	}
	#endregion

	#region renderers
	public class XFGlossEntryCellRenderer : EntryCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossAccessoryCellTracker.Apply(item, cell);

			return cell;
		}
	}

	public class XFGlossSwitchCellRenderer : SwitchCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossSwitchCellTracker.Apply(item, cell);
			return cell;
		}
	}

	public class XFGlossTextCellRenderer : TextCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossAccessoryCellTracker.Apply(item, cell);
			return cell;
		}
	}

	public class XFGlossImageCellRenderer : ImageCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossAccessoryCellTracker.Apply(item, cell);
			return cell;
		}
	}

	public class XFGlossViewCellRenderer : ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var cell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossAccessoryCellTracker.Apply(item, cell);
			return cell;
		}
	}
	#endregion

	#region Effects for adding to existing customized Cell-derived classes
	public class XFGlossEntryCellEffect : PlatformEffect
	{

		protected override void OnAttached()
		{
		}

		protected override void OnDetached()
		{
		}

		protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
		{
			if (args.PropertyName == BindableObject.BindingContextProperty.PropertyName)
			{
			}

			base.OnElementPropertyChanged(args);
		}
	}
	#endregion
}