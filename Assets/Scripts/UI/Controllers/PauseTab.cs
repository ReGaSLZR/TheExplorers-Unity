using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseTab : MonoBehaviour {

	private static float ALPHA_INACTIVE = 0.5f;
	private static float ALPHA_ACTIVE = 1f;

	[SerializeField]
	private TextMeshProUGUI m_text;
	[SerializeField]
	private Button m_button;

	[SerializeField]
	private GameObject m_content;

	private void Awake() {
		if(m_content == null) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Content is not defined.");
		}
	}

	public Button GetButton() {
		return m_button;
	}

	public void SetTabActive(bool isActive) {
		SetTabAlpha(isActive);

		m_text.fontStyle = isActive ? FontStyles.Underline : FontStyles.Normal;
		m_content.SetActive(isActive);
	}

	private void SetTabAlpha(bool isActive) {
		Color tempColor = m_text.color;
		tempColor.a = isActive ? ALPHA_ACTIVE : ALPHA_INACTIVE;

		m_text.color = tempColor;
	}

}
