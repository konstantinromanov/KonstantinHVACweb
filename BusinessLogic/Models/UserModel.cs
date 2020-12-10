using Microsoft.AspNetCore.Http;

namespace KonstantinHVACweb.BusinessLogic.Models
{
    public class UserModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private HttpContext HttpContext => _httpContextAccessor.HttpContext;

        private Language _language;
        public Language Language
        {
            get
            {
                if (_language == null)
                    Language = Language.GetLanguageByIsoCode(Language.DefaultLanguageIsoCode);

                return _language;
            }
            set
            {
                _language = value;
            }
        }

        public string SessionId { get; private set; }

        public UserModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            SessionId = (string)HttpContext.Items["SessionId"];
        }
    }
}
