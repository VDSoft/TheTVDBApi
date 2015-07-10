using System;
using TVDB.Model;
using Xunit;

namespace TVDB.Test.Model
{
	
	public class BannerTest
	{
		[Fact]
		public void DeserializeBannerTestSuccessfull()
		{
			string content =
				"<?xml version=\"1.0\" encoding=\"UTF-8\" ?><Banners><Banner><id>605881</id><BannerPath>fanart/original/83462-18.jpg</BannerPath><BannerType>fanart</BannerType><BannerType2>1920x1080</BannerType2><Colors>|217,177,118|59,40,68|214,192,205|</Colors><Language>de</Language><Rating>9.6765</Rating><RatingCount>34</RatingCount><SeriesName>false</SeriesName><ThumbnailPath>_cache/fanart/original/83462-18.jpg</ThumbnailPath><VignettePath>fanart/vignette/83462-18.jpg</VignettePath></Banner></Banners>";

			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.LoadXml(content);

			System.Xml.XmlNode bannersNode = doc.ChildNodes[1];
			System.Xml.XmlNode bannerNode = bannersNode.ChildNodes[0];

			Banner target = new Banner();
			target.Deserialize(bannerNode);

			Assert.Equal(605881, target.Id);
			Assert.Equal("fanart/original/83462-18.jpg", target.BannerPath);
			Assert.Equal(BannerTyp.fanart, target.Type);
			Assert.Equal("1920x1080", target.Dimension);
			Assert.Equal("|217,177,118|59,40,68|214,192,205|", target.Color);
			Assert.Equal("de", target.Language);
			Assert.Equal(9.6765, target.Rating);
			Assert.Equal(34, target.RatingCount);
			Assert.Equal(false, target.SeriesName);
			Assert.Equal("_cache/fanart/original/83462-18.jpg", target.ThumbnailPath);
			Assert.Equal("fanart/vignette/83462-18.jpg", target.VignettePath);
		}

		[Fact]
		public void DeserializeBannerTestFailedNoNode()
		{
			Banner target = new Banner();

			ArgumentNullException expected = new ArgumentNullException("node", "Provided node must not be null.");
			ArgumentNullException actual = Assert.Throws<ArgumentNullException>(() => target.Deserialize(null));

			Assert.Equal(expected.Message, actual.Message);
		}
	}
}
