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
    /// An Actor.
    /// </summary>
    public class Actor : INotifyPropertyChanged, Interfaces.IXmlSerializer, IComparable<Actor>
    {
        /// <summary>
        /// Name of the <see cref="Id"/> property.
        /// </summary>
        private const string IdName = "Id";

        /// <summary>
        /// Name of the <see cref="ImagePath"/> property.
        /// </summary>
        private const string ImagePathName = "ImagePath";

        /// <summary>
        /// Name of the <see cref="Name"/> property.
        /// </summary>
        private const string NameName = "Name";

        /// <summary>
        /// Name of the <see cref="Role"/> property.
        /// </summary>
        private const string RoleName = "Role";

        /// <summary>
        /// Name of the <see cref="SortOrder"/> property.
        /// </summary>
        private const string SortOrderName = "SortOrder";

        /// <summary>
        /// Id of the actor.
        /// </summary>
        private int id = 0;

        /// <summary>
        /// Path of the actors image.
        /// </summary>
        private string imagePath = null;

        /// <summary>
        /// Real name of the actor.
        /// </summary>
        private string name = null;

        /// <summary>
        /// Role the actor is playing.
        /// </summary>
        private string role = null;

        /// <summary>
        /// Number the actors are sorted.
        /// </summary>
        private int sortOrder = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        public Actor()
        {

        }

        /// <summary>
        /// Occurs when a property changes its value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the id of the actor.
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
        /// Gets or sets the path of the actors image.
        /// </summary>
        public string ImagePath
        {
            get
            {
                return this.imagePath;
            }

            set
            {
                if (value == this.imagePath)
                {
                    return;
                }

                this.imagePath = value;
                this.RaisePropertyChanged(ImagePathName);
            }
        }

        /// <summary>
        /// Gets or sets the real name of the actor.
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
        /// Gets or sets the role the actor is playing.
        /// </summary>
        public string Role
        {
            get
            {
                return this.role;
            }

            set
            {
                if (value == this.role)
                {
                    return;
                }

                this.role = value;
                this.RaisePropertyChanged(RoleName);
            }
        }

        /// <summary>
        /// Gets or sets the number the actors are sorted.
        /// </summary>
        public int SortOrder
        {
            get
            {
                return this.sortOrder;
            }

            set
            {
                if (value == this.sortOrder)
                {
                    return;
                }

                this.sortOrder = value;
                this.RaisePropertyChanged(SortOrderName);
            }
        }

        /// <summary>
        /// Deserializes the provided xml node.
        /// </summary>
        /// <param name="node">Node to deserialize.</param>
		/// <exception cref="ArgumentNullException">Occurs when the provided <see cref="System.Xml.XmlNode"/> is null.</exception>
		/// <example> This example shows how to use the deserialize method.
		/// <code>
		/// namespace Docunamespace
		/// {
		/// 	/// <summary>
		/// 	/// Class for the docu.
		/// 	/// </summary>
		/// 	class DocuClass
		/// 	{
		/// 		/// <summary>
		/// 		/// Xml document that contains all actors.
		/// 		/// </summary>
		/// 		private XmlDocument actorsDoc = null;
		/// 		
		/// 		/// <summary>
		/// 		/// Initializes a new instance of the DocuClass class.
		/// 		/// </summary>
		/// 		public DocuClass(string extractionPath)
		/// 		{
		/// 			// load actors xml.
		/// 			this.actorsDoc = new XmlDocument();
		/// 			this.actorsDoc.Load(System.IO.Path.Combine(this.extractionPath, "actors.xml"));
		/// 		}
		/// 		
		/// 		/// <summary>
		/// 		/// Deserializes all actors of the series.
		/// 		/// </summary>
		/// 		public List&#60;Actor&#62; DeserializeActors()
		/// 		{
		/// 			List&#60;Actor&#62; actors = new List&#60;Actor&#62;();
		/// 			
		/// 			// load the xml docs second child.
		/// 			XmlNode actorsNode = this.actorsDoc.ChildNodes[1];
		/// 
		/// 			// deserialize all actors.
		/// 			foreach (XmlNode currentNode in actorsNode)
		/// 			{
		/// 				if (currentNode.Name.Equals("Actor", StringComparison.OrdinalIgnoreCase))
		/// 				{
		/// 					Actor deserializes = new Actor();
		/// 					deserializes.Deserialize(currentNode);
		/// 
		/// 					actors.Add(deserializes);
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

            foreach (XmlNode currentNode in node.ChildNodes)
            {
                if (currentNode.Name.Equals("id"))
                {
                    int result = 0;
                    int.TryParse(currentNode.InnerText, out result);
                    this.Id = result;
                    continue;
                }
                else if (currentNode.Name.Equals("Image"))
                {
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.ImagePath = currentNode.InnerText; 
                    }
                    continue;
                }
                else if (currentNode.Name.Equals("Name"))
                {
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Name = currentNode.InnerText; 
                    }
                    continue;
                }
                else if (currentNode.Name.Equals("Role"))
                {
                    if (!string.IsNullOrEmpty(currentNode.InnerText))
                    {
                        this.Role = currentNode.InnerText; 
                    }
                    continue;
                }
                else if (currentNode.Name.Equals("SortOrder"))
                {
                    int result = 0;
                    int.TryParse(currentNode.InnerText, out result);
                    this.SortOrder = result;
                    continue;
                }
            }
        }

        /// <summary>
        /// Compares the <see cref="SortOrder"/> property of the provided actor and this.
        /// </summary>
        /// <param name="other">Actor to compare.</param>
        /// <returns>Sort indicator.</returns>
        public int CompareTo(Actor other)
        {
            if (other.SortOrder < this.SortOrder)
            {
                return -1;
            }
            else if (other.SortOrder > this.SortOrder)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Raises the <see cref="PropertyChanged"/> event.
        /// </summary>
        /// <param name="propertyName">Name of the property that changed its value.</param>
        private void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
