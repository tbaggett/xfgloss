using System;
using Xamarin.Forms;

namespace XFGloss
{
	public class SwitchGloss : ISwitchGloss
	{
		#region TintColor

		public static readonly BindableProperty TintColorProperty =
            BindableProperty.CreateAttached("TintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

		public static Color GetTintColor(BindableObject bindable)
		{
			return (Color)bindable.GetValue(TintColorProperty);
		}

		public static void SetTintColor(BindableObject bindable, Color value)
		{
			bindable.SetValue(TintColorProperty, value);
		}

		#endregion

		#region OnTintColor

		public static readonly BindableProperty OnTintColorProperty =
            BindableProperty.CreateAttached("OnTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

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
            BindableProperty.CreateAttached("ThumbTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

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
            BindableProperty.CreateAttached("ThumbOnTintColor", typeof(Color), typeof(Xamarin.Forms.Switch), Color.Default);

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

		WeakReference<BindableObject> _bindable;

		public SwitchGloss(BindableObject bindable)
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

		public Color TintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : SwitchGloss.GetTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SwitchGloss.SetTintColor(bindable, value);
				}
			}
		}

		public Color OnTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : SwitchGloss.GetOnTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SwitchGloss.SetOnTintColor(bindable, value);
				}
			}
		}

		public Color ThumbTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : SwitchGloss.GetThumbTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SwitchGloss.SetThumbTintColor(bindable, value);
				}
			}
		}

		public Color ThumbOnTintColor
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? Color.Default : SwitchGloss.GetThumbOnTintColor(bindable);
			}

			set
			{
				var bindable = Bindable;
				if (bindable != null)
				{
					SwitchGloss.SetThumbOnTintColor(bindable, value);
				}
			}
		}
		#endregion
	}
}