﻿using DIV2.Format.Exporter.ExtensionMethods;
using DIV2.Format.Exporter.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DIV2.Format.Exporter
{
    class ColorRangeTableEnumerator : IEnumerator<ColorRange>
    {
        #region Internal vars
        IList<ColorRange> _items;
        int _currentIndex;
        #endregion

        #region Properties
        public ColorRange Current { get; private set; }
        object IEnumerator.Current => this.Current;
        #endregion

        #region Constructor & Destructor
        public ColorRangeTableEnumerator(IList<ColorRange> items)
        {
            this._items = items;
            this.Current = default(ColorRange);
            this.Reset();
        }

        void IDisposable.Dispose()
        {
        }
        #endregion

        #region Methods & Functions
        public bool MoveNext()
        {
            if (++this._currentIndex >= this._items.Count)
                return false;
            else
                this.Current = this._items[this._currentIndex];

            return true;
        }

        public void Reset()
        {
            this._currentIndex = -1;
        }
        #endregion
    }

    /// <summary>
    /// A collection of 16 ranges that composes the <see cref="PAL"/> <see cref="ColorRange"/> table.
    /// </summary>
    /// <remarks>The color ranges are used only in DIV Games Studio color and drawing tools. Not are used in games. 
    /// By default is enough to creates a <see cref="ColorRangeTable"/> with default values.</remarks>
    public sealed class ColorRangeTable : ISerializableAsset, IEnumerable<ColorRange>
    {
        #region Constants
        readonly static IndexOutOfRangeException INDEX_OUT_OF_RANGE_EXCEPTION = 
            new IndexOutOfRangeException($"The index value must be a value beteween 0 and {LENGTH}.");

        /// <summary>
        /// Number of <see cref="ColorRange"/>s in the table.
        /// </summary>
        public const int LENGTH = 16;
        /// <summary>
        /// Memory size of the color range table.
        /// </summary>
        public const int SIZE = LENGTH * ColorRange.SIZE;
        #endregion

        #region Internal vars
        ColorRange[] _ranges = new ColorRange[LENGTH];
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the <see cref="ColorRange"/> value.
        /// </summary>
        /// <param name="index">Index of the range.</param>
        /// <returns>Returns the <see cref="ColorRange"/> value.</returns>
        public ColorRange this[int index]
        {
            get
            {
                if (!index.IsClamped(0, LENGTH))
                    throw INDEX_OUT_OF_RANGE_EXCEPTION;

                return this._ranges[index];
            }
            set
            {
                if (!index.IsClamped(0, LENGTH))
                    throw INDEX_OUT_OF_RANGE_EXCEPTION;

                this._ranges[index] = value;
            }
        }
        #endregion

        #region Operators
        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="a">Left <see cref="ColorRangeTable"/> value to compare.</param>
        /// <param name="b">Right <see cref="ColorRangeTable"/> value to compare.</param>
        /// <returns>Returns <see langword="true"/> if both values are equal.</returns>
        public static bool operator ==(ColorRangeTable a, ColorRangeTable b)
        {
            for (int i = 0; i < LENGTH; i++)
                if (a[i] != b[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="a">Left <see cref="ColorRangeTable"/> value to compare.</param>
        /// <param name="b">Right <see cref="ColorRangeTable"/> value to compare.</param>
        /// <returns>Returns <see langword="true"/> if both values are not equal.</returns>
        public static bool operator !=(ColorRangeTable a, ColorRangeTable b)
        {
            return !(a == b);
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new <see cref="ColorRangeTable"/> with default <see cref="ColorRange"/> values.
        /// </summary>
        public ColorRangeTable()
        {
            byte range = 0;
            for (int i = 0; i < LENGTH; i++)
                this._ranges[i] = new ColorRange(ref range);
        }

        /// <summary>
        /// Creates a new <see cref="ColorRangeTable"/> from memory.
        /// </summary>
        /// <param name="buffer">A <see cref="byte"/> array that contains a <see cref="ColorRangeTable"/> data.</param>
        public ColorRangeTable(byte[] buffer)
        {
            if (buffer.Length != SIZE)
                throw new ArgumentOutOfRangeException();

            using (var stream = new BinaryReader(new MemoryStream(buffer)))
            {
                for (int i = 0; i < LENGTH; i++)
                    this._ranges[i] = new ColorRange(stream.ReadBytes(ColorRange.SIZE));
            }
        }
        #endregion

        #region Methods & Functions
        /// <summary>
        /// Serializes this instance to binary format.
        /// </summary>
        /// <returns>Returns a <see cref="byte"/> array with the serialized data.</returns>
        public byte[] Serialize()
        {
            using (var buffer = new MemoryStream())
            {
                foreach (ColorRange range in this._ranges)
                    buffer.Write(range.Serialize());

                return buffer.ToArray();
            }
        }

        /// <summary>
        /// Writes this instance data in a <see cref="BinaryWriter"/> instance.
        /// </summary>
        /// <param name="stream"><see cref="BinaryWriter"/> instance.</param>
        public void Write(BinaryWriter stream)
        {
            stream.Write(this.Serialize());
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection. 
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<ColorRange> GetEnumerator()
        {
            return new ColorRangeTableEnumerator(this._ranges);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection. 
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal. 
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ColorRangeTable)) return false;

            return this == (ColorRangeTable)obj;
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
            return $"{{ {nameof(ColorRangeTable)}: {{ Hash: {this.GetHashCode()} }} }}";
        }
        #endregion
    }
}
