using DIV2.Format.Exporter.ExtensionMethods;
using DIV2.Format.Exporter.Interfaces;
using DIV2.Format.Exporter.Utils;
using System;
using System.IO;

namespace DIV2.Format.Exporter
{
    /// <summary>
    /// DIV Games Studio file header.
    /// </summary>
    [DocFxIgnore]
    sealed class DIVFileHeader : ISerializableAsset, IFormatValidable
    {
        #region Constants
        const int MAGIC_NUMBER = 658714; // 0x1A, 0x0D, 0x0A, 0x00
        const byte VERSION = 0; // Version never changes from DIV Games Studio 1 to DIV Games Studio 2.

        public const int SIZE = (sizeof(byte) * 3) + sizeof(int) + sizeof(byte);
        #endregion

        #region Internal vars
        readonly byte[] _id;
        readonly int _magicNumber;
        readonly byte _version;
        #endregion

        #region Constructors
        public DIVFileHeader(char x, char y, char z)
        {
            this._id = new char[] { x, y, z }.ToByteArray();
            this._magicNumber = MAGIC_NUMBER;
            this._version = VERSION;
        }

        public DIVFileHeader(byte[] buffer)
        {
            if (buffer.Length != SIZE)
                throw new ArgumentOutOfRangeException($"Error reading the {nameof(DIVFileHeader)}. The buffer length must be over {SIZE} bytes.");

            this._id = buffer[0..3];
            this._magicNumber = BitConverter.ToInt32(buffer[3..7]);
            this._version = buffer[7];
        }
        #endregion

        #region Methods & Functions
        public bool Validate(byte[] buffer)
        {
            if (buffer.Length == SIZE)
            {
                var header = new DIVFileHeader(buffer);

                bool id = header._id.ToASCIIString().Equals(this._id.ToASCIIString());
                bool magicNumber = header._magicNumber == MAGIC_NUMBER;
                bool version = header._version == VERSION;

                return id && magicNumber && version; 
            }

            return false;
        }

        public byte[] Serialize()
        {
            using (var stream = new BinaryWriter(new MemoryStream()))
            {
                stream.Write(this._id);
                stream.Write(MAGIC_NUMBER);
                stream.Write(VERSION);

                return (stream.BaseStream as MemoryStream).ToArray();
            }
        }

        public void Write(BinaryWriter stream)
        {
            stream.BaseStream.Position = 0;
            stream.Write(this.Serialize());
        }
        #endregion
    }

    /// <summary>
    /// An exception ocurred when the a DIV Games Studio file header is invalid.
    /// </summary>
    public sealed class DIVFormatHeaderException : Exception
    {
        #region Constructor
        internal DIVFormatHeaderException()
            : base("Invalid file header.")
        {
        } 
        #endregion
    }

    /// <summary>
    /// An exception ocurred when a DIV Games Studio content data format is invalid.
    /// </summary>
    /// <typeparam name="T"><see cref="IAssetFile"/> type that reprensents any DIV Games Studio format.</typeparam>
    public sealed class DIVFileFormatException<T> : Exception where T : IAssetFile
    {
        #region Constructor
        internal DIVFileFormatException(Exception exception)
            : base($"Error loading {typeof(T).Name} file.", exception)
        {
        } 
        #endregion
    }
}
