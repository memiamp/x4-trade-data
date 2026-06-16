namespace MPL.X4.TradeData.UI.Services.Logging;

/// <summary>
/// An interface that defines the behaviour of a logging queue.
/// </summary>
internal interface ILoggingQueue
{
    /// <summary>
    /// Adds the specified <paramref name="item"/> to the queue.
    /// </summary>
    /// <param name="item">A nullable <see cref="string"/> that is the item to be enqueued.</param>
    void Enqueue(string? item);

    /// <summary>
    /// Tries to dequeue an item.
    /// </summary>
    /// <param name="result">A nullable <see cref="string"/> that will be set to the dequeued item, or <see langword="null"/>.</param>
    /// <returns>A <see cref="bool"/> indicating success.</returns>
    bool TryDequeue(out string? result);
}
