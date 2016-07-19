[XFGloss icon](images/XFGlossIcon.png)

# XFGloss: Xamarin.Forms UI Enhancements

**XFGloss** adds new bindable properties to Xamarin.Forms standard UI components on the Android and iOS platforms. It uses attached bindable properties and enhanced renderers for the supported Xamarin.Forms UI components.

This package was inspired by "[Lighting Up Native Platform Features In Xamarin Forms](http://www.wintellect.com/devcenter/krome/lighting-up-native-platform-features-in-xamarin-forms-part-1)." Thanks to the series' author, Keith Rome, for the inspiration!

PRs, especially to add support for other platforms supported by Xamarin.Forms, are welcomed!

## Using the New Properties with Xamarin.Forms UI Components



## Using the New Properties with Custom UI Components

## New Bindable Properties

**AccessoryType (iOS only):** XFGloss.CellGlossAccessoryType enum value  
_Added to EntryCell, ImageCell, TextCell and ViewCell_

Allows specifying the accessory type to be shown on the right side of the cell. Possible values are _None_, _DisclosureIndicator_, _Checkmark_ and _EditIndicator_. 

The Android platform doesn't offer accessory types as part of its standard UI, so this property isn't supported on Android devices.

The iOS _DetailButton_ and _DetailDisclosureButton_ accessory types aren't currently supported due to Xamarin.Forms not providing the external access needed to react to the user tapping the Details button.

**BackgroundColor:** Xamarin.Forms Color  
_Added to EntryCell, ImageCell, SwitchCell, TextCell and ViewCell_

Allows a color value to be specified as a cell's background color. Possible values are either named colors or numeric color values.

**TintColor:** Xamarin.Forms Color  
_Added to all cell classes' accessory views (iOS only), Switch and SwitchCell_

Allows a color value to be specified as the Switch control's track color when it is in the &quot;off&quot; position for the Switch and SwitchCell classes, and for the accessory view on iOS. Possible values are either named colors or numeric color values.

**OnTintColor:** Xamarin.Forms Color  
_Added to Switch and SwitchCell_

Allows a color value to be specified as the Switch control's track color when it is in the &quot;on&quot; position for the Switch and SwitchCell classes. Possible values are either named colors or numeric color values.

**ThumbTintColor:** Xamarin.Forms Color  
_Added to Switch and SwitchCell_

Allows a color value to be specified as the Switch control's thumb color when it is in the &quot;off&quot; position for the Switch and SwitchCell classes. Possible values are either named colors or numeric color values.

**ThumbOnTintColor:** Xamarin.Forms Color  
_Added to Switch and SwitchCell_

Allows a color value to be specified as the Switch control's thumb color when it is in the &quot;on&quot; position for the Switch and SwitchCell classes. Possible values are either named colors or numeric color values.

---
#Known Issues

 - The XF EntryCell renderer on iOS doesn't take the UITableViewCell.AccessoryView into account when positioning/sizing the text entry field. I plan to submit a PR that corrects this issue.

 - OnPlatform won't assign a value to an attached property when the binding is declared in Xaml **if Xaml compilation is enabled** ([verified issue by Xamarin](https://bugzilla.xamarin.com/show_bug.cgi?id=37371)). An example can be seen in the sample app's AppMenu.xaml file. The workaround for this issue is to disable Xaml compilation for the Xaml files where you want to specify a binding for one of XFGloss's attached properties.

---
## About the Author
I am a Xamarin Certified Mobile Developer focused on Android, iOS and tvOS application development using Microsoft tools and C#, and Apple tools and Swift. I have 25+ years of professional software development experience and have successfully telecommuted on a variety of projects since 2008. Learn more on my [website](http://tommyb.com/) or [LinkedIn page](https://www.linkedin.com/in/tommybaggett).