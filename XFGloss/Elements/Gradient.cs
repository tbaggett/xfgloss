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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()

namespace XFGloss
{
	[ContentProperty("ChildStep")]
	/// <summary>
	/// The <see cref="T:XFGloss.Gradient"/> class manages the property values needed to render gradient fills. It also implements the generic 
	/// portion of the updating and rendering logic used to respond to property changes and instruct the 
	/// platform-specific renderers when changes need to be applied.
	/// </summary>
	public class Gradient : XFGlossElement<IGradientRenderer>, IEquatable<Gradient>, IDisposable
	{
		#region Consts

		// 
		/// <summary>
		/// Sentinel value so we can detect if angle should be changed to default angle if one isn't explicitly assigned.
		/// </summary>
		public const int UndefinedRotation = int.MinValue;
		/// <summary>
		/// Rotation value used if one isn't explicitly assigned. Gradients will be from top to bottom when this is used.
		/// </summary>
		public const int DefaultRotation = RotationTopToBottom;
		/// <summary>
		/// Specifies the gradient colors should be filled from left to right
		/// </summary>
		public const int RotationLeftToRight = 90;
		/// <summary>
		/// Specifies the gradient colors should be filled from right to left
		/// </summary>
		public const int RotationRightToLeft = 270;
		/// <summary>
		/// Specifies the gradient colors should be filled from top to bottom
		/// </summary>
		public const int RotationTopToBottom = 180;
		/// <summary>
		/// Specifies the gradient colors should be filled from bottom to top
		/// </summary>
		public const int RotationBottomToTop = 0;

		#endregion

		#region Instance access

		/// <summary>
		/// Default constructor. Initializes the steps collection to a new empty collection and the rotation angle to
		/// the default angle, which will fill from top to bottom. Note that the gradient won't be rendered when using
		/// this constructor without adding at least two GradientStep instances to the Steps collection.
		/// </summary>
		public Gradient()
		{
			Steps = new GradientStepCollection();
		}

