using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GoogleAuthenticator.Util
{
    public static class Base32String
    {
        private static readonly Dictionary<char, int> CHAR_MAP = null;
        private static readonly char[] DIGITS = null;
        private static readonly int MASK = 0;
        private static readonly int SHIFT;

        static Base32String()
        {
            char[] digits = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567".ToCharArray();
            DIGITS = digits;
            MASK = digits.Length - 1;
            SHIFT = BitOperations.TrailingZeroCount(digits.Length);
            CHAR_MAP = new Dictionary<char, int>();
            for (int i = 0; true; ++i)
            {
                if (i >= DIGITS.Length)
                {
                    return;
                }

                CHAR_MAP.Add(DIGITS[i], i);
            }
        }

        public static byte[] Decode(string encodedKey)
        {
            var trimmedKey = encodedKey.Trim().Replace("-", "").Replace(" ", "").ReplaceFirst("[=]*$", "").ToUpperInvariant();
            if (trimmedKey.Length == 0)
            {
                return Array.Empty<byte>();
            }

            byte[] buffer = new byte[trimmedKey.Length * SHIFT / 8];
            char[] keyChars = trimmedKey.ToCharArray();
            int i = 0;
            int i2 = 0;
            int i3 = 0;
            int i4 = 0;
            while (i < keyChars.Length)
            {
                char keyChar = keyChars[i];
                if (CHAR_MAP.ContainsKey(keyChar))
                {
                    int @char = CHAR_MAP.GetValueOrDefault(keyChar);
                    i2 = i2 << SHIFT | @char & MASK;
                    i3 += SHIFT;
                    if (i3 >= 8)
                    {
                        i3 += -8;
                        buffer[i4] = (byte)(i2 >> i3);
                        ++i4;
                    }

                    ++i;
                    continue;
                }

                throw new Exception("Illegal character: " + keyChar);
            }

            return buffer;
        }

        public static string Encode(byte[] key)
        {
            int length = key.Length;
            if (length == 0)
            {
                return "";
            }
            if (length < 268435456)
            {
                int shift = SHIFT;
                StringBuilder sb = new((length * 8 + shift - 1) / shift);
                int n = key[0];
                int shift2;
                int n5 = 0;
                for (int n2 = 1, n3 = 8; n3 > 0 || n2 < length; n3 = n5 - shift2)
                {
                    shift2 = SHIFT;
                    int n4 = n;
                
                    {
                        if ((n5 = n3) < shift2)
                        {
                            if (n2 < length)
                            {
                                n = (n << 8 | (key[n2] & 0xFF));
                                n5 = n3 + 8;
                                ++n2;
                                goto appendChar;
                            }
                            int n6 = shift2 - n3;
                            n4 = n << n6;
                            n5 = n3 + n6;
                        }
                        n = n4;
                    }
                appendChar:
                    sb.Append(DIGITS[n >> n5 - shift2 & MASK]);
                }
                return sb.ToString();
            }
            throw new ArgumentException();
        }

        public static string TryNormalizeEncoded(string key)
        {
            try
            {
                return Encode(Decode(key));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return key;
            }
        }
    }

    public static class StringExtensionMethods
    {
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
    }
}
