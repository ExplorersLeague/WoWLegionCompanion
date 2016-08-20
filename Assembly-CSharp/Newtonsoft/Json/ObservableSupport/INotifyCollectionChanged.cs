using System;

namespace Newtonsoft.Json.ObservableSupport
{
	public interface INotifyCollectionChanged
	{
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
