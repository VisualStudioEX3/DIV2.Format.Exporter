namespace DIV2.Format.Exporter.Interfaces
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IFormatValidable
    {
        #region Methods & Functions
        bool Validate(byte[] buffer);
        #endregion
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
