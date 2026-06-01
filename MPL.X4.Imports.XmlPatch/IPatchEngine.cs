using System.Xml.Linq;

namespace MPL.X4.Imports.XmlPatch;

/// <summary>
/// An interface that defines the behaviour of a patching engine for X4 XML data.
/// </summary>
internal interface IPatchEngine
{
    /// <summary>
    /// Applies an add operation to the specified <paramref name="originalRoot"/>.
    /// </summary>
    /// <param name="originalRoot">An <see cref="XElement"/> that is the original root element to apply the operation on.</param>
    /// <param name="change">An <see cref="XElement"/> containing the change to apply.</param>
    /// <param name="allowDoubles">A <see cref="bool"/> indicating whether to allow muliple operations.</param>
    void Add(XElement originalRoot, XElement change, bool allowDoubles);

    /// <summary>
    /// Applies a remove operation to the specified <paramref name="originalRoot"/>.
    /// </summary>
    /// <param name="originalRoot">An <see cref="XElement"/> that is the original root element to apply the operation on.</param>
    /// <param name="change">An <see cref="XElement"/> containing the change to apply.</param>
    void Remove(XElement originalRoot, XElement change);

    /// <summary>
    /// Applies a replace operation to the specified <paramref name="originalRoot"/>.
    /// </summary>
    /// <param name="originalRoot">An <see cref="XElement"/> that is the original root element to apply the operation on.</param>
    /// <param name="change">An <see cref="XElement"/> containing the change to apply.</param>
    void Replace(XElement originalRoot, XElement change);
}
