using DIV2.Format.Exporter.ExtensionMethods;
using DIV2.Format.Exporter.Interfaces;
using DIV2.Format.Exporter.Processors.Palettes;
using DIV2.Format.Exporter.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DIV2.Format.Exporter
{
    /// <summary>
    /// A representation of a DIV Games Studio PAL file.
    /// </summary>
    /// <remarks>Implements functions to import and export color palettes.</remarks>
    public sealed class PAL : IAssetFile, IEnumerable<Color>
    {
        #region Constants
        readonly static DIVFileHeader PAL_FILE_HEADER = new DIVFileHeader('p', 'a', 'l');
        readonly static PAL VALIDATOR = new PAL();
        readonly static IndexOutOfRangeException INDEX_OUT_OF_RANGE_EXCEPTION =
            new IndexOutOfRangeException($"The index value must be a value beteween 0 and {LENGTH}.");

        /// <value>
        /// Number of colors.
        /// </value>
        public const int LENGTH = ColorPalette.LENGTH;

        /// <value>
        /// Memory size.
        /// </value>
        public const int SIZE = DIVFileHeader.SIZE + ColorPalette.SIZE + ColorRangeTable.SIZE;
        #endregion

        #region Properties
        /// <summary>
        /// Palette colors, in DAC format.
        /// </summary>
        /// <value>Returns the <see cref="ColorPalette"/> instance.</value>
        public ColorPalette Colors { get; private set; }

        /// <summary>
        /// Color range table.
        /// </summary>
        /// <value>Returns the <see cref="ColorRangeTable"/> instance.</value>
        public ColorRangeTable Ranges { get; private set; }

        /// <summary>
        /// Gets or sets a <see cref="Color"/> value.
        /// </summary>
        /// <param name="index"><see cref="Color"/> index.</param>
        /// <returns>Returns the <see cref="Color"/> value.</returns>
        public Color this[int index]
        {
            get
            {
                return !index.IsClamped(0, LENGTH) ? 
                    throw INDEX_OUT_OF_RANGE_EXCEPTION : 
                    this.Colors[index];
            }
            set
            {
                if (!index.IsClamped(0, LENGTH))
                    throw INDEX_OUT_OF_RANGE_EXCEPTION;

                this.Colors[index] = value;
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="a">Left <see cref="PAL"/> value to compare.</param>
        /// <param name="b">Right <see cref="PAL"/> value to compare.</param>
        /// <returns>Returns <see langword="true"/> if both values are equal.</returns>
        public static bool operator ==(PAL a, PAL b)
        {
            return a.Colors == b.Colors &&
                   a.Ranges == b.Ranges;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="a">Left <see cref="PAL"/> value to compare.</param>
        /// <param name="b">Right <see cref="PAL"/> value to compare.</param>
        /// <returns>Returns <see langword="true"/> if both values are not equal.</returns>
        public static bool operator !=(PAL a, PAL b)
        {
            return !(a == b);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="PAL"/> instance.
        /// </summary>
        public PAL()
        {
            this.Colors = new ColorPalette();
            this.Ranges = new ColorRangeTable();
        }

        /// <summary>
        /// Creates a new <see cref="PAL"/> instance.
        /// </summary>
        /// <param name="colors"><see cref="ColorPalette"/> instance.</param>
        public PAL(ColorPalette colors)
        {
            this.Colors = colors;
            this.Ranges = new ColorRangeTable();
        }

        /// <summary>
        /// Creates a new <see cref="PAL"/> instance.
        /// </summary>
        /// <param name="colors"><see cref="ColorPalette"/> instance.</param>
        /// <param name="ranges"><see cref="ColorRangeTable"/> instance.</param>
        public PAL(ColorPalette colors, ColorRangeTable ranges)
        {
            this.Colors = colors;
            this.Ranges = ranges;
        }

        /// <summary>
        /// Creates a new <see cref="PAL"/> instance from a <see cref="Color"/> array.
        /// </summary>
        /// <param name="colors">A 256 length <see cref="Color"/> array.</param>
        public PAL(Color[] colors)
            : this(new ColorPalette(colors))
        {
        }

        /// <summary>
        /// Loads a <see cref="PAL"/> file.
        /// </summary>
        /// <param name="filename"><see cref="PAL"/> filename to load.</param>
        public PAL(string filename)
            : this(File.ReadAllBytes(filename))
        {
        }

        /// <summary>
        /// Loads a <see cref="PAL"/> file.
        /// </summary>
        /// <param name="buffer">A memory buffer that contains <see cref="PAL"/> file.</param>
        public PAL(byte[] buffer)
            : this()
        {
            try
            {
                using (var stream = new BinaryReader(new MemoryStream(buffer)))
                {
                    if (PAL_FILE_HEADER.Validate(stream.ReadBytes(DIVFileHeader.SIZE)))
                    {
                        this.Colors = new ColorPalette(stream.ReadBytes(ColorPalette.SIZE));
                        this.Ranges = new ColorRangeTable(stream.ReadBytes(ColorRangeTable.SIZE));

                        this.GetHashCode();
                    }
                    else
                        throw new DIVFormatHeaderException();
                }
            }
            catch (Exception ex)
            {
                throw new DIVFileFormatException<PAL>(ex);
            }

        }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Creates new <see cref="PAL"/> instance from a supported image file.
        /// </summary>
        /// <param name="filename">Image file to load.</param>
        /// <param name="sortColors">Indicates if is needed to sort colors of the imported palette. By default is <see langword="false"/>.</param>
        /// <returns>Returns a new <see cref="PAL"/> instance.</returns>
        /// <remarks>Supported image formats are JPEG, PNG, BMP, GIF and TGA. 
        /// Also supported 256 color PCX images, <see cref="MAP"/> and <see cref="FPG"/> files.</remarks>
        public static PAL FromImage(string filename, bool sortColors = false)
        {
            return FromImage(File.ReadAllBytes(filename), sortColors);
        }

        /// <summary>
        /// Creates new <see cref="PAL"/> instance from a supported image file.
        /// </summary>
        /// <param name="buffer">Memory buffer that contains a supported image file.</param>
        /// <param name="sortColors">Indicates if is needed to sort colors of the imported palette. By default is <see langword="false"/>.</param>
        /// <returns>Returns a new <see cref="PAL"/> instance.</returns>
        /// <remarks>Supported image formats are JPEG, PNG, BMP, GIF and TGA. 
        /// Also supported 256 color PCX images, <see cref="MAP"/> and <see cref="FPG"/> files.</remarks>
        public static PAL FromImage(byte[] buffer, bool sortColors = false)
        {
            PAL pal = PaletteProcessor.ProcessPalette(buffer);

            if (sortColors)
                pal.Sort();

            return pal;
        }

        /// <summary>
        /// Validates if the file is a valid <see cref="PAL"/> file.
        /// </summary>
        /// <param name="filename">File to validate.</param>
        /// <returns>Returns <see langword="true"/> if the file is a valid <see cref="PAL"/>.</returns>
        public static bool ValidateFormat(string filename)
        {
            return VALIDATOR.Validate(filename);
        }

        /// <summary>
        /// Validates if the file is a valid <see cref="PAL"/> file.
        /// </summary>
        /// <param name="buffer">Memory buffer that contain a <see cref="PAL"/> file data.</param>
        /// <returns>Returns <see langword="true"/> if the file is a valid <see cref="PAL"/>.</returns>
        public static bool ValidateFormat(byte[] buffer)
        {
            return VALIDATOR.Validate(buffer);
        }

        /// <summary>
        /// Validates if the file is a valid <see cref="PAL"/> file.
        /// </summary>
        /// <param name="filename">File to validate.</param>
        /// <returns>Returns <see langword="true"/> if the file is a valid <see cref="PAL"/>.</returns>
        public bool Validate(string filename)
        {
            return this.Validate(File.ReadAllBytes(filename));
        }

        /// <summary>
        /// Validates if the file is a valid <see cref="PAL"/> file.
        /// </summary>
        /// <param name="buffer">Memory buffer that contain a <see cref="PAL"/> file data.</param>
        /// <returns>Returns <see langword="true"/> if the file is a valid <see cref="PAL"/>.</returns>
        public bool Validate(byte[] buffer)
        {
            return PAL_FILE_HEADER.Validate(buffer[0..DIVFileHeader.SIZE]) && this.TryToReadFile(buffer);
        }

        bool TryToReadFile(byte[] buffer)
        {
            try
            {
                using (var stream = new BinaryReader(new MemoryStream(buffer)))
                {
                    stream.ReadBytes(DIVFileHeader.SIZE); // DIV Header.
                    stream.ReadBytes(ColorPalette.SIZE); // Color palette.
                    stream.ReadBytes(ColorRangeTable.SIZE); // Color Range table.
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        public byte[] Serialize()
        {
            using (var buffer = new MemoryStream())
            {
                buffer.Write(this.Colors.Serialize());
                buffer.Write(this.Ranges.Serialize());

                return buffer.ToArray();
            }
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        public void Write(BinaryWriter stream)
        {
            stream.Write(this.Serialize());
        }

        /// <summary>
        /// Saves the instance in a <see cref="PAL"/> file.
        /// </summary>
        /// <param name="filename">Filename to write the data.</param>
        public void Save(string filename)
        {
            using (var stream = new BinaryWriter(File.OpenWrite(filename)))
            {
                PAL_FILE_HEADER.Write(stream);
                this.Write(stream);
            }
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        public IEnumerator<Color> GetEnumerator()
        {
            return this.Colors.GetEnumerator();
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        public override bool Equals(object obj)
        {
            return obj is PAL pal && this == pal;
        }

        /// <summary>
        /// Generates a hash code for this instance.
        /// </summary>
        /// <returns>Returns an <see cref="int"/> SHA256 hash code from the MD5 hash created by the binary serialized data of this instance.</returns>
        public override int GetHashCode()
        {
            return this.Serialize().CalculateChecksum().GetSecureHashCode();
        }

        /// <summary>
        /// Serializes the relevant data of this instance in a <see cref="string"/> value.
        /// </summary>
        /// <returns>Returns a <see cref="string"/> value with the relevant serialized data in JSON format.</returns>
        public override string ToString()
        {
            return $"{{ {nameof(PAL)}: {{ Hash: {this.GetHashCode()} }} }}";
        }

        /// <summary>
        /// Creates a copy of the <see cref="Color"/> array converted to full RGB format [0..255].
        /// </summary>
        /// <returns>Returns a new <see cref="Color"/> array in full RGB format [0..255]. In most of the cases, these values are an 
        /// aproximation to the real RGB value.</returns>
        public Color[] ToRGB() => this.Colors.ToRGB();

        /// <summary>
        /// Sorts the <see cref="Color"/> values.
        /// </summary>
        /// <remarks>This method try to sort the colors using the Nearest Neighbour algorithm, trying to ensure that the black color 
        /// (0, 0, 0), if exists in palette, be the first color.</remarks>
        public void Sort() => this.Colors.Sort();
        #endregion
    }
}
