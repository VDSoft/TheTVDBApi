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
    /// A mirror to load the data.
    /// </summary>
    public class Mirror : INotifyPropertyChanged, Interfaces.IXmlSerializer, IComparable<Mirror>
    {
        /// <summary>
        /// Name of the <see cref="Id"/> property.
        /// </summary>
        private const string IdName = "Id";

        /// <summary>
        /// Name of the <see cref="Address"/> property.
        /// </summary>
        private const string AddressName = "Address";

        /// <summary>
        /// Name of the <see cref="ContainsXmlFile"/> property.
        /// </summary>
        private const string ContainsXmlFileName = "ContainsXmlFile";

        /// <summary>
        /// Name of the <see cref="ContainsBannerFile"/> property.
        /// </summary>
        private const string ContainsBannerFileName = "ContainsBannerFile";

        /// <summary>
        /// Name of the <see cref="ContainsZipFile"/> property.
        /// </summary>
        private const string ContainsZipFileName = "ContainsZipFile";

        /// <summary>
        /// Id of the mirror.
        /// </summary>
        private int id = 0;

        /// <summary>
        /// Address of the mirror.
        /// </summary>
        private string address = string.Empty;

        /// <summary>
        /// Value indicating whether the mirror provides xml files.
        /// </summary>
        private bool containsXmlFile = false;

        /// <summary>
        /// Value indicating whether the mirror provieds banner file.
        /// </summary>
        private bool containsBannerFile = false;

        /// <summary>
        /// Value indicating whether the mirror provides zip file.
        /// </summary>
        private bool containsZipFile = false;

        /// <summary>
        /// Initializes a enw isntance of the <see cref="Mirror"/> class.
        /// </summary>
        public Mirror()
        {

        }

        /// <summary>
        /// Gets or sets the id of the mirror.
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
        /// Gets or sets the address of the mirror.
        /// </summary>
        public string Address
        {
            get
            {
                return this.address;
            }

            set
            {
                if (value == this.address)
                {
                    return;
                }

                this.address = value;
                this.RaisePropertyChanged(AddressName);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the mirror provides xml files.
        /// </summary>
        public bool ContainsXmlFile
        {
            get
            {
                return this.containsXmlFile;
            }

            set
            {
                if (value == this.containsXmlFile)
                {
                    return;
                }

                this.containsXmlFile = value;
                this.RaisePropertyChanged(ContainsXmlFileName);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the mirror provieds banner file.
        /// </summary>
        public bool ContainsBannerFile
        {
            get
            {
                return this.containsBannerFile;
            }

            set
            {
                if (value == this.containsBannerFile)
                {
                    return;
                }

                this.containsBannerFile = value;
                this.RaisePropertyChanged(ContainsBannerFileName);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the mirror provides zip file.
        /// </summary>
        public bool ContainsZipFile
        {
            get
            {
                return this.containsZipFile;
            }

            set
            {
                if (value == this.containsZipFile)
                {
                    return;
                }

                this.containsZipFile = value;
                this.RaisePropertyChanged(ContainsZipFileName);
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
                throw new ArgumentNullException("node", "Provided node must not be null.");
            }

            foreach (System.Xml.XmlNode currentNode in node.ChildNodes)
            {
                if (currentNode.Name.Equals("id", StringComparison.OrdinalIgnoreCase))
                {
                    int result = 0;
                    int.TryParse(currentNode.InnerText, out result);
                    this.Id = result;
                    continue;
                }
                else if (currentNode.Name.Equals("mirrorpath", StringComparison.OrdinalIgnoreCase))
                {
                    this.Address = currentNode.InnerText;
                    continue;
                }
                else if (currentNode.Name.Equals("typemask", StringComparison.OrdinalIgnoreCase))
                {
                    int result = 0;
                    int.TryParse(currentNode.InnerText, out result);
                    this.ConvertTypeMask(result);
                    continue;
                }
            }
        }

        /// <summary>
        /// Compares the <see cref="Id"/> of the provided Mirror with this one.
        /// </summary>
        /// <param name="other">Mirror object to compare.</param>
        /// <returns>
        /// 0: Ids are euqal.
        /// -1: Provided id is smaller than this one.
        /// 1: Provided id is greater than this one.
        /// </returns>
        public int CompareTo(Mirror other)
        {
            if (other.Id.Equals(this.Id))
            {
                return 0;
            }
            else if (other.Id > this.Id)
            {
                return 1;
            }
            else if (other.Id < this.Id)
            {
                return -1;
            }
            else
            {
                return 0;
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

        /// <summary>
        /// Converts the provided typemask into the bool values.
        /// </summary>
        /// <param name="typemask">Typemask value to convert.</param>
        private void ConvertTypeMask(int typemask)
        {
            this.ContainsXmlFile = ((typemask >> 0) & 1) == 1;
            this.ContainsBannerFile = ((typemask >> 1) & 1) == 1;
            this.ContainsZipFile = ((typemask >> 2) & 1) == 1;
        }
    }
}
