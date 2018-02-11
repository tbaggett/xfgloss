using Xamarin.Forms;

namespace XFGlossSample.Utils
{
    public static class XFGlossDevices
    {
        public static T OnPlatform<T>(T iOS, T android)
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return iOS;
            }
            else if  (Device.RuntimePlatform == Device.Android)
            {
                return android;
            }

            return default(T);
        }
    }
}
