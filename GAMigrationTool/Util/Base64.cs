using System;

namespace GoogleAuthenticator.Util
{
    public static class Base64
    {
        public static byte[] Decode(string data)
        {
            return Convert.FromBase64String(data);
        }

        public static string Encode(byte[] data)
        {
            return Convert.ToBase64String(data);
        }
    }
}
