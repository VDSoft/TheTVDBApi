using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVDB.Model;

namespace TVDB.Test.Model
{
	[TestClass]
	public class SeriesDetailsTest
	{
		/// <summary>
		/// Path of the extracted test data.
		/// </summary>
		string testExtractionPath = @"..\..\..\TVDB.Test\TestData\Extracted\";

		#region Constructor tests

		[TestMethod]
		public void CosntructorTestSuccessfull()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");

			Assert.AreEqual("en", target.Language);
			Assert.IsNotNull(target.Actors);
			Assert.IsNotNull(target.Banners);
			Assert.IsNotNull(target.Series);
		}

		[TestMethod]
		[ExpectedException(typeof(System.IO.DirectoryNotFoundException))]
		public void ConstructorTestFailedDirectroyDoesNotExist()
		{
			SeriesDetails target = new SeriesDetails(@"Some\Director\", "en");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void ConstructorTestFailedNoLanguage()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, string.Empty);
		}

		#endregion

		#region Deserialize tests

		[TestMethod]
		public void DeserializeActorsTest()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");
			Type targetType = typeof(SeriesDetails);
			var method = targetType.GetMethod("DeserializeActors", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

			method.Invoke(target, new object[0]);

			Assert.IsNotNull(target.Actors);
			Assert.AreEqual(9, target.Actors.Count);
		}

		[TestMethod]
		public void DeserializeBannersTest()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");
			Type targetType = typeof(SeriesDetails);
			var method = targetType.GetMethod("DeserializeBanners", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

			method.Invoke(target, new object[0]);

			Assert.IsNotNull(target.Banners);
			Assert.AreEqual(125, target.Banners.Count);
		}

		[TestMethod]
		public void DeserializeSeriesTest()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");
			Type targetType = typeof(SeriesDetails);
			var method = targetType.GetMethod("DeserializeSeries", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

			method.Invoke(target, new object[0]);

			Assert.IsNotNull(target.Series);
			Assert.AreEqual(83462, target.Series.Id);
			Assert.AreEqual("Castle (2009)", target.Series.Name);
			Assert.AreEqual(121, target.Series.Episodes.Count);
		}

		#endregion

		#region Dispose tests

		[TestMethod]
		public void DisposeTestSuccessfull()
		{
			SeriesDetails target = new SeriesDetails(this.testExtractionPath, "en");

			var dummyCall = target.Actors;
			dummyCall = null;

			target.Dispose();

			Assert.AreEqual(string.Empty, target.Language);
			Assert.IsNull(target.Actors);
			Assert.IsNull(target.Banners);
			Assert.IsNull(target.Series);
		}

		#endregion
	}
}
