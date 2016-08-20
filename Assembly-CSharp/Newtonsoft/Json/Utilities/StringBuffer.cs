using System;

namespace Newtonsoft.Json.Utilities
{
	internal class StringBuffer
	{
		public StringBuffer()
		{
			this._buffer = StringBuffer._emptyBuffer;
		}

		public StringBuffer(int initalSize)
		{
			this._buffer = new char[initalSize];
		}

		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		public void Append(char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(1);
			}
			this._buffer[this._position++] = value;
		}

		public void Clear()
		{
			this._buffer = StringBuffer._emptyBuffer;
			this._position = 0;
		}

		private void EnsureSize(int appendLength)
		{
			char[] array = new char[(this._position + appendLength) * 2];
			Array.Copy(this._buffer, array, this._position);
			this._buffer = array;
		}

		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		public char[] GetInternalBuffer()
		{
			return this._buffer;
		}

		private char[] _buffer;

		private int _position;

		private static readonly char[] _emptyBuffer = new char[0];
	}
}
