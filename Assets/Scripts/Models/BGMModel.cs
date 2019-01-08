using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMModel : MonoBehaviour,
						BGMModel.Setter
{

	public interface Setter {

		void Pause();

		void Play();

		void Play(AudioClip clip);

	}

	private AudioSource _audioSource;

	private void Awake() {
		_audioSource = GetComponent<AudioSource>();
	}

	public void Pause() {
		_audioSource.Pause();
	}

	public void Play() {
		_audioSource.Play();
	}

	public void Play(AudioClip clip) {
		AudioUtil.PlaySingleClip(this.GetType(), clip, _audioSource);
	}

}
