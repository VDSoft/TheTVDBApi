// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

namespace TVDB.Model
{
	using System;
	using System.Collections.ObjectModel;
	using System.Xml;


	/// <summary>
	/// A Series with all details and episodes.
	/// </summary>
	public class Series : SeriesElement, Interfaces.IXmlSerializer
	{
		/// <summary>
		/// Name of the <see cref="Banner"/> property.
		/// </summary>
		private const string BannerName = "Banner";

		/// <summary>
		/// Name of the <see cref="Zap2ItId"/> property.
		/// </summary>
		private const string Zap2ItIdName = "Zap2ItId";

		/// <summary>
		/// Name of the <see cref="HasEpisodes"/> property.
		/// </summary>
		private const string HasEpisodesName = "HasEpisodes";

		/// <summary>
		/// Name of the <see cref="Episodes"/> property.
		/// </summary>
		private const string EpisodesName = "Episodes";

		/// <summary>
		/// Name of the <see cref="SeriesId"/> property.
		/// </summary>
		private const string SeriesIdName = "SeriesId";

		/// <summary>
		/// Name of the <see cref="Actorts"/> property.
		/// </summary>
		private const string ActortsName = "Actorts";

		/// <summary>
		/// Name of the <see cref="AirsDayOfWeel"/> property.
		/// </summary>
		private const string AirsDayOfWeelName = "AirsDayOfWeel";

		/// <summary>
		/// Name of the <see cref="AirsTime"/> property.
		/// </summary>
		private const string AirsTimeName = "AirsTime";

		/// <summary>
		/// Name of the <see cref="ContentRating"/> property.
		/// </summary>
		private const string ContentRatingName = "ContentRating";

		/// <summary>
		/// Name of the <see cref="Genre"/> property.
		/// </summary>
		private const string GenreName = "Genre";

		/// <summary>
		/// Name of the <see cref="Network"/> property.
		/// </summary>
		private const string NetworkName = "Network";

		/// <summary>
		/// Name of the <see cref="NetworkId"/> property.
		/// </summary>
		private const string NetworkIdName = "NetworkId";

		/// <summary>
		/// Name of the <see cref="RatingCount"/> property.
		/// </summary>
		private const string RatingCountName = "RatingCount";

		/// <summary>
		/// Name of the <see cref="Rating"/> property.
		/// </summary>
		private const string RatingName = "Rating";

		/// <summary>
		/// Name of the <see cref="Runtime"/> property.
		/// </summary>
		private const string RuntimeName = "Runtime";

		/// <summary>
		/// Name of the <see cref="Status"/> property.
		/// </summary>
		private const string StatusName = "Status";

		/// <summary>
		/// Name of the <see cref="AddedDate"/> property.
		/// </summary>
		private const string AddedDateName = "AddedDate";

		/// <summary>
		/// Name of the <see cref="AddedByUserId"/> property.
		/// </summary>
		private const string AddedByUserIdName = "AddedByUserId";

		/// <summary>
		/// Name of the <see cref="FanArt"/> property.
		/// </summary>
		private const string FanArtName = "FanArt";

		/// <summary>
		/// Name of the <see cref="LastUpdated"/> property.
		/// </summary>
		private const string LastUpdatedName = "LastUpdated";

		/// <summary>
		/// Name of the <see cref="Poster"/> property.
		/// </summary>
		private const string PosterName = "Poster";

		/// <summary>
		/// Name of the <see cref="TMSWanted"/> property.
		/// </summary>
		private const string TMSWantedName = "TMSWanted";

		/// <summary>
		/// Name of the <see cref="ActorCollection"/> property.
		/// </summary>
		private const string ActorCollectionName = "ActorCollection";

		/// <summary>
		/// Path of the banner for the series.
		/// </summary>
		private string bannerPath = null;

		/// <summary>
		/// Zap2It id of the series.
		/// </summary>
		private string zap2ItId = null;

		/// <summary>
		/// Value indicating whether the series has episodes assigend or not.
		/// </summary>
		private bool hasEpisodes = false;

		/// <summary>
		/// Collection of all assigned episodes.
		/// </summary>
		private ObservableCollection<Episode> episodes = new ObservableCollection<Episode>();

		/// <summary>
		/// Series ID.
		/// </summary>
		private int seriesId = -1;

		/// <summary>
		/// All actors of the series.
		/// </summary>
		private string actors = null;

		/// <summary>
		/// Day the series is aired.
		/// </summary>
		private string airsDayOfWeek = null;

		/// <summary>
		/// Time the series is aired.
		/// </summary>
		private string airsTime = null;

		/// <summary>
		/// The content rating of the series.
		/// </summary>
		private string contentRating = null;

		/// <summary>
		/// The genre of the series.
		/// </summary>
		private string genre = null;

