namespace MPL.X4.Services.DataParser;

/// <summary>
/// An interface that defines the behaviour of a data parser.
/// </summary>
/// <typeparam name="TData">The type of the data to be parsed.</typeparam>
public interface IDataParser<TData>
{
    /// <summary>
    /// Parses data from the specified <paramref name="reader"/>.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> that is the data reader.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. A <typeparamref name="TData"/> that is the result.</returns>
    Task<TData> Parse(IXmlReaderWrapper reader);

    /// <summary>
    /// Parses data from the specified <paramref name="reader"/>, using the specified <paramref name="positionOffset"/> to adjust any position data as needed.
    /// </summary>
    /// <param name="reader">An <see cref="IXmlReaderWrapper"/> that is the data reader.</param>
    /// <param name="positionOffset">An <see cref="ISectorPosition"/> that is the position offset.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. A <typeparamref name="TData"/> that is the result.</returns>
    //Task<TData> Parse(IXmlReaderWrapper reader, ISectorPosition positionOffset);
}
