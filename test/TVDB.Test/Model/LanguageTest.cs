using System;
using TVDB.Model;
using Xunit;

namespace TVDB.Test.Model
{
    public class LanguageTest
    {
        #region Constructor tests

        [Fact]
        public void DefaulConstructorTestSuccessfull()
        {
            Language target = new Language();

            Assert.Equal(0, target.Id);
            Assert.True(string.IsNullOrEmpty(target.Name));
            Assert.True(string.IsNullOrEmpty(target.Abbreviation));
        }

        #endregion

        #region Deserialize tests

        [Fact]
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

            Assert.Equal(expectedId, target.Id);
            Assert.Equal(expectedName, target.Name);
            Assert.Equal(expectedAbbreviation, target.Abbreviation);
        }

        [Fact]
        public void DeserializeTestFailedNoNode()
        {
            Language target = new Language();

			ArgumentNullException expected = new ArgumentNullException("node", "Provided node must not be null or empty");
			ArgumentNullException actual = Assert.Throws<ArgumentNullException>(() => target.Deserialize(null));

			Assert.Equal(expected.Message, actual.Message);
        }

        #endregion
    }
}
