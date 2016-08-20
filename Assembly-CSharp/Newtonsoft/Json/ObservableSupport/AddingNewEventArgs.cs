using System;

namespace Newtonsoft.Json.ObservableSupport
{
	public class AddingNewEventArgs
	{
		public AddingNewEventArgs()
		{
		}

		public AddingNewEventArgs(object newObject)
		{
			this.NewObject = newObject;
		}

		public object NewObject { get; set; }
	}
}
