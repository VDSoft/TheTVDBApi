// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

namespace TVDB.Model
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Base for the elements of a series.
    /// </summary>
    public abstract class SeriesElement : INotifyPropertyChanged
    {
        /// <summary>
        /// Name of the <see cref="Id"/> property.
        /// </summary>
        private const string IdName = "Id";

        /// <summary>
        /// Name of the <see cref="Name"/> property.
        /// </summary>
        private const string NameName = "Name";

        /// <summary>
        /// Name of the <see cref="Language"/> property.
        /// </summary>
        private const string LanguageName = "Language";

        /// <summary>
        /// Name of the <see cref="Overview"/> property.
        /// </summary>
        private const string OverviewName = "Overview";

        /// <summary>
        /// Name of the <see cref="FirstAired"/> property.
        /// </summary>
        private const string FirstAiredName = "FirstAired";

        /// <summary>
        /// Name of the <see cref="IMDBId"/> property.
        /// </summary>
        private const string IMDBIdName = "IMDBId";

        /// <summary>
        /// Id of the element.
        /// </summary>
        private int id = 0;

        /// <summary>
        /// Name of the element.
        /// </summary>
        private string name = null;

        /// <summary>
        /// Language of the element.
        /// </summary>
        private string language = null;

        /// <summary>
        /// The overview of the series.
        /// </summary>
        private string overview = null;

        /// <summary>
        /// Date the series was first aired.
        /// </summary>
        private DateTime firstAired = DateTime.MinValue;

        /// <summary>
        /// IMDB ID fo the series.
        /// </summary>
        private string imdbId = null;
        
        /// <summary>
        /// Occurs when a property changes its value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the id of the element.
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
        /// Gets or sets the name of the element.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (value == this.name)
                {
                    return;
                }

                this.name = value;
                this.RaisePropertyChanged(NameName);
            }
        }

        /// <summary>
        /// Gets or sets the language of the element.
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
        /// Gets or sets the overview of the series.
        /// </summary>
        public string Overview
        {
            get
            {
                return this.overview;
            }

            set
            {
                if (value == this.overview)
                {
                    return;
                }

                this.overview = value;
                this.RaisePropertyChanged(OverviewName);
            }
        }

        /// <summary>
        /// Gets or sets the date the series was first aired.
        /// </summary>
        public DateTime FirstAired
        {
            get
            {
                return this.firstAired;
            }

            set
            {
                if (value == this.firstAired)
                {
                    return;
                }

                this.firstAired = value;
                this.RaisePropertyChanged(FirstAiredName);
            }
        }

        /// <summary>
        /// Gets or sets the IMDB ID fo the series.
        /// </summary>
        public string IMDBId
        {
            get
            {
                return this.imdbId;
            }

            set
            {
                if (value == this.imdbId)
                {
                    return;
                }

                this.imdbId = value;
                this.RaisePropertyChanged(IMDBIdName);
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed its value.</param>
        protected void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Removes all "|" inside the provided text.
        /// </summary>
        /// <param name="text">Text to prepare.</param>
        /// <returns>Clean text.</returns>
        protected string PrepareText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }

            string result = string.Empty;

            if (text.Contains("|"))
            {
                result = text.Replace("|", ", ");
            }
            else
            {
                return text;
            }

            if (result.StartsWith(", "))
            {
                result = result.Remove(0, 1).Trim();
            }

            if (result.EndsWith(","))
            {
                result = result.Remove(result.LastIndexOf(","), 1).Trim();
            }

            return result;
        }
    }
}
