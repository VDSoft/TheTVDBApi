using System;
using TVDB.Model;
using Xunit;

namespace TVDB.Test.Model
{
	public class SeriesDetailsTest
	{
		/// <summary>
		/// Path of the extracted test data.
		/// </summary>
		string testExtractionPath = @"..\..\..\TVDB.Test\TestData\Extracted\";

		#region Constructor tests

		[Fact]
		public void CosntructorTestSuccessfull()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");

			Assert.Equal("en", target.Language);
			Assert.NotNull(target.Actors);
			Assert.NotNull(target.Banners);
			Assert.NotNull(target.Series);
		}

		[Fact]
		public void ConstructorTestFailedDirectroyDoesNotExist()
		{
			string fakePath = @"C:\Some\Director\";

			System.IO.DirectoryNotFoundException expected = new System.IO.DirectoryNotFoundException(string.Format("The directory \"{0}\" could not be found.", fakePath));
			System.IO.DirectoryNotFoundException actual = Assert.Throws<System.IO.DirectoryNotFoundException>(() => new SeriesDetails(fakePath, "en"));

			Assert.Equal(expected.Message, actual.Message);
		}

		[Fact]
		
		public void ConstructorTestFailedNoLanguage()
		{
			ArgumentNullException expected = new ArgumentNullException("language", "Provided language must not be null or empty.");
			ArgumentNullException actual = Assert.Throws<ArgumentNullException>(() => new SeriesDetails(this.testExtractionPath, string.Empty));

			Assert.Equal(expected.Message, actual.Message);
		}

		#endregion

		#region Deserialize tests

		[Fact]
		public void DeserializeActorsTest()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");
			Type targetType = typeof(SeriesDetails);
			var method = targetType.GetMethod("DeserializeActors", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

			method.Invoke(target, new object[0]);

			Assert.NotNull(target.Actors);
			Assert.Equal(9, target.Actors.Count);
		}

		[Fact]
		public void DeserializeBannersTest()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");
			Type targetType = typeof(SeriesDetails);
			var method = targetType.GetMethod("DeserializeBanners", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

			method.Invoke(target, new object[0]);

			Assert.NotNull(target.Banners);
			Assert.Equal(125, target.Banners.Count);
		}

		[Fact]
		public void DeserializeSeriesTest()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");
			Type targetType = typeof(SeriesDetails);
			var method = targetType.GetMethod("DeserializeSeries", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

			method.Invoke(target, new object[0]);

			Assert.NotNull(target.Series);
			Assert.Equal(83462, target.Series.Id);
			Assert.Equal("Castle (2009)", target.Series.Name);
			Assert.Equal(121, target.Series.Episodes.Count);
		}

		#endregion

		#region Dispose tests

		[Fact]
		public void DisposeTestSuccessfull()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");

			var dummyCall = target.Actors;
			dummyCall = null;

			target.Dispose();

			Assert.Equal(null, target.Language);
			Assert.Null(target.Actors);
			Assert.Null(target.Banners);
			Assert.Null(target.Series);
		}

		#endregion
	}
}
