using System;

namespace Newtonsoft.Json.ObservableSupport
{
	public interface INotifyPropertyChanging
	{
		event PropertyChangingEventHandler PropertyChanging;
	}
}
