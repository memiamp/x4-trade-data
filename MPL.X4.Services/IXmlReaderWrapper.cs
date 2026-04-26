using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace MPL.X4.Services;

/// <summary>
/// An interface that defines the behaviour of a wrapper around a <see cref="XmlReader"/>.
/// </summary>
public interface IXmlReaderWrapper : IDisposable
{
    /// <summary>
    /// Gets an indication of whether the current node matches the specified parameters.
    /// </summary>
    /// <param name="name">A <see cref="string"/> containing the name of the node.</param>
    /// <param name="nodeType">A <see cref="XmlNodeType"/> that is the type of the node.</param>
    /// <returns>A <see cref="bool"/> indicating the result.</returns>
    bool CheckNodeMatches(string name, XmlNodeType nodeType);

    /// <summary>
    /// Gets the value of the attribute with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name">A <see cref="string"/> that is the name of the attribute to get.</param>
    /// <returns>A nullable <see cref="string"/> that is the value, or <see langword="null"/> where there is no attribute with the <paramref name="name"/>.</returns>
    string? GetAttribute(string name);

    /// <summary>
    /// Asynchronously reads the next node from the stream.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. A <see cref="bool"/> indicating whether the next node was read. If <see langword="false"/> then no further nodes are to be read.</returns>
    Task<bool> ReadAsync();

    /// <summary>
    /// Asynchronously reads the content of the current node element as a string.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. A <see cref="string"/> containing the result.</returns>
    Task<string> ReadElementContentAsStringAsync();

    /// <summary>
    /// Asynchronously reads the content, including markup, of the current node element as a string.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. A <see cref="string"/> containing the result.</returns>
    Task<string> ReadInnerXmlAsync();

    /// <summary>
    /// Returns a new instance of <see cref="IXmlReaderWrapper"/> that can be used to read the current node and all of its descendents.
    /// </summary>
    /// <returns>An <see cref="IXmlReaderWrapper"/> that is the new instance.</returns>
    IXmlReaderWrapper ReadSubtree();

    /// <summary>
    /// Tries to get an attribute with the specified <paramref name="name"/> that matches <paramref name="predicate"/>.
    /// </summary>
    /// <param name="name">A <see cref="string"/> that is the name of the attribute to try and get.</param>
    /// <param name="predicate">A <see cref="Func{T, TResult}"/> of input <see cref="string"/> and output <see cref="bool"/> that tests the attribute value.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    bool TryGetAttribute(string name, Func<string, bool> predicate);

    /// <summary>
    /// Tries to get an attribute with the specified <paramref name="name"/> that matches <paramref name="predicate"/>.
    /// </summary>
    /// <param name="name">A <see cref="string"/> that is the name of the attribute to try and get.</param>
    /// <param name="predicate">A <see cref="Func{T, TResult}"/> of input <see cref="string"/> and output <see cref="bool"/> that tests the attribute value.</param>
    /// <param name="value">A nullable <see cref="string"/> that will be set to the value of the attribute, or <see langword="null"/>.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    bool TryGetAttribute(string name, Func<string, bool> predicate, [NotNullWhen(true)] out string? value);

    /// <summary>
    /// Tries to get an attribute with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name">A <see cref="string"/> that is the name of the attribute to try and get.</param>
    /// <param name="value">A nullable <see cref="double"/> that will be set to the value of the attribute, or <see langword="null"/>.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    bool TryGetAttribute(string name, [NotNullWhen(true)] out double? value);

    /// <summary>
    /// Tries to get an attribute with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name">A <see cref="string"/> that is the name of the attribute to try and get.</param>
    /// <param name="value">A nullable <see cref="int"/> that will be set to the value of the attribute, or <see langword="null"/>.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    bool TryGetAttribute(string name, [NotNullWhen(true)] out int? value);

    /// <summary>
    /// Tries to get an attribute with the specified <paramref name="name"/>.
    /// </summary>
    /// <param name="name">A <see cref="string"/> that is the name of the attribute to try and get.</param>
    /// <param name="value">A nullable <see cref="string"/> that will be set to the value of the attribute, or <see langword="null"/>.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    bool TryGetAttribute(string name, [NotNullWhen(true)] out string? value);

    /// <summary>
    /// Gets the qualified name of the current node.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the type of the current node.
    /// </summary>
    XmlNodeType NodeType { get; }
}
