using System;

public class Key
{
	public Key(uint field, Wire wireType)
	{
		this.Field = field;
		this.WireType = wireType;
	}

	public uint Field { get; set; }

	public Wire WireType { get; set; }

	public override string ToString()
	{
		return string.Format("[Key: {0}, {1}]", this.Field, this.WireType);
	}
}
