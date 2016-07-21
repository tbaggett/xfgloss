using System;
using Xamarin.Forms;

namespace XFGloss
{
	public class CellGloss
	{
		#region AccessoryType

		public static readonly BindableProperty AccessoryTypeProperty =
			BindableProperty.CreateAttached("AccessoryType", typeof(CellGlossAccessoryType), typeof(Xamarin.Forms.Cell), CellGlossAccessoryType.None);

		public static CellGlossAccessoryType GetAccessoryType(BindableObject bindable)
		{
			return (CellGlossAccessoryType)bindable.GetValue(AccessoryTypeProperty);
		}

		public static void SetAccessoryType(BindableObject bindable, CellGlossAccessoryType value)
		{
			bindable.SetValue(AccessoryTypeProperty, value);
		}

		#region AccessoryDetailAction

		public static readonly BindableProperty AccessoryDetailActionProperty =
			BindableProperty.CreateAttached("AccessoryDetailAction", typeof(Action<CellGloss>), typeof(Xamarin.Forms.Cell), null);

		public static Action<CellGloss> GetAccessoryDetailAction(BindableObject bindable)
		{
			return (Action<CellGloss>)bindable.GetValue(AccessoryDetailActionProperty);
		}

		public static void SetAccessoryDetailAction(BindableObject bindable, Action<CellGloss> value)
		{
			bindable.SetValue(AccessoryDetailActionProperty, value);
		}

		#endregion

		#endregion

		#region BackgroundColor

		public static readonly BindableProperty BackgroundColorProperty =
			BindableProperty.CreateAttached("BackgroundColor", typeof(Color), typeof(Xamarin.Forms.Cell), Color.Default);

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
			BindableProperty.CreateAttached("BackgroundGradient", typeof(GlossGradient), typeof(Xamarin.Forms.Cell), null);

		public static GlossGradient GetBackgroundGradient(BindableObject bindable)
		{
			return (GlossGradient)bindable.GetValue(BackgroundGradientProperty);
		}

		public static void SetBackgroundGradient(BindableObject bindable, GlossGradient value)
		{
			bindable.SetValue(BackgroundGradientProperty, value);
		}

		#endregion

		#region TintColor

		public static readonly BindableProperty TintColorProperty =
			BindableProperty.CreateAttached("TintColor", typeof(Color), typeof(Xamarin.Forms.Cell), Color.Default);

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

		public CellGloss(BindableObject bindable)
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

		public CellGlossAccessoryType AccessoryType
		{
			get
			{
				var bindable = Bindable;
				return (bindable == null) ? CellGlossAccessoryType.None : GetAccessoryType(bindable);
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

		public Action<CellGloss> AccessoryDetailAction
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

		public GlossGradient BackgroundGradient
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