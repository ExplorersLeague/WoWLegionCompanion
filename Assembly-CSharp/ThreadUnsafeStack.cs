using System;
using System.Collections.Generic;
using System.IO;

public class ThreadUnsafeStack : MemoryStreamStack, IDisposable
{
	public MemoryStream Pop()
	{
		if (this.stack.Count == 0)
		{
			return new MemoryStream();
		}
		return this.stack.Pop();
	}

	public void Push(MemoryStream stream)
	{
		this.stack.Push(stream);
	}

	public void Dispose()
	{
		this.stack.Clear();
	}

	private Stack<MemoryStream> stack = new Stack<MemoryStream>();
}
