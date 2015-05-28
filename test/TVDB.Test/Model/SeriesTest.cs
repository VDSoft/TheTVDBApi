using System;
using TVDB.Model;
using Xunit;

namespace TVDB.Test.Model
{
    public class SeriesTest
    {
        #region Initialize tests

        [Fact]
        public void InitializeTestSuccessfullNoEpisodes()
        {
            Series target = new Series();
            Type targetType = typeof(Series);
            var method = targetType.GetMethod("Initialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(target, new object[0]);

            Assert.False(target.HasEpisodes);
        }

		[Fact]
        public void InitializeTestSuccessfullWithEpisodes()
        {
            Series target = new Series();
            target.Episodes.Add(new Episode());
            Type targetType = typeof(Series);
            var method = targetType.GetMethod("Initialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            method.Invoke(target, new object[0]);

            Assert.True(target.HasEpisodes);
        }

        #endregion

        #region Deserialize tests

		[Fact]
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

            Assert.Equal(expectedId, target.Id);
            Assert.Equal(expectedLanguage, target.Language);
            Assert.Equal(expectedName, target.Name);
            Assert.Equal(expectedBannerPath, target.Banner);
            Assert.Equal(expectedOverview, target.Overview);
            Assert.Equal(expectedFirstAired, target.FirstAired);
            Assert.Equal(expectedIMDBId, target.IMDBId);
            Assert.Equal(expectedZap2itId, target.Zap2ItId);
            Assert.False(target.HasEpisodes);
            Assert.Equal(expectedActors, target.Actorts);
            Assert.Equal(expectedGener, target.Genre);
            Assert.Equal(expectedDayAired, target.AirsDayOfWeel);
            Assert.Equal(expectedTimeAired, target.AirsTime);
            Assert.Equal(expectedContentRating, target.ContentRating);
            Assert.Equal(expectedNetwork, target.Network);
            Assert.Equal(expectedNetworkID, target.NetworkId);
            Assert.Equal(expectedRating, target.Rating);
            Assert.Equal(expectedRatingCount, target.RatingCount);
            Assert.Equal(expectedRunTime, target.Runtime);
            Assert.Equal(expectedSeriesID, target.SeriesId);
            Assert.Equal(expectedStatus, target.Status);
            Assert.Equal(expectedAddedDate, target.AddedDate);
            Assert.Equal(expectedAddedByUserId, target.AddedByUserId);
            Assert.Equal(expectedBanner, target.Banner);
            Assert.Equal(expectedFanArt, target.FanArt);
            Assert.Equal(expectedPoster, target.Poster);
            Assert.Equal(expectedTMSWanted, target.TMSWanted);
        }

		[Fact]
        public void DeserializeTestFailedNoNode()
        {
            Series target = new Series();

			ArgumentNullException expected = new ArgumentNullException("node", "Provided node must not be null.");
			ArgumentNullException actual = Assert.Throws<ArgumentNullException>(() => target.Deserialize(null));

			Assert.Equal(expected.Message, actual.Message);
        }

        #endregion
    }
}