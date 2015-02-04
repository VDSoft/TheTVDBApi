using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVDB.Model;
using TVDB.Web;

namespace TVDB.Test.Web
{
	[TestClass]
	public class WebInterfaceTest
	{
		/// <summary>
		/// Mirror for testing.
		/// </summary>
		private readonly Mirror testMirror = new Mirror()
		{
			Address = "http://thetvdb.com",
			ContainsBannerFile = true,
			ContainsXmlFile = true,
			ContainsZipFile = true,
			Id = 1
		};

		/// <summary>
		/// Api key for testing.
		/// </summary>
		private readonly string apiKey = "CE1AECDA14314FD5";

		#region GetMirrors tests

		[TestMethod]
		public void GetMirrorsTestSuccesfull()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Mirror>> taskResult = target.GetMirrors();

			List<Mirror> result = taskResult.Result;

			Assert.IsTrue(result.Count > 0);
		}

		#endregion

		#region GetLanguagestests

		[TestMethod]
		public void GetLanguagesTestSuccessfull()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Language>> taskResult = target.GetLanguages();

			List<Language> result = taskResult.Result;

			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetLanguagesTestSuccessfullWithMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Language>> taskResult = target.GetLanguages(this.testMirror);

			List<Language> result = taskResult.Result;

			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetLanguagesTestFailedNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Language>> taskResult = target.GetLanguages(null);

