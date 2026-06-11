using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace MPL.X4.Services.Xml;

/// <summary>
/// An interface that defines the behaviour of a wrapper around an <see cref="XDocument"/>.
/// </summary>
public interface IXDocumentWrapper
{
    /// <summary>
    /// Select an element using the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <returns>An <see cref="XElement"> that is the result.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified <paramref name="xpathExpression"/> did not return an element.</exception>
    XElement SelectElement(string xpathExpression);

    /// <summary>
    /// Select an element using the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <returns>An <see cref="IXDocumentWrapper"> that is the result.</returns>
    /// <exception cref="ArgumentException">Thrown when the specified <paramref name="xpathExpression"/> did not return an element.</exception>
    IXDocumentWrapper SelectElementAsDocument(string xpathExpression);

    /// <summary>
    /// Selects elements matching the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="XElement"> that is the result.</returns>
    IEnumerable<XElement> SelectElements(string xpathExpression);

    /// <summary>
    /// Selects elements matching the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IXDocumentWrapper"> that is the result.</returns>
    IEnumerable<IXDocumentWrapper> SelectElementsAsDocument(string xpathExpression);

    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the node with the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="decimal"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out decimal? value);

    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the node with the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="double"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out double? value);

    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the node with the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="int"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out int? value);

    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the node with the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="string"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetAttribute(string xpathExpression, string attributeName, [NotNullWhen(true)] out string? value);

    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the root node.
    /// </summary>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="decimal"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetRootAttribute(string attributeName, [NotNullWhen(true)] out decimal? value);

    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the root node.
    /// </summary>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="double"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetRootAttribute(string attributeName, [NotNullWhen(true)] out double? value);

    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the root node.
    /// </summary>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="int"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetRootAttribute(string attributeName, [NotNullWhen(true)] out int? value);

    /// <summary>
    /// Tries to get the value of the specified <paramref name="attributeName"/> from the root node.
    /// </summary>
    /// <param name="attributeName">A <see cref="string"/> containing the name of the attribute to get the value of.</param>
    /// <param name="value">A nullable <see cref="string"/> that will be set to the value of the attribute, or <see langword="null"/> if not found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TryGetRootAttribute(string attributeName, [NotNullWhen(true)] out string? value);

    /// <summary>
    /// Tries to select an element using the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <param name="element">A nullable <see cref="XElement"> that will be set to the result, or <see langword="null"/> if the element wasn't found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TrySelectElement(string xpathExpression, [NotNullWhen(true)] out XElement? element);

    /// <summary>
    /// Tries to select an element using the specified <paramref name="xpathExpression"/>.
    /// </summary>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression to evaluate.</param>
    /// <param name="element">A nullable <see cref="IXDocumentWrapper"> that will be set to the result, or <see langword="null"/> if the element wasn't found.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool TrySelectElementAsDocument(string xpathExpression, [NotNullWhen(true)] out IXDocumentWrapper? element);
}
