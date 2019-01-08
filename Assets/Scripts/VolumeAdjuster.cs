using UnityEngine;
using UniRx;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class VolumeAdjuster : MonoBehaviour {

	[Tooltip("NOTE: Runtime changes on 'audioType' won't take effect. This is to make the class listen to one Observable only.")]
	[SerializeField] private AUDIO_TYPE m_audioType;

	[Inject] VolumeModel.Getter m_volumeModel;

	private AudioSource _audioSource;

	private void Awake() {
		_audioSource = this.GetComponent<AudioSource>();
	}

	private void Start () {
		if(AUDIO_TYPE.BGM == m_audioType) {
			m_volumeModel.GetVolumeBgm()
				.Subscribe(bgmVol => _audioSource.volume = bgmVol)
				.AddTo(this);
		} else {
			m_volumeModel.GetVolumeSfx()
				.Subscribe(sfxVol => _audioSource.volume = sfxVol)
				.AddTo(this);
		}
	}

}
