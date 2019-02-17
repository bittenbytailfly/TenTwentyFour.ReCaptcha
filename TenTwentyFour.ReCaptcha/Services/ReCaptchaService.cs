using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Dynamic;
using System.Net.Http;
using TenTwentyFour.ReCaptcha.Interfaces;

namespace TenTwentyFour.ReCaptcha.Services
{
    public class ReCaptchaService : IReCaptchaService
    {
        private NameValueCollection ConfigSettings { get; set; }

        public ReCaptchaService(NameValueCollection configSettings)
        {
            this.ConfigSettings = configSettings;
        }

        public ReCaptchaService() : this(ConfigurationManager.AppSettings) { }

        public bool Validate(string response)
        {
            return Validate(this.ConfigSettings[ConfigurationKeys.Secret], response);
        }

        public bool Validate(string secret, string response)
        {
            var formData = new Dictionary<string, string>();
            formData.Add("secret", secret);
            formData.Add("response", response);

            var apiResponse = this.Post($"{this.ConfigSettings[ConfigurationKeys.ApiUrl]}/siteverify", formData);
            dynamic reCaptchaData = apiResponse.Content.ReadAsAsync<ExpandoObject>().Result;
            return reCaptchaData.success;
        }

        protected HttpResponseMessage Post(string requestUrl, Dictionary<string, string> postData)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.PostAsync(requestUrl, new FormUrlEncodedContent(postData)).Result;
                response.EnsureSuccessStatusCode();
                return response;
            }
        }
    }
}
