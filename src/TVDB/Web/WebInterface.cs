// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

namespace TVDB.Web
{
	using Model;
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.IO.Compression;
	using System.Linq;
	using System.Net;
	using System.Threading.Tasks;
	using System.Xml;
	
	/// <summary>
	/// Communication with the XML interface of the TVDB.
	/// </summary>
	public class WebInterface : TVDB.Web.ITvDb
	{
		/// <summary>
		/// Api key for the application.
		/// </summary>
		private readonly string APIKey = null;

        /// <summary>
        /// Directory for writing zip and extracted files
        /// </summary>
	    public string FileDirectory { get; set; }

	    /// <summary>
	    /// Path of the full series zip file.
	    /// </summary>
	    private string LoadedSeriesPath
	    {
	        get { return Path.Combine(FileDirectory, "loaded.zip"); }
	    }

	    /// <summary>
		/// Default mirror site to connect to the api.
		/// </summary>
		private Mirror defaultMirror = null;

		/// <summary>
		/// WebClient to download the file.
		/// </summary>
		private WebClient client = new WebClient();

		/// <summary>
		/// Prevents a default instance of the <see cref="WebInterface"/> class from being created.
		/// </summary>
		private WebInterface()
		{ 
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WebInterface"/> class, using the provided API key.
		/// </summary>
		/// <param name="apiKey">API key obtained from TheTVDB.com to access the XML api.</param>
		public WebInterface(string apiKey)
		{
			this.APIKey = apiKey;
		    this.FileDirectory = string.Empty;
		}
		
		/// <summary>
		/// Get all available mirrors.
		/// </summary>
		/// <returns>A Collection of mirrors.</returns>
		/// <exception cref="Exception">Occurs when the main source of the TheTVDB seems to be offline.</exception>
		/// <example>Shows how to request all available mirrors.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets all mirrors that are available.
		/// 		/// </summary>
		/// 		public List&#60;Mirror&#62; GetAllMirrors()
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			List&#60;Mirror&#62; mirrors = await instance.GetMirrors();
		/// 
		/// 			return mirrors
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<List<Mirror>> GetMirrors()
		{
			string url = "http://thetvdb.com/api/{0}/mirrors.xml";

			byte[] result = null;

			try
			{
				 result = this.client.DownloadData(string.Format(url, this.APIKey));
			}
			catch (Exception ex)
			{
				throw new Exception("Source seems to be offline.", ex);
			}

			MemoryStream resultStream = new MemoryStream(result);
			XmlDocument doc = new XmlDocument();
			doc.Load(resultStream);

			XmlNode dataNode = doc.ChildNodes[1];

			List<Mirror> receivedMirrors = new List<Mirror>();

			foreach (XmlNode current in dataNode)
			{
				Mirror deserialized = new Mirror();
				deserialized.Deserialize(current);

				if (this.defaultMirror == null)
				{
					if (deserialized.ContainsBannerFile && 
						deserialized.ContainsXmlFile && 
						deserialized.ContainsZipFile)
					{
						this.defaultMirror = deserialized;
					}
				}

				receivedMirrors.Add(deserialized);
			}

			return receivedMirrors;
		}

		/// <summary>
		/// Gets a list of all available languages.
		/// </summary>
		/// <returns>Collection of all available languages.</returns>
		/// <example>Shows how to get all languages.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets all mirrors that are available.
		/// 		/// </summary>
		/// 		public List&#60;Language&#62; GetAllLanguages(Mirror mirror)
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			List&#60;Language&#62; languages = await instance.GetLanguages(mirror);
		/// 
		/// 			return languages
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<List<Language>> GetLanguages()
		{
			// get the default mirror.
			if (this.defaultMirror == null)
			{
				await this.GetMirrors();
			}

			return this.GetLanguages(this.defaultMirror).Result;
		}

		/// <summary>
		/// Gets a list of all available languages.
		/// </summary>
		/// <param name="mirror">Mirror to use.</param>
		/// <returns>Collection of all available languages.</returns>
		/// <example>Shows how to get all languages.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets all mirrors that are available.
		/// 		/// </summary>
		/// 		public List&#60;Language&#62; GetAllLanguages(Mirror mirror)
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			List&#60;Language&#62; languages = await instance.GetLanguages(mirror);
		/// 
		/// 			return languages
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<List<Language>> GetLanguages(Mirror mirror)
		{
			if (mirror == null)
			{
				return null;
			}

			string url = "{0}/api/{1}/languages.xml";

			byte[] result = this.client.DownloadData(string.Format(url, mirror.Address, APIKey));
			MemoryStream resultStream = new MemoryStream(result);

			XmlDocument doc = new XmlDocument();
			doc.Load(resultStream);
			XmlNode dataNode = doc.ChildNodes[1];

			List<Language> receivedLanguages = new List<Language>();

			foreach (XmlNode currentNode in dataNode.ChildNodes)
			{
				Language deserialized = new Language();
				deserialized.Deserialize(currentNode);

				receivedLanguages.Add(deserialized);
			}

			return receivedLanguages.OrderBy(x => x.Name).ToList<Language>();
		}

		/// <summary>
		/// Gets all series that match with the provided name.
		/// </summary>
		/// <param name="name">Name of the series.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <remarks>
		/// When calling this method a default language, which is english will be used to find all series that match the name.
		/// </remarks>
		/// <returns>List of series that matches the provided name.</returns>
		/// <example>Shows how to get a series by name.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets series by name.
		/// 		/// </summary>
		/// 		public List&#60;Series&#62; GetSeries(string name, Mirror mirror, Language language)
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			List&#60;Series&#62; series = await instance.GetSeriesByName(name, language.Abbreviation, mirror);
		/// 
		/// 			return series;
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<List<Series>> GetSeriesByName(string name, Mirror mirror)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}

