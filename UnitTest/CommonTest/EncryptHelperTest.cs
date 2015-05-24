using System;
using System.Text;
using Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommonTest
{
    [TestClass]
    public class EncryptHelperTest
    {
        [TestMethod]
        public void MD5HashTest()
        {
            //多次MD5结果要一致
            string input = Guid.NewGuid() + DateTime.Now.ToLongTimeString();
            Assert.AreEqual(EncryptHelper.MD5Hash(input), EncryptHelper.MD5Hash(input));
            Assert.AreNotEqual(EncryptHelper.MD5Hash(input), EncryptHelper.MD5Hash(input + "1"));
        }

        [TestMethod]
        public void EncryptAndDecryptTest()
        {
            //加密之后再解密测试
            for (int i = 0; i < 10000; i++)
            {
                string input = Guid.NewGuid().ToString();
                Assert.AreEqual(input, EncryptHelper.Decrypt(EncryptHelper.Encrypt(input)));
            }
        }
    }
}
