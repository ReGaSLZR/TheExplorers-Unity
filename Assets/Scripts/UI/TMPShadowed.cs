using UnityEngine;
using TMPro;

public class TMPShadowed : MonoBehaviour {

	[SerializeField]
	private TextMeshProUGUI m_textMain;

	[SerializeField]
	private TextMeshProUGUI m_textShadow;

	private void Awake() {
		if(m_textMain == null || m_textShadow == null) {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): incomplete TextMeshProUGUI requirements.");
		}	
	}

	public void SetShadowedText(string text) {
		m_textMain.text = text;
		m_textShadow.text = text;
	}

}
