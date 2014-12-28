using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVDB.Model;

namespace TVDB.Test.Model
{
    [TestClass]
    public class MirrorTest
    {
        #region Constructor tests

        [TestMethod]
        public void BaseContructorTest()
        {
            Mirror target = new Mirror();

            Assert.AreEqual(0, target.Id);
            Assert.IsTrue(string.IsNullOrEmpty(target.Address));
            Assert.IsFalse(target.ContainsBannerFile);
            Assert.IsFalse(target.ContainsXmlFile);
            Assert.IsFalse(target.ContainsZipFile);
        }

        #endregion

        #region ConvertTypeMaskTests

        [TestMethod]
        public void ConvertTypeMaskTestSuccessfullValue0()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 0;
            method.Invoke(target, new object[] { typeMask });

            Assert.IsFalse(target.ContainsBannerFile);
            Assert.IsFalse(target.ContainsXmlFile);
            Assert.IsFalse(target.ContainsZipFile);
        }

        [TestMethod]
        public void ConvertTypeMaskTestSuccessfullValue1()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 1;
            method.Invoke(target, new object[] { typeMask });

            Assert.IsFalse(target.ContainsBannerFile);
            Assert.IsTrue(target.ContainsXmlFile);
            Assert.IsFalse(target.ContainsZipFile);
        }

        [TestMethod]
        public void ConvertTypeMaskTestSuccessfullValue2()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 2;
            method.Invoke(target, new object[] { typeMask });

            Assert.IsTrue(target.ContainsBannerFile);
            Assert.IsFalse(target.ContainsXmlFile);
            Assert.IsFalse(target.ContainsZipFile);
        }

        [TestMethod]
        public void ConvertTypeMaskTestSuccessfullValue4()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 4;
            method.Invoke(target, new object[] { typeMask });

            Assert.IsFalse(target.ContainsBannerFile);
            Assert.IsFalse(target.ContainsXmlFile);
            Assert.IsTrue(target.ContainsZipFile);
        }

        [TestMethod]
        public void ConvertTypeMaskTestSuccessfullValue3()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 3;
            method.Invoke(target, new object[] { typeMask });

            Assert.IsTrue(target.ContainsBannerFile);
            Assert.IsTrue(target.ContainsXmlFile);
            Assert.IsFalse(target.ContainsZipFile);
        }

        [TestMethod]
        public void ConvertTypeMaskTestSuccessfullValue5()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 5;
            method.Invoke(target, new object[] { typeMask });

            Assert.IsFalse(target.ContainsBannerFile);
            Assert.IsTrue(target.ContainsXmlFile);
            Assert.IsTrue(target.ContainsZipFile);
        }

        [TestMethod]
        public void ConvertTypeMaskTestSuccessfullValue6()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 6;
            method.Invoke(target, new object[] { typeMask });

            Assert.IsTrue(target.ContainsBannerFile);
            Assert.IsFalse(target.ContainsXmlFile);
            Assert.IsTrue(target.ContainsZipFile);
        }

        [TestMethod]
        public void ConvertTypeMaskTestSuccessfullValue7()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 7;
            method.Invoke(target, new object[] { typeMask });

            Assert.IsTrue(target.ContainsBannerFile);
            Assert.IsTrue(target.ContainsXmlFile);
            Assert.IsTrue(target.ContainsZipFile);
        }

        #endregion

        #region Deserialize test

        [TestMethod]
        public void DeserializeTestSuccessfull()
        {
            string xmlContent =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Mirrors><Mirror><id>1</id><mirrorpath>http://thetvdb.com</mirrorpath><typemask>7</typemask></Mirror></Mirrors>";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xmlContent);

            System.Xml.XmlNode dataNode = doc.ChildNodes[1];
            System.Xml.XmlNode mirrorNode = dataNode.ChildNodes[0];

            Mirror target = new Mirror();
            target.Deserialize(mirrorNode);

            int expectedID = 1;
            string expectedAddress = "http://thetvdb.com";

            Assert.AreEqual(expectedID, target.Id);
            Assert.AreEqual(expectedAddress, target.Address);
            Assert.IsTrue(target.ContainsBannerFile);
            Assert.IsTrue(target.ContainsXmlFile);
            Assert.IsTrue(target.ContainsZipFile);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeserializeTestSuccesfullNoNode()
        {
            Mirror target = new Mirror();
            target.Deserialize(null);
        }

        #endregion
    }
}
