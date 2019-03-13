﻿using System;

public abstract class PacketFormat
{
	public abstract int Decode(byte[] bytes, int offset, int available);

	public abstract byte[] Encode();

	public abstract bool IsLoaded();
}
