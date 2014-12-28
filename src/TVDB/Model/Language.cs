// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

namespace TVDB.Model
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;


	/// <summary>
	/// TODO: Update Docu.
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
		private string name = string.Empty;

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
					this.Name = currentNode.InnerText;
					continue;
				}
				else if (currentNode.Name.Equals("abbreviation", StringComparison.OrdinalIgnoreCase))
				{
					this.Abbreviation = currentNode.InnerText;
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
		/// Compares the <see cref="Id"/> of the provided Language with this one.
		/// </summary>
		/// <param name="other">Language object to compare.</param>
		/// <returns>
		/// 0: Ids are euqal.
		/// -1: Provided id is smaller than this one.
		/// 1: Provided id is greater than this one.
		/// </returns>
		////public int CompareTo(Language other)
		////{
		////	if (this.Name < other.Name)
		////	{
		////		return -1;
		////	}
		////	else if (this.Name > other.Name)
		////	{
		////		return 1;
		////	}
		////	else
		////	{
		////		return 0;
		////	}
		////}

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
