using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TopMostApp.Utils
{
    internal class LangHelper
    {

        public static string CurrentCulture { get; set; }

        public static CultureInfo CurrentCultureInfo { get; set; }

        public static void SelectCulture(string culture)
        {
            CurrentCultureInfo ??= CultureInfo.CurrentUICulture;

            #region Validation

            //If none selected, fallback to english.
            if (string.IsNullOrEmpty(culture))
                culture = "en-US";

            if (culture.Equals("auto") || culture.Length < 2)
                culture = CurrentCultureInfo.Name;//zh-CN

            #endregion

            //Copy all MergedDictionarys into a auxiliar list.
            var dictionaryList = Application.Current.Resources.MergedDictionaries.ToList();

            #region Selected Culture

            //Search for the specified culture.
            var requestedCulture = $"/Resources/Lang/{culture}.xaml";
            var requestedResource = dictionaryList.FirstOrDefault(d => d.Source?.OriginalString == requestedCulture);

            #endregion

            #region Generic Branch Fallback

            //Fallback to a more generic version of the language. Example: pt-BR to pt.
            while (requestedResource == null && !string.IsNullOrEmpty(culture))
            {
                Debug.Write(culture);
                culture = CultureInfo.GetCultureInfo(culture).Parent.Name;
                requestedCulture = $"/Resources/Lang/{culture}.xaml";
                requestedResource = dictionaryList.FirstOrDefault(d =>  d.Source?.OriginalString == requestedCulture );
            }

            #endregion

            #region English Fallback

            //If not present, fall back to english.
            if (requestedResource == null)
            {
                culture = "en-US";
                requestedCulture = "/Resources/Lang/en-US.xaml";
                requestedResource = dictionaryList.FirstOrDefault(d => d.Source?.OriginalString == requestedCulture);
            }

            #endregion

            //If we have the requested resource, remove it from the list and place at the end.
            //Then this language will be our current string table.
            Application.Current.Resources.MergedDictionaries.Remove(requestedResource);
            Application.Current.Resources.MergedDictionaries.Add(requestedResource);

            CurrentCulture = culture;

            //Inform the threads of the new culture.
            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CurrentCulture;

            #region English Fallback of the Current Language

            //Only non-English resources need a fallback, because the English resource is evergreen.
            if (culture.StartsWith("en"))
                return;

            var englishResource = dictionaryList.FirstOrDefault(d => d.Source?.OriginalString == "/Resources/Lang/en-US.xaml");

            if (englishResource != null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(englishResource);
                Application.Current.Resources.MergedDictionaries.Insert(Application.Current.Resources.MergedDictionaries.Count - 1, englishResource);
            }

            #endregion

            GC.Collect(0);

          
            
        }
    }
}
