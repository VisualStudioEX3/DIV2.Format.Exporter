namespace DIV2.Format.Exporter.Interfaces
{
    /// <summary>
    /// Implements an asset file.
    /// </summary>
    public interface IAssetFile : IFormatValidable, ISerializableAsset
    {
        #region Methods & Functions
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        bool Validate(string filename);
        void Save(string filename);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        #endregion
    }
}
