using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using QZ.Instrument.Utility;
using QZ.Foundation.Monad;

namespace QZ.Test.UnitTest
{
    public class UtilityTest
    {
        string cipher_key = "9BF70E91907CA25135B36B5328AF36C4";
        string cipher_key1 = "0BF71E02907CA25135B36B5328AF36C4";
        string raw_str = "EgMjifqNyn2HHuQ3QEaaTUiPFQhT8gdJIxlVWfbd0fGmNR/hwONbIYIveYH3a9et0hD9N5rIoX6LTqO2ZnIlrsj0gd8K+S3KtiTJRROZZySaVgtN7Ql2sgd6MHPO7eoGo3LKzHAyOCM8jeYoaBaCryJp7pslL/obUaLwV6FHtrIRUecKCfJ9ty5hre6lYQF4";
        [Fact]
        public void Test_Cipher_Aes()
        {
            var list = new List<string>();
            var list1 = list.Where(l => l.Equals("r")).ToList();
            string str23 = string.Format("{0}_{1}_{2}", 1, 2, 4);
            var mail = new MailBinding() { uid = "30533", u_name = "newqqqqq", email = "oe_luna@163.com" };
            var str = "{\"Org_Android_Version\":\"3.0.1\",\"cl_cookieId\":\"867922028675582\",\"cl_screenSize\":\"KIW - TL00H\",\"cl_token\":\"h4DFuGvB2pFCkKs1rqKbU1MUG5pwfMcu9G3QLrWuKSDmX9mpYNAQNGz2mps1nSAELM1sgpWd5k18KKDG3KzRmSnII1wDtU1G4arzpOceuUA\u003d\"}";
            //var json = "MLSRugdZezCbdpK8ZdjPQyu+5mBikdEVEaRvNsLcsJbiZKeSf6hGE0Bexwc/JBoPkFB8XAFLOpod7DvyEr/geVYeqv+5mnQh8KQ1yt+lS4Tj6rU3wJW6/TioCld1IRGM3A/uIrPpQB+WsX+SlSaOGwY1Qxtkfiz6GmwK2mQPf+OS6+K6i+HulFUV2FqIsj8Z1FQ +WjpkseEeurxS8/NHAkyiNivapFyOk+XsenR5yxpSzqcKmgzEYmjrqTHcB7asBDdk33U3dtvq7VmlUxmyv6xwxz5U2k2ueD2ok4Rz8YE=";
            var enstr = Cipher_Aes.DecryptFromBase64(raw_str, cipher_key1);
            var str1 = Cipher_Aes.EncryptToBase64(str, cipher_key);
            Assert.Equal("txttxt", str);
        }

        [Fact]
        public void Test_Maybe()
        {
            var a = new A<MailBinding>();
            a.Test();
        }
    }
    public class A<T> where T : new()
    {
        public void Test()
        {
            string str = "";
            var res = str.ToMaybe().Select<bool, T>(s => string.IsNullOrEmpty(str),
                                          (s, b) => {
                                              if (b)
                                                  return default(T);
                                              else
                                                  return new T();
                                          });
            Assert.Equal(res.HasValue, true);
        }
    }

    public class MailBinding
    {
        public string uid { get; set; }
        public string u_name { get; set; }
        public string email { get; set; }
    }
}
