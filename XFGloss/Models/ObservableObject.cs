using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace XFGloss
{
	// This class was duplicated from the Xamarin Forms Labs project. Thanks!

	public class ObservableObject : INotifyPropertyChanged
	{
		public ObservableObject()
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var eventHandler = PropertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> propertyExpression)
		{
			var propertyName = GetPropertyName(propertyExpression);
			return SetProperty<T>(ref storage, value, propertyName);
		}

		protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value))
			{
				return false;
			}

			storage = value;
			NotifyPropertyChanged(propertyName);
			return true;
		}

		string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
		{
			if (propertyExpression == null)
			{
				throw new ArgumentNullException("propertyExpression");
			}

			if (propertyExpression.Body.NodeType != ExpressionType.MemberAccess)
			{
				throw new ArgumentException("Should be a member access lambda expression", "propertyExpression");
			}

			var memberExpression = (MemberExpression)propertyExpression.Body;
			return memberExpression.Member.Name;
		}

		protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}
	}
}