using System;
using UnityEngine;

[ExecuteInEditMode]
public class NativeEvents : MonoBehaviour
{
	private void Start()
	{
		NativeEvents.RegisterForEvents();
		NativeEvents.WatchForDllLoaded();
	}

	private void OnDestroy()
	{
		Events.Clear();
	}

	[ContextMenu("Re-register events now")]
	private void ResetContextMenu()
	{
		NativeEvents.RegisterForEvents();
	}

	private static void RegisterForEvents()
	{
		Events.Clear();
		EventInterface.RegisterForAllEvents();
	}

	private void LateUpdate()
	{
		Events.SignalPendingUniqueEvents();
	}

	private static void WatchForDllLoaded()
	{
	}
}
