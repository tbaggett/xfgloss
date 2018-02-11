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
using System.Linq;
using System.Reflection;
using Android.Content;
using Android.OS;
using Xamarin.Forms.Platform.Android;

namespace XFGloss.Droid
{
	/// <summary>
	/// Library class used to provide static initializer to be called from XFGloss Android client projects to insure
	/// the XFGloss library is included in the client project's build.
	/// </summary>
	public class Library
	{
		/// <summary>
		/// Specifies if the Android AppCompat library is being used
		/// </summary>
		/// <value><c>true</c> if using AppCompat; otherwise, <c>false</c>.</value>
		public static bool UsingAppCompat { get; private set; }

		/// <summary>
		/// Gets or sets a value indicating whether the regular XF SwitchCell should use the custom XFGloss
		/// SwitchCompatCellRenderer so the cell's Switch component will have the material design style. This property
		/// is set to true by default. Set it to false if you do NOT want the SwitchCompat new style look in older
		/// (pre-21) Android systems. This value is checked when the Init method is executed. It must be changed to
		/// false prior to calling the Init method if you don't want the SwitchCompatCellRenderer to be used.
		/// </summary>
		/// <value><c>true</c> if using the SwitchCompatCellRenderer; otherwise, <c>false</c>.</value>
		public static bool UsingSwitchCompatCell { get; set; } = true;

		/// <summary>
		/// Gets or sets a value indicating if the alpha value of colors should be forced when applied via the
		/// AppCompat library on API levels older than API 21. This feature is disabled by default as most devices
		/// running a pre-21 API are low powered and may have noticeably poor performance when translucent (partially
		/// opaque) color values are applied to on screen elements.
		/// </summary>
		/// <value><c>true</c> if AppCompat alpha values should be forced; otherwise, <c>false</c>.</value>
		public static bool UsingAppCompatAlpha { get; set; } = false;

		/// <summary>
		/// Initializer to be called from XFGloss Android client project to insure the XFGloss library is inclued in the
		/// client project's build.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="bundle">Bundle.</param>
		public static void Init(Context context, Bundle bundle)
		{
			// We need to check for the AppCompat library being used if we're running on a pre-Marshmallow API. The
			// AppCompat library requires some runtime reflection hacks due to some of the Xamarin.Forms framework 
			// classes that we need to access/extend being marked as internal or private.
			if (Android.OS.Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.M)
			{
				var appCompatActivity = ContextAsAppCompatActivity(context);
				if (appCompatActivity != null)
				{
					// App is using AppCompat library. Set our UsingAppCompat boolean property to true
					UsingAppCompat = true;

					VerifyLoadApplicationCalled(appCompatActivity);
					RegisterAppCompatRenderers();
				}
			}
		}

		internal const string appCompatWarning = "XFGloss: Android control tinting isn't supported prior to Android " +
												 "API 23 (Marshmallow) unless you're using the Android AppCompat " +
												 "library, which provides support back to API 16 (JellyBean).";

		static FormsAppCompatActivity ContextAsAppCompatActivity(Context context)
		{
			return context as FormsAppCompatActivity;
		}

		static void VerifyLoadApplicationCalled(FormsAppCompatActivity appCompatActivity)
		{
			try
			{
				// Check for the app's main activity having already called FormsAppCompatActivity::LoadApplication(...)
				Type appActivityType = appCompatActivity.GetType();
				while (appActivityType != null && appActivityType.Name != nameof(FormsAppCompatActivity))
				{
					appActivityType = appActivityType.BaseType;
				}
				if (appActivityType != null)
				{
					// The private _renderersAdded boolean field will be set to true if LoadApplication has been called.
					var fi = appActivityType.GetField("_renderersAdded", BindingFlags.NonPublic | BindingFlags.Instance);
					bool renderersAdded = (bool)fi.GetValue(appCompatActivity);
					if (!renderersAdded)
					{
						// LoadApplication hasn't been called. Throw an exception to force the dev to rearrange the
						// calling order so that our Init method isn't called until after LoadApplication has been called.
						throw new InvalidOperationException("XFGloss.Droid.Library.Init(...) should be called AFTER " +
															"your main activity has called LoadApplication(...)");
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("XFGloss initialization failed: " + e);
				// Rethrow if we intentionally threw the exception
				if (e is InvalidOperationException)
				{
					throw;
				}
			}
		}

		static void RegisterAppCompatRenderers()
		{
			// Substitute our AppCompat-based custom renderers for the default ones.
			var assembly = AppDomain.CurrentDomain.GetAssemblies().
				             	SingleOrDefault(a => a.FullName.StartsWith("Xamarin.Forms.Core", 
							                                               StringComparison.InvariantCultureIgnoreCase));
			var registrarType = assembly?.GetType("Xamarin.Forms.Registrar");
			var registrarMi = registrarType?.GetMethod("get_Registered", BindingFlags.NonPublic | BindingFlags.Static);
			var registrar = registrarMi?.Invoke(null, null);
			var registerMi = registrar?.GetType().GetMethod("Register");

			// We should have a MethodInfo instance pointing to the Registrar's Register method now. Throw an exception
			// if we don't.
			if (registerMi == null)
			{
				throw new InvalidOperationException("XFGloss.Droid.Library.Init(...) failed to register the needed " +
													"AppCompat version of the XFGloss custom renderers. Please report " +
				                                    "an issue at https://github.com/tbaggett/xfgloss.");
			}

			// Replace the regular XFGloss renderers with the AppCompat versions
			registerMi.Invoke(registrar, new object[] { typeof(Xamarin.Forms.Switch), 
														typeof(XFGloss.Droid.Renderers.XFGlossSwitchCompatRenderer) });

			// Only register our custom XFGlossSwitchCompatCellRenderer if the boolean property is set to true.
			if (UsingSwitchCompatCell)
			{
				registerMi.Invoke(registrar, new object[] { typeof(Xamarin.Forms.SwitchCell),
													typeof(XFGloss.Droid.Renderers.XFGlossSwitchCompatCellRenderer) });
			}
		}
	}
}