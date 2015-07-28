// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

namespace TVDB.Model
{
	using System;
	using System.ComponentModel;
	using System.Xml;

	/// <summary>
	/// Types of a banner
	/// </summary>
	public enum BannerTyp
	{
		/// <summary>
		/// A Fanart.
		/// </summary>
		fanart,
		/// <summary>
		/// Original poster.
		/// </summary>
		poster,
		/// <summary>
		/// Season image.
		/// </summary>
		season,
		/// <summary>
		/// Series image.
		/// </summary>
		series,
		/// <summary>
		/// Default value.
		/// </summary>
		unknown
	}

	/// <summary>
	/// Image of a series.
	/// </summary>
	public class Banner : INotifyPropertyChanged, Interfaces.IXmlSerializer
	{
		/// <summary>
		/// Name of the <see cref="Id"/> property.
		/// </summary>
		private const string IdName = "Id";

		/// <summary>
		/// Name of the <see cref="BannerPath"/> property.
		/// </summary>
		private const string BannerPathName = "BannerPath";

		/// <summary>
		/// Name of the <see cref="Type"/> property.
		/// </summary>
		private const string TypeName = "Type";

		/// <summary>
		/// Name of the <see cref="Dimension"/> property.
		/// </summary>
		private const string DimensionName = "Dimension";

		/// <summary>
		/// Name of the <see cref="Color"/> property.
		/// </summary>
		private const string ColorName = "Color";

		/// <summary>
		/// Name of the <see cref="Language"/> property.
		/// </summary>
		private const string LanguageName = "Language";

		/// <summary>
		/// Name of the <see cref="Rating"/> property.
		/// </summary>
		private const string RatingName = "Rating";

		/// <summary>
		/// Name of the <see cref="RatingCount"/> property.
		/// </summary>
		private const string RatingCountName = "RatingCount";

		/// <summary>
		/// Name of the <see cref="SeriesName"/> property.
		/// </summary>
		private const string SeriesNameName = "SeriesName";

		/// <summary>
		/// Name of the <see cref="ThumbnailPath"/> property.
		/// </summary>
		private const string ThumbnailPathName = "ThumbnailPath";

		/// <summary>
		/// Name of the <see cref="VignettePath"/> property.
		/// </summary>
		private const string VignettePathName = "VignettePath";

		/// <summary>
		/// Name of the <see cref="Season"/> propety.
		/// </summary>
		private const string SeasonName = "Season";

		/// <summary>
		/// Id of the banner.
		/// </summary>
		private int id = -1;

		/// <summary>
		/// Path of the image.
		/// </summary>
		private string bannerPath = null;

		/// <summary>
		/// Type of the banner.
		/// </summary>
		private BannerTyp type = BannerTyp.unknown;

		/// <summary>
		/// Dimension of the image.
		/// </summary>
		private string dimension = null;

		/// <summary>
		/// Colors of the banner.
		/// </summary>
		private string colors = null;

		/// <summary>
		/// Language of the banner image.
		/// </summary>
		private string language = null;

		/// <summary>
		/// Rating fo the banner.
		/// </summary>
		private double rating = -1.0;

		/// <summary>
		/// Number of ratings.
		/// </summary>
		private int ratingCount = -1;

		/// <summary>
		/// Series name.
		/// </summary>
		private bool seriesName = false;

		/// <summary>
		/// Path to the thumbnail of the image.
		/// </summary>
		private string thumbnailPath = null;

		/// <summary>
		/// Path to the vignette image.
		/// </summary>
		private string vignettePath = null;

		/// <summary>
		/// The season of the banner.
		/// </summary>
		private int season = -1;

		/// <summary>
		/// Initializes a new instance of the <see cref="Banner"/> class.
		/// </summary>
		public Banner()
		{
		}

		/// <summary>
		/// Occures when a property changed its value
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Gets or sets the Id of the banner.
		/// </summary>
		public int Id
		{
			get
			{
				return this.id;
			}

			set
			{
				if (value == this.id)
				{
					return;
				}

				this.id = value;
				this.RaisePropertyChanged(IdName);
			}
		}

		/// <summary>
		/// Gets or sets the Path of the image.
		/// </summary>
		public string BannerPath
		{
			get
			{
				return this.bannerPath;
			}

			set
			{
				if (value == this.bannerPath)
				{
					return;
				}

				this.bannerPath = value;
				this.RaisePropertyChanged(BannerPathName);
			}
		}

		/// <summary>
		/// Gets or sets the Type of the banner.
		/// </summary>
		public BannerTyp Type
		{
			get
			{
				return this.type;
			}

			set
			{
				if (value == this.type)
				{
					return;
				}

				this.type = value;
				this.RaisePropertyChanged(TypeName);
			}
		}

		/// <summary>
		/// Gets or sets the Dimension of the image.
		/// </summary>
		public string Dimension
		{
			get
			{
				return this.dimension;
			}

			set
			{
				if (value == this.dimension)
				{
					return;
				}

				this.dimension = value;
				this.RaisePropertyChanged(DimensionName);
			}
		}

		/// <summary>
		/// Gets or sets the Colors of the banner.
		/// </summary>
		public string Color
		{
			get
			{
				return this.colors;
			}

			set
			{
				if (value == this.colors)
				{
					return;
				}

				this.colors = value;
				this.RaisePropertyChanged(ColorName);
			}
		}

		/// <summary>
		/// Gets or sets the Language of the banner image.
		/// </summary>
		public string Language
		{
			get
			{
				return this.language;
			}

			set
			{
				if (value == this.language)
				{
					return;
				}

				this.language = value;
				this.RaisePropertyChanged(LanguageName);
			}
		}

