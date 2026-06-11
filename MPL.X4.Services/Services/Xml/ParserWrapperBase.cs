using Microsoft.Extensions.Logging;

namespace MPL.X4.Services.Xml;

/// <summary>
/// A class that implements the base functionality of a parser wrapper.
/// </summary>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class ParserWrapperBase(
                                 ILogger<ParserWrapperBase> logger)
{
    /// <summary>
    /// A delegate that defines a try parse method.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="source">A nullable <see cref="string"/> that is the source value.</param>
    /// <param name="result">A nullable <typeparamref name="T"/> that will be set to the result, or <see langword="null"/> if not parsed.</param>
    /// <returns>A <see cref="bool"/> indicating sucess.</returns>
    private protected delegate bool TryParseDelegate<T>(string? source, out T result)
        where T : struct;

    /// <summary>
    /// Gets the logger.
    /// </summary>
    private ILogger Logger => logger;
}
