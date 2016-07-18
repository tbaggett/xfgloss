using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace XFGloss.Trackers
{
	#region XFGlossCellTracker base class (derived from in platform projects)
	public class XFGlossCellTracker
	{
		static protected readonly List<XFGlossCellTracker> _propTrackers = new List<XFGlossCellTracker>();
	}

	public class XFGlossCellTracker<T> : XFGlossCellTracker where T : class
	{
		protected static void Apply(Cell cell, T nativeCell, Func<XFGlossCellTracker<T>> attachedCellFactory)
		{
			XFGlossCellTracker<T> attachedCell = null;

			// See if we're already tracking this cell
			foreach (var item in _propTrackers.ToArray())
			{
				Cell cellRef;
				if ((item as XFGlossCellTracker<T>)._cell.TryGetTarget(out cellRef))
				{
					if (cellRef == cell)
					{
						attachedCell = item as XFGlossCellTracker<T>;
						break;
					}
				}
				else
				{
					// Remove the dead entry from our list
					_propTrackers.Remove(item);
				}
			}

			// if not already tracking, set up new entry and monitor for property changes
			if (attachedCell == null)
			{
				attachedCell = attachedCellFactory();
				attachedCell._cell = new WeakReference<Cell>(cell);
				cell.PropertyChanged += attachedCell.CellPropertyChanged;
				_propTrackers.Add(attachedCell);
			}

			// Update the target native cell of the tracked xaml cell if needed
			T currentNativeCell;
			if (attachedCell._nativeCell == null ||
				!attachedCell._nativeCell.TryGetTarget(out currentNativeCell) ||
				currentNativeCell != nativeCell)
			{
				attachedCell._nativeCell = new WeakReference<T>(nativeCell);
			}

			// Force immediate update of properties
			attachedCell.Reapply();
		}

		WeakReference<Cell> _cell;
		WeakReference<T> _nativeCell;

		protected virtual void CellPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Nothing to do in the base class. Derived class should check for any properties changing that they support
			throw new NotSupportedException("Derived class should override this method implementation");
		}

		protected virtual void Reapply(string propertyName = null)
		{
			// Derived classes should declare local XF cell and native cell vars and call Reapply(out Cell cell, out T nativeCell),
			// then proceed to handle changes for all the properties they support
			throw new NotSupportedException("Derived class should override this method implementation");
		}

		protected bool Reapply(out Cell cell, out T nativeCell)
		{
			// Do a little tracker management here in the base class implementation. Derived classes are responsible for
			// checking and reacting to changes in whatever bindable properties their implementation supports.

			// This method returns false if derived classes shouldn't continue applying changes
			cell = null;
			nativeCell = null;

			if (!_cell.TryGetTarget(out cell))
			{
				// Remove dead entry from our list
				_propTrackers.Remove(this);
				return false;
			}

			if (!_nativeCell.TryGetTarget(out nativeCell))
			{
				// If a property change fires for a dead native cell (but the xaml cell isn't dead), ignore it
				return false;
			}

			return true;
		}
	}
	#endregion
}