using System;
using System.Collections.Generic;
using System.IO;

public class ThreadSafeStack : MemoryStreamStack, IDisposable
{
	public MemoryStream Pop()
	{
		object obj = this.stack;
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
		object obj = this.stack;
		lock (obj)
		{
			this.stack.Push(stream);
		}
	}

	public void Dispose()
	{
		object obj = this.stack;
		lock (obj)
		{
			this.stack.Clear();
		}
	}

	private Stack<MemoryStream> stack = new Stack<MemoryStream>();
}
