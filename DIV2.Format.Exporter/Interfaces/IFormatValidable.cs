using DIV2.Format.Exporter.Utils;

namespace DIV2.Format.Exporter.Interfaces
{
    /// <summary>
    /// Implements a format validator.
    /// </summary>
    [DocFxIgnore]
    public interface IFormatValidable
    {
        #region Methods & Functions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        bool Validate(byte[] buffer);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion
    }
}
