using Microsoft.Extensions.Logging;
using MPL.X4.Parser;
using MPL.X4.Services.Xml;

namespace MPL.X4.GameResources.Data.Parser;

/// <summary>
/// A class that implements a data parser for an <see cref="IQuaternion"/>.
/// </summary>
/// <param name="dataParser">An <see cref="IDataParser"/> that is the data parser to use.</param>
/// <param name="logger">An <see cref="ILogger{TCategoryName}"/> that is the logger to use.</param>
internal class QuaternionParser(
                                IDataParser dataParser,
                                ILogger<QuaternionParser> logger)
    : DataParserBase<IQuaternion>(dataParser, logger)
{
    private protected override Task<IQuaternion> OnParse(IXmlReaderWrapper reader)
    {
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.QW, out double? w);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.QX, out double? x);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.QY, out double? y);
        reader.TryGetAttribute(Constants.XmlDataFile.AttributeName.QZ, out double? z);

        var returnValue = new QuaternionRecord
        {
            W = w ?? 0,
            X = x ?? 0,
            Y = y ?? 0,
            Z = z ?? 0
        };

        return Task.FromResult<IQuaternion>(returnValue);
    }
}