			Assert.IsNull(taskResult.Result);
		}

		#endregion

		#region GetSeriesByName tests

		[TestMethod]
		public void GetSeriesByNameTestSuccessfull()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Chuck", this.testMirror);

			List<Series> result = taskResult.Result;

			Assert.IsTrue(result.Count > 0);

			Series firstElement = result[0];
			Assert.AreEqual("en", firstElement.Language);
			Assert.AreEqual("Chuck", firstElement.Name);
			Assert.AreEqual("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.AreEqual(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.AreEqual("NBC", firstElement.Network);
			Assert.AreEqual("tt0934814", firstElement.IMDBId);
			Assert.AreEqual("EP00930779", firstElement.Zap2ItId);
			Assert.AreEqual(80348, firstElement.Id);
		}

		[TestMethod]
		public void GetSeriesByNameTestSuccessfullWithNameAndLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Chuck", "en", this.testMirror);

			List<Series> result = taskResult.Result;

			Series firstElement = result[0];
			Assert.AreEqual("en", firstElement.Language);
			Assert.AreEqual("Chuck", firstElement.Name);
			Assert.AreEqual("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.AreEqual(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.AreEqual("NBC", firstElement.Network);
			Assert.AreEqual("tt0934814", firstElement.IMDBId);
			Assert.AreEqual("EP00930779", firstElement.Zap2ItId);
			Assert.AreEqual(80348, firstElement.Id);
		}

		[TestMethod]
		public void GetSeriesByNameTestFailedNoSeriesName()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName(string.Empty, this.testMirror);
			Assert.IsNull(taskResult.Result);
		}

		[TestMethod]
		public void GetSeriesByNameTestFailedNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Hugo", null);
			Assert.IsNull(taskResult.Result);
		}

		[TestMethod]
		public void GetSeriesByNameTestFailedSeriesNameNoLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Hugo", string.Empty, this.testMirror);
			Assert.IsNull(taskResult.Result);
		}

		[TestMethod]
		public void GetSeriesByNameTestFailedSeriesNameLanguageNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Hugo", "en", null);
			Assert.IsNull(taskResult.Result);
		}
		#endregion

		#region GetSeriesByRemoteId tests

		[TestMethod]
		public void GetSeriesByRemoteIdTestSuccessfullImdbdId()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", string.Empty, this.testMirror);

			List<Series> result = taskResult.Result;

			Assert.IsTrue(result.Count == 1);

			Series firstElement = result[0];
			Assert.AreEqual("en", firstElement.Language);
			Assert.AreEqual("Chuck", firstElement.Name);
			Assert.AreEqual("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.AreEqual(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.AreEqual("tt0934814", firstElement.IMDBId);
			Assert.AreEqual("EP00930779", firstElement.Zap2ItId);
			Assert.AreEqual(80348, firstElement.Id);
		}

		[TestMethod]
		public void GetSeriesByRemoteIdTestSuccessfullZap2ItId()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId(string.Empty, "EP00930779", this.testMirror);
			List<Series> result = taskResult.Result;

			Assert.IsTrue(result.Count == 1);

			Series firstElement = result[0];
			Assert.AreEqual("en", firstElement.Language);
			Assert.AreEqual("Chuck", firstElement.Name);
			Assert.AreEqual("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.AreEqual(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.AreEqual("tt0934814", firstElement.IMDBId);
			Assert.AreEqual("EP00930779", firstElement.Zap2ItId);
			Assert.AreEqual(80348, firstElement.Id);
		}

		[TestMethod]
		public void GetSeriesByRemoteIdTestSuccessfullImdbIdWithLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", string.Empty, "en", this.testMirror);
			List<Series> result = taskResult.Result;

			Assert.IsTrue(result.Count == 1);

			Series firstElement = result[0];
			Assert.AreEqual("en", firstElement.Language);
			Assert.AreEqual("Chuck", firstElement.Name);
			Assert.AreEqual("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.AreEqual(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.AreEqual("tt0934814", firstElement.IMDBId);
			Assert.AreEqual("EP00930779", firstElement.Zap2ItId);
			Assert.AreEqual(80348, firstElement.Id);
		}

		[TestMethod]
		public void GetSeriesByRemoteIdTestSuccessfullZap2ItIdWithLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId(string.Empty, "EP00930779", "en", this.testMirror);
			List<Series> result = taskResult.Result;

			Assert.IsTrue(result.Count == 1);

			Series firstElement = result[0];
			Assert.AreEqual("en", firstElement.Language);
			Assert.AreEqual("Chuck", firstElement.Name);
			Assert.AreEqual("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.AreEqual(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.AreEqual("tt0934814", firstElement.IMDBId);
			Assert.AreEqual("EP00930779", firstElement.Zap2ItId);
			Assert.AreEqual(80348, firstElement.Id);
		}

		[TestMethod]
		public void GetSeriesByRemoteIdTestFailedNoId()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId(string.Empty, string.Empty, this.testMirror);

			Assert.IsNull(taskResult.Result);
		}

		[TestMethod]
		public void GetSeriesByRemoteIdTestFailedBothIds()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", "EP00930779", string.Empty, null);

			Assert.IsNull(taskResult.Result);
		}

		[TestMethod]
		public void GetSeriesByRemoteIdTestFailedNoLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", string.Empty, string.Empty, this.testMirror);

			Assert.IsNull(taskResult.Result);
		}

		[TestMethod]
		public void GetSeriesByRemoteIdTestFailedNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", string.Empty, string.Empty, null);

			Assert.IsNull(taskResult.Result);
		}

		#endregion

		#region GetFullSeriesById tests

		[TestMethod]
		public void GetFullSeriesByIdTestSuccessfullWithDefaultLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(83462, this.testMirror);
			SeriesDetails result = taskResult.Result;
			Episode firstEpisode = result.Series.Episodes.First(x => x.SeasonNumber == 1 && x.Number == 1);

			// check details
			Assert.AreEqual("en", result.Language);

			Assert.IsNotNull(result.Actors);
			Assert.AreEqual(9, result.Actors.Count);

			Assert.IsNotNull(result.Banners);
			Assert.IsTrue(result.Banners.Count > 10);

			// check series
			Assert.IsNotNull(result);
			Assert.AreEqual(83462, result.Series.Id);
			Assert.AreEqual("Castle (2009)", result.Series.Name);
			Assert.AreEqual("en", result.Series.Language);

			// check episodes
			Assert.AreEqual(1, firstEpisode.SeasonNumber);
			Assert.AreEqual(1, firstEpisode.Number);
			Assert.AreEqual(398671, firstEpisode.Id);
			Assert.AreEqual("Flowers for Your Grave", firstEpisode.Name);

			result.Dispose();
		}

		[TestMethod]
		public void GetFullSeriesByIdTestSuccessfullWithLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(83462, "de", this.testMirror);
			SeriesDetails result = taskResult.Result;


			Assert.AreEqual("de", result.Language);


			Episode firstEpisode = result.Series.Episodes.First(x => x.SeasonNumber == 1 && x.Number == 1);

			// check series
			Assert.IsNotNull(result);
			Assert.AreEqual(83462, result.Series.Id);
			Assert.AreEqual("Castle", result.Series.Name);
			Assert.AreEqual("de", result.Series.Language);

			// check episodes
			Assert.AreEqual(1, firstEpisode.SeasonNumber);
			Assert.AreEqual(1, firstEpisode.Number);
			Assert.AreEqual(398671, firstEpisode.Id);
			Assert.AreEqual("Blumen für Dein Grab", firstEpisode.Name);

			result.Dispose();
		}

		[TestMethod]
		public void GetFullSeriesByIdFailedNoId()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(0, this.testMirror);

			Assert.IsNull(taskResult.Result);
		}

		[TestMethod]
		public void GetFullSeriesByIdFailedNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(83462, null);

			Assert.IsNull(taskResult.Result);
		}

		[TestMethod]
		public void GetFullSeriesByIdFailedNoLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(83462, string.Empty, this.testMirror);

			Assert.IsNull(taskResult.Result);
		}

		#endregion
	}
}
