using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVDB.Model;

namespace TVDB.Test.Model
{
	[TestClass]
	public class BannerTest
	{
		[TestMethod]
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

			Assert.AreEqual(605881, target.Id);
			Assert.AreEqual("fanart/original/83462-18.jpg", target.BannerPath);
			Assert.AreEqual(BannerTyp.fanart, target.Type);
			Assert.AreEqual("1920x1080", target.Dimension);
			Assert.AreEqual("|217,177,118|59,40,68|214,192,205|", target.Color);
			Assert.AreEqual("de", target.Language);
			Assert.AreEqual(9.6765, target.Rating);
			Assert.AreEqual(34, target.RatingCount);
			Assert.AreEqual(false, target.SeriesName);
			Assert.AreEqual("_cache/fanart/original/83462-18.jpg", target.ThumbnailPath);
			Assert.AreEqual("fanart/vignette/83462-18.jpg", target.VignettePath);
		}
		
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void DeserializeBannerTestFailedNoNode()
		{
			Banner target = new Banner();

			target.Deserialize(null);
		}
	}
}