		/// <summary>
		/// Copy constructor. Will copy the properties of another <see cref="T:XFGloss.Gradient"/> instance to the 
		/// newly-constructed one.  The Steps <see cref="T:XFGloss.GradientStepCollection"/> is deeply copied, i.e., 
		/// new <see cref="T:XFGloss.GradientStep"/> instances are constructed using the 
		/// <see cref="T:XFGloss.GradientStep"/> copy constructor, then assigned to a new GradientStepCollection 
		/// instance.
		/// </summary>
		/// <param name="other">Specifies the other Gradient instance whose properties will be copied to the new instance.</param>
		public Gradient(Gradient other)
		{
			Rotation = other.Rotation;
			Steps = new GradientStepCollection(other.Steps);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Gradient"/> class and assigns the specified rotation 
		/// value to the new instance's Rotation property.
		/// </summary>
		/// <param name="rotation">Rotation angle. An integer value between 0 and 359.</param>
		public Gradient(int rotation)
		{
			Rotation = rotation;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Gradient"/> class and creates a two step gradient 
		/// using the specified start and end Color values. The gradient's rotation angle is set to the DefaultRotation 
		/// const value, which results in a top to bottom vertical fill.
		/// </summary>
		/// <param name="startColor">Start color. Specifies the first color used in the two step gradient.</param>
		/// <param name="endColor">End color. Specifies the last color used in the two step gradient.</param>
		public Gradient(Color startColor, Color endColor)
		{
			Rotation = DefaultRotation;
			StartColor = startColor;
			EndColor = endColor;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Gradient"/> class and creates a two step gradient 
		/// using the specified start and end Color values and Rotation angle.
		/// </summary>
		/// <param name="rotation">Rotation. Specifies an integer number between 0 and 359.</param>
		/// <param name="startColor">Start color. Specifies the first color used in the two step gradient.</param>
		/// <param name="endColor">End color. Specifies the last color used in the two step gradient.</param>
		public Gradient(int rotation, Color startColor, Color endColor)
		{
			Rotation = rotation;
			StartColor = startColor;
			EndColor = endColor;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Gradient"/> class and creates a multiple step gradient
		/// using the provided rotation angle and <see cref="T:XFGloss.GradientStepCollection"/> steps parameters.
		/// </summary>
		/// <param name="rotation">Rotation. Specifies an integer number between 0 and 359.</param>
		/// <param name="steps">Steps. Specifies a <see cref="T:XFGloss.GradientStepCollection"/> collection of 
		/// <see cref="T:XFGloss.GradientStep"/>instances used to specify the color and position of each step in a
		/// multiple step gradient fill.</param>
		public Gradient(int rotation, GradientStepCollection steps)
		{
			Rotation = rotation;
			Steps = steps;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XFGloss.Gradient"/> class and creates a multiple step gradient
		/// using the default rotation angle and the provided <see cref="T:XFGloss.GradientStepCollection"/> steps 
		/// parameters.
		/// </summary>
		/// <param name="steps">Steps.Specifies a <see cref="T:XFGloss.GradientStepCollection"/> collection of 
		/// <see cref="T:XFGloss.GradientStep"/>instances used to specify the color and position of each step in a
		/// multiple step gradient fill.</param>
		public Gradient(GradientStepCollection steps)
		{
			Rotation = DefaultRotation;
			Steps = steps;
		}

		/// <summary>
		/// Releases all resources used by the <see cref="T:XFGloss.Gradient"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:XFGloss.Gradient"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="T:XFGloss.Gradient"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="T:XFGloss.Gradient"/> so the garbage
		/// collector can reclaim the memory that the <see cref="T:XFGloss.Gradient"/> was occupying.</remarks>
		public void Dispose()
		{
			Steps = null;
		}

		/// <summary>
		/// Determines whether the specified <see cref="XFGloss.Gradient"/> is equal to the current <see cref="T:XFGloss.Gradient"/>.
		/// </summary>
		/// <param name="other">The <see cref="XFGloss.Gradient"/> to compare with the current <see cref="T:XFGloss.Gradient"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="XFGloss.Gradient"/> is equal to the current
		/// <see cref="T:XFGloss.Gradient"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(Gradient other)
		{
			if (other == null)
			{
				return false;
			}

			if (!Rotation.Equals(other.Rotation) ||
				!Steps.Equals(other.Steps))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:XFGloss.Gradient"/>.
		/// </summary>
		/// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:XFGloss.Gradient"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:XFGloss.Gradient"/>;
		/// otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return Equals(obj as Gradient);
		}

		/// <summary>
		/// The rotation angle to orient the gradient fill in a desired direction. Must be an integer value between 0
		/// and 359. Values outside that range will cause an ArgumentOutOfRangeException exception to be thrown.
		/// </summary>
		int _rotation = UndefinedRotation;
		public int Rotation
		{
			get { return _rotation; }
			set
			{
				// Check for an invalid value (0 to 359 or UndefinedAngle) being assigned
				if (value != UndefinedRotation && (value < 0 || value > 359))
				{
					throw new ArgumentOutOfRangeException(nameof(Rotation),
														  value,
														  "Value should be between 0 and 359 or the UndefinedAngle const");
				}

				SetProperty(ref _rotation, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:XFGloss.Gradient"/> rotation angle is from left 
		/// to right. If this property is set to false, the rotation angle is changed to the 
		/// <see cref="T:XFGloss.Gradient.DefaultRotation"/> value, resulting in a top to bottom vertical gradient fill.
		/// </summary>
		/// <value><c>true</c> if the rotation angle is from left to right; otherwise, <c>false</c>.</value>
		public bool IsRotationLeftToRight
		{
			get { return Rotation == RotationLeftToRight; }
			set { SetRotation(value, RotationLeftToRight); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:XFGloss.Gradient"/> rotation angle is from right 
		/// to left. If this property is set to false, the rotation angle is changed to the 
		/// <see cref="T:XFGloss.Gradient.DefaultRotation"/> value, resulting in a top to bottom vertical gradient fill.
		/// </summary>
		/// <value><c>true</c> if the rotation angle is from right to left; otherwise, <c>false</c>.</value>
		public bool IsRotationRightToLeft
		{
			get { return Rotation == RotationRightToLeft; }
			set { SetRotation(value, RotationRightToLeft); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:XFGloss.Gradient"/> rotation angle is from top to 
		/// bottom. NOTE: Attempting to set this value to false will not appear to be applied since the 
		/// <see cref="T:XFGloss.Gradient.DefaultRotation"/> value also specifies a top to bottom rotation angle.
		/// </summary>
		/// <value><c>true</c> if the rotation angle is from top to bottom; otherwise, <c>false</c>.</value>
		public bool IsRotationTopToBottom
		{
			get { return Rotation == RotationTopToBottom; }
			set { SetRotation(value, RotationTopToBottom); }
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:XFGloss.Gradient"/> rotation angle is from bottom 
		/// to top. If this property is set to false, the rotation angle is changed to the 
		/// <see cref="T:XFGloss.Gradient.DefaultRotation"/> value, resulting in a top to bottom vertical gradient fill.
		/// </summary>
		/// <value><c>true</c> if is rotation bottom to top; otherwise, <c>false</c>.</value>
		public bool IsRotationBottomToTop
		{
			get { return Rotation == RotationBottomToTop; }
			set { SetRotation(value, RotationBottomToTop); }
		}

		/// <summary>
		/// Internal helper used by the IsRotation___To___ properties.
		/// </summary>
		/// <param name="shouldSet">If set to <c>true</c> should set.</param>
		/// <param name="rotation">Rotation.</param>
		void SetRotation(bool shouldSet, int rotation)
		{
			Rotation = shouldSet ? rotation : UndefinedRotation;
		}

		/// <summary>
		/// Gets or sets the start/first color of the gradient fill.
		/// </summary>
		/// <value>The start color.</value>
		// No property change notifications needed for StartColor. Changes to Steps will handle notifications.
		public Color StartColor
		{
			get
			{
				return (Steps?.Count > 0) ? Steps[0].StepColor : Color.Default;
			}
			set
			{
				// Confirm start color has been set
				if (Steps?.Count > 0)
				{
					if (value == Steps[0].StepColor)
					{
						return;
					}

					Steps[0].StepColor = value;
				}
				else
				{
					if (Steps == null)
					{
						Steps = new GradientStepCollection();
					}

					Steps.Add(new GradientStep(value, 0));
				}

				// Verify rotation has been set if EndColor value is changing and StartColor has been set
				if (Rotation == UndefinedRotation && Steps.IsValid)
				{
					Rotation = DefaultRotation;
				}
			}
		}

		/// <summary>
		/// Gets or sets the end/last color of the gradient fill.
		/// </summary>
		/// <value>The end color.</value>
		// No property change notifications needed for EndColor. Changes to Steps will handle notifications.
		public Color EndColor
		{
			get
			{
				return (Steps?.Count > 1) ? Steps[Steps.Count - 1].StepColor : Color.Default;
			}
			set
			{
				var lastStepIndex = Steps?.Count - 1;
				if (lastStepIndex != null && lastStepIndex > 0)
				{
					// Check for the same value being reassigned
					if (value == Steps[(int)lastStepIndex].StepColor)
					{
						return;
					}

					Steps[(int)lastStepIndex].StepColor = value;
				}
				else
				{
					// Instantiate the observable collection if it doesn't exist
					if (Steps == null)
					{
						Steps = new GradientStepCollection();
					}

					// Add placeholder for start color if an entry doesn't already exist
					if (Steps.Count == 0)
					{
						Steps.Add(new GradientStep());
					}

					Steps.Add(new GradientStep(value, 1));
				}

				// Verify rotation has been set if EndColor value is changing and StartColor has been set
				if (Rotation == UndefinedRotation && Steps.IsValid)
				{
					Rotation = DefaultRotation;
				}
			}
		}

		/// <summary>
		/// Specifies the steps in the gradient fill. Each step is defined by a <see cref="T:XFGloss.GradientStep"/> 
		/// instance.
		/// </summary>
		GradientStepCollection _steps;
		public GradientStepCollection Steps
		{
			get { return _steps; }
			set
			{
				if (value == _steps)
				{
					return;
				}

				if (Steps != null)
				{
					Steps.CollectionChanged -= OnStepsCollectionChanged;
				}

				_steps = value;

				if (Steps != null)
				{
					Steps.CollectionChanged += OnStepsCollectionChanged;
				}
			}
		}

		/// <summary>
		/// Adds a <see cref="T:XFGloss.GradientStep"/> instance to the end of the Steps 
		/// <see cref="T:XFGloss.GradientStepCollection"/> collection.
		/// </summary>
		/// <param name="stepColor">Step color. Specifies a Xamarin.Forms.Color value.</param>
		/// <param name="stepPercentage">Step percentage. Specifies a double value between 0 and 1, with 0 indicating 
		/// the start of the fill and 1 indicating the end of the fill. The specified value must also be equal to or
		/// greater than the percentage specified in the previous step.</param>
		public void AddStep(Color stepColor, double stepPercentage)
		{
			ChildStep = new GradientStep(stepColor, stepPercentage);
		}

		/// <summary>
		/// Faux property used along with the [ChildProperty] attribute on the <see cref="Gradient"/> class to allow 
		/// GradientStep instances to be directly assigned in Xaml instead of having to wrap them in a 
		/// &lt;<see cref="GradientStepCollection"/>&gt; node.
		/// </summary>
		/// <value>The child step. A <see cref="GradientStep"/> instance.</value>
		// 
		public GradientStep ChildStep
		{
			set
			{
				if (Steps == null)
				{
					Steps = new GradientStepCollection();
				}

				Steps.Add(value);
			}
		}

		// Bounce any collection changed notifications up to anyone listening for changes to the Steps property
		void OnStepsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			NotifyPropertyChanged(nameof(Steps));
		}

		#endregion

		#region XFGlossElement abstract implementations

		// Follow this pattern in derived classes to handle checking property names
		static readonly HashSet<string> _propertyNames = new HashSet<string> { nameof(Rotation), nameof(Steps) };
		public override bool IsPropertyOf(string propertyName)
		{
			// StartColor and EndColor are computed properties that impact Steps, so they aren't checked for changes.
			return _propertyNames.Contains(propertyName);
		}

		// Helper called by parent/containing elements to create/update gradients.
		// Returns true if a gradient was created or updated, false otherwise.
		public override bool UpdateProperties(string elementName, IGradientRenderer renderer, string elementPropertyChangedName = null)
		{
			// Check for this being the first time application of a gradient or an update
			if (!renderer.IsUpdating(elementName))
			{
				// If this is the first time application, check for all the needed properties having values assigned
				if (elementPropertyChangedName == null || IsPropertyOf(elementPropertyChangedName))
				{
					if (Rotation != UndefinedRotation && (bool)(Steps?.IsValid))
					{
						// Steps has either been set directly by the user or we've populated it from the start and end 
						// colors.
						renderer.CreateNativeElement(elementName, this);
						return true;
					}
				}
			}
			else
			{
				// We are supposed to update an existing gradient here - Check each property

				if (elementPropertyChangedName == null)
				{
					bool result = false;

					// Update everything
					if (Rotation != UndefinedRotation)
					{
						renderer.UpdateRotation(elementName, Rotation);
						result = true;
					}

					if ((bool)(Steps?.IsValid))
					{
						renderer.UpdateSteps(elementName, Steps);
						result = true;
					}
					else
					{
						renderer.RemoveNativeElement(elementName);
					}

					return result;
				}
				else
				{
					switch (elementPropertyChangedName)
					{
						case nameof(Rotation):
							renderer.UpdateRotation(elementName, Rotation);
							return true;

						case nameof(Steps):
							if ((bool)(Steps?.IsValid))
							{
								renderer.UpdateSteps(elementName, Steps);
								return true;
							}
							break;
					}
				}
			}

			return false;
		}

		#endregion
	}


	/// <summary>
	/// Class used to store each step's properties for a <see cref="T:XFGloss.Gradient"/> fill.
	/// </summary>
	public class GradientStep : ObservableObject, IEquatable<GradientStep>
	{
		/// <summary>
		/// Color value for this <see cref="XFGloss.Gradient"/> step.
		/// </summary>
		Color _stepColor = Color.Default;
		public Color StepColor
		{
			get
			{
				return _stepColor;
			}
			set
			{
				SetProperty(ref _stepColor, value);
			}
		}

		// Gradient fill percentage to stop at for this step, between 0.0 and 1.0
		double _stepPercentage;
		public double StepPercentage
		{
			get
			{
				return _stepPercentage;
			}
			set
			{
				if (value < 0 || value > 1)
				{
					throw new ArgumentOutOfRangeException(nameof(StepPercentage), 
					                                      value, 
					                                      "Value should be between 0.0 and 1.0");
				}

				SetProperty(ref _stepPercentage, value);
			}
		}

		public GradientStep()
		{
		}

		public GradientStep(GradientStep other)
		{
			StepColor = other.StepColor;
			StepPercentage = other.StepPercentage;
		}

		public GradientStep(Color stepColor, double stepPercentage)
		{
			StepColor = stepColor;
			StepPercentage = stepPercentage;
		}

		public bool Equals(GradientStep other)
		{
			if (other == null ||
			    !other.StepColor.Equals(StepColor) ||
			    !other.StepPercentage.Equals(StepPercentage))
			{
				return false;
			}

			return true;
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return Equals(obj as GradientStep);
		}
	}

	// ObservableCollection-based collection to store a gradient's steps in
	public class GradientStepCollection : ObservableCollection<GradientStep>, IEquatable<GradientStepCollection>
	{
		public GradientStepCollection()
		{
		}

		public GradientStepCollection(IEnumerable<GradientStep> other)
		{
			foreach (var step in other)
			{
				Add(new GradientStep(step));
			}
		}

		public void AddRange(IEnumerable<GradientStep> collection)
		{
			foreach (var item in collection)
			{
				Add(item);
			}
		}

		public bool Equals(GradientStepCollection other)
		{
			if (other == null ||
			    other.Count != Count)
			{
				return false;
			}

			for (int index = 0; index < Count; index++)
			{
				if (!this[index].Equals(other[index]))
				{
					return false;
				}
			}

			return true;
		}

		// Verify the passed steps array is either null or contains at least two instances.
		public bool IsValid
		{
			get
			{
				GradientStep prevStep = null;
				foreach (var step in this)
				{
					// Step percentages should always be equal to or greater than their predecessor
					// and NOT be set to Color.Default
					if (step.StepColor == Color.Default ||
						(prevStep != null && step.StepPercentage < prevStep.StepPercentage))
					{
						return false;
					}

					prevStep = step;
				}

				// A valid collection will contain at least two steps
				return Count > 1;
			}
		}
	}

	// Helper class used to convert from an angle in degrees to start and end point X/Y values between 0.0 and 1.0,
	// currently needed for iOS platform renderer, may be needed for other XF platforms in the future.
	public class RotationToPositionsConverter
	{
		public Point StartPoint { get; private set; }
		public Point EndPoint { get; private set; }

		public RotationToPositionsConverter(int rotation)
		{
			// Values need to be between 0 and 1
			double startRadians = (360 - rotation) * Math.PI / 180;
			double startX = (Math.Sin(startRadians) / 2) + 0.5;
			double startY = (Math.Cos(startRadians) / 2) + 0.5;
			StartPoint = new Point(startX, startY);

			double endRadians = (180 - rotation) * Math.PI / 180;
			double endX = (Math.Sin(endRadians) / 2) + 0.5;
			double endY = (Math.Cos(endRadians) / 2) + 0.5;
			EndPoint = new Point(endX, endY);
		}
	}
}

#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()