using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using XFGloss.Models;

namespace XFGloss.Views
{
	public class Cell
	{
		#region AccessoryType

		public static readonly BindableProperty AccessoryTypeProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.AccessoryType, typeof(CellAccessoryType), typeof(Xamarin.Forms.Cell), CellAccessoryType.None);

		public static CellAccessoryType GetAccessoryType(BindableObject bindable)
		{
			return (CellAccessoryType)bindable.GetValue(AccessoryTypeProperty);
		}

		public static void SetAccessoryType(BindableObject bindable, CellAccessoryType value)
		{
			bindable.SetValue(AccessoryTypeProperty, value);
		}

		#region AccessoryDetailAction

		public static readonly BindableProperty AccessoryDetailActionProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.AccessoryDetailAction, typeof(Action<Cell>), typeof(Xamarin.Forms.Cell), null);

		public static Action<Cell> GetAccessoryDetailAction(BindableObject bindable)
		{
			return (Action<Cell>)bindable.GetValue(AccessoryDetailActionProperty);
		}

		public static void SetAccessoryDetailAction(BindableObject bindable, Action<Cell> value)
		{
			bindable.SetValue(AccessoryDetailActionProperty, value);
		}

		#endregion

		#endregion

		#region BackgroundColor

		public static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.BackgroundColor, typeof(Color), typeof(Xamarin.Forms.Cell), Color.Default);

		public static Color GetBackgroundColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(BackgroundColorProperty);
		}

		public static void SetBackgroundColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(BackgroundColorProperty, value);
		}

		#endregion

		#region BackgroundGradient

		public static readonly BindableProperty BackgroundGradientProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.BackgroundGradient, typeof(Gradient), typeof(Xamarin.Forms.Cell), null);

		public static Gradient GetBackgroundGradient(BindableObject bindable)
		{
			return (Gradient)bindable.GetValue(BackgroundGradientProperty);
		}

		public static void SetBackgroundGradient(BindableObject bindable, Gradient value)
		{
			bindable.SetValue(BackgroundGradientProperty, value);
		}

		#endregion

		#region TintColor

		public static readonly BindableProperty TintColorProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.TintColor, typeof(Color), typeof(Xamarin.Forms.Cell), Color.Default);

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

		public Cell(BindableObject bindable)
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

		public CellAccessoryType AccessoryType
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? CellAccessoryType.None : GetAccessoryType(bindable);
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

		public Action<Cell> AccessoryDetailAction
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? null : GetAccessoryDetailAction(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetAccessoryDetailAction(bindable, value);
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

		public Gradient BackgroundGradient
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? null : GetBackgroundGradient(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetBackgroundGradient(bindable, value);
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