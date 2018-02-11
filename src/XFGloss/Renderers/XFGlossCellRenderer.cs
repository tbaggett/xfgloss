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

	/// <summary>
	/// Empty base class used by XFGlossCellRendererLocator and inherited from below in the cross-platform implementation
	/// </summary>
	public abstract class XFGlossCellRenderer { }

	/// <summary>
	/// The base cross-platform XFGloss cell renderer class used by the platform-specific implementations. Used to 
	/// manage tracking of property changes in the associated <see cref="T:Xamarin.Forms.Cell"/>-derived classes and
	/// their platform-specific implementations.
	/// </summary>
	public abstract class XFGlossCellRenderer<TNativeView> : XFGlossCellRenderer, IDisposable where TNativeView : class 
	{
		// Static method intended for calling from platform-specific XF cell renderers
		/// <summary>
		/// Static method called from the platform-specific XFGlossCellRenderer classes to apply current or updated
		/// property values to the platform-specific feature implementations
		/// </summary>
		/// <param name="cell">The associated <see cref="T:Xamarin.Forms.Cell"/> instance</param>
		/// <param name="nativeCell">The platform-specific native cell component instance</param>
		/// <param name="createRendererFunc">A factory function used to create a new platform-specific 
		/// XFGlossCellRenderer-derived class instance if one doesn't currently exist.</param>
		public static void UpdateProperties(Cell cell, TNativeView nativeCell, Func<XFGlossCellRenderer<TNativeView>> createRendererFunc)
		{
			XFGlossCellRenderer<TNativeView> renderer = XFGlossCellRendererLocator<TNativeView>.GetRenderer(cell, nativeCell);
			// if not already managing, set up new renderer and monitor for property changes
			if (renderer == null)
			{
				renderer = createRendererFunc();
				renderer._cell = new WeakReference<Cell>(cell);

				// cell.PropertyChanging += cellTracker.CellPropertyChanging;
				WeakEvent.RegisterEvent<Cell, Xamarin.Forms.PropertyChangingEventArgs>(cell,
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

		/// <summary>
		/// Convenience method to retrieve XF and native cell instance references when only the property being changed 
		/// is available.
		/// </summary>
		/// <param name="propertyName">Property name.</param>
		protected virtual void UpdateProperties(string propertyName = null)
		{
			Cell cell;
			TNativeView nativeCell;

			if (GetCells(out cell, out nativeCell))
			{
				UpdateProperties(cell, nativeCell, propertyName);
			}
		}

		/// <summary>
		/// This method should be implemented by inheriting platform-specific classes and properties should be applied
		/// to the native elements.
		/// </summary>
		/// <param name="cell">The associated <see cref="T:Xamarin.Forms.Cell"/>-derived class instance</param>
		/// <param name="nativeCell">The platform-specific native cell control class instance</param>
		/// <param name="propertyName">The name of the property whose value has changed</param>
		protected abstract void UpdateProperties(Cell cell, TNativeView nativeCell, string propertyName);

		/// <summary>
		/// Releases all resources used by the <see cref="T:XFGloss.XFGlossCellRenderer"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:XFGloss.XFGlossCellRenderer"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="T:XFGloss.XFGlossCellRenderer"/> in an unusable state. After
		/// calling <see cref="Dispose"/>, you must release all references to the
		/// <see cref="T:XFGloss.XFGlossCellRenderer"/> so the garbage collector can reclaim the memory that the
		/// <see cref="T:XFGloss.XFGlossCellRenderer"/> was occupying.</remarks>
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
		/// <summary>
		/// Gets a strong reference to the <see cref="T:Xamarin.Forms.Cell"/>-based class instance if it is still
		/// available.
		/// </summary>
		/// <returns>A <see cref="T:Xamarin.Forms.Cell/> instance if the associated cell hasn't been garbage 
		/// collected."/></returns>
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
		/// <summary>
		/// Gets a strong reference to the platform-specific cell control-based class instance if it is still
		/// available.
		/// </summary>
		/// <returns>A platform-specific cell control's instance if the associated instance hasn't been garbage 
		/// collected."/></returns>
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
		/// <summary>
		/// Gets strong references to both the <see cref="T:Xamarin.Forms.Cell"/>-derived class instance and the
		/// platform-specific cell control instance if they haven't been garbage collected.
		/// </summary>
		/// <returns><c>true</c>, if both cell controls are still available, <c>false</c> otherwise.</returns>
		/// <param name="cell">A <see cref="T:Xamarin.Forms.Cell"/> field/property to assign the Cell to if it is
		/// still available</param>
		/// <param name="nativeCell">A platform-specific field/property to assign the native cell to if it is still
		/// available.</param>
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

		/// <summary>
		/// Private event handler needed to redirect call to a virtual method that can be overridden by deriving classes.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void BindablePropertyChangingHandler(object sender, Xamarin.Forms.PropertyChangingEventArgs args)
		{
			ElementPropertyChanging(sender, args);
		}

		/// <summary>
		/// A virtual method that is called whenever the PropertyChanging event has been fired for the associated
		/// <see cref="T:Xamarin.Forms.Cell"/> instance.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		protected virtual void ElementPropertyChanging(object sender, Xamarin.Forms.PropertyChangingEventArgs args)
		{
			if (args.PropertyName == CellGloss.BackgroundGradientProperty.PropertyName)
			{
				var bkgrndGradient = (Gradient)GetCell()?.GetValue(CellGloss.BackgroundGradientProperty);
				bkgrndGradient?.DetachRenderer(this as IGradientRenderer);
			}
		}

		/// <summary>
		/// Private event handler needed to redirect call to a virtual method that can be overridden by deriving classes.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void BindablePropertyChangedHandler(object sender, PropertyChangedEventArgs args)
		{
			ElementPropertyChanged(sender, args);
		}

		/// <summary>
		/// A virtual method that is called whenever the PropertyChanged event has been fired for the associated
		/// <see cref="T:Xamarin.Forms.Cell"/> instance.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
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

	/// <summary>
	/// Locator base class containing a single static list of <see cref="T:XFGloss.XFGlossCellRenderer"/> instances
	/// </summary>
	abstract class XFGlossCellRendererLocator
	{
		static protected readonly List<XFGlossCellRenderer> _renderers = new List<XFGlossCellRenderer>();
	}

	/// <summary>
	/// XFGlossCellRenderer locator class used to track and locate <see cref="T:XFGloss.XFGlossCellRenderer"/> 
	/// instances when needed.
	/// </summary>
	class XFGlossCellRendererLocator<TNativeView> : XFGlossCellRendererLocator where TNativeView : class
	{
		/// <summary>
		/// Locates a previously created <see cref="T:XFGloss.XFGlossCellRenderer"/> instance for the provided
		/// <see cref="T:Xamarin.Forms.Cell"/> and platform-specific native cell component instance if one exists.
		/// </summary>
		/// <returns>The <see cref="T:XFGloss.XFGlossCellRenderer"/> instance if found or null if not found.</returns>
		/// <param name="cell">The <see cref="T:Xamarin.Forms.Cell"/> instance</param>
		/// <param name="nativeCell">The platform-specific native cell component instance</param>
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

		/// <summary>
		/// Adds a <see cref="T:XFGloss.XFGlossCellRenderer"/> instance to the locator for tracking
		/// </summary>
		/// <param name="renderer">The <see cref="T:XFGloss.XFGlossCellRenderer"/> to be tracked.</param>
		public static void AddRenderer(XFGlossCellRenderer<TNativeView> renderer)
		{
			_renderers.Add(renderer);
		}

		/// <summary>
		/// Removes the <see cref="T:XFGloss.XFGlossCellRenderer"/> instance from the locator list if found.
		/// </summary>
		/// <returns><c>true</c>, if renderer was removed, <c>false</c> otherwise.</returns>
		/// <param name="renderer">The <see cref="T:XFGloss.XFGlossCellRenderer"/> that should no longer be tracked.
		/// </param>
		public static bool RemoveRenderer(XFGlossCellRenderer<TNativeView> renderer)
		{
			return _renderers.Remove(renderer);
		}
	}

	#endregion
}