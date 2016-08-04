/*
 * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
 * http://github.com/tbaggett
 * http://twitter.com/tbaggett
 * http://tommyb.com
 * http://ansuria.com
 * 
 * The MIT License (MIT) see GitHub For more information
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace XFGloss
{
	#region XFGlossCellRenderer base class

	// Empty base class used for list in the above locator and inherited from below in the cross-platform implementation
	public class XFGlossCellRenderer { }

	public abstract class XFGlossCellRenderer<TNativeView> : XFGlossCellRenderer, IDisposable where TNativeView : class 
	{
		// Static method intended for calling from platform-specific XF cell renderers
		public static void UpdateProperties(Cell cell, TNativeView nativeCell, Func<XFGlossCellRenderer<TNativeView>> createRendererFunc)
		{
			XFGlossCellRenderer<TNativeView> renderer = XFGlossCellRendererLocator<TNativeView>.GetRenderer(cell, nativeCell);
			// if not already managing, set up new renderer and monitor for property changes
			if (renderer == null)
			{
				renderer = createRendererFunc();
				renderer._cell = new WeakReference<Cell>(cell);

				// cell.PropertyChanging += cellTracker.CellPropertyChanging;
				WeakEvent.RegisterEvent<Cell, PropertyChangingEventArgs>(cell,
																		 nameof(cell.PropertyChanging),
																		 renderer.BindablePropertyChangingHandler);

				// cell.PropertyChanged += cellTracker.CellPropertyChanged;
				WeakEvent.RegisterEvent<Cell, PropertyChangedEventArgs>(cell,
																		nameof(cell.PropertyChanged),
																		renderer.BindablePropertyChangedHandler);

				XFGlossCellRendererLocator<TNativeView>.AddRenderer(renderer);

				// Attach this instance to any XFGlossElement instances
				var bkgrndGradient = (Gradient)cell.GetValue(CellGloss.BackgroundGradientProperty);
				bkgrndGradient?.AttachRenderer(CellGloss.BackgroundGradientProperty.PropertyName, renderer as IGradientRenderer);
			}

			// Update the target native cell of the tracked xaml cell if needed
			TNativeView currentNativeCell;
			if (renderer._nativeCell == null ||
				!renderer._nativeCell.TryGetTarget(out currentNativeCell) ||
				currentNativeCell != nativeCell)
			{
				if (renderer._nativeCell == null)
				{
					renderer._nativeCell = new WeakReference<TNativeView>(nativeCell);
				}
				else
				{
					renderer._nativeCell.SetTarget(nativeCell);
				}
			}

			// Force immediate update of properties
			renderer.UpdateProperties();
		}

		// Convenience method to retrieve XF and native cell instance references when only the property being changed is
		// available
		protected virtual void UpdateProperties(string propertyName = null)
		{
			Cell cell;
			TNativeView nativeCell;

			if (GetCells(out cell, out nativeCell))
			{
				UpdateProperties(cell, nativeCell, propertyName);
			}
		}

		// This method should be implemented by inheriting platform-specific classes and properties should be applied
		// to the native elements.
		protected abstract void UpdateProperties(Cell cell, TNativeView nativeCell, string propertyName);

		// Detach ourselves from any remaining XFGlossElement instances
		public virtual void Dispose()
		{
			if (this is IGradientRenderer)
			{
				var cell = GetCell();
				var bkgrndGradient = (Gradient)cell?.GetValue(CellGloss.BackgroundGradientProperty);
				bkgrndGradient?.DetachRenderer(this as IGradientRenderer);
			}
		}

		#region XF and native cell access

		WeakReference<Cell> _cell;
		public Cell GetCell()
		{
			Cell result;

			if (_cell == null || !_cell.TryGetTarget(out result))
			{
				// Remove dead entry from our locator - kept external here so it can be removed at any time the 
				// reference is found to be dead.
				if (_cell != null)
				{
					XFGlossCellRendererLocator<TNativeView>.RemoveRenderer(this);
				}

				return null;
			}

			return result;
		}

		WeakReference<TNativeView> _nativeCell;
		public TNativeView GetNativeCell()
		{
			TNativeView result;

			if (_nativeCell == null || !_nativeCell.TryGetTarget(out result))
			{
				return null;
			}

			return result;
		}

		// Returns true if both cells are valid
		protected bool GetCells(out Cell cell, out TNativeView nativeCell)
		{
			// Do a little locator management here in the base class implementation. Derived classes are responsible for
			// checking and reacting to changes in whatever bindable properties their implementation supports.

			// This method returns false if derived classes shouldn't continue applying changes
			cell = GetCell();
			nativeCell = GetNativeCell();

			// If a property change fires for a dead native cell (but the xaml cell isn't dead), ignore it
			return cell != null && nativeCell != null;
		}

		#endregion

		#region PropertyChanging/Changed Handlers

		// Private event handler needed to redirect call to method that returns a bool to indicate if overridden versions
		// should continue processing.
		void BindablePropertyChangingHandler(object sender, PropertyChangingEventArgs args)
		{
			ElementPropertyChanging(sender, args);
		}

		protected virtual bool ElementPropertyChanging(object sender, PropertyChangingEventArgs args)
		{
			if (args.PropertyName == CellGloss.BackgroundGradientProperty.PropertyName)
			{
				var bkgrndGradient = (Gradient)GetCell()?.GetValue(CellGloss.BackgroundGradientProperty);
				bkgrndGradient?.DetachRenderer(this as IGradientRenderer);
			}

			return true;
		}

		// Private event handler needed to redirect call to method that returns a bool to indicate if overridden versions
		// should continue processing.
		void BindablePropertyChangedHandler(object sender, PropertyChangedEventArgs args)
		{
			ElementPropertyChanged(sender, args);
		}

		protected virtual void ElementPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Check all the properties that all cell types support for changes
			if (args.PropertyName == CellGloss.BackgroundGradientProperty.PropertyName)
			{
				var bkgrndGradient = (Gradient)GetCell()?.GetValue(CellGloss.BackgroundGradientProperty);
				bkgrndGradient?.AttachRenderer(CellGloss.BackgroundGradientProperty.PropertyName, 
				                                        this as IGradientRenderer);
			}

			// Check all the properties that this implementation supports for changes
			if (args.PropertyName == CellGloss.TintColorProperty.PropertyName ||
				args.PropertyName == CellGloss.BackgroundGradientProperty.PropertyName ||
				args.PropertyName == CellGloss.BackgroundColorProperty.PropertyName)
			{
				UpdateProperties(args.PropertyName);
			}
		}

		#endregion
	}
	#endregion

	#region XFGlossCellRendererLocator

	public abstract class XFGlossCellRendererLocator
	{
		static protected readonly List<XFGlossCellRenderer> _renderers = new List<XFGlossCellRenderer>();
	}

	public class XFGlossCellRendererLocator<TNativeView> : XFGlossCellRendererLocator where TNativeView : class
	{
		public static XFGlossCellRenderer<TNativeView> GetRenderer(Cell cell, TNativeView nativeCell)
		{
			XFGlossCellRenderer<TNativeView> renderer = null;

			// See if we're already tracking this cell
			foreach (var item in _renderers.ToArray())
			{
				Cell cellRef = (item as XFGlossCellRenderer<TNativeView>).GetCell();
				if (cellRef != null)
				{
					if (cellRef == cell)
					{
						renderer = item as XFGlossCellRenderer<TNativeView>;
						break;
					}
				}
			}

			return renderer;
		}

		public static void AddRenderer(XFGlossCellRenderer<TNativeView> renderer)
		{
			_renderers.Add(renderer);
		}

		public static bool RemoveRenderer(XFGlossCellRenderer<TNativeView> renderer)
		{
			return _renderers.Remove(renderer);
		}
	}

	#endregion
}