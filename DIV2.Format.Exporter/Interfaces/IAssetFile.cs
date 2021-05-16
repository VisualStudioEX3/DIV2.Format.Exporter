namespace DIV2.Format.Exporter.Interfaces
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface IAssetFile : IFormatValidable, ISerializableAsset
    {
        #region Methods & Functions
        bool Validate(string filename);
        void Save(string filename); 
        #endregion
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
