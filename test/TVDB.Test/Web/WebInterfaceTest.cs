using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVDB.Model;
using TVDB.Web;
using Xunit;

namespace TVDB.Test.Web
{
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

		[Fact]
		public void GetMirrorsTestSuccesfull()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Mirror>> taskResult = target.GetMirrors();

			List<Mirror> result = taskResult.Result;

			Assert.True(result.Count > 0);
		}

		#endregion

		#region GetLanguagestests

		[Fact]
		public void GetLanguagesTestSuccessfull()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Language>> taskResult = target.GetLanguages();

			List<Language> result = taskResult.Result;

			Assert.True(result.Count > 0);
		}

		[Fact]
		public void GetLanguagesTestSuccessfullWithMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Language>> taskResult = target.GetLanguages(this.testMirror);

			List<Language> result = taskResult.Result;

			Assert.True(result.Count > 0);
		}

		[Fact]
		public void GetLanguagesTestFailedNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Language>> taskResult = target.GetLanguages(null);

			Assert.Null(taskResult.Result);
		}

		#endregion

		#region GetSeriesByName tests

		[Fact]
		public void GetSeriesByNameTestSuccessfull()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Chuck", this.testMirror);

			List<Series> result = taskResult.Result;

			Assert.True(result.Count > 0);

			Series firstElement = result[0];
			Assert.Equal("en", firstElement.Language);
			Assert.Equal("Chuck", firstElement.Name);
			Assert.Equal("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.Equal(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.Equal("NBC", firstElement.Network);
			Assert.Equal("tt0934814", firstElement.IMDBId);
			Assert.Equal("EP00930779", firstElement.Zap2ItId);
			Assert.Equal(80348, firstElement.Id);
		}

		[Fact]
		public void GetSeriesByNameTestSuccessfullWithNameAndLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Chuck", "en", this.testMirror);

			List<Series> result = taskResult.Result;

			Series firstElement = result[0];
			Assert.Equal("en", firstElement.Language);
			Assert.Equal("Chuck", firstElement.Name);
			Assert.Equal("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.Equal(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.Equal("NBC", firstElement.Network);
			Assert.Equal("tt0934814", firstElement.IMDBId);
			Assert.Equal("EP00930779", firstElement.Zap2ItId);
			Assert.Equal(80348, firstElement.Id);
		}

		[Fact]
		public void GetSeriesByNameTestFailedNoSeriesName()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName(string.Empty, this.testMirror);
			Assert.Null(taskResult.Result);
		}

		[Fact]
		public void GetSeriesByNameTestFailedNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Hugo", null);
			Assert.Null(taskResult.Result);
		}

		[Fact]
		public void GetSeriesByNameTestFailedSeriesNameNoLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Hugo", string.Empty, this.testMirror);
			Assert.Null(taskResult.Result);
		}

		[Fact]
		public void GetSeriesByNameTestFailedSeriesNameLanguageNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByName("Hugo", "en", null);
			Assert.Null(taskResult.Result);
		}
		#endregion

		#region GetSeriesByRemoteId tests

		[Fact]
		public void GetSeriesByRemoteIdTestSuccessfullImdbdId()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", string.Empty, this.testMirror);

			List<Series> result = taskResult.Result;

			Assert.True(result.Count == 1);

			Series firstElement = result[0];
			Assert.Equal("en", firstElement.Language);
			Assert.Equal("Chuck", firstElement.Name);
			Assert.Equal("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.Equal(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.Equal("tt0934814", firstElement.IMDBId);
			Assert.Equal("EP00930779", firstElement.Zap2ItId);
			Assert.Equal(80348, firstElement.Id);
		}

		[Fact]
		public void GetSeriesByRemoteIdTestSuccessfullZap2ItId()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId(string.Empty, "EP00930779", this.testMirror);
			List<Series> result = taskResult.Result;

			Assert.True(result.Count == 1);

			Series firstElement = result[0];
			Assert.Equal("en", firstElement.Language);
			Assert.Equal("Chuck", firstElement.Name);
			Assert.Equal("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.Equal(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.Equal("tt0934814", firstElement.IMDBId);
			Assert.Equal("EP00930779", firstElement.Zap2ItId);
			Assert.Equal(80348, firstElement.Id);
		}

		[Fact]
		public void GetSeriesByRemoteIdTestSuccessfullImdbIdWithLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", string.Empty, "en", this.testMirror);
			List<Series> result = taskResult.Result;

			Assert.True(result.Count == 1);

			Series firstElement = result[0];
			Assert.Equal("en", firstElement.Language);
			Assert.Equal("Chuck", firstElement.Name);
			Assert.Equal("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.Equal(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.Equal("tt0934814", firstElement.IMDBId);
			Assert.Equal("EP00930779", firstElement.Zap2ItId);
			Assert.Equal(80348, firstElement.Id);
		}

		[Fact]
		public void GetSeriesByRemoteIdTestSuccessfullZap2ItIdWithLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId(string.Empty, "EP00930779", "en", this.testMirror);
			List<Series> result = taskResult.Result;

			Assert.True(result.Count == 1);

			Series firstElement = result[0];
			Assert.Equal("en", firstElement.Language);
			Assert.Equal("Chuck", firstElement.Name);
			Assert.Equal("graphical/80348-g26.jpg", firstElement.Banner);
			Assert.Equal(new DateTime(2007, 09, 24), firstElement.FirstAired);
			Assert.Equal("tt0934814", firstElement.IMDBId);
			Assert.Equal("EP00930779", firstElement.Zap2ItId);
			Assert.Equal(80348, firstElement.Id);
		}

		[Fact]
		public void GetSeriesByRemoteIdTestFailedNoId()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId(string.Empty, string.Empty, this.testMirror);

			Assert.Null(taskResult.Result);
		}

		[Fact]
		public void GetSeriesByRemoteIdTestFailedBothIds()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", "EP00930779", string.Empty, null);

			Assert.Null(taskResult.Result);
		}

		[Fact]
		public void GetSeriesByRemoteIdTestFailedNoLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", string.Empty, string.Empty, this.testMirror);

			Assert.Null(taskResult.Result);
		}

		[Fact]
		public void GetSeriesByRemoteIdTestFailedNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<List<Series>> taskResult = target.GetSeriesByRemoteId("tt0934814", string.Empty, string.Empty, null);

			Assert.Null(taskResult.Result);
		}

		#endregion

		#region GetFullSeriesById tests

		[Fact]
		public void GetFullSeriesByIdTestSuccessfullWithDefaultLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(83462, this.testMirror);
			SeriesDetails result = taskResult.Result;
			Episode firstEpisode = result.Series.Episodes.First(x => x.SeasonNumber == 1 && x.Number == 1);

			// check details
			Assert.Equal("en", result.Language);

			Assert.NotNull(result.Actors);
			Assert.Equal(9, result.Actors.Count);

			Assert.NotNull(result.Banners);
			Assert.True(result.Banners.Count > 10);

			// check series
			Assert.NotNull(result);
			Assert.Equal(83462, result.Series.Id);
			Assert.Equal("Castle (2009)", result.Series.Name);
			Assert.Equal("en", result.Series.Language);

			// check episodes
			Assert.Equal(1, firstEpisode.SeasonNumber);
			Assert.Equal(1, firstEpisode.Number);
			Assert.Equal(398671, firstEpisode.Id);
			Assert.Equal("Flowers for Your Grave", firstEpisode.Name);

			result.Dispose();
		}

		[Fact]
		public void GetFullSeriesByIdTestSuccessfullWithLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(83462, "de", this.testMirror);
			SeriesDetails result = taskResult.Result;


			Assert.Equal("de", result.Language);


			Episode firstEpisode = result.Series.Episodes.First(x => x.SeasonNumber == 1 && x.Number == 1);

			// check series
			Assert.NotNull(result);
			Assert.Equal(83462, result.Series.Id);
			Assert.Equal("Castle", result.Series.Name);
			Assert.Equal("de", result.Series.Language);

			// check episodes
			Assert.Equal(1, firstEpisode.SeasonNumber);
			Assert.Equal(1, firstEpisode.Number);
			Assert.Equal(398671, firstEpisode.Id);
			Assert.Equal("Blumen für Dein Grab", firstEpisode.Name);

			result.Dispose();
		}

		[Fact]
		public void GetFullSeriesByIdFailedNoId()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(0, this.testMirror);

			Assert.Null(taskResult.Result);
		}

		[Fact]
		public void GetFullSeriesByIdFailedNoMirror()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(83462, null);

			Assert.Null(taskResult.Result);
		}

		[Fact]
		public void GetFullSeriesByIdFailedNoLanguage()
		{
			WebInterface target = new WebInterface(apiKey);
			Task<SeriesDetails> taskResult = target.GetFullSeriesById(83462, string.Empty, this.testMirror);

			Assert.Null(taskResult.Result);
		}

		#endregion
	}
}
