using System;
using System.Runtime.InteropServices;
using System.Text;

namespace bgs
{
	public static class MemUtils
	{
		public static void FreePtr(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return;
			}
			Marshal.FreeHGlobal(ptr);
		}

		public static byte[] PtrToBytes(IntPtr ptr, int size)
		{
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			if (size == 0)
			{
				return null;
			}
			byte[] array = new byte[size];
			Marshal.Copy(ptr, array, 0, size);
			return array;
		}

		public static IntPtr PtrFromBytes(byte[] bytes)
		{
			return MemUtils.PtrFromBytes(bytes, 0);
		}

		public static IntPtr PtrFromBytes(byte[] bytes, int offset)
		{
			if (bytes == null)
			{
				return IntPtr.Zero;
			}
			int len = bytes.Length - offset;
			return MemUtils.PtrFromBytes(bytes, offset, len);
		}

		public static IntPtr PtrFromBytes(byte[] bytes, int offset, int len)
		{
			if (bytes == null)
			{
				return IntPtr.Zero;
			}
			if (len <= 0)
			{
				return IntPtr.Zero;
			}
			IntPtr intPtr = Marshal.AllocHGlobal(len);
			Marshal.Copy(bytes, offset, intPtr, len);
			return intPtr;
		}

		public static byte[] StructToBytes<T>(T t)
		{
			int num = Marshal.SizeOf(typeof(T));
			byte[] array = new byte[num];
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr(t, intPtr, true);
			Marshal.Copy(intPtr, array, 0, num);
			Marshal.FreeHGlobal(intPtr);
			return array;
		}

		public static T StructFromBytes<T>(byte[] bytes)
		{
			return MemUtils.StructFromBytes<T>(bytes, 0);
		}

		public static T StructFromBytes<T>(byte[] bytes, int offset)
		{
			Type typeFromHandle = typeof(T);
			int num = Marshal.SizeOf(typeFromHandle);
			if (bytes == null)
			{
				return default(T);
			}
			if (bytes.Length - offset < num)
			{
				return default(T);
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(bytes, offset, intPtr, num);
			T result = (T)((object)Marshal.PtrToStructure(intPtr, typeFromHandle));
			Marshal.FreeHGlobal(intPtr);
			return result;
		}

		public static IntPtr Utf8PtrFromString(string managedString)
		{
			if (managedString == null)
			{
				return IntPtr.Zero;
			}
			int num = 1 + Encoding.UTF8.GetByteCount(managedString);
			byte[] array = new byte[num];
			Encoding.UTF8.GetBytes(managedString, 0, managedString.Length, array, 0);
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.Copy(array, 0, intPtr, num);
			return intPtr;
		}

		public static string StringFromUtf8Ptr(IntPtr ptr)
		{
			int num;
			return MemUtils.StringFromUtf8Ptr(ptr, out num);
		}

		public static string StringFromUtf8Ptr(IntPtr ptr, out int len)
		{
			len = 0;
			if (ptr == IntPtr.Zero)
			{
				return null;
			}
			len = MemUtils.StringPtrByteLen(ptr);
			if (len == 0)
			{
				return null;
			}
			byte[] array = new byte[len];
			Marshal.Copy(ptr, array, 0, len);
			return Encoding.UTF8.GetString(array);
		}

		public static int StringPtrByteLen(IntPtr ptr)
		{
			if (ptr == IntPtr.Zero)
			{
				return 0;
			}
			int num = 0;
			while (Marshal.ReadByte(ptr, num) != 0)
			{
				num++;
			}
			return num;
		}
	}
}
