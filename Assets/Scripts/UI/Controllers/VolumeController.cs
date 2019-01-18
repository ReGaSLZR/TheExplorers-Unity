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
		SetSliderListeners();
	}

	private void Start() {
		SetSliderDefaultValues();
	}

	private void SetSliderDefaultValues() {
		m_SliderBGM.value = m_volumeGetter.GetVolumeBgm().Value;
		m_SliderSFX.value = m_volumeGetter.GetVolumeSfx().Value;
	}

	private void SetSliderListeners() {
		m_SliderBGM.OnPointerUpAsObservable()
			.Subscribe(_ => m_volumeSetter.SetVolumeBgm(m_SliderBGM.value))
			.AddTo(this);	

		m_SliderSFX.OnPointerUpAsObservable()
			.Subscribe(_ => m_volumeSetter.SetVolumeSfx(m_SliderSFX.value))
			.AddTo(this);	
	}

}