			if (mirror == null)
			{
				return null;
			}

			return this.GetSeriesByName(name, "en", mirror).Result;
		}

		/// <summary>
		/// Gets all series that match with the provided name.
		/// </summary>
		/// <param name="name">Name of the series.</param>
		/// <param name="languageAbbreviation">Abbreviation of the language to search the series.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <returns>List of series that matches the provided name.</returns>
		/// <example>Shows how to get a series by name.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets series by name.
		/// 		/// </summary>
		/// 		public List&#60;Series&#62; GetSeries(string name, Mirror mirror, Language language)
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			List&#60;Series&#62; series = await instance.GetSeriesByName(name, language.Abbreviation, mirror);
		/// 
		/// 			return series;
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<List<Series>> GetSeriesByName(string name, string languageAbbreviation, Mirror mirror)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}

			if (mirror == null)
			{
				return null;
			}

			if (string.IsNullOrEmpty(languageAbbreviation))
			{
				return null;
			}

			string url = "{0}/api/GetSeries.php?seriesname={1}&language={2}";

			byte[] result = this.client.DownloadData(string.Format(url, mirror.Address, name, languageAbbreviation));
			MemoryStream resultStream = new MemoryStream(result);

			XmlDocument doc = new XmlDocument();
			doc.Load(resultStream);
			XmlNode dataNode = doc.ChildNodes[1];

			List<Series> series = new List<Series>();
			foreach (XmlNode currentNode in dataNode.ChildNodes)
			{
				Series deserialized = new Series();
				deserialized.Deserialize(currentNode);

				series.Add(deserialized);
			}

			return series.Where(x => x.Language.Equals(languageAbbreviation)).ToList<Series>();
		}

		/// <summary>
		/// Gets the series by either its imdb or zap2it id.
		/// </summary>
		/// <param name="imdbId">IMDB id</param>
		/// <param name="zap2Id">Zap2It id</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <returns>The Series belonging to the provided id.</returns>
		/// <remarks>
		/// It is not allowed to provide both imdb and zap2it id, this will lead to a null value as return value.
		/// </remarks>
		/// <example>Shows how the get a series by imdb and zap2 Id.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets series by id.
		/// 		/// </summary>
		/// 		public List&#60;Series&#62; GetSeries(string imdbId, string zap2Id, Mirror mirror, Language language)
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			List&#60;Series&#62; series = await instance.GetSeriesByRemoteId(imdbId, zap2Id, language.Abbreviation, mirror);
		/// 
		/// 			return series;
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<List<Series>> GetSeriesByRemoteId(string imdbId, string zap2Id, Mirror mirror)
		{
			if (string.IsNullOrEmpty(imdbId) && string.IsNullOrEmpty(zap2Id))
			{
				return null;
			}

			if (!string.IsNullOrEmpty(imdbId) && !string.IsNullOrEmpty(zap2Id))
			{
				return null;
			}

			if (mirror == null)
			{
				return null;
			}

			return this.GetSeriesByRemoteId(imdbId, zap2Id, "en", mirror).Result;
		}

		/// <summary>
		/// Gets the series by either its imdb or zap2it id.
		/// </summary>
		/// <param name="imdbId">IMDB id</param>
		/// <param name="zap2Id">Zap2It id</param>
		/// <param name="languageAbbreviation">The language abbreviation.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <returns>The Series belonging to the provided id.</returns>
		/// <remarks>
		/// It is not allowed to provide both imdb and zap2it id, this will lead to a null value as return value.
		/// </remarks>
		/// <example>Shows how the get a series by imdb and zap2 Id.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets series by id.
		/// 		/// </summary>
		/// 		public List&#60;Series&#62; GetSeries(string imdbId, string zap2Id, Mirror mirror, Language language)
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			List&#60;Series&#62; series = await instance.GetSeriesByRemoteId(imdbId, zap2Id, language.Abbreviation, mirror);
		/// 
		/// 			return series;
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<List<Series>> GetSeriesByRemoteId(string imdbId, string zap2Id, string languageAbbreviation, Mirror mirror)
		{
			if (string.IsNullOrEmpty(imdbId) && string.IsNullOrEmpty(zap2Id))
			{
				return null;
			}

			if (!string.IsNullOrEmpty(imdbId) && !string.IsNullOrEmpty(zap2Id))
			{
				return null;
			}

			if (mirror == null)
			{
				return null;
			}

			if (string.IsNullOrEmpty(languageAbbreviation))
			{
				return null;
			}

			string url = "{0}/api/GetSeriesByRemoteID.php?imdbid={1}&language={2}&zap2it={3}";

			byte[] result = this.client.DownloadData(string.Format(url, mirror.Address, imdbId, languageAbbreviation, zap2Id));
			MemoryStream resultStream = new MemoryStream(result);

			XmlDocument doc = new XmlDocument();
			doc.Load(resultStream);

			XmlNode dataNode = doc.ChildNodes[1];

			List<Series> series = new List<Series>();
			foreach (XmlNode currentNode in dataNode.ChildNodes)
			{
				Series deserialized = new Series();
				deserialized.Deserialize(currentNode);

				series.Add(deserialized);
			}

			return series;
		}

		/// <summary>
		/// Gets all details of the series.
		/// </summary>
		/// <param name="id">Id of the series.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <returns>All details of the series.</returns>
		/// <example>Shows how the get all details of a series by its id.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets all details of a series.
		/// 		/// </summary>
		/// 		public SeriesDetails GetSeries(int id, Mirror mirror, Language language)
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			SeriesDetails details = await instance.GetFullSeriesById(id, language.Abbreviation, mirror);
		/// 
		/// 			return details;
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<SeriesDetails> GetFullSeriesById(int id, Mirror mirror)
		{
			if (id == 0)
			{
				return null;
			}

			if (mirror == null)
			{
				return null;
			}

			return this.GetFullSeriesById(id, "en", mirror).Result;
		}

		/// <summary>
		/// Gets all details of the series.
		/// </summary>
		/// <param name="id">Id of the series.</param>
		/// <param name="languageAbbreviation">The language abbreviation.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <returns>All details of the series.</returns>
		/// <example>Shows how the get all details of a series by its id.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Gets all details of a series.
		/// 		/// </summary>
		/// 		public SeriesDetails GetSeries(int id, Mirror mirror, Language language)
		/// 		{
		///				string apiKey = "ABCD12345";
		/// 			TVDB.Web.ITvDb instance = new TVDB.Web.WebInterface(apiKey);
		/// 			SeriesDetails details = await instance.GetFullSeriesById(id, language.Abbreviation, mirror);
		/// 
		/// 			return details;
		/// 		}
		/// 	}
		/// }
		/// </code>
		/// </example>
		public async Task<SeriesDetails> GetFullSeriesById(int id, string languageAbbreviation, Mirror mirror)
		{
			if (id == 0)
			{
				return null;
			}

			if (string.IsNullOrEmpty(languageAbbreviation))
			{
				return null;
			}

			if (mirror == null)
			{
				return null;
			}

			string url = "{0}/api/{1}/series/{2}/all/{3}.zip";
			byte[] result = this.client.DownloadData(string.Format(url, mirror.Address, this.APIKey, id, languageAbbreviation));

			// store the zip file.
			using (FileStream zipFile = new FileStream(LoadedSeriesPath, FileMode.Create, FileAccess.Write))
			{
				zipFile.Write(result, 0, (int)result.Length);
				zipFile.Flush();
				zipFile.Close();
			}

		    DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(FileDirectory, "extraction"));
			if (!dirInfo.Exists)
			{
				dirInfo.Create();
			}


			// extract the file.
			using (ZipArchive archive = ZipFile.OpenRead(LoadedSeriesPath))
			{
				foreach (ZipArchiveEntry entry in archive.Entries)
				{
					entry.ExtractToFile(Path.Combine(dirInfo.FullName, entry.Name), true);
				}
			}

			SeriesDetails details = new SeriesDetails(dirInfo.FullName, languageAbbreviation);

			return details;
		}
	}
}
