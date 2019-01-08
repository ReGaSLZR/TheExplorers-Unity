using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using Zenject;

public class VolumeController : MonoBehaviour
{

	[Inject]
	VolumeModel.Getter m_volumeGetter;

	[Inject]
	VolumeModel.Setter m_volumeSetter;

	[SerializeField]
	private Slider m_SliderBGM;

	[SerializeField]
	private Slider m_SliderSFX;

	private void Awake() {
		if(m_SliderBGM == null || m_SliderSFX == null) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Missing slider/s.");
		}
	}

	private void OnEnable() {
		SetSliderDefaultValues();
		SetSliderListeners();
	}

	private void SetSliderDefaultValues() {
		m_SliderBGM.value = m_volumeGetter.GetVolumeBgm().Value;
		m_SliderSFX.value = m_volumeGetter.GetVolumeSfx().Value;
	}

	private void SetSliderListeners() {
		m_SliderBGM.OnValueChangedAsObservable()
			.Subscribe(value => m_volumeSetter.SetVolumeBgm(value))
			.AddTo(this);	

		m_SliderSFX.OnValueChangedAsObservable()
			.Subscribe(value => m_volumeSetter.SetVolumeSfx(value))
			.AddTo(this);	
	}

}

