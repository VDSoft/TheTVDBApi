using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVDB.Model;

namespace TVDB.Test.Model
{
    [TestClass]
    public class LanguageTest
    {
        #region Constructor tests

        [TestMethod]
        public void DefaulConstructorTestSuccessfull()
        {
            Language target = new Language();

            Assert.AreEqual(0, target.Id);
            Assert.IsTrue(string.IsNullOrEmpty(target.Name));
            Assert.IsTrue(string.IsNullOrEmpty(target.Abbreviation));
        }

        #endregion

        #region Deserialize tests

        [TestMethod]
        public void DeserializeTestSuccessfull()
        {
            string xmlContent =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Languages><Language><name>Dansk</name><abbreviation>da</abbreviation><id>10</id></Language></Languages>";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xmlContent);

            System.Xml.XmlNode dataNode = doc.ChildNodes[1];
            System.Xml.XmlNode languageNode = dataNode.ChildNodes[0];

            Language target = new Language();
            target.Deserialize(languageNode);

            int expectedId = 10;
            string expectedName = "Dansk";
            string expectedAbbreviation = "da";

            Assert.AreEqual(expectedId, target.Id);
            Assert.AreEqual(expectedName, target.Name);
            Assert.AreEqual(expectedAbbreviation, target.Abbreviation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeserializeTestFailedNoNode()
        {
            Language target = new Language();
            target.Deserialize(null);
        }

        #endregion
    }
}
