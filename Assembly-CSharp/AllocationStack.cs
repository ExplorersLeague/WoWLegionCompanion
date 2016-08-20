using System;
using System.IO;

public class AllocationStack : IDisposable, MemoryStreamStack
{
	public MemoryStream Pop()
	{
		return new MemoryStream();
	}

	public void Push(MemoryStream stream)
	{
	}

	public void Dispose()
	{
	}
}
