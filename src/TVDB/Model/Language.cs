// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

namespace TVDB.Model
{
	using System;
	using System.ComponentModel;

	/// <summary>
	/// Defines the language of the series.
	/// </summary>
	public class Language : INotifyPropertyChanged, Interfaces.IXmlSerializer//, IComparable<Language>
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
		/// Name of the <see cref="Abbreviation"/> property.
		/// </summary>
		private const string AbbreviationName = "Abbreviation";

		/// <summary>
		/// Id of the language.
		/// </summary>
		private int id = 0;

		/// <summary>
		/// The name of the language.
		/// </summary>
		private string name = null;

		/// <summary>
		/// The abbreviation of the language.
		/// </summary>
		private string abbreviation = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="Language"/> class.
		/// </summary>
		public Language()
		{
		}

		/// <summary>
		/// Gets or sets the id of the language.
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
		/// Gets or sets the name of the language.
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
		/// Gets or sets the abbreviation of the language.
		/// </summary>
		public string Abbreviation
		{
			get
			{
				return this.abbreviation;
			}

			set
			{
				if (value == this.abbreviation)
				{
					return;
				}

				this.abbreviation = value;
				this.RaisePropertyChanged(AbbreviationName);
			}
		}

		/// <summary>
		/// Occures when a property changed its value.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

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
		/// 		/// Xml document that contains all banners.
		/// 		/// </summary>
		/// 		private XmlDocument languageDoc = null;
		/// 		
		/// 		/// <summary>
		/// 		/// Initializes a new instance of the DocuClass class.
		/// 		/// </summary>
		/// 		public DocuClass(string extractionPath)
		/// 		{
		/// 			// load actors xml.
		/// 			this.languageDoc = new XmlDocument();
		/// 			this.languageDoc.Load(System.IO.Path.Combine(this.extractionPath, "languages.xml"));
		/// 		}
		/// 		
		/// 		/// <summary>
		/// 		/// Deserializes all languages that are available.
		/// 		/// </summary>
		/// 		public List&#60;Language&#62; DeserializeLanguages()
		/// 		{
		/// 			List&#60;Language&#62; languages = new List&#60;Language&#62;();
		/// 			
		/// 			// load the xml docs second child.
		/// 			XmlNode languageNode = this.languageDoc.ChildNodes[1];
		/// 	
		/// 			// deserialize all languages.
		/// 			foreach (XmlNode currentNode in languageNode.ChildNodes)
		/// 			{
		/// 				Language deserialized = new Language();
		/// 				deserialized.Deserialize(currentNode);
		/// 
		/// 				languages.Add(deserialized);
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
				throw new ArgumentNullException("node", "Provided node must not be null or empty");
			}

			foreach (System.Xml.XmlNode currentNode in node.ChildNodes)
			{
				if (currentNode.Name.Equals("name", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Name = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("abbreviation", StringComparison.OrdinalIgnoreCase))
				{
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Abbreviation = currentNode.InnerText; 
                    }
					continue;
				}
				else if (currentNode.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
				{
					int result = 0;
					int.TryParse(currentNode.InnerText, out result);
					this.Id = result;
					continue;
				}
			}
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanged"/> event.
		/// </summary>
		/// <param name="propertyName">Name fo the property which changed its value.</param>
		private void RaisePropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
