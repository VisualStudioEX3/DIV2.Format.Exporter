using System.IO;

namespace DIV2.Format.Exporter.Interfaces
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public interface ISerializableAsset
    {
        #region Methods & Functions
        byte[] Serialize();
        void Write(BinaryWriter stream);
        #endregion
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
