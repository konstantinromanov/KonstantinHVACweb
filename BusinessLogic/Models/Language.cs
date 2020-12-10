using KonstantinHVACweb.BusinessLogic.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace KonstantinHVACweb.BusinessLogic.Models
{
    public class Language
    {
        public const string DefaultLanguageName = "English";
        public const string DefaultLanguageIsoCode = "en";
        public const string DefaultLanguageCulture = "en-GB";
        public const int DefaultLanguageId = 1;

        public static readonly List<Language> AllLanguages = new List<Language>
        {
            new Language
            {
                Name = "English",
                IsoCode = "en",
                Culture = "en-GB",
                Id = 1
            },
            new Language
            {
                Name = "Latvian",
                IsoCode = "lv",
                Culture = "lv-LV",
                Id = 2
            },
            new Language
            {
                Name = "Russian",
                IsoCode = "ru",
                Culture = "ru-RU",
                Id = 3
            }
        };

        public string Name { get; private set; } = DefaultLanguageName;
        public string IsoCode { get; private set; } = DefaultLanguageIsoCode;
        public string Culture { get; private set; } = DefaultLanguageCulture;
        public int Id { get; private set; } = DefaultLanguageId;

        public static Language GetLanguageByLanguageId(int languageId) =>
            AllLanguages.FirstOrDefault(x => x.Id == languageId) ?? new Language();

        public static Language GetLanguageByIsoCode(string isoCode) =>
            AllLanguages.FirstOrDefault(x => x.IsoCode.ToLower() == isoCode.MyToStringTrimLower()) ?? new Language();

        public static Language GetLanguageByCultureCode(string culture) =>
            AllLanguages.FirstOrDefault(x => x.Culture.ToLower() == culture.MyToStringTrimLower()) ?? new Language();
    }
}
