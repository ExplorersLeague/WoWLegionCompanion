using System;
using System.Linq;
using UnityEngine;

namespace WoWCompanionApp
{
	[CreateAssetMenu(menuName = "WoWCompanion/SoundConfig")]
	public class SoundConfig : ScriptableObject
	{
		protected void OnValidate()
		{
			foreach (string str in (from clip in this.clips
			where this.clips.Count((SoundConfig.AudioClipConfig c) => c.name == clip.name) > 1
			select clip.name).Distinct<string>())
			{
				Debug.LogError("Multiple audio clips defined with name \"" + str + "\"!");
			}
			foreach (SoundConfig.AudioClipConfig audioClipConfig in from clip in this.clips
			where clip.clip == null
			select clip)
			{
				Debug.LogError("No file selected for audio clip: \"" + audioClipConfig.name + "\"");
			}
			foreach (SoundConfig.AudioClipConfig audioClipConfig2 in from clip in this.clips
			where clip.volume == 0f
			select clip)
			{
				Debug.LogWarning("Volume set to 0 for audio clip: \"" + audioClipConfig2.name + "\"");
			}
			foreach (SoundConfig.AudioClipConfig audioClipConfig3 in from clip in this.clips
			where clip.maxInstances == 0
			select clip)
			{
				Debug.LogWarning("Max instances set to 0 for audio clip: \"" + audioClipConfig3.name + "\"");
			}
		}

		public SoundConfig.AudioClipConfig[] clips;

		[Serializable]
		public class AudioClipConfig
		{
			public string name;

			public AudioClip clip;

			[Range(0f, 1f)]
			public float volume = 1f;

			public int maxInstances = 3;
		}
	}
}
