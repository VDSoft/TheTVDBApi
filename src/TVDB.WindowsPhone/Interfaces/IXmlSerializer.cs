using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TVDB.Interfaces
{
    /// <summary>
    ///  Defines the methods to deserialize xml file.
    /// </summary>
    public interface IXmlSerializer
    {
        void Deserialize(XElement element);
    }
}