		/// <summary>
		/// The network name that aires the series.
		/// </summary>
		private string network = null;

		/// <summary>
		/// Id of the network.
		/// </summary>
		private int networkId = -1;

		/// <summary>
		/// Count of rates.
		/// </summary>
		private int ratingCount = -1;

		/// <summary>
		/// Rating fo the series.
		/// </summary>
		private double rating = -1.0;

		/// <summary>
		/// Runtime of the series.
		/// </summary>
		private double runtime = -1.0;

		/// <summary>
		/// Status of the series.
		/// </summary>
		private string status = null;

		/// <summary>
		/// Date the series was added to the system.
		/// </summary>
		private DateTime addedDate = DateTime.MinValue;

		/// <summary>
		/// Id of the user who added the series.
		/// </summary>
		private int addedByUserId = -1;

		/// <summary>
		/// Path of the fan art.
		/// </summary>
		private string fanArt = null;

		/// <summary>
		/// Last updated id.
		/// </summary>
		private long lastUpdated = -1;

		/// <summary>
		/// Path of the poster.
		/// </summary>
		private string poster = null;

		/// <summary>
		/// Value indicating whether the tms is wanted for the series or not.
		/// </summary>
		private bool tmsWanted = false;

		/// <summary>
		/// Collection of the actors of the series.
		/// </summary>
		private ObservableCollection<Actor> actorsCollection = new ObservableCollection<Actor>();

		/// <summary>
		/// Initializes a new instance of the <see cref="Series"/> class.
		/// </summary>
		public Series()
		{

		}

