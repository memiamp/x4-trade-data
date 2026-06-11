using Microsoft.Extensions.Logging;
using MPL.X4.Services.Xml;

namespace MPL.X4.Parser;

/// <summary>
/// A class that implements the base functionality of a data parser that supports document parsing.
/// </summary>
/// <typeparam name="TData">The type of the data to be parsed.</typeparam>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal abstract class DocumentDataParserBase<TData>(
                                                      IDataParser dataParser,
                                                      ILogger<DocumentDataParserBase<TData>> logger)
    : DataParserBase<TData>(dataParser, logger)
{
    private protected override async Task<TData> OnParse(IXmlReaderWrapper reader)
    {
        var document = await reader.ToXDocument();

        return await OnParse(document);
    }

    /// <summary>
    /// Parses the element at <paramref name="xpathExpression"/> from the specified <paramref name="document"/> as a <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to parse the element to.</typeparam>
    /// <param name="document">An <see cref="IXDocumentWrapper"/> that is the document.</param>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression of the element to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A <typeparamref name="T"/> that is the result.</returns>
    private protected Task<T> ParseElement<T>(IXDocumentWrapper document, string xpathExpression)
    {
        var element = document.SelectElementAsDocument(xpathExpression);

        return DataParser.Parse<T>(element);
    }

    /// <summary>
    /// Optionally parses the element at <paramref name="xpathExpression"/> from the specified <paramref name="document"/> as a <typeparamref name="T"/>, returning <see langword="null"/> if the element wasn't found.
    /// </summary>
    /// <typeparam name="T">The type to parse the element to.</typeparam>
    /// <param name="document">An <see cref="IXDocumentWrapper"/> that is the document.</param>
    /// <param name="xpathExpression">A <see cref="string"/> containing the XPath expression of the element to parse.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation.  A nullable <typeparamref name="T"/> that is the result.</returns>
    private protected async Task<T?> ParseElementOptional<T>(IXDocumentWrapper document, string xpathExpression)
    {
        T? returnValue = default;

        if (document.TrySelectElementAsDocument(xpathExpression, out var element))
        {
           returnValue = await DataParser.Parse<T>(element);
        }
        
        return returnValue;
    }
}
