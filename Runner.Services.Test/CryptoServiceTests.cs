using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using Runner.Services;

namespace Runner.Services.Test
{
    [TestFixture]
    public class CryptoServiceTests
    {
        [Test]
        public void CanEncrypt_NotEmptyString_Success()
        {
            string privateKey = "q2#r44C&m(%mytp0";
            string s1 = "";
            string sEnc = String.Empty;
            string s2 = string.Empty;
            sEnc = CryptoService.Encrypt(s1,privateKey);
            s2 = CryptoService.Decrypt(sEnc, privateKey);
            Debug.Write(s2);
            Assert.AreEqual(s1,s2);
        }
    }
}
