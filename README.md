# XFGloss: Xamarin.Forms UI Enhancements
---
**XFGloss** adds new properties to Xamarin.Forms standard UI components on the Android and iOS platforms. The new properties can also be used with custom UI components that derive from standard XF UI components without modifying their source code.

XFGloss accomplishes its goals using a combination of:

 - Attached bindable properties for Xaml support
 - Static and per-instance getters/setters 
 - Replacement custom renderers for the standard Xamarin.Forms UI components being enhanced
 - Effects that can be easily added to custom UI components on a per-instance basis

replacement custom renderers for the 

_Note: PRs, especially to add support to other Xamarin.Forms-supported platforms, are welcomed!_

(TODO: Add pics here)

---
## New Properties

**AccessoryType (iOS only):** XFGlossCellAccessoryTypes enum value
_Added to cell classes (EntryCell, ImageCell, SwitchCell, TextCell, ViewCell)_

Allows specifying the accessory type to be shown on the right side of the cell. Possible values are _None_, _DisclosureIndicator_, _DetailDisclosureButton_, _Checkmark_, _DetailButton_ and _EditIndicator_.

**BackgroundColor:** Xamarin.Forms Color
_Added to cell classes (EntryCell, ImageCell, SwitchCell, TextCell, ViewCell)_

Allows a color value to be specified as a cell&#39;s background color. Possible values are either named colors or numeric color values.

**TintColor:** Xamarin.Forms Color
_Added to all cell classes&#39; accessory views (iOS only), Switch, SwitchCell_

Allows a color value to be specified as the Switch control&#39;s track color when it is in the &quot;off&quot; position for the Switch and SwitchCell classes, and for the accessory view on iOS. Possible values are either named colors or numeric color values.

**OnTintColor:** Xamarin.Forms Color
_Added to Switch, SwitchCell_

Allows a color value to be specified as the Switch control&#39;s track color when it is in the &quot;on&quot; position for the Switch and SwitchCell classes. Possible values are either named colors or numeric color values.

**ThumbTintColor:** Xamarin.Forms Color
_Added to Switch, SwitchCell_

Allows a color value to be specified as the Switch control&#39;s thumb color when it is in the &quot;off&quot; position for the Switch and SwitchCell classes. Possible values are either named colors or numeric color values.

**ThumbOnTintColor:** Xamarin.Forms Color
_Added to Switch, SwitchCell_

Allows a color value to be specified as the Switch control&#39;s thumb color when it is in the &quot;on&quot; position for the Switch and SwitchCell classes. Possible values are either named colors or numeric color values.

---
## Using the New Properties with Xamarin.Forms UI Components

---
## Using the New Properties with Custom UI Components

---
#Known Issues

 - OnPlatform won't assign value to attached property when declared in Xaml if Xaml compilation is enabled ([verified issue by Xamarin](https://bugzilla.xamarin.com/show_bug.cgi?id=37371)). An example can be seen in the sample app's AppMenu.xaml file.

---
## About the Author
I am a Xamarin Certified Mobile Developer focused on Android, iOS and tvOS application development using Microsoft tools and C#, and Apple tools and Swift. I have 25+ years of experience and have successfully telecommuted on a variety of projects since 2008. Learn more on my [website](http://tommyb.com/) or [LinkedIn page](https://www.linkedin.com/in/tommybaggett).