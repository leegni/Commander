using System.Text;
using System.Security.Cryptography;

namespace  Commander.Utilities
{
    public class DES
    {
        private static byte[] IV_192;
        private static byte[] KEY_192;
        private static string T_key = "b;=iS@@BlJQhkz?2Y^PyBdMy";
        private static string N_key = "01234567890123456789012345678901234567890123456789012345678901234567890123456789";

        public static void GetTripleDesConfiguration()
        {
            try
            {
                string s = T_key;
                if (s.Length > 24)
                    s = s.Substring(0, 24);
                else if (s.Length < 24)
                    s += "000000000000000000000000".Substring(0, 24 - s.Length);
                DES.KEY_192 = Encoding.ASCII.GetBytes(s);
                DES.IV_192 = new byte[24]
                {
          (byte) 55,
          (byte) 103,
          (byte) 246,
          (byte) 79,
          (byte) 36,
          (byte) 99,
          (byte) 167,
          (byte) 99,
          (byte) 42,
          (byte) 222,
          (byte) 62,
          (byte) 83,
          (byte) 184,
          (byte) 246,
          (byte) 209,
          (byte) 222,
          (byte) 145,
          (byte) 23,
          (byte) 58,
          (byte) 58,
          (byte) 173,
          (byte) 222,
          (byte) 121,
          (byte) 222
                };
            }
            catch
            {
                throw;
            }
        }

        public static string Trienc(string toEnc)
        {
            string str = string.Empty;
            try
            {
                DES.GetTripleDesConfiguration();
                if (toEnc != "")
                {
                    long unixTimestamp = (long)DateConverter.ConvertToUnixTimestamp(System.DateTime.Now);
                    toEnc += unixTimestamp.ToString();
                    TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                    MemoryStream memoryStream = new MemoryStream();
                    CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, cryptoServiceProvider.CreateEncryptor(DES.KEY_192, DES.IV_192), CryptoStreamMode.Write);
                    StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream);
                    streamWriter.Write(toEnc);
                    streamWriter.Flush();
                    cryptoStream.FlushFinalBlock();
                    memoryStream.Flush();
                    str = Convert.ToBase64String(memoryStream.GetBuffer(), 0, int.Parse(memoryStream.Length.ToString()));
                }
            }
            catch
            {
                throw;
            }
            return str;
        }

        public static string Trides(string toDec)
        {
             string empty1 = string.Empty;
            try
            {
                DES.GetTripleDesConfiguration();
                if (toDec != "")
                {
                    TripleDESCryptoServiceProvider cryptoServiceProvider = new TripleDESCryptoServiceProvider();
                    string end = new StreamReader((Stream)new CryptoStream((Stream)new MemoryStream(Convert.FromBase64String(toDec)), cryptoServiceProvider.CreateDecryptor(DES.KEY_192, DES.IV_192), CryptoStreamMode.Read)).ReadToEnd();
                    string empty2 = string.Empty;
                    if (end.Length >= 13)
                    {
                        string s = end.Substring(end.Length - 13, 13);
                        if (long.TryParse(s, out long _) && System.DateTime.TryParse(DateConverter.ConvertFromUnixTimestamp((double)long.Parse(s)).ToString("yyyy-MM-dd HH:mm:ss.ms"), out System.DateTime _))
                            return end.Substring(0, end.Length - 13);
                    }
                    return int.TryParse(end.Substring(end.Length - 10, 10), out int _) ? end.Substring(0, end.Length - 10) : throw new ArgumentException("There_is_an_error_in_encrypted_data");
                }
            }
            catch
            {
                throw;
            }
            return empty1;
        }

        public static string Encdes(string enc, int type)
        {
           string outstring = "";
            int num = 0;

            // 0 : enc , 1 : dec
            if (type == 0) num = 0; else if (type == 1) num = 2;

            for (int i2 = 0; i2 < enc.Length; i2++)
            {
                outstring = outstring + (char)(enc[i2] + N_key[i2] - N_key[i2 + 1] + num);
            }

            return outstring;
        }
    }
}
