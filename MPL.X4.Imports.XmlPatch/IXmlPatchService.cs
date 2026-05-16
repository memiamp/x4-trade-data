using System.Xml.Linq;

namespace MPL.X4.Imports.XmlPatch;

/// <summary>
/// An interface that defines the behaviour of a patching service for X4 XML.
/// </summary>
public interface IXmlPatchService
{
    /// <summary>
    /// Patxhes the specified <paramref name="sourceXml"/> with <paramref name="differenceXml"/> and returns the result.
    /// </summary>
    /// <param name="sourceXml">A <see cref="string"/> containing the source XML to be patched.</param>
    /// <param name="differenceXml">A <see cref="string"/> containing the difference XML.</param>
    /// <returns>An <see cref="XDocument"/> that is the result.</returns>
    XDocument Patch(string sourceXml, string differenceXml);

    /// <summary>
    /// Patxhes the specified <paramref name="source"/> with <paramref name="differenceXml"/> and returns the result.
    /// </summary>
    /// <param name="source">An <see cref="XDocument"/> that is to be patched.</param>
    /// <param name="differenceXml">A <see cref="string"/> containing the difference XML.</param>
    /// <returns>An <see cref="XDocument"/> that is the result.</returns>
    XDocument Patch(XDocument source, string differenceXml);

    /// <summary>
    /// Patxhes the specified <paramref name="source"/> with <paramref name="difference"/> and returns the result.
    /// </summary>
    /// <param name="source">An <see cref="XDocument"/> that is to be patched.</param>
    /// <param name="difference">An <see cref="XDocument"/> that is the difference.</param>
    /// <returns>An <see cref="XDocument"/> that is the result.</returns>
    XDocument Patch(XDocument source, XDocument difference);

    /// <summary>
    /// Patxhes the specified <paramref name="sourceXml"/> with <paramref name="differenceXml"/> and returns the result as an XML string.
    /// </summary>
    /// <param name="sourceXml">A <see cref="string"/> containing the source XML to be patched.</param>
    /// <param name="differenceXml">A <see cref="string"/> containing the difference XML.</param>
    /// <returns>A <see cref="string"/> that is the result.</returns>
    string PatchToString(string sourceXml, string differenceXml);
}
