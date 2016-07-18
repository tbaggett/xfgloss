using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using XFGloss.Models;

namespace XFGloss.Views
{
	public class XFGlossCellProperties
	{
		#region Accessory

		public static readonly BindableProperty AccessoryTypeProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.AccessoryType, typeof(XFGlossCellAccessoryType), typeof(Cell), XFGlossCellAccessoryType.None);

		public static XFGlossCellAccessoryType GetAccessoryType(BindableObject bindable)
		{
			return (XFGlossCellAccessoryType)bindable.GetValue(AccessoryTypeProperty);
		}

		public static void SetAccessoryType(BindableObject bindable, XFGlossCellAccessoryType value)
		{
			bindable.SetValue(AccessoryTypeProperty, value);
		}

		#endregion

		#region BackgroundColor

		public static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.BackgroundColor, typeof(Color), typeof(Cell), Color.Default);

		public static Color GetBackgroundColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(BackgroundColorProperty);
		}

		public static void SetBackgroundColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(BackgroundColorProperty, value);
		}

		#endregion

		#region IsVisible

		public static readonly BindableProperty IsVisibleProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.IsVisible, typeof(bool), typeof(Cell), true);

		public static bool GetIsVisible(BindableObject bindable)
		{
			return (bool)bindable.GetValue(IsVisibleProperty);
		}

		public static void SetIsVisible(BindableObject bindable, bool value)
		{
			bindable.SetValue(IsVisibleProperty, value);
		}

		#endregion

		#region TintColor

		public static readonly BindableProperty TintColorProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.TintColor, typeof(Color), typeof(Cell), Color.Default);

		public static Color GetTintColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(TintColorProperty);
		}

		public static void SetTintColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(TintColorProperty, value);
		}

		#endregion

		#region Instance access

		WeakReference<BindableObject> _bindable;

		public XFGlossCellProperties(BindableObject bindable)
		{
			_bindable = new WeakReference<BindableObject>(bindable);
		}

		public BindableObject Bindable
		{
			get
			{
				BindableObject bindable;
				if (_bindable.TryGetTarget(out bindable))
				{
					return bindable;
				}

				return null;
			}

			set
			{
				_bindable.SetTarget(value);
			}
		}

		public XFGlossCellAccessoryType AccessoryType
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? XFGlossCellAccessoryType.None : GetAccessoryType(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetAccessoryType(bindable, value);
				}
			}
		}

		public Color BackgroundColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : GetBackgroundColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetTintColor(bindable, value);
				}
			}
		}

		public bool IsVisible
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? true : GetIsVisible(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetIsVisible(bindable, value);
				}
			}
		}

		public Color TintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : GetTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetTintColor(bindable, value);
				}
			}
		}
		#endregion
	}
}