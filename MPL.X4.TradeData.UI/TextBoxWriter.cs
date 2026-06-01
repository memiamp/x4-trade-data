using System.Text;

namespace MPL.X4.TradeData.UI;

/// <summary>
/// A class that implements a <see cref="TextWriter"/> for a <see cref="TextBox"/>.
/// </summary>
/// <param name="textBox">A <see cref="TextBox"/> that is the target for output.</param>
public class TextBoxWriter(
                           TextBox textBox)
    : TextWriter
{
    private readonly TextBox _textBox = textBox;

    public override void Write(char value)
        => Write(value.ToString());

    public override void Write(string? value)
    {
        if (_textBox.InvokeRequired)
        {
            _textBox.Invoke(() => Write(value));
            return;
        }

        _textBox.AppendText(value ?? string.Empty);
    }

    public override Encoding Encoding => Encoding.UTF8;
}
