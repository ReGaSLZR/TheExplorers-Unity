using UnityEngine;
using UniRx;
using UniRx.Triggers;
using TMPro;

public class MainMenu_PlayerNameController : MonoBehaviour {

	[SerializeField]
	private TMP_InputField m_fieldPlayerName;

	private void Awake() {
		if(m_fieldPlayerName == null) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Input Field PlayerName is missing.");
		}
	}

	private void OnEnable() {
		m_fieldPlayerName.OnDeselectAsObservable()
			.Subscribe(_ => PlayerPrefsUtil.SavePlayerName(m_fieldPlayerName.text))
			.AddTo(this);
	}

	private void Start() {
		m_fieldPlayerName.text = PlayerPrefsUtil.GetPlayerName();
	}

}
