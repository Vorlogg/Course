using Microsoft.VisualStudio.TestTools.UnitTesting;
using App3.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace App3.ViewModels.Tests
{
    [TestClass()]
    public class VigCipherTests
    {
        public string CryptoText = "бщцфаирщри, бл ячъбиуъ щбюэсяёш гфуаа!!!";
        public string DecryptoText = "поздравляю, ты получил исходный текст!!!";
        public string Key = "скорпион";

        [TestMethod()]
        public void EncryptTest()
        {
            var ciprner = new VigCipher();
            string rez = ciprner.Encrypt(DecryptoText, Key);
            Assert.AreEqual(CryptoText,rez);
        }

        [TestMethod()]
        public void DecryptTest()
        {
            var ciprner = new VigCipher();
            string rez = ciprner.Decrypt(CryptoText, Key);
            Assert.AreEqual(DecryptoText, rez);
        }
    }
}