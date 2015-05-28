using System;
using TVDB.Model;
using Xunit;

namespace TVDB.Test.Model
{
    public class EpisodeTest
    {
        #region Initializer tests

        [Fact]
        public void InitializerTestSuccessfull()
        {
            Episode target = new Episode();
            target.GuestStars = "Mieko Hillman|Kristine Blackport|Jim Pirri|Diana Gitelman|Mel Fair|Lynn A. Henderson|Odessa Rae|Jordan Potter|Tasha Campbell|Dale Dye|Matthew Bomer|Bruno Amato|Nicolas Pajon|Wendy Makkena";
            target.Writer = "Josh Schwartz|Chris Fedak";

            string expectedGuestStars = "Mieko Hillman, Kristine Blackport, Jim Pirri, Diana Gitelman, Mel Fair, Lynn A. Henderson, Odessa Rae, Jordan Potter, Tasha Campbell, Dale Dye, Matthew Bomer, Bruno Amato, Nicolas Pajon, Wendy Makkena";
            string expectedWriters = "Josh Schwartz, Chris Fedak";

            Type targetType = typeof(Episode);
            var methode = targetType.GetMethod("Initialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methode.Invoke(target, new object[0]);

            string actualGuestStars = target.GuestStars;
            string actualWriters = target.Writer;

            Assert.Equal(expectedGuestStars, actualGuestStars);
            Assert.Equal(expectedWriters, actualWriters);
        }

        [Fact]
        public void InitializerTestSuccessfullGuestStarsNull()
        {
            Episode target = new Episode();
            target.GuestStars = string.Empty;
            target.Writer = "Josh Schwartz|Chris Fedak";

            string expectedGuestStars = string.Empty;
            string expectedWriters = "Josh Schwartz, Chris Fedak";

            Type targetType = typeof(Episode);
            var methode = targetType.GetMethod("Initialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methode.Invoke(target, new object[0]);

            string actualGuestStars = target.GuestStars;
            string actualWriters = target.Writer;

            Assert.Equal(expectedGuestStars, actualGuestStars);
            Assert.Equal(expectedWriters, actualWriters);
        }

        [Fact]
        public void InitializerTestSuccessfullWriterNull()
        {
            Episode target = new Episode();
            target.GuestStars = "Mieko Hillman|Kristine Blackport|Jim Pirri|Diana Gitelman|Mel Fair|Lynn A. Henderson|Odessa Rae|Jordan Potter|Tasha Campbell|Dale Dye|Matthew Bomer|Bruno Amato|Nicolas Pajon|Wendy Makkena";
            target.Writer = string.Empty;

            string expectedGuestStars = "Mieko Hillman, Kristine Blackport, Jim Pirri, Diana Gitelman, Mel Fair, Lynn A. Henderson, Odessa Rae, Jordan Potter, Tasha Campbell, Dale Dye, Matthew Bomer, Bruno Amato, Nicolas Pajon, Wendy Makkena";
            string expectedWriters = string.Empty;

            Type targetType = typeof(Episode);
            var methode = targetType.GetMethod("Initialize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methode.Invoke(target, new object[0]);

            string actualGuestStars = target.GuestStars;
            string actualWriters = target.Writer;

            Assert.Equal(expectedGuestStars, actualGuestStars);
			Assert.Equal(expectedWriters, actualWriters);
        }

        #endregion

        #region Deserialize test

        [Fact]
        public void DeserializeSuccessfull()
        {
            string xmlContent =
                "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Data><Episode><id>398671</id><Combined_episodenumber>1.0</Combined_episodenumber><Combined_season>1</Combined_season><DVD_chapter></DVD_chapter><DVD_discid></DVD_discid><DVD_episodenumber>1.0</DVD_episodenumber><DVD_season>1</DVD_season><Director>Rob Bowman</Director><EpImgFlag>2</EpImgFlag><EpisodeName>Blumen für Dein Grab</EpisodeName><EpisodeNumber>1</EpisodeNumber><FirstAired>2009-03-09</FirstAired><GuestStars>Stephen J. Cannell|James Patterson|Dan Castellaneta|Keir Dullea|Colby French|Brian Avers|Monet Mazur</GuestStars><IMDB_ID>tt1303973</IMDB_ID><Language>de</Language><Overview>Der Anwalt Marvin Fisk und die Sozialarbeiterin Alison Tisdale werden ermordet aufgefunden. Detective Kate Beckett fällt auf, dass es eine Verbindung zwischen den Morden gibt: Es handelt sich um Verbrechen aus Romanen des Kriminalautors Rick Castle. Dieser fühlt sich durch den Nachahmer geschmeichelt, und willigt ein, der Polizei bei den Ermittlungen zu helfen. Beckett ist davon alles andere als begeistert, doch schon bald erweist sich Castles Hilfe als unentbehrlich ...</Overview><ProductionCode>101</ProductionCode><Rating>7.9</Rating><RatingCount>125</RatingCount><SeasonNumber>1</SeasonNumber><Writer>Andrew W. Marlowe</Writer><absolute_number>1</absolute_number><filename>episodes/83462/398671.jpg</filename><lastupdated>1376283694</lastupdated><seasonid>36354</seasonid><seriesid>83462</seriesid><thumb_added></thumb_added><thumb_height>225</thumb_height><thumb_width>400</thumb_width><tms_export>1</tms_export><tms_review_blurry>0</tms_review_blurry><tms_review_by></tms_review_by><tms_review_dark>0</tms_review_dark><tms_review_date></tms_review_date>  <tms_review_logo>0</tms_review_logo><tms_review_other>0</tms_review_other><tms_review_unsure>0</tms_review_unsure></Episode></Data>";
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.LoadXml(xmlContent);

            System.Xml.XmlNode dataNode = doc.ChildNodes[1];
            System.Xml.XmlNode episodeNode = dataNode.ChildNodes[0];
            Episode target = new Episode();
            target.Deserialize(episodeNode);

            string expectedGuestStars =
                "Stephen J. Cannell, James Patterson, Dan Castellaneta, Keir Dullea, Colby French, Brian Avers, Monet Mazur";
            string expectedOverview = "Der Anwalt Marvin Fisk und die Sozialarbeiterin Alison Tisdale werden ermordet aufgefunden. Detective Kate Beckett fällt auf, dass es eine Verbindung zwischen den Morden gibt: Es handelt sich um Verbrechen aus Romanen des Kriminalautors Rick Castle. Dieser fühlt sich durch den Nachahmer geschmeichelt, und willigt ein, der Polizei bei den Ermittlungen zu helfen. Beckett ist davon alles andere als begeistert, doch schon bald erweist sich Castles Hilfe als unentbehrlich ...";
            string expectedWriter = "Andrew W. Marlowe";

            Assert.Equal(398671, target.Id);
            Assert.Equal(1.0, target.CombinedEpisodeNumber);
            Assert.Equal(1, target.CombinedSeason);
            Assert.Equal(0, target.DVDChapter);
            Assert.Equal(0, target.DVDDiscId);
            Assert.Equal(1.0, target.DVDEpisodeNumber);
            Assert.Equal(1, target.DVDSeason);
            Assert.Equal("Rob Bowman", target.Director);
            Assert.Equal(2, target.EpImageFlag);
            Assert.Equal("Blumen für Dein Grab", target.Name);
            Assert.Equal(1, target.Number);
            Assert.Equal(new DateTime(2009, 3, 9, 0, 0, 0), target.FirstAired);
            Assert.Equal(expectedGuestStars, target.GuestStars);
            Assert.Equal("tt1303973", target.IMDBId);
            Assert.Equal("de", target.Language);
            Assert.Equal(expectedOverview, target.Overview);
            Assert.Equal(101, target.ProductionCode);
            Assert.Equal(7.9, target.Rating);
            Assert.Equal(1, target.SeasonNumber);
            Assert.Equal(expectedWriter, target.Writer);
            Assert.Equal(1, target.AbsoluteNumber);
            Assert.Equal("episodes/83462/398671.jpg", target.PictureFilename);
            Assert.Equal(1376283694, target.LastUpdated);
            Assert.Equal(36354, target.SeasonId);
            Assert.Equal(83462, target.SeriesId);
            Assert.Equal(125, target.RatingCount);
            Assert.Equal(0, target.Thumbadded);
            Assert.Equal(225, target.ThumbHeight);
            Assert.Equal(400, target.ThumbWidth);
            Assert.Equal(true, target.IsTMSExport);
            Assert.Equal(false, target.IsTMSReviewBlurry);
            Assert.Equal(0, target.TMSReviewById);
            Assert.Equal(false, target.IsTMSReviewDark);
            Assert.Equal(DateTime.MinValue, target.TMSReviewDate);
            Assert.Equal(0, target.TMSReviewLogoId);
            Assert.Equal(0, target.TMSReviewOther);
            Assert.Equal(false, target.IsTMSReviewUnsure);
        }

        [Fact]
        public void DesereializeFailedNoNode()
        {
            Episode target = new Episode();

			ArgumentNullException expected = new ArgumentNullException("node", "Provided node must not be null.");
			ArgumentNullException actual = Assert.Throws<ArgumentNullException>(() => target.Deserialize(null));

			Assert.Equal(expected.Message, actual.Message);
        }

        #endregion
    }
}
