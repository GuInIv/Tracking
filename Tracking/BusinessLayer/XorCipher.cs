namespace Tracking.BusinessLayer
{
    public static class XorCipher
    {
        private static string Cipher(string text, string secretKey)
        {
            var currentKey = GetRepeatKey(secretKey, text.Length);
            var res = string.Empty;
            for (var i = 0; i < text.Length; i++)
            {
                res += ((char)(text[i] ^ currentKey[i])).ToString();
            }

            return res;
        }

        private static string GetRepeatKey(string s, int n)
        {
            var r = s;
            while (r.Length < n)
            {
                r += r;
            }

            return r.Substring(0, n);
        }

        public static string Encrypt(string plainText, string password) => Cipher(plainText, password);

        public static string Decrypt(string encryptedText, string password) => Cipher(encryptedText, password);
    }
}
