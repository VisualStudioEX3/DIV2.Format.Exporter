using DIV2.Format.Exporter.Utils;
using System.IO;

namespace DIV2.Format.Exporter.Interfaces
{
    /// <summary>
    /// Implements a serializable asset.
    /// </summary>
    [DocFxIgnore]
    public interface ISerializableAsset
    {
        #region Methods & Functions
        /// <summary>
        /// Serializes this instance to binary format.
        /// </summary>
        /// <returns>Returns a <see cref="byte"/> array with the serialized data.</returns>
        byte[] Serialize();
        /// <summary>
        /// Writes this instance data in a <see cref="BinaryWriter"/> instance.
        /// </summary>
        /// <param name="stream"><see cref="BinaryWriter"/> instance.</param>
        void Write(BinaryWriter stream);
        #endregion
    }
}
