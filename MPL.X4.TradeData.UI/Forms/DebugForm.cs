using MPL.X4.TradeData.UI.Services.Logging;

namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements a debug form for the application.
/// </summary>
internal partial class DebugForm : Form
{
    private readonly CancellationTokenSource _cts = new();
    private readonly ILoggingQueue _loggingQueue;

    /// <summary>
    /// Creates a new instance of the <see cref="DebugForm"/> class with the specified parameters.
    /// </summary>
    /// <param name="loggingQueue">An <see cref="ILoggingQueue"/> that is the logging queue to use.</param>
    public DebugForm(
                     ILoggingQueue loggingQueue)
    {
        _loggingQueue = loggingQueue;

        InitializeComponent();

        Task.Run(ProcessQueueAsync);
    }


    private void AppendTextSafe(List<string> lines)
    {
        lines.Reverse();
        lines.AddRange(OutputTextBox.Lines);
        OutputTextBox.Lines = [.. lines.Take(300)];
    }

    private Task AppendToTextBoxAsync(List<string> lines, CancellationToken cancellationToken)
    {
        if (OutputTextBox.InvokeRequired)
        {
            return OutputTextBox.InvokeAsync(() => AppendTextSafe(lines), cancellationToken);
        }
        else
        {
            AppendTextSafe(lines);
            return Task.CompletedTask;
        }
    }
    
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _cts.Cancel();
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private async Task ProcessQueueAsync()
    {
        while (!_cts.IsCancellationRequested)
        {
            try
            {
                var batch = new List<string>();

                while (batch.Count < 20 &&
                       _loggingQueue.TryDequeue(out var message))
                {
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        batch.Add(message);
                    }
                }

                if (batch.Count > 0)
                {
                    await AppendToTextBoxAsync(batch, _cts.Token);
                }

                await Task.Delay(10, _cts.Token);
            }
            catch { }
        }
    }
}
