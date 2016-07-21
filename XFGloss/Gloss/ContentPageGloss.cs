using System;
using Xamarin.Forms;

namespace XFGloss
{
	public class ContentPageGloss
	{
		#region BackgroundGradient

		public static readonly BindableProperty BackgroundGradientProperty =
			BindableProperty.CreateAttached("BackgroundGradient", typeof(GlossGradient), typeof(Xamarin.Forms.Page), null);

		public static GlossGradient GetBackgroundGradient(BindableObject bindable)
		{
			return (GlossGradient)bindable.GetValue(BackgroundGradientProperty);
		}

		public static void SetBackgroundGradient(BindableObject bindable, GlossGradient value)
		{
			bindable.SetValue(BackgroundGradientProperty, value);
		}

		#endregion

		#region Instance access

		WeakReference<BindableObject> _bindable;

		public ContentPageGloss(BindableObject bindable)
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

		#endregion
	}
}