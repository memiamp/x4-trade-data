using System.Diagnostics.CodeAnalysis;

namespace MPL.X4.Services.Xml;

/// <summary>
/// A class that implements extensions to an <see cref="IXmlReaderWrapper"/>.
/// </summary>
internal static class XmlReaderWrapperExtensions
{
    /// <summary>
    /// Gets a <see cref="ITextResourceReference"/> from an attribute with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapperFactory"/> to get the result from.</param>
    /// <param name="name">A <see cref="string"/> that is the name of the attribute to get./param>
    /// <returns>A nullable <see cref="ITextResourceReference"/> that is the result.</returns>
    internal static ITextResourceReference? ParseTextResourceReference(this IXmlReaderWrapper reader, string name)
        => reader.TryGetAttribute(name, out string? value)
            ? TextResourceReference.Parse(value)
            : null;

    /// <summary>
    /// Tries to parse a <see cref="ITextResourceReference"/> from an attribute with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapperFactory"/> to get the result from.</param>
    /// <param name="name">A <see cref="string"/> that is the name of the attribute to get./param>
    /// <param name="value">A nullable <see cref="ITextResourceReference"/> that will be set to the result.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    internal static bool TryParseTextResourceReference(this IXmlReaderWrapper reader, string name, [NotNullWhen(true)] out ITextResourceReference? value)
    {
        value = null;

        if (reader.TryGetAttribute(name, out string? attributeValue) &&
            TextResourceReference.TryParse(attributeValue, out var valueObject))
        {
            value = valueObject;
            return true;
        }

        return false;
    }
}
