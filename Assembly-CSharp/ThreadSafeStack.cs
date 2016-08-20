using System;
using System.Collections.Generic;
using System.IO;

public class ThreadSafeStack : IDisposable, MemoryStreamStack
{
	public MemoryStream Pop()
	{
		Stack<MemoryStream> obj = this.stack;
		MemoryStream result;
		lock (obj)
		{
			if (this.stack.Count == 0)
			{
				result = new MemoryStream();
			}
			else
			{
				result = this.stack.Pop();
			}
		}
		return result;
	}

	public void Push(MemoryStream stream)
	{
		Stack<MemoryStream> obj = this.stack;
		lock (obj)
		{
			this.stack.Push(stream);
		}
	}

	public void Dispose()
	{
		Stack<MemoryStream> obj = this.stack;
		lock (obj)
		{
			this.stack.Clear();
		}
	}

	private Stack<MemoryStream> stack = new Stack<MemoryStream>();
}
