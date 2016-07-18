using System;
using Xamarin.Forms;
using XFGloss.Models;

namespace XFGloss.Views
{
	public class Page
	{
		#region BackgroundGradient

		public static readonly BindableProperty BackgroundGradientProperty =
			BindableProperty.CreateAttached(XFGlossPropertyNames.BackgroundGradient, typeof(Gradient), typeof(Xamarin.Forms.Page), null);

		public static Gradient GetBackgroundGradient(BindableObject bindable)
		{
			return (Gradient)bindable.GetValue(BackgroundGradientProperty);
		}

		public static void SetBackgroundGradient(BindableObject bindable, Gradient value)
		{
			bindable.SetValue(BackgroundGradientProperty, value);
		}

		#endregion

		#region Instance access

		WeakReference<BindableObject> _bindable;

		public Page(BindableObject bindable)
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

		#endregion
	}
}