using System;

public class KeyValue
{
	public KeyValue(Key key, byte[] value)
	{
		this.Key = key;
		this.Value = value;
	}

	public Key Key { get; set; }

	public byte[] Value { get; set; }

	public override string ToString()
	{
		return string.Format("[KeyValue: {0}, {1}, {2} bytes]", this.Key.Field, this.Key.WireType, this.Value.Length);
	}
}
