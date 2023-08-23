using System.Windows;

namespace ALauncher
{
    static class Locale
    {
        public static string GetLocaleString(string key)
            => Application.Current.Resources[key].ToString() ?? "***";
    }
}
