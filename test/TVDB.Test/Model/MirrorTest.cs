using System;
using TVDB.Model;
using Xunit;

namespace TVDB.Test.Model
{
    public class MirrorTest
    {
        #region Constructor tests

        [Fact]
        public void BaseContructorTest()
        {
            Mirror target = new Mirror();

            Assert.Equal(0, target.Id);
            Assert.True(string.IsNullOrEmpty(target.Address));
            Assert.False(target.ContainsBannerFile);
            Assert.False(target.ContainsXmlFile);
            Assert.False(target.ContainsZipFile);
        }

        #endregion

        #region ConvertTypeMaskTests

        [Fact]
        public void ConvertTypeMaskTestSuccessfullValue0()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 0;
            method.Invoke(target, new object[] { typeMask });

            Assert.False(target.ContainsBannerFile);
            Assert.False(target.ContainsXmlFile);
            Assert.False(target.ContainsZipFile);
        }

        [Fact]
        public void ConvertTypeMaskTestSuccessfullValue1()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 1;
            method.Invoke(target, new object[] { typeMask });

            Assert.False(target.ContainsBannerFile);
            Assert.True(target.ContainsXmlFile);
            Assert.False(target.ContainsZipFile);
        }

        [Fact]
        public void ConvertTypeMaskTestSuccessfullValue2()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 2;
            method.Invoke(target, new object[] { typeMask });

            Assert.True(target.ContainsBannerFile);
            Assert.False(target.ContainsXmlFile);
            Assert.False(target.ContainsZipFile);
        }

        [Fact]
        public void ConvertTypeMaskTestSuccessfullValue4()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 4;
            method.Invoke(target, new object[] { typeMask });

            Assert.False(target.ContainsBannerFile);
            Assert.False(target.ContainsXmlFile);
            Assert.True(target.ContainsZipFile);
        }

        [Fact]
        public void ConvertTypeMaskTestSuccessfullValue3()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 3;
            method.Invoke(target, new object[] { typeMask });

            Assert.True(target.ContainsBannerFile);
            Assert.True(target.ContainsXmlFile);
            Assert.False(target.ContainsZipFile);
        }

        [Fact]
        public void ConvertTypeMaskTestSuccessfullValue5()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 5;
            method.Invoke(target, new object[] { typeMask });

            Assert.False(target.ContainsBannerFile);
            Assert.True(target.ContainsXmlFile);
            Assert.True(target.ContainsZipFile);
        }

        [Fact]
        public void ConvertTypeMaskTestSuccessfullValue6()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 6;
            method.Invoke(target, new object[] { typeMask });

            Assert.True(target.ContainsBannerFile);
            Assert.False(target.ContainsXmlFile);
            Assert.True(target.ContainsZipFile);
        }

        [Fact]
        public void ConvertTypeMaskTestSuccessfullValue7()
        {
            Mirror target = new Mirror();
            Type targetType = typeof(Mirror);

            var method = targetType.GetMethod("ConvertTypeMask", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            int typeMask = 7;
            method.Invoke(target, new object[] { typeMask });

            Assert.True(target.ContainsBannerFile);
            Assert.True(target.ContainsXmlFile);
            Assert.True(target.ContainsZipFile);
        }

        #endregion

        #region Deserialize test

        [Fact]
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

            Assert.Equal(expectedID, target.Id);
            Assert.Equal(expectedAddress, target.Address);
            Assert.True(target.ContainsBannerFile);
            Assert.True(target.ContainsXmlFile);
            Assert.True(target.ContainsZipFile);
        }

        [Fact]
        public void DeserializeTestSuccesfullNoNode()
        {
            Mirror target = new Mirror();

			ArgumentNullException expected = new ArgumentNullException("node", "Provided node must not be null.");
			ArgumentNullException actual = Assert.Throws<ArgumentNullException>(() => target.Deserialize(null));

			Assert.Equal(expected.Message, actual.Message);
        }

        #endregion
    }
}
