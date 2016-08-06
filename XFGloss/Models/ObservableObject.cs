// This code originally appeared in the Xamarin Forms Labs project (https://github.com/XLabs/Xamarin-Forms-Labs). Thanks!

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace XFGloss
{
	/// <summary>
	/// A base class used to provide a common implementation of the 
	/// <see cref="T:System.ComponentModel.INotifyPropertyChanged"/> interface. Originally created as part of the 
	/// Xamarin Forms Labs project - https://github.com/XLabs/Xamarin-Forms-Labs.
	/// </summary>
	public class ObservableObject : INotifyPropertyChanged
	{
		/// <summary>
		/// An event that occurs after one of the class's properties has changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Method that can be called directly to dispatch the PropertyChanged event to all registered handlers. This
		/// method is useful if you have composite properties that are made up of one or more other properties of the
		/// class, i.e., "FullName" being comprised of both the "FirstName" and "LastName" properties. In such case,
		/// you would call this method, passing "FullName" as the property name, whenever the "FirstName" or "LastName"
		/// property values change.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Sets the property and dispatches the PropertyChanged event if the provided value is not equal to the 
		/// currently assigned value.
		/// </summary>
		/// <returns><c>true</c>, if the property was changed, <c>false</c> otherwise.</returns>
		/// <param name="storage">The actual field/property that the new value should be assigned to if the value is
		/// changing.</param>
		/// <param name="value">The new value to be compared with the current value and assigned if different.</param>
		/// <param name="propertyExpression">Property expression.</param>
		/// <typeparam name="T">The type of passed field and value.</typeparam>
		protected bool SetProperty<T>(ref T storage, T value, Expression<Func<T>> propertyExpression)
		{
			var propertyName = GetPropertyName(propertyExpression);
			return SetProperty<T>(ref storage, value, propertyName);
		}

		/// <summary>
		/// Sets the property and dispatches the PropertyChanged event if the provided value is not equal to the 
		/// currently assigned value.
		/// </summary>
		/// <returns><c>true</c>, if the property was changed, <c>false</c> otherwise.</returns>
		/// <param name="storage">The actual field/property that the new value should be assigned to if the value is
		/// changing.</param>
		/// <param name="value">The new value to be compared with the current value and assigned if different.</param>
		/// <param name="propertyName">The name of the property that was changed.</param>
		/// <typeparam name="T">The type of passed field and value.</typeparam>
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

		/// <summary>
		/// Private method used to retrieve the property name for the provided property expression
		/// </summary>
		/// <returns>The property name.</returns>
		/// <param name="propertyExpression">Property expression.</param>
		/// <typeparam name="T">The type of value that is expected.</typeparam>
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

		/// <summary>
		/// Method that is called when the PropertyChanged event has fired or the 
		/// <see cref="T:XFGloss.ObservableObject.NotifyPropertyChanged"/> method has been called.
		/// </summary>
		/// <param name="e">The property changed event arguments</param>
		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			var eventHandler = PropertyChanged;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}
	}
}