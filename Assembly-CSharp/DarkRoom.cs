using System;
using UnityEngine;
using UnityEngine.UI;

public class DarkRoom
{
	public static void MakeSnapshot(FreezeFrame freezeFrame)
	{
		GameObject gameObject = GameObject.Find("MainCanvas");
		if (gameObject == null)
		{
			Debug.LogError("Could not find MainCanvas, did you rename it?");
			return;
		}
		GameObject gameObject2 = new GameObject(freezeFrame.name + "_SnaphotCamera");
		gameObject2.transform.SetParent(null, false);
		gameObject2.transform.Translate(0f, 0f, -1f);
		Camera camera = gameObject2.AddComponent<Camera>();
		RenderTexture renderTexture = new RenderTexture(320, 320, 24);
		Material material = new Material(Shader.Find("UI/Unlit/Transparent"));
		material.SetTexture("_MainTex", renderTexture);
		camera.orthographic = true;
		camera.orthographicSize = 100f;
		camera.targetTexture = renderTexture;
		camera.depth = -1f;
		camera.clearFlags = 2;
		camera.backgroundColor = new Color(0f, 0f, 0f, 1f);
		GameObject gameObject3 = new GameObject(freezeFrame.name + "_DarkRoomCanvas");
		Canvas canvas = gameObject3.AddComponent<Canvas>();
		canvas.planeDistance = 500f;
		canvas.renderMode = 1;
		canvas.worldCamera = camera;
		CanvasScaler canvasScaler = gameObject3.AddComponent<CanvasScaler>();
		canvasScaler.uiScaleMode = 1;
		canvasScaler.referenceResolution = new Vector2(1200f, 900f);
		canvasScaler.matchWidthOrHeight = 1f;
		GameObject gameObject4 = new GameObject(freezeFrame.name + "_Snapshot");
		gameObject4.transform.SetParent(gameObject.transform, false);
		Image image = gameObject4.AddComponent<Image>();
		gameObject4.GetComponent<RectTransform>().sizeDelta = new Vector2(320f, 320f);
		image.material = material;
		freezeFrame.transform.SetParent(gameObject3.transform, false);
		camera.Render();
	}
}
