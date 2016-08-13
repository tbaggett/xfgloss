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
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics.Drawable;
using Android.Util;

namespace XFGloss.Droid.Utils
{
	/// <summary>
	/// Helper class used to convert or retrieve various <see cref="T:Android.Graphics.Color"/> and dimension related 
	/// values.
	/// </summary>
	public static class ThemeUtil
	{
		static TypedValue typedVal;

		/// <summary>
		/// Helper method used to convert from density-independent pixels to screen pixels using the provided context's
		/// display metrics setting.
		/// </summary>
		/// <returns>The screen pixel value for the provided density-independent pixel value</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the conversion</param>
		/// <param name="dp">The density-independent pixel value to be converted to an equivalent screen pixel value</param>
		public static int DpToPx(Context context, int dp)
		{
			return (int)(TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, context.Resources.DisplayMetrics) + 0.5f);
		}

		/// <summary>
		/// Helper method used to convert from scale-independent pixels to screen pixels using the provided context's
		/// display metrics setting.
		/// </summary>
		/// <returns>The screen pixel value for the provided scale-independent pixel value</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the conversion</param>
		/// <param name="sp">The scale-independent pixel value to be converted to an equivalent screen pixel value</param>
		public static int SpToPx(Context context, int sp)
		{
			return (int)(TypedValue.ApplyDimension(ComplexUnitType.Sp, sp, context.Resources.DisplayMetrics) + 0.5f);
		}

		/// <summary>
		/// Converts a 32-bit integer color value to an <see cref="T:Android.Graphics.Color"/> instance
		/// </summary>
		/// <returns>The converted <see cref="T:Android.Graphics.Color"/> value</returns>
		/// <param name="colorValue">The 32-bit integer color value to be converted</param>
		public static Color IntToColor(int colorValue)
		{
			return new Color(Color.GetRedComponent(colorValue), 
			                 Color.GetGreenComponent(colorValue), 
			                 Color.GetBlueComponent(colorValue),
			                 Color.GetAlphaComponent(colorValue));
		}

		/// <summary>
		/// Retrieves a 32-bit integer color value from the specified resource ID
		/// </summary>
		/// <returns>The specified resource ID's color value if found, the specified default value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="resourceId">The resource ID to retrieve the value for</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int ColorFromResourceId(Context context, int resourceId, int defaultValue)
		{
			int result = defaultValue;

			if (typedVal == null)
			{
				typedVal = new TypedValue();
			}

			try
			{
				Android.Content.Res.Resources.Theme theme = context.Theme;
				if (theme != null && theme.ResolveAttribute(resourceId, typedVal, true))
				{
					if (typedVal.Type >= DataType.FirstInt && typedVal.Type <= DataType.LastInt)
					{
						result = typedVal.Data;
					}
					else if (typedVal.Type == DataType.String)
					{
						result = ContextCompat.GetColor(context, typedVal.ResourceId);
					}
				}
			}
			#pragma warning disable RECS0022 // A catch clause that catches System.Exception and has an empty body
			catch (Exception) { }
			#pragma warning restore RECS0022 // A catch clause that catches System.Exception and has an empty body

			return result;
		}

		/// <summary>
		/// Retrieves the window background 32-bit integer color value
		/// </summary>
		/// <returns>The window background color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int WindowBackground(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.WindowBackground, defaultValue);
		}

		/// <summary>
		/// Retrieves the primary text color 32-bit integer color value
		/// </summary>
		/// <returns>The primary text color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int TextColorPrimary(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.TextColorPrimary, defaultValue);
		}

		/// <summary>
		/// Retrieves the secondary text color 32-bit integer color value
		/// </summary>
		/// <returns>The secondary text color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int TextColorSecondary(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.TextColorSecondary, defaultValue);
		}

		/// <summary>
		/// Retrieves the primary color 32-bit integer color value
		/// </summary>
		/// <returns>The primary color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int ColorPrimary(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.ColorPrimary, defaultValue);
		}

		/// <summary>
		/// Retrieves the primary dark 32-bit integer color value
		/// </summary>
		/// <returns>The primary dark color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int ColorPrimaryDark(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.ColorPrimaryDark, defaultValue);
		}

		/// <summary>
		/// Retrieves the accent color 32-bit integer color value
		/// </summary>
		/// <returns>The accent color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int ColorAccent(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.ColorAccent, defaultValue);
		}

		/// <summary>
		/// Retrieves the normal state control color 32-bit integer color value
		/// </summary>
		/// <returns>The normal state control color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int ColorControlNormal(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.ColorControlNormal, defaultValue);
		}

		/// <summary>
		/// Retrieves the activated state control color 32-bit integer color value
		/// </summary>
		/// <returns>The activated state control color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int ColorControlActivated(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.ColorControlActivated, defaultValue);
		}

		/// <summary>
		/// Retrieves the highlighted state control color 32-bit integer color value
		/// </summary>
		/// <returns>The highlighted state control color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int ColorControlHighlight(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.ColorControlHighlight, defaultValue);
		}

		/// <summary>
		/// Retrieves the normal state button color 32-bit integer color value
		/// </summary>
		/// <returns>The normal state button color if found in the specified Context's resource, the specified default 
		/// value if not found</returns>
		/// <param name="context">The <see cref="T:Android.Content.Context"/> context to be used for the retrieval</param>
		/// <param name="defaultValue">The default color value to return if the specified resource ID isn't found</param>
		public static int ColorButtonNormal(Context context, int defaultValue)
		{
			return ColorFromResourceId(context, Android.Resource.Attribute.ColorButtonNormal, defaultValue);
		}

		public static void SetLayerTint(LayerDrawable drawable, int index, Color color)
		{
			if (drawable?.NumberOfLayers > index)
			{
				var layer = drawable.GetDrawable(index);
				var wrapper = DrawableCompat.Wrap(layer);
				DrawableCompat.SetTint(wrapper, color);
			}
		}

		public static void SetLayerTintList(LayerDrawable drawable, int index, ColorStateList colorList)
		{
			if (drawable?.NumberOfLayers > index)
			{
				var layer = drawable.GetDrawable(index);
				var wrapper = DrawableCompat.Wrap(layer);
				DrawableCompat.SetTintList(wrapper, colorList);
			}
		}

		// Helper values for use with the AppCompat library
		public static readonly int DefaultColorControlThumb = new Color(175, 175, 175, 255).ToArgb();
		public static readonly int DefaultColorControlThumbActivated = new Color(252, 69, 125, 255).ToArgb();

		public static readonly int DefaultColorControlTrack = new Color(175, 175, 175, 77).ToArgb();
		public static readonly int DefaultColorControlTrackActivated = new Color(252, 69, 125, 77).ToArgb();
	}
}