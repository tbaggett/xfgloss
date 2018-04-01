// /*
//  * Copyright (C) 2016 Ansuria Solutions LLC & Tommy Baggett: 
//  * http://github.com/tbaggett
//  * http://twitter.com/tbaggett
//  * http://tommyb.com
//  * http://ansuria.com
//  * 
//  * The MIT License (MIT) see GitHub For more information
//  *
//  * Unless required by applicable law or agreed to in writing, software
//  * distributed under the License is distributed on an "AS IS" BASIS,
//  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  * See the License for the specific language governing permissions and
//  * limitations under the License.
//  */
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace XFGloss
{
	/// <summary>
	/// Base marker class used to simplify the constraint in the IXFGlossRenderer's CreateNativeElement method 
	/// declaration.
	/// </summary>
	public abstract class XFGlossElement : ObservableObject { }

	/// <summary>
	/// Base class to be used when creating a new XFGlossElement-based class, currently used by the Gradient class
	/// </summary>
	public abstract class XFGlossElement<TXFGlossRenderer> : XFGlossElement where TXFGlossRenderer : class, IXFGlossRenderer
	{
		/// <summary>
		/// Indicates if the provided propertyName string is the name of a property of the 
		/// <see cref="T:XFGloss.XFGlossElement"/> class.
		/// </summary>
		/// <returns><c>true</c>, if property name is a property of the class, <c>false</c> otherwise.</returns>
		/// <param name="propertyName">Property name.</param>
		public abstract bool IsPropertyOf(string propertyName);

		/// <summary>
		/// Helper called by parent/containing elements to create/update gradients.
		/// </summary>
		/// <returns><c>true</c>, if the <see cref="T:XFGloss.XFGlossElement"/> was created or updated, 
		/// <c>false</c> otherwise.</returns>
		/// <param name="glossPropertyName">Name of the XFGloss bindable property being updated</param>
		/// <param name="renderer">Reference to class instance that implements the XFGloss 
		/// <see cref="T:XFGloss.IXFGlossRenderer"/> interface.</param>
		/// <param name="elementPropertyChangedName">Element property changed name.</param>
		public abstract bool UpdateProperties(string glossPropertyName, TXFGlossRenderer renderer, 
		                                      string elementPropertyChangedName = null);

		/// <summary>
		/// Attaches the renderer. Dependent <see cref="T:XFGloss.XFGlossRenderer"/> registration/deregistration 
		/// allows <see cref="T:XFGloss.XFGlossElement"/> instances to self-monitor their own properties and 
		/// automatically call on the renderers to update the platform-specific renderings when one of the 
		/// <see cref="T:XFGloss.XFGlossElement"/> instances' properties change. This approach minimizes boilerplate 
		/// code in the XFGloss renderers.
		/// </summary>
		/// <returns><c>true</c>, if renderer was attached, <c>false</c> otherwise.</returns>
		/// <param name="glossPropertyName">XFGloss bindable property name.</param>
		/// <param name="renderer">Renderer. XFGlossRenderer implementation instance.</param>
		public bool AttachRenderer(string glossPropertyName, TXFGlossRenderer renderer)
		{
			var entry = new XFGlossDependentRenderer(glossPropertyName, renderer);
			if (_dependentRenderers.FirstOrDefault(e => e.Equals(entry)) != null)
			{
				// Already registered!
				return false;
			}

			// Add property changed handler if this is the first registration
			if (_dependentRenderers.Count == 0)
			{
				PropertyChanged += OnGlossElementPropertyChanged;
			}

			_dependentRenderers.Add(entry);

			// Force immediate update
			UpdateProperties(glossPropertyName, renderer);

			return true;
		}

		/// <summary>
		/// Detaches the renderer. Dependent <see cref="T:XFGloss.XFGlossRenderer"/> registration/deregistration 
		/// allows <see cref="T:XFGloss.XFGlossElement"/> instances to self-monitor their own properties and 
		/// automatically call on the renderers to update the platform-specific renderings when one of the 
		/// <see cref="T:XFGloss.XFGlossElement"/> instances' properties change. This approach minimizes boilerplate 
		/// code in the XFGloss renderers.
		/// </summary>
		/// <returns><c>true</c>, if renderer was detached, <c>false</c> otherwise.</returns>
		/// <param name="renderer">Renderer.</param>
		public bool DetachRenderer(TXFGlossRenderer renderer)
		{
			bool result = false;

			// Clean out both dead and matching entries
			List<XFGlossDependentRenderer> toRemove = new List<XFGlossDependentRenderer>();

			foreach (var dependent in _dependentRenderers)
			{
				TXFGlossRenderer rendererEntry;
				if (dependent.RendererRef.TryGetTarget(out rendererEntry))
				{
					if (rendererEntry == renderer)
					{
						toRemove.Add(dependent);
						result = true;
					}
				}
				else
				{
					// Dead entry. Dependent renderer has been GC'd.
					toRemove.Add(dependent);
				}
			}

			foreach (var dependent in toRemove)
			{
				_dependentRenderers.Remove(dependent);
			}

			// Remove property changed handler if this was the last registration
			if (_dependentRenderers.Count == 0)
			{
				PropertyChanged -= OnGlossElementPropertyChanged;
			}

			return result;
		}

		List<XFGlossDependentRenderer> _dependentRenderers = new List<XFGlossDependentRenderer>();

		/// <summary>
		/// Event handler used to monitor PropertyChangedEvent notifications for the XFGlossElement instance
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="args">Arguments.</param>
		void OnGlossElementPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			// Clean out any GC'd entries
			List<XFGlossDependentRenderer> toRemove = new List<XFGlossDependentRenderer>();

			// Iterate our dependent renderer list and update their properties
			foreach (var dependent in _dependentRenderers)
			{
				TXFGlossRenderer renderer;
				if (dependent.RendererRef.TryGetTarget(out renderer))
				{
					UpdateProperties(dependent.GlossPropertyName, renderer, args.PropertyName);
				}
				else
				{
					// GC'd entry found. Add it to our removal list
					toRemove.Add(dependent);
				}
			}

			// Remove any GC'd entries found while iterating our dependent renderers
			foreach (var dependent in toRemove)
			{
				_dependentRenderers.Remove(dependent);
			}
		}

		/// <summary>
		/// Helper class used to track dependent XFGlossRenderers without creating a strong reference so they
		/// can be used to execute the needed platform-specific functionality called for when a 
		/// <see cref="T:XFGloss.XFGlossElement"/> instance property changes.
		/// </summary>
		class XFGlossDependentRenderer : IEquatable<XFGlossDependentRenderer>
		{
			/// <summary>
			/// Gets the name of the XFGloss bindable property.
			/// </summary>
			/// <value>The name of the gloss property.</value>
			public string GlossPropertyName { get; private set; }
			/// <summary>
			/// Gets a weak reference to the dependent <see cref="T:XFGloss.XFGlossRenderer"/> instance.
			/// </summary>
			/// <value>The renderer reference.</value>
			public WeakReference<TXFGlossRenderer> RendererRef { get; private set; }

			/// <summary>
			/// Initializes a new instance of the <see cref="T:XFGloss.XFGlossElement`1.XFGlossDependentRenderer"/> class.
			/// </summary>
			/// <param name="glossPropertyName">XFGloss bindable property name.</param>
			/// <param name="rendererRef">XFGlossRenderer reference.</param>
			public XFGlossDependentRenderer(string glossPropertyName, TXFGlossRenderer rendererRef)
			{
				GlossPropertyName = glossPropertyName;
				RendererRef = new WeakReference<TXFGlossRenderer>(rendererRef);
			}

			/// <summary>
			/// Determines whether the specified <see cref="T:XFGloss.XFGlossElement`1.XFGlossDependentRenderer"/>
			/// is equal to the current <see cref="T:XFGloss.XFGlossElement`1.XFGlossDependentRenderer"/>.
			/// </summary>
			/// <param name="other">The <see cref="T:XFGloss.XFGlossElement`1.TXFGlossRenderer.XFGlossDependentRenderer"/> 
			/// to compare with the current <see cref="T:XFGloss.XFGlossElement`1.XFGlossDependentRenderer"/>.</param>
			/// <returns><c>true</c> if the specified <see cref="T:XFGloss.XFGlossElement`1.XFGlossDependentRenderer"/> 
			/// is equal to the current <see cref="T:XFGloss.XFGlossElement`1.XFGlossDependentRenderer"/>; otherwise, 
			/// <c>false</c>.</returns>
			public bool Equals(XFGlossDependentRenderer other)
			{
				if (GlossPropertyName == other.GlossPropertyName)
				{
					TXFGlossRenderer renderer, otherRenderer;
					if (RendererRef.TryGetTarget(out renderer) &&
						other.RendererRef.TryGetTarget(out otherRenderer) &&
						renderer == otherRenderer)
					{
						return true;
					}
				}

				return false;
			}
		}
	}
}