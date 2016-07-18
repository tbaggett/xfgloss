using System;

namespace XFGloss.Models
{
	// Supported on iOS only currently

	// DetailDisclosureButton and DetailButton are disabled until we can access the detail button 
	// tapped method in the table view source for both the ListView (currently not possible) and 
	// TableView (currently possible) classes.

	public enum CellAccessoryType
	{
		None,
		DisclosureIndicator,
		//DetailDisclosureButton,
		Checkmark,
		//DetailButton,
		EditIndicator
	}
}