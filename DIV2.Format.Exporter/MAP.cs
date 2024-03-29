﻿using DIV2.Format.Exporter.Converters;
using DIV2.Format.Exporter.ExtensionMethods;
using DIV2.Format.Exporter.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using DIV2.Format.Exporter.Utils;

namespace DIV2.Format.Exporter
{
    /// <summary>
    /// <see cref="MAP"/> Control Point data.
    /// </summary>
    [Serializable]
    public struct ControlPoint : ISerializableAsset
    {
        #region Constants
        /// <value>
        /// Number of items.
        /// </value>
        public const int LENGTH = 2;
        /// <value>
        /// Memory size.
        /// </value>
        public const int SIZE = sizeof(short) * LENGTH;
        #endregion

        #region Public vars
        /// <value>
        /// Horizontal coordinate.
        /// </value>
        public short x;
        /// <value>
        /// Vertical coordinate.
        /// </value>
        public short y;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the coordinate value.
        /// </summary>
        /// <param name="index">Index of the coordinate in the structure.</param>
        /// <returns>Returns the coordinate value.</returns>
        public short this[int index]
        {
            get
            {
                return index switch
                {
                    0 => this.x,
                    1 => this.y,
                    _ => throw new IndexOutOfRangeException(),
                };
            }
            set
            {
                switch (index)
                {
                    case 0: this.x = value; break;
                    case 1: this.y = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="a">Left <see cref="ControlPoint"/> value to compare.</param>
        /// <param name="b">Right <see cref="ControlPoint"/> value to compare.</param>
        /// <returns>Returns <see langword="true"/> if both values are equal.</returns>
        public static bool operator ==(ControlPoint a, ControlPoint b)
        {
            return a.x == b.x &&
                   a.y == b.y;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="a">Left <see cref="ControlPoint"/> value to compare.</param>
        /// <param name="b">Right <see cref="ControlPoint"/> value to compare.</param>
        /// <returns>Returns <see langword="true"/> if both values are not equal.</returns>
        public static bool operator !=(ControlPoint a, ControlPoint b)
        {
            return !(a == b);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public ControlPoint(short x, short y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public ControlPoint(int x, int y)
            : this((short)x, (short)y)
        {
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public ControlPoint(float x, float y)
            : this((short)x, (short)y)
        {
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        public ControlPoint(double x, double y)
            : this((short)x, (short)y)
        {
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="buffer">4 <see cref="byte"/> length array that contains the X and Y coordinates in <see cref="short"/> format.</param>
        public ControlPoint(byte[] buffer)
        {
            if (buffer.Length != SIZE)
                throw new ArgumentOutOfRangeException($"The array must be {SIZE} bytes length.");

            this.x = BitConverter.ToInt16(buffer, 0);
            this.y = BitConverter.ToInt16(buffer, 2);
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="stream">A <see cref="BinaryReader"/> instance, that contains, in the current position, with 2 <see cref="short"/> 
        /// values for X and Y coordinates.</param>
        public ControlPoint(BinaryReader stream)
            : this(stream.ReadInt16(), stream.ReadInt16())
        {
        }
        #endregion

        #region Methods & Functions
        /// <inheritdoc/>
        [DocFxIgnore]
        public byte[] Serialize()
        {
            using (var stream = new BinaryWriter(new MemoryStream()))
            {
                stream.Write(this.x);
                stream.Write(this.y);

                return (stream.BaseStream as MemoryStream).ToArray();
            }
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        public void Write(BinaryWriter stream)
        {
            stream.Write(this.Serialize());
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        public override bool Equals(object obj)
        {
            return obj is ControlPoint point && this == point;
        }

        /// <summary>
        /// Generates a hash code for this instance.
        /// </summary>
        /// <returns>Returns an <see cref="int"/> value of X and Y coordinates XOR operation.</returns>
        public override int GetHashCode()
        {
            return this.x ^ this.y;
        }

        /// <summary>
        /// Serializes the relevant data of this instance in a <see cref="string"/> value.
        /// </summary>
        /// <returns>Returns a <see cref="string"/> value with the relevant serialized data in JSON format.</returns>
        public override string ToString()
        {
            return $"{{ {nameof(ControlPoint)}: {{ x: {this.x}, y: {this.y} }} }}";
        }
        #endregion
    }

    class MAPEnumerator : IEnumerator<byte>
    {
        #region Internal vars
        readonly IList<byte> _bitmap;
        int _currentIndex;
        #endregion

        #region Properties
        public byte Current { get; private set; }
        object IEnumerator.Current => this.Current;
        #endregion

        #region Constructor & Destructor
        public MAPEnumerator(IList<byte> bitmap)
        {
            this._bitmap = bitmap;
            this.Current = default;
            this.Reset();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

        #region Methods & Functions
        public bool MoveNext()
        {
            if (++this._currentIndex >= this._bitmap.Count)
                return false;
            else
                this.Current = this._bitmap[this._currentIndex];

            return true;
        }

        public void Reset()
        {
            this._currentIndex = -1;
        }
        #endregion
    }

    /// <summary>
    /// A representation of a DIV Games Studio MAP file.
    /// </summary>
    /// <remarks>Implements functions to import and export graphic maps.</remarks>
    public sealed class MAP : IAssetFile, IEnumerable<byte>
    {
        #region Constants
        readonly static DIVFileHeader MAP_FILE_HEADER = new DIVFileHeader('m', 'a', 'p');
        readonly static MAP VALIDATOR = new MAP();
        readonly static string PIXEL_OUT_OF_RANGE_EXCEPTION_MESSAGE = "{0} min value accepted is " + MIN_PIXEL_SIZE + " ({0}: {1})";
        readonly static ArgumentOutOfRangeException GRAPHID_OUT_OF_RANGE =
            new ArgumentOutOfRangeException($"GraphId must be a value between {MIN_GRAPH_ID} and {MAX_GRAPH_ID}.");
        const string INDEX_OUT_OF_RANGE_EXCEPTION_MESSAGE = "The index value must be a value beteween 0 and {0} (Index: {1}).";
        const string COORDINATE_OUT_OF_RANGE_EXCEPTION_MESSAGE = "{0} coordinate must be a value beteween 0 and {1} ({0}: {2}).";

        /// <value>
        /// Min supported size value for width or height properties.
        /// </value>
        public const int MIN_PIXEL_SIZE = 1;
        /// <value>
        /// Max description character length.
        /// </value>
        public const int DESCRIPTION_LENGTH = 32;
        /// <value>
        /// Min allowed graph id value.
        /// </value>
        public const int MIN_GRAPH_ID = 1;
        /// <value>
        /// Max allowed graph id value.
        /// </value>
        public const int MAX_GRAPH_ID = 999;
        /// <value>
        /// Max supported <see cref="ControlPoint"/>s.
        /// </value>
        public const int MAX_CONTROL_POINTS = 1000;
        #endregion

        #region Internal vars
        int _graphId;
        byte[] _bitmap;
        #endregion

        #region Properties
        /// <summary>
        /// Width of the graphic map.
        /// </summary>
        /// <value>Returns the width value in pixels.</value>
        public short Width { get; }
        /// <summary>
        /// Height of the graphic map.
        /// </summary>
        /// <value>Returns the Height value in pixels.</value>
        public short Height { get; }
        /// <summary>
        /// Graphic identifier used in <see cref="FPG"/> files.
        /// </summary>
        /// <value>Gets or sets the graphic indentifier for this <see cref="MAP"/> object.</value>
        public int GraphId
        {
            get => this._graphId;
            set
            {
                if (!value.IsClamped(MIN_GRAPH_ID, MAX_GRAPH_ID))
                    throw GRAPHID_OUT_OF_RANGE;

                this._graphId = value;
            }
        }
        /// <summary>
        /// Optional graphic description.
        /// </summary>
        /// <value>Gets or sets the description for this <see cref="MAP"/> object.</value>
        /// <remarks>The description field in the file only allows a 32 length ASCII null terminated string.
        /// If the input string is shorter than 32 characters, the string is filled with null chars.
        /// If the input string is longer than 32 characters, getting a 32 characters length substring.</remarks>
        public string Description { get; set; }
        /// <summary>
        /// Color palette used by this graphic map.
        /// </summary>
        /// <value>Returns the <see cref="PAL"/> instance for this <see cref="MAP"/> object.</value>
        public PAL Palette { get; private set; }
        /// <summary>
        /// Optional control point list.
        /// </summary>
        /// <value>Returns the <see cref="ControlPoint"/> list for this <see cref="MAP"/> object.</value>
        public List<ControlPoint> ControlPoints { get; private set; }
        /// <summary>
        /// Number of pixels in the bitmap.
        /// </summary>
        /// <value>Returns the number of pixels for this <see cref="MAP"/> object.</value>
        public int Count => this._bitmap.Length;
        /// <summary>
        /// Gets or sets the color index in the bitmap.
        /// </summary>
        /// <param name="index">Pixel index in the bitmap array.</param>
        /// <returns>Returns the color index in the <see cref="PAL"/> instance.</returns>
        public byte this[int index]
        {
            get
            {
                return !index.IsClamped(0, this._bitmap.Length)
                    ? throw new IndexOutOfRangeException(string.Format(INDEX_OUT_OF_RANGE_EXCEPTION_MESSAGE, this._bitmap.Length, index))
                    : this._bitmap[index];
            }
            set
            {
                if (!index.IsClamped(0, this._bitmap.Length))
                    throw new IndexOutOfRangeException(string.Format(INDEX_OUT_OF_RANGE_EXCEPTION_MESSAGE, this._bitmap.Length, index));

                this._bitmap[index] = value;
            }
        }
        /// <summary>
        /// Gets or sets the color index in the bitmap.
        /// </summary>
        /// <param name="x">Horizontal coordinate of the pixel in the bitmap.</param>
        /// <param name="y">Vertical coordinate of the pixel in the bitmap.</param>
        /// <returns>Returns the color index in the <see cref="PAL"/> instance.</returns>
        public byte this[int x, int y]
        {
            get
            {
                return !x.IsClamped(0, this.Width - 1)
                    ? throw new IndexOutOfRangeException(string.Format(COORDINATE_OUT_OF_RANGE_EXCEPTION_MESSAGE, "X", this.Width, x))
                    : !y.IsClamped(0, this.Height - 1)
                        ? throw new IndexOutOfRangeException(string.Format(COORDINATE_OUT_OF_RANGE_EXCEPTION_MESSAGE, "Y", this.Height, y))
                        : this._bitmap[this.GetIndex(x, y)];
            }
            set
            {
                if (!x.IsClamped(0, this.Width - 1))
                    throw new IndexOutOfRangeException(string.Format(COORDINATE_OUT_OF_RANGE_EXCEPTION_MESSAGE, "X", this.Width, x));

                if (!y.IsClamped(0, this.Height - 1))
                    throw new IndexOutOfRangeException(string.Format(COORDINATE_OUT_OF_RANGE_EXCEPTION_MESSAGE, "Y", this.Height, y));

                this._bitmap[this.GetIndex(x, y)] = value;
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="a">Left <see cref="MAP"/> value to compare.</param>
        /// <param name="b">Right <see cref="MAP"/> value to compare.</param>
        /// <returns>Returns <see langword="true"/> if both values are equal.</returns>
        public static bool operator ==(MAP a, MAP b)
        {
            if (a.Width == b.Width &&
                a.Height == b.Height &&
                a.GraphId == b.GraphId &&
                a.Description == b.Description &&
                a.Palette == b.Palette)
            {
                if (a.ControlPoints.Count != b.ControlPoints.Count)
                    return false;

                for (int i = 0; i < a.ControlPoints.Count; i++)
                    if (a.ControlPoints[i] != b.ControlPoints[i])
                        return false;

                for (int i = 0; i < a._bitmap.Length; i++)
                    if (a[i] != b[i])
                        return false;

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="a">Left <see cref="MAP"/> value to compare.</param>
        /// <param name="b">Right <see cref="MAP"/> value to compare.</param>
        /// <returns>Returns <see langword="true"/> if both values are not equal.</returns>
        public static bool operator !=(MAP a, MAP b)
        {
            return !(a == b);
        }
        #endregion

        #region Constructors
        MAP()
        {
            this.ControlPoints = new List<ControlPoint>();
        }

        /// <summary>
        /// Creates a new <see cref="MAP"/> instance.
        /// </summary>
        /// <param name="palette"><see cref="PAL"/> instance for this <see cref="MAP"/> instance.</param>
        /// <param name="width">Bitmap width.</param>
        /// <param name="height">Bitmap height.</param>
        /// <param name="graphId"><see cref="MAP"/> graphic identifiers. By default is 1.</param>
        /// <param name="description">Optional <see cref="MAP"/> description.</param>
        /// <remarks>The <paramref name="description"/> field in the file only allows a 32 length ASCII null terminated string.
        /// If the input string is shorter than 32 characters, the string is filled with null chars.
        /// If the input string is longer than 32 characters, getting a 32 characters length substring.</remarks>
        public MAP(PAL palette, short width, short height, int graphId = MIN_GRAPH_ID, string description = "")
            : this()
        {
            if (width < MIN_PIXEL_SIZE)
                throw new ArgumentOutOfRangeException(string.Format(PIXEL_OUT_OF_RANGE_EXCEPTION_MESSAGE, nameof(this.Width), width));
            if (height < MIN_PIXEL_SIZE)
                throw new ArgumentOutOfRangeException(string.Format(PIXEL_OUT_OF_RANGE_EXCEPTION_MESSAGE, nameof(this.Height), height));
            if (!graphId.IsClamped(MIN_GRAPH_ID, MAX_GRAPH_ID))
                throw GRAPHID_OUT_OF_RANGE;

            this._bitmap = new byte[width * height];

            this.Palette = palette;
            this.Width = width;
            this.Height = height;
            this.GraphId = graphId;
            this.Description = description;
        }

        /// <summary>
        /// Loads a <see cref="MAP"/> file.
        /// </summary>
        /// <param name="filename">Filename to load.</param>
        public MAP(string filename)
            : this(File.ReadAllBytes(filename))
        {
        }

        /// <summary>
        /// Loads a <see cref="MAP"/> file from memory.
        /// </summary>
        /// <param name="buffer"><see cref="byte"/> array that contain the <see cref="MAP"/> file data to load.</param>
        public MAP(byte[] buffer)
            : this()
        {
            try
            {
                using (var stream = new BinaryReader(new MemoryStream(buffer)))
                {
                    if (MAP_FILE_HEADER.Validate(stream.ReadBytes(DIVFileHeader.SIZE)))
                    {
                        this.Width = stream.ReadInt16();
                        this.Height = stream.ReadInt16();
                        this.GraphId = stream.ReadInt32();
                        this.Description = stream.ReadBytes(DESCRIPTION_LENGTH).ToASCIIString();
                        this.Palette = new PAL(new ColorPalette(stream.ReadBytes(ColorPalette.SIZE)),
                                               new ColorRangeTable(stream.ReadBytes(ColorRangeTable.SIZE)));

                        short points = Math.Min(stream.ReadInt16(), (short)MAX_CONTROL_POINTS);
                        for (int i = 0; i < points; i++)
                        {
                            var point = new ControlPoint(stream);
                            if (point.x < 0 || point.y < 0)
                            {
                                // If the control point has values under zero (x:-1, y:-1) means that this point has not defined values in DIV Games Studio MAP editor.
                                // DIV Games Studio read this values but when used for drawing a MAP this values are the MAP center coordintates.
                                point.x = (short)(this.Width / 2);
                                point.y = (short)(this.Height / 2);
                            }
                            this.ControlPoints.Add(point);
                        }

                        this._bitmap = stream.ReadBytes(this.Width * this.Height);
                    }
                    else
                        throw new DIVFormatHeaderException();
                }
            }
            catch (Exception ex)
            {
                throw new DIVFileFormatException<MAP>(ex);
            }
        }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Creates a new <see cref="MAP"/> instance from a supported image format.
        /// </summary>
        /// <param name="filename">Image file to load.</param>
        /// <returns>Returns a new <see cref="MAP"/> instance from the loaded image.</returns>
        /// <remarks>Supported image formats are JPEG, PNG, BMP, GIF and TGA. Also supported 256 color PCX images.</remarks>
        public static MAP FromImage(string filename)
        {
            return ValidateFormat(filename)
                ? throw new ArgumentException($"The filename is a {nameof(MAP)} file. Use the constructor to load a {nameof(MAP)} file or " +
                $"indicate a {nameof(PAL)} file to apply color conversion.")
                : FromImage(File.ReadAllBytes(filename));
        }

        /// <summary>
        /// Creates a new <see cref="MAP"/> instance from a supported image format.
        /// </summary>
        /// <param name="buffer"><see cref="byte"/> array that contain a supported image.</param>
        /// <returns>Returns a new <see cref="MAP"/> instance from the loaded image.</returns>
        /// <remarks>Supported image formats are JPEG, PNG, BMP, GIF and TGA. Also supported 256 color PCX images.</remarks>
        public static MAP FromImage(byte[] buffer)
        {
            if (ValidateFormat(buffer))
                throw new ArgumentException($"The buffer contains a {nameof(MAP)} file. Use the constructor to load a {nameof(MAP)} file or " +
                    $"indicate a {nameof(PAL)} file to apply color conversion.");

            BMP256Converter.Convert(buffer, out byte[] palette, out short width, out short height, out byte[] bitmap);

            var pal = new PAL(palette.ToColorArray().ToDAC());
            return new MAP(pal, width, height) { _bitmap = bitmap };
        }

        /// <summary>
        /// Creates a new <see cref="MAP"/> instance from a supported image format and converts the colors to the selected <see cref="PAL"/> instance.
        /// </summary>
        /// <param name="filename">Image file to load.</param>
        /// <param name="palette"><see cref="PAL"/> instance to convert the loaded image.</param>
        /// <returns>Returns a new <see cref="MAP"/> instance from the loaded image.</returns>
        /// <remarks>Supported image formats are JPEG, PNG, BMP, GIF and TGA. 
        /// Also supported 256 color PCX images and <see cref="MAP"/> files, that will be converted to the new setup <see cref="PAL"/>.</remarks>
        public static MAP FromImage(string filename, PAL palette)
        {
            return FromImage(File.ReadAllBytes(filename), palette);
        }

        /// <summary>
        /// Creates a new <see cref="MAP"/> instance from a supported image format and converts the colors to the selected <see cref="PAL"/> instance.
        /// </summary>
        /// <param name="buffer"><see cref="byte"/> array that contain a supported image.</param>
        /// <param name="palette"><see cref="PAL"/> instance to convert the loaded image.</param>
        /// <returns>Returns a new <see cref="MAP"/> instance from the loaded image.</returns>
        /// <remarks>Supported image formats are JPEG, PNG, BMP, GIF and TGA. 
        /// Also supported 256 color PCX images and <see cref="MAP"/> files, that will be converted to the new setup <see cref="PAL"/>.</remarks>
        public static MAP FromImage(byte[] buffer, PAL palette)
        {
            BMP256Converter.ConvertTo(buffer, palette.ToRGB().ToByteArray(), out short width, out short height, out byte[] bitmap);

            var map = new MAP(palette, width, height) { _bitmap = bitmap };

            if (ValidateFormat(buffer))
            {
                var old = new MAP(buffer);

                map.GraphId = old.GraphId;
                map.Description = old.Description;
                map.ControlPoints = old.ControlPoints;
            }

            return map;
        }

        int GetIndex(int x, int y)
        {
            return (this.Width * y) + x;
        }

        /// <summary>
        /// Gets the bitmap array data of this instance.
        /// </summary>
        /// <returns>Returns a <see cref="byte"/> array with all pixels with their color indexes from the <see cref="PAL"/> instance.</returns>
        public byte[] GetBitmapArray() => this._bitmap;

        /// <summary>
        /// Sets the bitmap array data for this instance.
        /// </summary>
        /// <param name="pixels"><see cref="byte"/> array that contains pixel data for this instance.</param>
        public void SetBitmapArray(byte[] pixels)
        {
            if (pixels.Length != this._bitmap.Length)
                throw new ArgumentOutOfRangeException($"The pixel array must be had the same length that this bitmap instance ({this._bitmap.Length} bytes).");

            this._bitmap = pixels;
        }

        /// <summary>
        /// Clear the bitmap.
        /// </summary>
        /// <remarks>This function sets all pixels to zero palette color (mostly transparent black).</remarks>
        public void Clear()
        {
            this._bitmap = new byte[this.Width * this.Height];
        }

        /// <summary>
        /// Validates if the file is a valid <see cref="MAP"/> file.
        /// </summary>
        /// <param name="filename">File to validate.</param>
        /// <returns>Returns <see langword="true"/> if the file is a valid <see cref="MAP"/>.</returns>
        public static bool ValidateFormat(string filename)
        {
            return VALIDATOR.Validate(filename);
        }

        /// <summary>
        /// Validates if the file is a valid <see cref="MAP"/> file.
        /// </summary>
        /// <param name="buffer">Memory buffer that contain a <see cref="MAP"/> file data.</param>
        /// <returns>Returns <see langword="true"/> if the file is a valid <see cref="MAP"/>.</returns>
        public static bool ValidateFormat(byte[] buffer)
        {
            return VALIDATOR.Validate(buffer);
        }

        /// <summary>
        /// Validates if the file is a valid <see cref="MAP"/> file.
        /// </summary>
        /// <param name="filename">File to validate.</param>
        /// <returns>Returns <see langword="true"/> if the file is a valid <see cref="MAP"/>.</returns>
        public bool Validate(string filename)
        {
            return this.Validate(File.ReadAllBytes(filename));
        }

        /// <summary>
        /// Validates if the file is a valid <see cref="MAP"/> file.
        /// </summary>
        /// <param name="buffer">Memory buffer that contain a <see cref="MAP"/> file data.</param>
        /// <returns>Returns <see langword="true"/> if the file is a valid <see cref="MAP"/>.</returns>
        public bool Validate(byte[] buffer)
        {
            return MAP_FILE_HEADER.Validate(buffer[0..DIVFileHeader.SIZE]) && this.TryToReadFile(buffer);
        }

        bool TryToReadFile(byte[] buffer)
        {
            try
            {
                using (var stream = new BinaryReader(new MemoryStream(buffer)))
                {
                    stream.ReadBytes(DIVFileHeader.SIZE); // DIV Header.

                    short width = stream.ReadInt16();
                    short height = stream.ReadInt16();
                    int bitmapSize = width * height;

                    stream.ReadInt32(); // GraphId.
                    stream.ReadBytes(DESCRIPTION_LENGTH); // Description.

                    // Palette:
                    stream.ReadBytes(ColorPalette.SIZE);
                    stream.ReadBytes(ColorRangeTable.SIZE);

                    short points = stream.ReadInt16(); // Control points counter.
                    if (points > 0)
                        stream.ReadBytes(points * ControlPoint.SIZE); // Control points list.

                    stream.ReadBytes(bitmapSize); // Bitmap data.
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
            using (var stream = new BinaryWriter(new MemoryStream()))
            {
                stream.Write(this.Width);
                stream.Write(this.Height);
                stream.Write(this.GraphId);

                byte[] description = this.Description.GetASCIIZString(DESCRIPTION_LENGTH);
                stream.Write(description);

                this.Palette.Write(stream);

                short count = (short)Math.Min(this.ControlPoints.Count, MAX_CONTROL_POINTS);
                stream.Write(count);

                for (int i = 0; i < count; i++)
                    this.ControlPoints[i].Write(stream);

                stream.Write(this._bitmap);

                return (stream.BaseStream as MemoryStream).ToArray();
            }
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        public void Write(BinaryWriter stream)
        {
            stream.Write(this.Serialize());
        }

        /// <summary>
        /// Saves the instance in a <see cref="MAP"/> file.
        /// </summary>
        /// <param name="filename">Filename to write the data.</param>
        public void Save(string filename)
        {
            using (var stream = new BinaryWriter(File.OpenWrite(filename)))
            {
                MAP_FILE_HEADER.Write(stream);
                this.Write(stream);
            }
        }

        internal byte[] SerializeFile()
        {
            using (var stream = new BinaryWriter(new MemoryStream()))
            {
                MAP_FILE_HEADER.Write(stream);
                this.Write(stream);

                return (stream.BaseStream as MemoryStream).ToArray();
            }
        }

        /// <inheritdoc/>
        [DocFxIgnore]
        public IEnumerator<byte> GetEnumerator()
        {
            return new MAPEnumerator(this._bitmap);
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
            return obj is MAP map && this == map;
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
        /// Converts all pixel indexes to the RGB <see cref="Color"/> value from this associated <see cref="PAL"/> instance.
        /// </summary>
        /// <returns>Returns a new <see cref="Color"/> array with all pixel data from this bitmap. All colors are RGB format [0..255].</returns>
        /// <remarks>Use this function when need to render this <see cref="MAP"/> in any modern system that works in 24 or 32 bits color space.</remarks>
        public Color[] GetRGBTexture()
        {
            return this._bitmap.Select(e => this.Palette[e].ToRGB()).ToArray();
        }

        /// <summary>
        /// Serializes the relevant data of this instance in a <see cref="string"/> value.
        /// </summary>
        /// <returns>Returns a <see cref="string"/> value with the relevant serialized data in JSON format.</returns>
        public override string ToString()
        {
            int controlPointsHash = 0;
            foreach (ControlPoint point in this.ControlPoints)
                controlPointsHash ^= point.GetHashCode();

            var sb = new StringBuilder();

            sb.Append($"{{ {nameof(MAP)}: ");
            sb.Append($"{{ Hash: {this.GetHashCode()}, ");
            sb.Append($"Width: {this.Width}, ");
            sb.Append($"Height: {this.Height}, ");
            sb.Append($"Graph Id: {this.GraphId}, ");
            sb.Append($"Description: \"{this.Description}\", ");
            sb.Append($"Palette Hash: {this.Palette.GetHashCode()}, ");
            sb.Append($"Control Points: {{ ");
            sb.Append($"Count: {this.ControlPoints.Count}, ");
            sb.Append($"Hash: {controlPointsHash} }}, ");
            sb.Append($"Bitmap: {{ ");
            sb.Append($"Length: {this.Count}, ");
            sb.Append($"Hash: {this._bitmap.CalculateChecksum().GetSecureHashCode()} }} }}");
            sb.Append(" }");

            return sb.ToString();
        }
        #endregion
    }
}
