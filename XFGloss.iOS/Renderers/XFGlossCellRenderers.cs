using System;
using System.ComponentModel;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFGloss.iOS.Extensions;
using XFGloss.iOS.Views;

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
		Color _lastBkgrndColor = Color.Default;

		public static void Apply(Cell cell, UITableViewCell nativeCell)
		{
			Apply(cell, nativeCell, () => new iOSXFGlossCellTracker());
		}

		protected override void CellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == CellGloss.BackgroundColorProperty.PropertyName ||
			    args.PropertyName == CellGloss.BackgroundGradientProperty.PropertyName ||
			    args.PropertyName == CellGloss.TintColorProperty.PropertyName)
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
			    propertyName == CellGloss.TintColorProperty.PropertyName)
			{
				Color tintColor = (Color)cell.GetValue(CellGloss.TintColorProperty);
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

			// BackgroundColor and BackgroundGradient properties
			// We shouldn't apply BOTH a background gradient and solid color. The gradient takes preference.
			if (propertyName == null || propertyName == CellGloss.BackgroundGradientProperty.PropertyName)
			{
				GlossGradient backgroundGradient = (GlossGradient)cell.GetValue(CellGloss.BackgroundGradientProperty);
				if (backgroundGradient != null)
				{
					if (nativeCell.BackgroundView is UIBackgroundGradientView)
					{
						XFGlossGradientLayer.UpdateGradientLayer(nativeCell.BackgroundView, backgroundGradient);
					}
					else
					{
						DisposeBackgroundView(nativeCell);
						nativeCell.BackgroundView = new UIBackgroundGradientView(CGRect.Empty, backgroundGradient);
					}

					// We don't need to handle BackgroundColor if a BackgroundGradient is assigned
					return;
				}
				else
				{
					DisposeBackgroundView(nativeCell);
				}
			}

			if (propertyName == null || propertyName == CellGloss.BackgroundColorProperty.PropertyName)
			{
				Color bkgrndColor = (Color)cell.GetValue(CellGloss.BackgroundColorProperty);

				if (bkgrndColor != Color.Default)
				{
					if (bkgrndColor != _lastBkgrndColor)
					{
						_lastBkgrndColor = bkgrndColor;

						UIColor uiColor = bkgrndColor.ToUIColor();

						DisposeBackgroundView(nativeCell);

						UIView bkgrndView = new UIView(CGRect.Empty);
						bkgrndView.BackgroundColor = uiColor;

						nativeCell.BackgroundView = bkgrndView;
					}
				}
				else
				{
					DisposeBackgroundView(nativeCell);
				}
			}
		}

		void DisposeBackgroundView(UITableViewCell nativeCell)
		{
			if (nativeCell?.BackgroundView != null)
			{
				nativeCell.BackgroundView.Dispose();
				nativeCell.BackgroundView = null;
			}
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
			if (args.PropertyName == CellGloss.AccessoryTypeProperty.PropertyName)
			{
				Reapply(args.PropertyName);
			}

			base.CellPropertyChanged(sender, args);
		}

		protected override void ReapplyProperties(Cell cell, UITableViewCell nativeCell, string propertyName)
		{
			// AccessoryType property
			if (propertyName == null ||
			    propertyName == CellGloss.AccessoryTypeProperty.PropertyName)
			{
				var accessoryType = (CellGlossAccessoryType)cell.GetValue(CellGloss.AccessoryTypeProperty);
				UIView accView;
				if (_accessoryView != null && _accessoryView.TryGetTarget(out accView))
				{
					accView.Dispose();
					_accessoryView = null;
					nativeCell.AccessoryView = null;
				}

				switch (accessoryType)
				{
					case CellGlossAccessoryType.None:
						nativeCell.Accessory = UITableViewCellAccessory.None;
						nativeCell.AccessoryView = new UIView(new CGRect(0, 0, 20, 40));
						_accessoryView = new WeakReference<UIView>(nativeCell.AccessoryView);
						break;

					case CellGlossAccessoryType.Checkmark:
						nativeCell.Accessory = UITableViewCellAccessory.Checkmark;
						break;

					// Disabled until we can access the detail button tapped method in the table view source
					// for both the ListView (currently not possible) and TableView (currently possible) classes.
					/*
					case CellAccessoryType.DetailButton:
						nativeCell.Accessory = UITableViewCellAccessory.DetailButton;
						break;

					case CellAccessoryType.DetailDisclosureButton:
						nativeCell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;
						break;
					*/

					case CellGlossAccessoryType.DisclosureIndicator:
						nativeCell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
						break;

					case CellGlossAccessoryType.EditIndicator:
						nativeCell.Accessory = UITableViewCellAccessory.None;
						nativeCell.AccessoryView = CreateEditIndicatorAccessoryView((Color)cell.GetValue(CellGloss.TintColorProperty));
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
			if (tintColor != Color.Default)
			{
				view.TintColor = tintColor.ToUIColor();
			}

			return view;
		}
	}
	#endregion

	#region iOSXFGlossSwitchCellTracker
	internal class iOSXFGlossSwitchCellTracker : iOSXFGlossCellTracker
	{
		SwitchCellGloss _properties;

		public iOSXFGlossSwitchCellTracker(BindableObject bindable)
		{
			_properties = new SwitchCellGloss(bindable);
		}

		new public static void Apply(Cell cell, UITableViewCell nativeCell)
		{
			Apply(cell, nativeCell, () => new iOSXFGlossSwitchCellTracker(cell));
		}

		protected override void CellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == SwitchCellGloss.OnTintColorProperty.PropertyName ||
			    args.PropertyName == SwitchCellGloss.ThumbTintColorProperty.PropertyName ||
			    args.PropertyName == SwitchCellGloss.ThumbOnTintColorProperty.PropertyName)
			{
				Reapply(args.PropertyName);
			}

			// Special handling of state change to make XF Switch and Switch property names consistent
			if (args.PropertyName == SwitchCell.OnProperty.PropertyName)
			{
				base.Reapply(Switch.IsToggledProperty.PropertyName);
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
}