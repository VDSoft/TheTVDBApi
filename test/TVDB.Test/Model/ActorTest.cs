using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVDB.Model;

namespace TVDB.Test.Model
{
    [TestClass]
    public class ActorTest
    {
        #region deserialize test

        [TestMethod]
        public void DeserializeTestSuccessfull()
        {
            string xmlContent = 
                "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Actors><Actor><id>79415</id><Image>actors/79415.jpg</Image><Name>Nathan Fillion</Name><Role>Richard Castle</Role><SortOrder>0</SortOrder></Actor></Actors>";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xmlContent);

            System.Xml.XmlNode actoresNode = doc.ChildNodes[1];
            System.Xml.XmlNode actorNode = actoresNode.ChildNodes[0];
            Actor target = new Actor();
            target.Deserialize(actorNode);

            Assert.AreEqual(79415, target.Id);
            Assert.AreEqual("actors/79415.jpg", target.ImagePath);
            Assert.AreEqual("Nathan Fillion", target.Name);
            Assert.AreEqual("Richard Castle", target.Role);
            Assert.AreEqual(0, target.SortOrder);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeserializeTestFailedNoNode()
        {
            Actor target = new Actor();
            target.Deserialize(null);
        }


        #endregion
    }
}
