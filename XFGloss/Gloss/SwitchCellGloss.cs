using System;
using Xamarin.Forms;

namespace XFGloss
{
	public class SwitchCellGloss : CellGloss, ISwitchGloss
	{
		#region OnTintColor

		public static readonly BindableProperty OnTintColorProperty =
			BindableProperty.CreateAttached("OnTintColor", typeof(Color), typeof(Xamarin.Forms.SwitchCell), Color.Default);

		public static Color GetOnTintColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(OnTintColorProperty);
		}

		public static void SetOnTintColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(OnTintColorProperty, value);
		}

		#endregion

		#region ThumbTintColor

		public static readonly BindableProperty ThumbTintColorProperty =
			BindableProperty.CreateAttached("ThumbTintColor", typeof(Color), typeof(Xamarin.Forms.SwitchCell), Color.Default);

		public static Color GetThumbTintColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(ThumbTintColorProperty);
		}

		public static void SetThumbTintColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(ThumbTintColorProperty, value);
		}

		#endregion

		#region ThumbOnTintColor

		public static readonly BindableProperty ThumbOnTintColorProperty =
			BindableProperty.CreateAttached("ThumbOnTintColor", typeof(Color), typeof(Xamarin.Forms.SwitchCell), Color.Default);

		public static Color GetThumbOnTintColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(ThumbOnTintColorProperty);
		}

		public static void SetThumbOnTintColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(ThumbOnTintColorProperty, value);
		}

		#endregion

		#region Interface implementation

		public SwitchCellGloss(BindableObject bindable) : base(bindable)
		{
		}

		public Color OnTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : GetOnTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetOnTintColor(bindable, value);
				}
			}
		}

		public Color ThumbTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : GetThumbTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetThumbTintColor(bindable, value);
				}
			}
		}

		public Color ThumbOnTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : GetThumbOnTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SetThumbOnTintColor(bindable, value);
				}
			}
		}

		#endregion
	}
}