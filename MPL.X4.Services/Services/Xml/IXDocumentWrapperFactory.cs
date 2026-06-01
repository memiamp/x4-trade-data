namespace MPL.X4.Services.Xml;

/// <summary>
/// An interface that defines the behaviour of a factory for <see cref="IXDocumentWrapper"/> instances.
/// </summary>
public interface IXDocumentWrapperFactory
{
    /// <summary>
    /// Creates a new instance of the wrapper for the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> to read the document from.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. An <see cref="IXDocumentWrapper"/> that was created.</returns>
    Task<IXDocumentWrapper> CreateInstance(IXmlReaderWrapper reader);
}