		/// <summary>
		/// Gets or sets the Rating fo the banner.
		/// </summary>
		public double Rating
		{
			get
			{
				return this.rating;
			}

			set
			{
				if (value == this.rating)
				{
					return;
				}

				this.rating = value;
				this.RaisePropertyChanged(RatingName);
			}
		}

		/// <summary>
		/// Gets or sets the Number of ratings.
		/// </summary>
		public int RatingCount
		{
			get
			{
				return this.ratingCount;
			}

			set
			{
				if (value == this.ratingCount)
				{
					return;
				}

				this.ratingCount = value;
				this.RaisePropertyChanged(RatingCountName);
			}
		}

		/// <summary>
		/// Gets or sets the Series name.
		/// </summary>
		public bool SeriesName
		{
			get
			{
				return this.seriesName;
			}

			set
			{
				if (value == this.seriesName)
				{
					return;
				}

				this.seriesName = value;
				this.RaisePropertyChanged(SeriesNameName);
			}
		}

		/// <summary>
		/// Gets or sets the Path to the thumbnail of the image.
		/// </summary>
		public string ThumbnailPath
		{
			get
			{
				return this.thumbnailPath;
			}

			set
			{
				if (value == this.thumbnailPath)
				{
					return;
				}

				this.thumbnailPath = value;
				this.RaisePropertyChanged(ThumbnailPathName);
			}
		}

		/// <summary>
		/// Gets or sets the Path to the vignette image.
		/// </summary>
		public string VignettePath
		{
			get
			{
				return this.vignettePath;
			}

			set
			{
				if (value == this.vignettePath)
				{
					return;
				}

				this.vignettePath = value;
				this.RaisePropertyChanged(VignettePathName);
			}
		}

		/// <summary>
		/// Gets or sets the season.
		/// </summary>
		public int Season
		{
			get
			{
				return this.season;
			}

			set
			{
				if (value == this.season)
				{
					return;
				}

				this.season = value;
				this.RaisePropertyChanged(SeasonName);
			}
		}

		/// <summary>
		/// Deserializes a banner xml node.
		/// </summary>
		/// <param name="node">Node to deserialize.</param>
		/// <exception cref="ArgumentNullException">Occurs when the provided <see cref="System.Xml.XmlNode"/> is null.</exception>
		/// <example>This example shows how to use the deserialize method.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Xml document that contains all banners.
		/// 		/// </summary>
		/// 		private XmlDocument bannersDoc = null;
		/// 		
		/// 		/// <summary>
		/// 		/// Initializes a new instance of the DocuClass class.
		/// 		/// </summary>
		/// 		public DocuClass(string extractionPath)
		/// 		{
		/// 			// load actors xml.
		/// 			this.bannersDoc = new XmlDocument();
		/// 			this.bannersDoc.Load(System.IO.Path.Combine(this.extractionPath, "banners.xml"));
		/// 		}
		/// 		
		/// 		/// <summary>
		/// 		/// Deserializes all banners of the series.
		/// 		/// </summary>
		/// 		public List&#60;Banner&#62; DeserializeBanners()
		/// 		{
		/// 			List&#60;Banner&#62; banners = new List&#60;Banner&#62;();
		/// 			
		/// 			// load the xml docs second child.
		/// 			XmlNode bannersNode = this.bannersDoc.ChildNodes[1];
		/// 
		/// 			// deserialize all banners.
		/// 			foreach (XmlNode currentNode in bannersNode.ChildNodes)
		/// 			{
		/// 				if (currentNode.Name.Equals("Banner", StringComparison.OrdinalIgnoreCase))
		/// 				{
		/// 					Banner deserialized = new Banner();
		/// 					deserialized.Deserialize(currentNode);
		/// 
		/// 					banners.Add(deserialized);
		/// 				}
		/// 			}
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public void Deserialize(XmlNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node", "Provided node must not be null.");
			}

			System.Globalization.CultureInfo cultureInfo = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

			foreach (XmlNode currentNode in node.ChildNodes)
			{
				if (currentNode.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
				{
					int result = -1;
					int.TryParse(currentNode.InnerText, out result);
					this.Id = result;
					continue;
				}
				else if (currentNode.Name.Equals("BannerPath", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.BannerPath = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("BannerType", StringComparison.OrdinalIgnoreCase))
				{
					BannerTyp currentTyp = BannerTyp.unknown;
					Enum.TryParse<BannerTyp>(currentNode.InnerText, out currentTyp);
					this.Type = currentTyp;
					continue;
				}
				else if (currentNode.Name.Equals("BannerType2", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Dimension = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("Colors", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Color = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("Language", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Language = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("Rating", StringComparison.OrdinalIgnoreCase))
				{
					double result = -1.0;
					double.TryParse(currentNode.InnerText, System.Globalization.NumberStyles.Number, cultureInfo, out result);
					this.Rating = result;
					continue;
				}
				else if (currentNode.Name.Equals("RatingCount", StringComparison.OrdinalIgnoreCase))
				{
					int result = -1;
					int.TryParse(currentNode.InnerText, out result);
					this.RatingCount = result;
					continue;
				}
				else if (currentNode.Name.Equals("SeriesName", StringComparison.OrdinalIgnoreCase))
				{
					bool result = false;
					bool.TryParse(currentNode.InnerText, out result);
					this.SeriesName = result;
					continue;
				}
				else if (currentNode.Name.Equals("ThumbnailPath", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.ThumbnailPath = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("VignettePath", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.VignettePath = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("Season", StringComparison.OrdinalIgnoreCase))
				{
					int result = -1;
					int.TryParse(currentNode.InnerText, out result);
					this.Season = result;
					continue;
				}
			}
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">Name fo the property that changed its value.</param>
		private void RaisePropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
