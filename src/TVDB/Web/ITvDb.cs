// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TVDB.Model;

namespace TVDB.Web
{
	/// <summary>
	/// Interface which defines all methods needed to communicate with the XML api from TheTVDB.com.
	/// </summary>
	/// <seealso cref="WebInterface"/>
	public interface ITvDb
	{
		/// <summary>
		/// Gets all details of the series.
		/// </summary>
		/// <param name="id">Id of the series.</param>
		/// <param name="languageAbbreviation">The language abbreviation.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <returns>All details of the series.</returns>
		Task<SeriesDetails> GetFullSeriesById(int id, string languageAbbreviation, Mirror mirror);

		/// <summary>
		/// Gets all details of the series.
		/// </summary>
		/// <param name="id">Id of the series.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <returns>All details of the series.</returns>
		Task<SeriesDetails> GetFullSeriesById(int id, Mirror mirror);

		/// <summary>
		/// Gets a list of all available languages.
		/// </summary>
		/// <returns>Collection of all available languages.</returns>
		Task<List<Language>> GetLanguages();

		/// <summary>
		/// Gets a list of all available languages.
		/// </summary>
		/// <param name="mirror">Mirror to use.</param>
		/// <returns>Collection of all available languages.</returns>
		Task<List<Language>> GetLanguages(Mirror mirror);

		/// <summary>
		/// Get all available mirrors.
		/// </summary>
		/// <returns>A Collection of mirrors.</returns>
		/// <exception cref="Exception">Occurs when the main source of the TheTVDB seems to be offline.</exception>
		Task<List<Mirror>> GetMirrors();

		/// <summary>
		/// Gets all series that match with the provided name.
		/// </summary>
		/// <param name="name">Name of the series.</param>
		/// <param name="languageAbbreviation">Abbreviation of the language to search the series.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <returns>List of series that matches the provided name.</returns>
		Task<List<Series>> GetSeriesByName(string name, string languageAbbreviation, Mirror mirror);

		/// <summary>
		/// Gets all series that match with the provided name.
		/// </summary>
		/// <param name="name">Name of the series.</param>
		/// <param name="mirror">The mirror to use.</param>
		/// <remarks>
		/// When calling this method a default language, which is English will be used to find all series that match the name.
		/// </remarks>
		/// <returns>List of series that matches the provided name.</returns>
		Task<List<Series>> GetSeriesByName(string name, Mirror mirror);

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
		Task<List<Series>> GetSeriesByRemoteId(string imdbId, string zap2Id, string languageAbbreviation, Mirror mirror);

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
		Task<List<Series>> GetSeriesByRemoteId(string imdbId, string zap2Id, Mirror mirror);
	}
}
