using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XFGloss
{
	public class GlossGradient : ObservableObject, IDisposable
	{
		public List<GlossGradientStep> Steps { get; set; }

		public const int UNDEFINED_ANGLE = int.MinValue;
		public const int DEFAULT_ANGLE = VERTICAL_ANGLE;

		const int HORIZONTAL_ANGLE = 90;
		const int REVERSE_HORIZONTAL_ANGLE = 270;

		const int VERTICAL_ANGLE = 180;
		const int REVERSE_VERTICAL_ANGLE = 360;

		public bool IsHorizontal
		{
			get
			{
				return Angle == HORIZONTAL_ANGLE;
			}
			set
			{
				if (IsHorizontal != value)
				{
					Angle = value ? HORIZONTAL_ANGLE : DEFAULT_ANGLE;
				}
			}
		}

		public bool IsReverseHorizontal
		{
			get
			{
				return Angle == REVERSE_HORIZONTAL_ANGLE;
			}
			set
			{
				if (IsReverseHorizontal != value)
				{
					Angle = value ? REVERSE_HORIZONTAL_ANGLE : DEFAULT_ANGLE;
				}
			}
		}

		public bool IsVertical
		{
			get
			{
				return Angle == VERTICAL_ANGLE;
			}
			set
			{
				// Vertical angle IS the default angle, so no real change happening here, but it will kick off an update
				if (IsVertical != value)
				{
					Angle = value ? VERTICAL_ANGLE : DEFAULT_ANGLE;
				}
			}
		}

		public bool IsReverseVertical
		{
			get
			{
				return Angle == REVERSE_VERTICAL_ANGLE;
			}
			set
			{
				if (IsReverseVertical != value)
				{
					Angle = value ? REVERSE_VERTICAL_ANGLE : DEFAULT_ANGLE;
				}
			}
		}

		int _angle = UNDEFINED_ANGLE;
		public int Angle
		{
			get
			{
				return _angle;
			}
			set
			{
				if (_angle != value)
				{
					SetProperty(ref _angle, value);

					var converter = new AngleToPositionsConverter(value);
					StartPoint = converter.StartPoint;
					EndPoint = converter.EndPoint;

					if (IsHorizontal)
					{
						NotifyPropertyChanged(nameof(IsHorizontal));
					}
					else if (IsReverseHorizontal)
					{
						NotifyPropertyChanged(nameof(IsReverseHorizontal));
					}
					else if (IsVertical)
					{
						NotifyPropertyChanged(nameof(IsVertical));
					}
					else if (IsReverseVertical)
					{
						NotifyPropertyChanged(nameof(IsReverseVertical));
					}

					NotifyPropertyChanged("");
				}
			}
		}

		public Color StartColor
		{
			get
			{
				return (Steps.Count > 0) ? Steps[0].StepColor : Color.Default;
			}
			set
			{
				if (Steps.Count > 0)
				{
					if (value != Steps[0].StepColor)
					{
						Steps[0].StepColor = value;
						NotifyPropertyChanged(nameof(Steps));
					}

					// Verify angle has been set
					if (Angle == UNDEFINED_ANGLE)
					{
						Angle = DEFAULT_ANGLE;
					}
				}
				else
				{
					Steps.Add(new GlossGradientStep(value, 0));
					NotifyPropertyChanged(nameof(Steps));
				}
			}
		}

		public Color EndColor
		{
			get
			{
				return (Steps.Count > 1) ? Steps[Steps.Count - 1].StepColor : Color.Default;
			}
			set
			{
				if (Steps.Count > 1)
				{
					if (value != Steps[Steps.Count - 1].StepColor)
					{
						Steps[Steps.Count - 1].StepColor = value;
						NotifyPropertyChanged(nameof(Steps));
					}
				}
				else
				{
					if (Steps.Count == 0)
					{
						Steps.Add(new GlossGradientStep(Color.White, 0));
					}
					Steps.Add(new GlossGradientStep(value, 1));
					NotifyPropertyChanged(nameof(Steps));
				}

				// Verify angle has been set
				if (Angle == UNDEFINED_ANGLE)
				{
					Angle = DEFAULT_ANGLE;
				}
			}
		}

		Point _startPoint;
		public Point StartPoint
		{
			get
			{
				return _startPoint;
			}
			set
			{
				if (value.X < 0 || value.X > 1 || value.Y < 0 || value.Y > 1)
				{
					throw new ArgumentOutOfRangeException("StartPoint", value, "StartPoint X and Y values should be between 0.0 and 1.0");
				}

				SetProperty(ref _startPoint, value);
			}
		}

		Point _endPoint;
		public Point EndPoint
		{
			get
			{
				return _endPoint;
			}
			set
			{
				if (value.X < 0 || value.X > 1 || value.Y < 0 || value.Y > 1)
				{
					throw new ArgumentOutOfRangeException("EndPoint", value, "EndPoint X and Y values should be between 0.0 and 1.0");
				}

				SetProperty(ref _endPoint, value);
			}
		}

		// Default initialization. Properties will need to be explicitly set.
		public GlossGradient()
		{
			init();
		}

		public GlossGradient(GlossGradient other)
		{
			ShallowCopy(other);
		}

		public GlossGradient(bool isHorizontal)
			: this(isHorizontal ? HORIZONTAL_ANGLE : VERTICAL_ANGLE)
		{
		}

		// Convenience initializer to set gradient angle with GradientStep instances manually added
		public GlossGradient(int gradientAngle)
		{
			init();

			Angle = gradientAngle;
		}

		// Convenience initializer to create a simple two color horizontal or vertical gradient
		public GlossGradient(Color startColor, Color endColor, bool isHorizontal = false)
			: this(startColor, endColor, isHorizontal ? HORIZONTAL_ANGLE : VERTICAL_ANGLE)
		{
		}

		// Convenience initializer to create a simple two color gradient at the specified angle.
		// Gradient origin is assumed to be in the view center.
		public GlossGradient(Color startColor, Color endColor, int gradientAngle)
		{
			init();

			Steps.Add(new GlossGradientStep(startColor, 0));
			Steps.Add(new GlossGradientStep(endColor, 1));

			Angle = gradientAngle;
		}

		// Make a shallow copy of another instance. NOTE: Steps are shared across instances!
		public void ShallowCopy(GlossGradient other)
		{
			_angle = other.Angle;
			_startPoint = other.StartPoint;
			_endPoint = other.EndPoint;
			this.Steps = other.Steps;
		}

		// Convenience method to add gradient step to gradient without caller having to instantiate a 
		// GradientStep instance
		public void AddStep(Color stepColor, double stepPercentage)
		{
			// Compare StepPercentage to previous if one exists
			if (Steps.Count > 0 && stepPercentage < Steps[Steps.Count - 1].StepPercentage)
			{
				throw new ArgumentOutOfRangeException("StepPercentage", stepPercentage, "The completion percentage of " +
													  "the step being added should be greater than the previous step's " +
													  "completion percentage.");
			}

			Steps.Add(new GlossGradientStep(stepColor, stepPercentage));
			NotifyPropertyChanged(nameof(Steps));
		}

		void init()
		{
			Steps = new List<GlossGradientStep>();

			Angle = DEFAULT_ANGLE;
		}

		public void Dispose()
		{
			Steps.Clear();
			Steps = null;
		}

		// Helper class used to convert from an angle in degrees to start and end point X/Y values between 0.0 and 1.0
		class AngleToPositionsConverter
		{
			public Point StartPoint { get; private set; }
			public Point EndPoint { get; private set; }

			public AngleToPositionsConverter(int angle)
			{
				// Values need to be between 0 and 1
				double startRadians = (360 - angle) * Math.PI / 180;
				double startX = (Math.Sin(startRadians) / 2) + 0.5;
				double startY = (Math.Cos(startRadians) / 2) + 0.5;
				StartPoint = new Point(startX, startY);

				double endRadians = (180 - angle) * Math.PI / 180;
				double endX = (Math.Sin(endRadians) / 2) + 0.5;
				double endY = (Math.Cos(endRadians) / 2) + 0.5;
				EndPoint = new Point(endX, endY);
			}
		}
	}

	public class GlossGradientStep : ObservableObject
	{
		// Color value for this gradient step
		Color _stepColor;
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
					throw new ArgumentOutOfRangeException("StepPercentage", value, "Value should be between 0.0 and 1.0");
				}

				SetProperty(ref _stepPercentage, value);
			}
		}

		public GlossGradientStep()
		{
		}

		public GlossGradientStep(Color stepColor, double stepPercentage)
		{
			StepColor = stepColor;
			StepPercentage = stepPercentage;
		}
	}
}