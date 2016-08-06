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
	#region iOSXFGlossCellRenderer

	internal class iOSXFGlossCellRenderer : XFGlossCellRenderer<UITableViewCell>, IGradientRenderer
	{
		#region IGradientRenderer implementation

		public virtual void CreateNativeElement<TElement>(string propertyName, TElement element) where TElement : XFGlossElement
		{
			// No need to check property name yet. BackgroundGradient is the only property currently supported.
			//if (propertyName == CellGloss.BackgroundGradientProperty.PropertyName && element is Gradient)
			//{
			if (element is Gradient)
			{
				CreateBackgroundGradientView(GetNativeCell(), element as Gradient);
			}
			//}
		}

		public virtual bool CanUpdate(string propertyName)
		{
			// No need to check property name yet. BackgroundGradient is the only property currently supported.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				return GetBackgroundGradientView(nativeCell) != null;
			}

			return false;
		}

		public virtual void RemoveNativeElement(string propertyName)
		{
			// No need to check property name yet. BackgroundGradient is the only property currently supported.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				RemoveBackgroundGradientView(nativeCell);
			}
		}

		public void UpdateRotation(string propertyName, int rotation)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				GetBackgroundGradientView(nativeCell)?.UpdateRotation(rotation);
			}
		}

		public void UpdateSteps(string propertyName, GradientStepCollection steps)
		{
			// No need to check property name yet, BackgroundGradient is the only one being handled here.
			var nativeCell = GetNativeCell();
			if (nativeCell != null)
			{
				GetBackgroundGradientView(nativeCell)?.UpdateSteps(steps);
			}
		}

		// Helper func to create new background gradient view if one is assigned
		UIBackgroundGradientView CreateBackgroundGradientView(UITableViewCell nativeCell, Gradient gradient)
		{
			RemoveBackgroundGradientView(nativeCell);

			if (nativeCell != null)
			{
				nativeCell.BackgroundView = new UIBackgroundGradientView(CGRect.Empty, gradient);
			}
			return nativeCell?.BackgroundView as UIBackgroundGradientView;
		}

		// Helper func to get existing background gradient view if one is assigned
		UIBackgroundGradientView GetBackgroundGradientView(UITableViewCell nativeCell)
		{
			if (nativeCell != null && nativeCell.BackgroundView is UIBackgroundGradientView)
			{
				return nativeCell.BackgroundView as UIBackgroundGradientView;
			}

			return null;
		}

		void RemoveBackgroundGradientView(UITableViewCell nativeCell)
		{
			if (nativeCell != null)
			{
				nativeCell.BackgroundView?.Dispose();
				nativeCell.BackgroundView = null;
			}
		}

		#endregion

		#region UpdateProperties

		public static void UpdateProperties(Cell cell, UITableViewCell nativeCell)
		{
			UpdateProperties(cell, nativeCell, () => new iOSXFGlossCellRenderer());
		}

		protected override void UpdateProperties(Cell cell, UITableViewCell nativeCell, string propertyName)
		{
			// TintColor property - to be passed to CreateEditIndicatorAccessoryView and possibly others in the future
			if (propertyName == null || propertyName == CellGloss.TintColorProperty.PropertyName)
			{
				var tintColor = (Color)cell.GetValue(CellGloss.TintColorProperty);
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
			Gradient bkgrndGradient = (Gradient)cell.GetValue(CellGloss.BackgroundGradientProperty);
			if (bkgrndGradient != null && bkgrndGradient.UpdateProperties(CellGloss.BackgroundGradientProperty.PropertyName, 
			                                                              this, propertyName))
			{
				// We don't need to handle BackgroundColor if a BackgroundGradient is assigned/updated
				return ;
			}

			if (propertyName == null || propertyName == CellGloss.BackgroundColorProperty.PropertyName)
			{
				Color bkgrndColor = (Color)cell.GetValue(CellGloss.BackgroundColorProperty);

				if (bkgrndColor != Color.Default)
				{
					UIColor uiColor = bkgrndColor.ToUIColor();

					// First check for a background color view being already assigned. Update it if found
					if (nativeCell.BackgroundView is UIBackgroundColorView &&
					    nativeCell.BackgroundView.BackgroundColor != uiColor)
					{
						nativeCell.BackgroundView.BackgroundColor = uiColor;
					}
					else
					{
						// Dispose of any previously assigned background gradient view before replacing it with a background color view
						if (nativeCell.BackgroundView is UIBackgroundGradientView)
						{
							nativeCell.BackgroundView.Dispose();
							nativeCell.BackgroundView = null;
						}

						UIBackgroundColorView bkgrndView = new UIBackgroundColorView(CGRect.Empty);
						bkgrndView.BackgroundColor = uiColor;

						nativeCell.BackgroundView = bkgrndView;
					}
				}
				else
				{
					// Dispose of any previously assigned background color view as a color is no longer assigned
					if (nativeCell.BackgroundView is UIBackgroundColorView)
					{
						nativeCell.BackgroundView.Dispose();
						nativeCell.BackgroundView = null;
					}
				}
			}
		}

		// Creating a marker class so we can confirm if an instance is assigned to the UINativeCell.BackgroundView property
		class UIBackgroundColorView : UIView
		{
			public UIBackgroundColorView(CGRect rect) : base(rect) { }
		}

		#endregion
	}
	#endregion

	#region iOSXFGlossAccessoryCellRenderer
	internal class iOSXFGlossAccessoryCellRenderer : iOSXFGlossCellRenderer
	{
		WeakReference<UIView> _accessoryView;

		new public static void UpdateProperties(Cell cell, UITableViewCell nativeCell)
		{
			UpdateProperties(cell, nativeCell, () => new iOSXFGlossAccessoryCellRenderer());
		}

		protected override void ElementPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == CellGloss.AccessoryTypeProperty.PropertyName)
			{
				UpdateProperties(args.PropertyName);
			}

			base.ElementPropertyChanged(sender, args);
		}

		protected override void UpdateProperties(Cell cell, UITableViewCell nativeCell, string propertyName)
		{
			// AccessoryType property
			if (propertyName == null ||
			    propertyName == CellGloss.AccessoryTypeProperty.PropertyName)
			{
				var accessoryType = (CellGlossAccessoryType)cell.GetValue(CellGloss.AccessoryTypeProperty);
				UIView accView;
				if (_accessoryView != null && _accessoryView.TryGetTarget(out accView))
				{
					if (accessoryType != CellGlossAccessoryType.EditIndicator)
					{
						accView.Dispose();
						_accessoryView = null;
						nativeCell.AccessoryView = null;
					}
				}

				switch (accessoryType)
				{
					case CellGlossAccessoryType.None:
						nativeCell.Accessory = UITableViewCellAccessory.None;
						//nativeCell.AccessoryView = new UIView(new CGRect(0, 0, 20, 40));
						//_accessoryView = new WeakReference<UIView>(nativeCell.AccessoryView);
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
						if (!(nativeCell.AccessoryView is EditIndicatorView))
						{
							nativeCell.Accessory = UITableViewCellAccessory.None;
							nativeCell.AccessoryView = CreateEditIndicatorAccessoryView((Color)cell.GetValue(CellGloss.TintColorProperty));
							_accessoryView = new WeakReference<UIView>(nativeCell.AccessoryView);
						}
						break;
				}
			}

			base.UpdateProperties(cell, nativeCell, propertyName);
		}

		// Creating a marker class so we can confirm if an instance is assigned to the UINativeCell.AccessoryView property
		class EditIndicatorView : UIImageView
		{
			public EditIndicatorView(UIImage image) : base(image) { }
		}

		EditIndicatorView CreateEditIndicatorAccessoryView(Color tintColor)
		{
			// Load our custom edit indicator image
			UIImage image = new UIImage("acc_edit_indicator.png");

			// Set custom tint color if one was passed to us
			if (tintColor != Color.Default)
			{
				image = image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
			}

			EditIndicatorView view = new EditIndicatorView(image);
			if (tintColor != Color.Default)
			{
				view.TintColor = tintColor.ToUIColor();
			}

			return view;
		}
	}
	#endregion

	#region iOSXFGlossSwitchCellRenderer
	internal class iOSXFGlossSwitchCellRenderer : iOSXFGlossCellRenderer
	{
		SwitchCellGloss _properties;

		public iOSXFGlossSwitchCellRenderer(BindableObject bindable)
		{
			_properties = new SwitchCellGloss(bindable);
		}

		new public static void UpdateProperties(Cell cell, UITableViewCell nativeCell)
		{
			UpdateProperties(cell, nativeCell, () => new iOSXFGlossSwitchCellRenderer(cell));
		}

		protected override void ElementPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == SwitchCellGloss.OnTintColorProperty.PropertyName ||
			    args.PropertyName == SwitchCellGloss.ThumbTintColorProperty.PropertyName ||
			    args.PropertyName == SwitchCellGloss.ThumbOnTintColorProperty.PropertyName)
			{
				UpdateProperties(args.PropertyName);
			}

			// Special handling of state change to make XF Switch and Switch property names consistent
			if (args.PropertyName == SwitchCell.OnProperty.PropertyName)
			{
				base.UpdateProperties(Switch.IsToggledProperty.PropertyName);
			}

			base.ElementPropertyChanged(sender, args);
		}

		protected override void UpdateProperties(Cell cell, UITableViewCell nativeCell, string propertyName)
		{
			if (nativeCell.AccessoryView is UISwitch)
			{
				var uiSwitch = nativeCell.AccessoryView as UISwitch;
				uiSwitch.UpdateColorProperty(_properties, propertyName);
			}

			base.UpdateProperties(cell, nativeCell, propertyName);
		}
	}
	#endregion

	#region Xamarin.Forms renderers
	public class XFGlossEntryCellRenderer : EntryCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var nativeCell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossAccessoryCellRenderer.UpdateProperties(item, nativeCell);

			return nativeCell;
		}
	}

	public class XFGlossSwitchCellRenderer : SwitchCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var nativeCell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossSwitchCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	public class XFGlossTextCellRenderer : TextCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var nativeCell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossAccessoryCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	public class XFGlossImageCellRenderer : ImageCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var nativeCell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossAccessoryCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}

	public class XFGlossViewCellRenderer : ViewCellRenderer
	{
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			var nativeCell = base.GetCell(item, reusableCell, tv);
			iOSXFGlossAccessoryCellRenderer.UpdateProperties(item, nativeCell);
			return nativeCell;
		}
	}
	#endregion
}