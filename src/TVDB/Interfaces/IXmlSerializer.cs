// -----------------------------------------------------------------------
// <copyright company="Christoph van der Fecht - VDsoft">
// This code can be used in commercial, free and open source projects.
// </copyright>
// -----------------------------------------------------------------------

namespace TVDB.Interfaces
{
    using System.Xml;

    /// <summary>
    /// Deserializes an object form the received xml.
    /// </summary>
    public interface IXmlSerializer
    {
        /// <summary>
        /// Deserializes the provided xml node.
        /// </summary>
        /// <param name="node">Node to deserialize.</param>
        void Deserialize(XmlNode node);
    }
}