		/// <summary>
		/// Gets or sets the path of the banner for the series.
		/// </summary>
		public string Banner
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
				this.RaisePropertyChanged(BannerName);
			}
		}

		/// <summary>
		/// Gets or sets the Zap2It id of the series.
		/// </summary>
		public string Zap2ItId
		{
			get
			{
				return this.zap2ItId;
			}

			set
			{
				if (value == this.zap2ItId)
				{
					return;
				}

				this.zap2ItId = value;
				this.RaisePropertyChanged(Zap2ItIdName);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the series has episodes assigend or not.
		/// </summary>
		public bool HasEpisodes
		{
			get
			{
				return this.hasEpisodes;
			}

			set
			{
				if (value == this.hasEpisodes)
				{
					return;
				}

				this.hasEpisodes = value;
				this.RaisePropertyChanged(HasEpisodesName);
			}
		}

		/// <summary>
		/// Gets or sets a collection of all assigned episodes.
		/// </summary>
		public ObservableCollection<Episode> Episodes
		{
			get
			{
				return this.episodes;
			}

			set
			{
				if (value == this.episodes)
				{
					return;
				}

				this.episodes = value;
				this.RaisePropertyChanged(EpisodesName);
			}
		}

		/// <summary>
		/// Gets or sets the series id.
		/// </summary>
		public int SeriesId
		{
			get
			{
				return this.seriesId;
			}

			set
			{
				if (value == this.seriesId)
				{
					return;
				}

				this.seriesId = value;
				this.RaisePropertyChanged(SeriesIdName);
			}
		}

		/// <summary>
		/// Gets or sets all actors of the series.
		/// </summary>
		public string Actorts
		{
			get
			{
				return this.actors;
			}

			set
			{
				if (value == this.actors)
				{
					return;
				}

				this.actors = value;
				this.RaisePropertyChanged(ActortsName);
			}
		}

		/// <summary>
		/// Gets or sets the day the series is aired.
		/// </summary>
		public string AirsDayOfWeel
		{
			get
			{
				return this.airsDayOfWeek;
			}

			set
			{
				if (value == this.airsDayOfWeek)
				{
					return;
				}

				this.airsDayOfWeek = value;
				this.RaisePropertyChanged(AirsDayOfWeelName);
			}
		}

		/// <summary>
		/// Gets or sets the time the series is aired.
		/// </summary>
		public string AirsTime
		{
			get
			{
				return this.airsTime;
			}

			set
			{
				if (value == this.airsTime)
				{
					return;
				}

				this.airsTime = value;
				this.RaisePropertyChanged(AirsTimeName);
			}
		}

		/// <summary>
		/// Gets or sets the content rating of the series.
		/// </summary>
		public string ContentRating
		{
			get
			{
				return this.contentRating;
			}

			set
			{
				if (value == this.contentRating)
				{
					return;
				}

				this.contentRating = value;
				this.RaisePropertyChanged(ContentRatingName);
			}
		}

		/// <summary>
		/// Gets or sets the genre of the series.
		/// </summary>
		public string Genre
		{
			get
			{
				return this.genre;
			}

			set
			{
				if (value == this.genre)
				{
					return;
				}

				this.genre = value;
				this.RaisePropertyChanged(GenreName);
			}
		}

		/// <summary>
		/// Gets or sets the network name that aires the series.
		/// </summary>
		public string Network
		{
			get
			{
				return this.network;
			}

			set
			{
				if (value == this.network)
				{
					return;
				}

				this.network = value;
				this.RaisePropertyChanged(NetworkName);
			}
		}

		/// <summary>
		/// Gets or sets the id of the network.
		/// </summary>
		public int NetworkId
		{
			get
			{
				return this.networkId;
			}

			set
			{
				if (value == this.networkId)
				{
					return;
				}

				this.networkId = value;
				this.RaisePropertyChanged(NetworkIdName);
			}
		}

		/// <summary>
		/// Gets or sets the count of rates.
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
		/// Gets or sets the rating fo the series.
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
		/// Gets or sets the runtime of the series.
		/// </summary>
		public double Runtime
		{
			get
			{
				return this.runtime;
			}

			set
			{
				if (value == this.runtime)
				{
					return;
				}

				this.runtime = value;
				this.RaisePropertyChanged(RuntimeName);
			}
		}

		/// <summary>
		/// Gets or sets the status of the series.
		/// </summary>
		public string Status
		{
			get
			{
				return this.status;
			}

			set
			{
				if (value == this.status)
				{
					return;
				}

				this.status = value;
				this.RaisePropertyChanged(StatusName);
			}
		}

		/// <summary>
		/// Gets or sets the date the series was added to the system.
		/// </summary>
		public DateTime AddedDate
		{
			get
			{
				return this.addedDate;
			}

			set
			{
				if (value == this.addedDate)
				{
					return;
				}

				this.addedDate = value;
				this.RaisePropertyChanged(AddedDateName);
			}
		}

		/// <summary>
		/// Gets or sets the id of the user who added the series.
		/// </summary>
		public int AddedByUserId
		{
			get
			{
				return this.addedByUserId;
			}

			set
			{
				if (value == this.addedByUserId)
				{
					return;
				}

				this.addedByUserId = value;
				this.RaisePropertyChanged(AddedByUserIdName);
			}
		}

		/// <summary>
		/// Gets or sets the path of the fan art.
		/// </summary>
		public string FanArt
		{
			get
			{
				return this.fanArt;
			}

			set
			{
				if (value == this.fanArt)
				{
					return;
				}

				this.fanArt = value;
				this.RaisePropertyChanged(FanArtName);
			}
		}

		/// <summary>
		/// Gets or sets the last updated id.
		/// </summary>
		public long LastUpdated
		{
			get
			{
				return this.lastUpdated;
			}

			set
			{
				if (value == this.lastUpdated)
				{
					return;
				}

				this.lastUpdated = value;
				this.RaisePropertyChanged(LastUpdatedName);
			}
		}

		/// <summary>
		/// Gets or sets the path of the poster.
		/// </summary>
		public string Poster
		{
			get
			{
				return this.poster;
			}

			set
			{
				if (value == this.poster)
				{
					return;
				}

				this.poster = value;
				this.RaisePropertyChanged(PosterName);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the tms is wanted for the series or not.
		/// </summary>
		public bool TMSWanted
		{
			get
			{
				return this.tmsWanted;
			}

			set
			{
				if (value == this.tmsWanted)
				{
					return;
				}

				this.tmsWanted = value;
				this.RaisePropertyChanged(TMSWantedName);
			}
		}

		/// <summary>
		/// Gets or sets a collection of the actors of the series.
		/// </summary>
		public ObservableCollection<Actor> ActorCollection
		{
			get
			{
				return this.actorsCollection;
			}

			set
			{
				if (value == this.actorsCollection)
				{
					return;
				}

				this.actorsCollection = value;
				this.RaisePropertyChanged(ActorCollectionName);
			}
		}

		/// <summary>
		/// Deserializes the provided xml node.
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
		/// 		/// Xml document that contains all details of a series.
		/// 		/// </summary>
		/// 		private XmlDocument languageDoc = null;
		/// 		
		/// 		/// <summary>
		/// 		/// Initializes a new instance of the DocuClass class.
		/// 		/// </summary>
		/// 		public DocuClass(string extractionPath)
		/// 		{
		/// 			// load series xml.
		/// 			this.languageDoc = new XmlDocument();
		/// 			this.languageDoc.Load(System.IO.Path.Combine(this.extractionPath, string.Format("{0}.xml", this.Language)));
		/// 		}
		/// 		
		/// 		/// <summary>
		/// 		/// Deserializes all actors of the series.
		/// 		/// </summary>
		/// 		public Series DeserializeSeries()
		/// 		{
		/// 			Series series = new Series();
		/// 			
		/// 			// load the xml docs second child.
		/// 			XmlNode dataNode = this.languageDoc.ChildNodes[1];
		/// 
		/// 			// deserialize all episodes and series details.
		/// 			foreach (XmlNode currentNode in dataNode.ChildNodes)
		/// 			{
		/// 				if (currentNode.Name.Equals("Episode", StringComparison.OrdinalIgnoreCase))
		/// 				{
		/// 					Episode deserialized = new Episode();
		/// 					deserialized.Deserialize(currentNode);
		/// 
		/// 					series.AddEpisode(deserialized);
		/// 					continue;
		/// 				}
		/// 				else if (currentNode.Name.Equals("Series", StringComparison.OrdinalIgnoreCase))
		/// 				{
		/// 					series.Deserialize(currentNode);
		/// 					continue;
		/// 				}
		/// 			}
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public void Deserialize(System.Xml.XmlNode node)
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
				else if (currentNode.Name.Equals("SeriesID", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					int result = -1;
					int.TryParse(currentNode.InnerText, out result);
					this.SeriesId = result;
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
				else if (currentNode.Name.Equals("SeriesName", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Name = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("banner", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Banner = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("Overview", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Overview = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("FirstAired", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					this.FirstAired = DateTime.Parse(currentNode.InnerText);
					continue;
				}
				else if (currentNode.Name.Equals("IMDB_ID", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					this.IMDBId = currentNode.InnerText;
					continue;
				}
				else if (currentNode.Name.Equals("zap2it_id", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					this.Zap2ItId = currentNode.InnerText;
					continue;
				}
				else if (currentNode.Name.Equals("Actors", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Actorts = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("Airs_DayOfWeek", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					this.AirsDayOfWeel = currentNode.InnerText;
					continue;
				}
				else if (currentNode.Name.Equals("Airs_Time", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					this.AirsTime = currentNode.InnerText;
					continue;
				}
				else if (currentNode.Name.Equals("ContentRating", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.ContentRating = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("Genre", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					this.Genre = currentNode.InnerText;
					continue;
				}
				else if (currentNode.Name.Equals("Network", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					this.Network = currentNode.InnerText;
				}
				else if (currentNode.Name.Equals("NetworkID", StringComparison.OrdinalIgnoreCase))
				{
					int result = -1;
					int.TryParse(currentNode.InnerText, out result);
					this.NetworkId = result;
					continue;
				}
				else if (currentNode.Name.Equals("Rating", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

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
				else if (currentNode.Name.Equals("Runtime", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					double result = -1.0;
					double.TryParse(currentNode.InnerText, System.Globalization.NumberStyles.Number, cultureInfo, out result);
					this.Runtime = result;
					continue;
				}
				else if (currentNode.Name.Equals("Status", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					this.Status = currentNode.InnerText;
					continue;
				}
				else if (currentNode.Name.Equals("added", StringComparison.OrdinalIgnoreCase))
				{
					if (string.IsNullOrEmpty(currentNode.InnerText))
					{
						continue;
					}

					this.AddedDate = DateTime.Parse(currentNode.InnerText);
					continue;
				}
				else if (currentNode.Name.Equals("addedBy", StringComparison.OrdinalIgnoreCase))
				{
					int result = -1;
					int.TryParse(currentNode.InnerText, out result);
					this.AddedByUserId = result;
					continue;
				}
				else if (currentNode.Name.Equals("fanart", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.FanArt = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("lastupdated", StringComparison.OrdinalIgnoreCase))
				{
                    if (string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        continue;
                    }

					long result = -1;
					long.TryParse(currentNode.InnerText, out result);
					this.LastUpdated = result;
					continue;
				}
				else if (currentNode.Name.Equals("poster", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Poster = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("tms_wanted", StringComparison.OrdinalIgnoreCase))
				{
					int result = -1;
					int.TryParse(currentNode.InnerText, out result);

					this.TMSWanted = result > 0 ? true : false;
					continue;
				}

			}

			this.Initialize();
		}

		/// <summary>
		/// Adds the provided episode to the series.
		/// </summary>
		/// <param name="toAdd">Episode to add.</param>
		/// <exception cref="ArgumentNullException">Occurs when the provided <see cref="Episode"/> is null.</exception>
		public void AddEpisode(Episode toAdd)
		{
			if (toAdd == null)
			{
				throw new ArgumentNullException("toAdd", "Episode to add must not be null.");
			}

			this.Episodes.Add(toAdd);
		}

		/// <summary>
		/// Initializes the properties.
		/// </summary>
		private void Initialize()
		{
			if (this.Episodes.Count > 0)
			{
				this.HasEpisodes = true;
			}

			this.Actorts = this.PrepareText(this.actors);
			this.Genre = this.PrepareText(this.genre);
		}
	}
}
