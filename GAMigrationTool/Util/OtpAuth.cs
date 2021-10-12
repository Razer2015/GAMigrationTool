using System;

namespace GoogleAuthenticator.Util
{
    public static class OtpAuth
    {
        public static string GetData(string uriString)
        {
            var queryString = new Uri(uriString).Query;
            var queryDictionary = System.Web.HttpUtility.ParseQueryString(queryString);
            return queryDictionary.Get("data");
        }

        public static string WriteData(string data)
        {
            var uriBuilder = new UriBuilder("otpauth-migration://offline");
            var queryDictionary = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);
            queryDictionary.Add("data", data);
            uriBuilder.Query = queryDictionary.ToString();
            return uriBuilder.Uri.ToString();
        }
    }
}
