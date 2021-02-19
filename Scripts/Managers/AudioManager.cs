using UnityEngine;
using UnityEngine.Audio;
using NaughtyAttributes;

class AudioManager : MonoBehaviour
{

	[SerializeField, OnValueChanged(nameof(valueChanged))] Sound[] sounds;
	public static AudioManager i { get; private set; }
	void Awake()
	{
		i = this;
	}

	void Start()
	{
		foreach (var sfx in sounds)
		{
			if (sfx.clip == null) continue;
			var source = gameObject.AddComponent<AudioSource>();
			source.volume = sfx.volume;
			source.pitch = sfx.pitch;
			sfx.source = source;
			source.clip = sfx.clip;
		}
	}
	private void valueChanged()
	{
		if (!Application.isPlaying) return;
		foreach (var sfx in sounds)
		{
			var source = sfx.source;
			source.volume = sfx.volume;
			source.pitch = sfx.pitch;
		}
	}

	public void Play(string name)
	{
		var sound = System.Array.Find(sounds, sfx => sfx.name == name);
		sound.source.Play();

	}
	public void Stop(string name)
	{
		var sound = System.Array.Find(sounds, sfx => sfx.name == name);
		sound.source.Stop();

	}
	public void PlayTimed(string name, float seconds)
	{
		var sound = System.Array.Find(sounds, sfx => sfx.name == name);
		sound.source.Play();
		InvokeHandler.i.InvokeAction(() => Stop(name), seconds);

	}

	[SerializeField] int index;
	[NaughtyAttributes.Button] private void Play() => Play(sounds[index].name);


}

[System.Serializable]
class Sound
{
	public string name;
	public AudioClip clip;
	public AudioSource source;
	[Range(0f, 1f)] public float volume;
	[Range(0.1f, 1.5f)] public float pitch = 0.7f;


}