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
	// The Gradient class manages the property values needed to render gradient fills.
	// It also implements the generic portion of the updating and rendering logic used to respond to property changes 
	// and instruct the platform-specific renderers when changes need to be applied.
	[ContentProperty("ChildStep")]
	public class Gradient : XFGlossElement<IGradientRenderer>, IEquatable<Gradient>, IDisposable
	{
		#region Consts

		// Sentinel value so we can detect if angle should be changed to default angle if one isn't explicitly
		// assigned
		public const int UndefinedRotation = int.MinValue;

		public const int DefaultRotation = RotationTopToBottom;

		public const int RotationLeftToRight = 90;
		public const int RotationRightToLeft = 270;

		public const int RotationTopToBottom = 180;
		public const int RotationBottomToTop = 0;

		#endregion

		#region Instance access

		public Gradient()
		{
			Steps = new GradientStepCollection();
		}

		public Gradient(Gradient other)
		{
			Rotation = other.Rotation;
			Steps = new GradientStepCollection(other.Steps);
		}

		public Gradient(int rotation)
		{
			Rotation = rotation;
		}

		public Gradient(Color startColor, Color endColor)
		{
			Rotation = DefaultRotation;
			StartColor = startColor;
			EndColor = endColor;
		}

		public Gradient(int rotation, Color startColor, Color endColor)
		{
			Rotation = rotation;
			StartColor = startColor;
			EndColor = endColor;
		}

		public Gradient(int rotation, GradientStepCollection steps)
		{
			Rotation = rotation;
			Steps = steps;
		}

		public Gradient(GradientStepCollection steps)
		{
			Rotation = DefaultRotation;
			Steps = steps;
		}

		public void Dispose()
		{
			Steps = null;
		}

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

		public bool IsRotationLeftToRight
		{
			get { return Rotation == RotationLeftToRight; }
			set { SetRotation(value, RotationLeftToRight); }
		}

		public bool IsRotationRightToLeft
		{
			get { return Rotation == RotationRightToLeft; }
			set { SetRotation(value, RotationRightToLeft); }
		}

		public bool IsRotationTopToBottom
		{
			get { return Rotation == RotationTopToBottom; }
			set { SetRotation(value, RotationTopToBottom); }
		}

		public bool IsRotationBottomToTop
		{
			get { return Rotation == RotationBottomToTop; }
			set { SetRotation(value, RotationBottomToTop); }
		}

		void SetRotation(bool shouldSet, int rotation)
		{
			Rotation = shouldSet ? rotation : UndefinedRotation;
		}

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

		public void AddStep(Color stepColor, double stepPercentage)
		{
			ChildStep = new GradientStep(stepColor, stepPercentage);
		}

		// Faux property used to allow GradientStep instances to be directly assigned in Xaml instead of having
		// to wrap them in a <GradientStepCollection> node.
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

		// Follow this pattern in derived class to handle checking property names
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


	// Class used to store each step's properties for a gradient
	public class GradientStep : ObservableObject, IEquatable<GradientStep>
	{
		// Color value for this gradient step
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