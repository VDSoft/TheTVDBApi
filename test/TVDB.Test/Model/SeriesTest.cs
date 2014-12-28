using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVDB.Model;

namespace TVDB.Test.Model
{
    [TestClass]
    public class SeriesTest
    {
        #region Initialize tests

        [TestMethod]
        public void InitializeTestSuccessfullNoEpisodes()
        {
            Series target = new Series();
            Type targetType = typeof(Series);
            var method = targetType.GetMethod("Initialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(target, new object[0]);

            Assert.IsFalse(target.HasEpisodes);
        }

        [TestMethod]
        public void InitializeTestSuccessfullWithEpisodes()
        {
            Series target = new Series();
            target.Episodes.Add(new Episode());
            Type targetType = typeof(Series);
            var method = targetType.GetMethod("Initialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(target, new object[0]);

            Assert.IsTrue(target.HasEpisodes);
        }

        #endregion

        #region Deserialize tests

        [TestMethod]
        public void DeserializeTestSuccessfull()
        {
            string xmlContent =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Data><Series><id>83462</id><Actors>|Nathan Fillion|Stana Katic|Molly C. Quinn|Jon Huertas|Seamus Dever|Tamala Jones|Susan Sullivan|Ruben Santiago-Hudson|Penny Johnson|</Actors><Airs_DayOfWeek>Monday</Airs_DayOfWeek><Airs_Time>10:00 PM</Airs_Time><ContentRating>TV-PG</ContentRating><FirstAired>2009-03-09</FirstAired><Genre>|Comedy|Crime|Drama|</Genre><IMDB_ID>tt1219024</IMDB_ID><Language>en</Language><Network>ABC</Network><NetworkID></NetworkID><Overview>Rick Castle is one of the world's most successful crime authors. But when his rock star lifestyle isn't enough, this bad boy goes looking for new trouble and finds it working with smart, beautiful Detective Kate Beckett. Inspired by her professional record and intrigued by her buttoned-up personality, Castle's found the model for his bold new character whether she likes it or not. Now with the mayor's permission, Castle is helping solve crime with his own twist.</Overview><Rating>8.8</Rating><RatingCount>346</RatingCount><Runtime>60</Runtime><SeriesID>75394</SeriesID><SeriesName>Castle (2009)</SeriesName><Status>Continuing</Status><added>2008-10-17 15:05:50</added><addedBy>3071</addedBy><banner>graphical/83462-g10.jpg</banner><fanart>fanart/original/83462-33.jpg</fanart><lastupdated>1378896827</lastupdated><poster>posters/83462-6.jpg</poster><tms_wanted>1</tms_wanted><zap2it_id>EP01085588</zap2it_id></Series></Data>";
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xmlContent);

            System.Xml.XmlNode dataNode = doc.ChildNodes[1];
            System.Xml.XmlNode seriesNode = dataNode.ChildNodes[0];
            Series target = new Series();
            
            target.Deserialize(seriesNode);

            int expectedId = 83462;
            string expectedLanguage = "en";
            string expectedName = "Castle (2009)";
            string expectedBannerPath = "graphical/83462-g10.jpg";
            string expectedOverview = "Rick Castle is one of the world's most successful crime authors. But when his rock star lifestyle isn't enough, this bad boy goes looking for new trouble and finds it working with smart, beautiful Detective Kate Beckett. Inspired by her professional record and intrigued by her buttoned-up personality, Castle's found the model for his bold new character whether she likes it or not. Now with the mayor's permission, Castle is helping solve crime with his own twist.";
            DateTime expectedFirstAired = new DateTime(2009,3,9,0,0,0);
            string expectedIMDBId = "tt1219024";
            string expectedZap2itId = "EP01085588";
            string expectedActors = "Nathan Fillion, Stana Katic, Molly C. Quinn, Jon Huertas, Seamus Dever, Tamala Jones, Susan Sullivan, Ruben Santiago-Hudson, Penny Johnson";
            string expectedGener = "Comedy, Crime, Drama";
            string expectedDayAired = "Monday";
            string expectedTimeAired = "10:00 PM";
            string expectedContentRating = "TV-PG";
            string expectedNetwork = "ABC";
            int expectedNetworkID = 0;
            double expectedRating = 8.8;
            int expectedRatingCount = 346;
            double expectedRunTime = 60.0;
            int expectedSeriesID = 75394;
            string expectedStatus = "Continuing";
            DateTime expectedAddedDate = new DateTime(2008, 10, 17, 15, 05, 50);
            int expectedAddedByUserId = 3071;
            string expectedBanner = "graphical/83462-g10.jpg";
            string expectedFanArt = "fanart/original/83462-33.jpg";
            string expectedPoster = "posters/83462-6.jpg";
            bool expectedTMSWanted = true;

            Assert.AreEqual(expectedId, target.Id);
            Assert.AreEqual(expectedLanguage, target.Language);
            Assert.AreEqual(expectedName, target.Name);
            Assert.AreEqual(expectedBannerPath, target.Banner);
            Assert.AreEqual(expectedOverview, target.Overview);
            Assert.AreEqual(expectedFirstAired, target.FirstAired);
            Assert.AreEqual(expectedIMDBId, target.IMDBId);
            Assert.AreEqual(expectedZap2itId, target.Zap2ItId);
            Assert.IsFalse(target.HasEpisodes);
            Assert.AreEqual(expectedActors, target.Actorts);
            Assert.AreEqual(expectedGener, target.Genre);
            Assert.AreEqual(expectedDayAired, target.AirsDayOfWeel);
            Assert.AreEqual(expectedTimeAired, target.AirsTime);
            Assert.AreEqual(expectedContentRating, target.ContentRating);
            Assert.AreEqual(expectedNetwork, target.Network);
            Assert.AreEqual(expectedNetworkID, target.NetworkId);
            Assert.AreEqual(expectedRating, target.Rating);
            Assert.AreEqual(expectedRatingCount, target.RatingCount);
            Assert.AreEqual(expectedRunTime, target.Runtime);
            Assert.AreEqual(expectedSeriesID, target.SeriesId);
            Assert.AreEqual(expectedStatus, target.Status);
            Assert.AreEqual(expectedAddedDate, target.AddedDate);
            Assert.AreEqual(expectedAddedByUserId, target.AddedByUserId);
            Assert.AreEqual(expectedBanner, target.Banner);
            Assert.AreEqual(expectedFanArt, target.FanArt);
            Assert.AreEqual(expectedPoster, target.Poster);
            Assert.AreEqual(expectedTMSWanted, target.TMSWanted);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DeserializeTestFailedNoNode()
        {
            Series target = new Series();
            target.Deserialize(null);
        }

        #endregion
    }
}