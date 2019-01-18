using UnityEngine;
using UniRx;

public class VolumeModel : MonoBehaviour,
						   VolumeModel.Getter,
						   VolumeModel.Setter
{

	/* INTERFACES ---------------------------------------------------------------------------------------- */

	public interface Getter {

		ReactiveProperty<float> GetVolumeBgm();

		ReactiveProperty<float> GetVolumeSfx();

	}

	public interface Setter {

		void SetVolumeBgm(float volumeBgm);

		void SetVolumeSfx(float volumeSfx);

		void ResetVolume();

	}

	/* ---------------------------------------------------------------------------------------- */

	private ReactiveProperty<float> m_bgm;
	private ReactiveProperty<float> m_sfx;

	public VolumeModel() {
		m_bgm = new ReactiveProperty<float>();
		m_sfx = new ReactiveProperty<float>();

		ResetVolume();
	}

	private void OnEnable() {
		m_bgm.Value = PlayerPrefsUtil.GetVolumeBgm();
		m_sfx.Value = PlayerPrefsUtil.GetVolumeSfx();
	}

	private bool IsVolumeValid(float volume) {
		bool isValid = ((volume <= TheExplorersConfig.VOLUME_MAX) && (volume >= TheExplorersConfig.VOLUME_MIN));
		LogUtil.PrintInfo(this.gameObject, this.GetType(), "IsVolumeValid(): " + volume + " == " + isValid);

		return isValid;
	}

	/* VolumeModel_Getter ---------------------------------------------------------------------------------------- */

	public ReactiveProperty<float> GetVolumeBgm() {
		return m_bgm;
	}

	public ReactiveProperty<float> GetVolumeSfx() {
		return m_sfx;
	}

	/* VolumeModel_Setter ---------------------------------------------------------------------------------------- */

	public void SetVolumeBgm(float volumeBgm) {
		LogUtil.PrintInfo(this.gameObject, this.GetType(), "SetVolumeBgm()");

		m_bgm.Value = IsVolumeValid(volumeBgm) ? volumeBgm : m_bgm.Value;
		PlayerPrefsUtil.SaveVolumeBgm(m_bgm.Value);
	}

	public void SetVolumeSfx(float volumeSfx) {
		LogUtil.PrintInfo(this.gameObject, this.GetType(), "SetVolumeSfx()");

		m_sfx.Value = IsVolumeValid(volumeSfx) ? volumeSfx : m_sfx.Value;
		PlayerPrefsUtil.SaveVolumeSfx(m_sfx.Value);
	}

	public void ResetVolume() {
		m_bgm.Value = TheExplorersConfig.VOLUME_DEFAULT;
		m_sfx.Value = TheExplorersConfig.VOLUME_DEFAULT;
	}

} //end of class VolumeModel