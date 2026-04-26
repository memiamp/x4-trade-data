using System.Xml;

namespace MPL.X4.Services;

/// <summary>
/// An interface that defines the behaviour of a factory for <see cref="IXmlReaderWrapper"/> instances.
/// </summary>
public interface IXmlReaderWrapperFactory
{
    /// <summary>
    /// Creates a new instance of the wrapper for the specified <paramref name="inputUri"/> using the specified <paramref name="settings"/>.
    /// </summary>
    /// <param name="inputUri">A <see cref="string"/> containing the URI from which the XML document can be found.</param>
    /// <param name="settings">A <see cref="XmlReaderSettings"/> that are the settings to use.</param>
    /// <returns>An <see cref="IXmlReaderWrapper"/> that was created.</returns>
    IXmlReaderWrapper CreateXmlReader(string inputUri, XmlReaderSettings settings);
    
    /// <summary>
    /// Creates a new instance of the wrapper for the specified <paramref name="inputUri"/> using the default settings.
    /// </summary>
    /// <param name="inputUri">A <see cref="string"/> containing the URI from which the XML document can be found.</param>
    /// <returns>An <see cref="IXmlReaderWrapper"/> that was created.</returns>
    IXmlReaderWrapper CreateXmlReader(string inputUri);

    /// <summary>
    /// Creates a new instance of the wrapper for the specified <paramref name="source"/> reader.
    /// </summary>
    /// <param name="source">A <see cref="XmlReader"/> that is the source reader to use.</param>
    /// <returns>An <see cref="IXmlReaderWrapper"/> that was created.</returns>
    IXmlReaderWrapper CreateXmlReader(XmlReader source);
}
