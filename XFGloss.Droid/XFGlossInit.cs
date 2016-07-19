using System;
using Android.Content;
using Android.OS;

namespace XFGloss.Droid
{
	public class Library
	{
		public static void Init(Context context, Bundle bundle)
		{
			// We don't currently need access to the context or bundle, but we probably will in the future. Requiring
			// them to be passed in now so there won't be a breaking change required when they're needed.
		}
	}
}