using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace MPL.X4.Services.Xml;

/// <summary>
/// An interface that defines the behaviour of a wrapper around an <see cref="XDocument"/>.
/// </summary>
public interface IXDocumentWrapper
{
    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the node with the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="string"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out string? value);

    /// <summary>
    /// Select an element using the specified XPath <paramref name="expression"/>.
    /// </summary>
    /// <param name="expression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <returns>A nullable <see cref="XElement"> that is the result, or <see langword="null"/> if the element wasn't found.</returns>
    XElement? XPathSelectElement(string expression);
}
