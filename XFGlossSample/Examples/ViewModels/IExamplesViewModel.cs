using System;
using XFGloss;

namespace XFGlossSample.Examples.ViewModels
{
	public interface IExamplesViewModel
	{
		// The name of the property's type
		string PropertyType { get; }
		// The property's default value
		string PropertyDefault { get; }
		// One or more paragraphs of descriptive text
		string[] PropertyDescription { get; }
		// Comma separated list of classes that the property can be applied to
		string TargetClasses { get; }
	}
}