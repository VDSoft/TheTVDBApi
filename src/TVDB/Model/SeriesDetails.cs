// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

namespace TVDB.Model
{
	using System;
	using System.Collections.Generic;
	using System.Xml;

	/// <summary>
	/// Contains all details for the requested series like Actors, Banners and all episodes of the series.
	/// </summary>
	public class SeriesDetails : IDisposable
	{
		/// <summary>
		/// Xml document that contains all actors.
		/// </summary>
		private XmlDocument actorsDoc = null;

		/// <summary>
		/// Xml document that contains all banners.
		/// </summary>
		private XmlDocument bannersDoc = null;

		/// <summary>
		/// Xml document that contains all details of a series.
		/// </summary>
		private XmlDocument languageDoc = null;

		/// <summary>
		/// The Language.
		/// </summary>
		private string language = null;

		/// <summary>
		/// The Actors.
		/// </summary>
		private List<Actor> actors = null;

		/// <summary>
		/// The Series.
		/// </summary>
		private Series series;

		/// <summary>
		/// The Banners.
		/// </summary>
		private List<Banner> banners;

		/// <summary>
		/// Path of the extracted files.
		/// </summary>
		private string extractionPath = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="SeriesDetails"/> class.
		/// </summary>
		/// <param name="extractionPath">Path of the extracted files.</param>
		/// <param name="language">Language of the series.</param>
		/// <exception cref="System.IO.DirectoryNotFoundException">Occurs when the provided extraction path doesn't exists.</exception>
		/// <exception cref="ArgumentNullException">Occurs when to provided language is null or empty.</exception>
		public SeriesDetails(string extractionPath, string language)
		{
			System.IO.DirectoryInfo dirInfo = new System.IO.DirectoryInfo(extractionPath);
			if (!dirInfo.Exists)
			{
				throw new System.IO.DirectoryNotFoundException(string.Format("The directory \"{0}\" could not be found.", dirInfo.FullName));
			}

			if (string.IsNullOrEmpty(language))
			{
				throw new ArgumentNullException("language", "Provided language must not be null or empty.");
			}

			this.extractionPath = extractionPath;
			this.Language = language;

			// load actors xml.
			this.actorsDoc = new XmlDocument();
			this.actorsDoc.Load(System.IO.Path.Combine(this.extractionPath, "actors.xml"));

			// load banners xml.
			this.bannersDoc = new XmlDocument();
			this.bannersDoc.Load(System.IO.Path.Combine(this.extractionPath, "banners.xml"));

			// load series xml.
			this.languageDoc = new XmlDocument();
			this.languageDoc.Load(System.IO.Path.Combine(this.extractionPath, string.Format("{0}.xml", this.Language)));
		}

		/// <summary>
		/// Gets or sets the Language property.
		/// </summary>
		public string Language
		{
			get
			{
				return this.language;
			}

			private set
			{
				if (value == this.language)
				{
					return;
				}

				this.language = value;
			}
		}

		/// <summary>
		/// Gets or sets the Actors property.
		/// </summary>
		public List<Actor> Actors
		{
			get
			{
				if (this.actors == null)
				{
					this.DeserializeActors();
				}

				return this.actors;
			}

			private set
			{
				if (value == this.actors)
				{
					return;
				}

				this.actors = value;
			}
		}

		/// <summary>
		/// Gets or sets the Series property.
		/// </summary>
		public Series Series
		{
			get
			{
				if (this.series == null)
				{
					this.DeserializeSeries();
				}

				return this.series;
			}

			private set
			{
				if (value == this.series)
				{
					return;
				}

				this.series = value;
			}
		}

		/// <summary>
		/// Gets or sets the Banners property.
		/// </summary>
		public List<Banner> Banners
		{
			get
			{
				if (this.banners == null)
				{
					this.DeserializeBanners();
				}

				return this.banners;
			}

			set
			{
				if (value == this.banners)
				{
					return;
				}

				this.banners = value;
			}
		}

		/// <summary>
		/// Releases all resources of the class.
		/// </summary>
		public void Dispose()
		{
			this.language = null;
			this.extractionPath = null;

			this.actorsDoc = null;
			this.bannersDoc = null;
			this.languageDoc = null;

			if (this.Actors != null)
			{
				this.actors.Clear();
				this.actors = null;
			}

			if (this.Banners != null)
			{
				this.banners.Clear();
				this.banners = null;
			}

			if (this.Series != null)
			{
				this.series = null;
			}
		}

		/// <summary>
		/// Deserializes all actors of the series.
		/// </summary>
		private void DeserializeActors()
		{
			if (this.actorsDoc == null || this.actorsDoc.ChildNodes.Count == 0)
			{
				return;
			}

			this.Actors = new List<Actor>();
			XmlNode actorsNode = this.actorsDoc.ChildNodes[1];

			foreach (XmlNode currentNode in actorsNode)
			{
				if (currentNode.Name.Equals("Actor", StringComparison.OrdinalIgnoreCase))
				{
					Actor deserializes = new Actor();
					deserializes.Deserialize(currentNode);

					this.Actors.Add(deserializes);
				}
			}
		}

		/// <summary>
		/// Deserializes all banners of the series.
		/// </summary>
		private void DeserializeBanners()
		{
			if (this.bannersDoc == null || this.bannersDoc.ChildNodes.Count == 0)
			{
				return;
			}

			this.Banners = new List<Banner>();
			XmlNode bannersNode = this.bannersDoc.ChildNodes[1];

			foreach (XmlNode currentNode in bannersNode.ChildNodes)
			{
				if (currentNode.Name.Equals("Banner", StringComparison.OrdinalIgnoreCase))
				{
					Banner deserialized = new Banner();
					deserialized.Deserialize(currentNode);

					this.Banners.Add(deserialized);
				}
			}
		}

		/// <summary>
		/// Deserializes the series.
		/// </summary>
		private void DeserializeSeries()
		{
			if (this.languageDoc == null || this.languageDoc.ChildNodes.Count == 0)
			{
				return;
			}

			this.Series = new Series();
			if (this.Actors != null && this.Actors.Count > 0)
			{
				this.Series.ActorCollection = new System.Collections.ObjectModel.ObservableCollection<Actor>(this.Actors);
			}

			XmlNode dataNode = this.languageDoc.ChildNodes[1];

			foreach (XmlNode currentNode in dataNode.ChildNodes)
			{
				if (currentNode.Name.Equals("Episode", StringComparison.OrdinalIgnoreCase))
				{
					Episode deserialized = new Episode();
					deserialized.Deserialize(currentNode);

					this.Series.AddEpisode(deserialized);
					continue;
				}
				else if (currentNode.Name.Equals("Series", StringComparison.OrdinalIgnoreCase))
				{
					this.Series.Deserialize(currentNode);
					continue;
				}
			}
		}
	}
}
