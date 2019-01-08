using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SFXModel : MonoBehaviour,
				        SFXModel.Setter
{

	public interface Setter {

		void PlaySFX(AudioClip clip);

	}

	private AudioSource _audioSource;

	private void Awake() {
		_audioSource = GetComponent<AudioSource>();
	}

	public void PlaySFX(AudioClip clip) {
		AudioUtil.PlaySingleClip(this.GetType(), clip, _audioSource);
	}
	
}
