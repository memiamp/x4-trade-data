namespace MPL.X4.TradeData.UI.Forms;

/// <summary>
/// A class that implements a debug form for the application.
/// </summary>
internal partial class DebugForm : Form
{
    /// <summary>
    /// Creates a new instance of the <see cref="DebugForm"/> class with the specified parameters.
    /// </summary>
    public DebugForm()
    {
        InitializeComponent();

        var writer = new TextBoxWriter(OutputTextBox);
        Console.SetOut(writer);
        Console.SetError(writer);
    }
}
