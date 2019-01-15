using UnityEngine;
using UnityEngine.UI;

public class MainMenu_PanelModel : MonoBehaviour,
								   MainMenu_PanelModel.Setter
{

	public interface Setter {

		void DisableAllElements();

	}

	[SerializeField]
	private Button[] m_buttons;

	[Space]
	[SerializeField]
	private GameObject m_panelLoading;

	private void Awake() {
		if(m_panelLoading != null) {
			m_panelLoading.SetActive(false);
		}
		else {
			LogUtil.PrintError(this.gameObject, this.GetType(), "Awake(): Panel Loading is NULL.");
		}
	}

	public void DisableAllElements() {
		LogUtil.PrintInfo(this.gameObject, this.GetType(), "DisableAllElements(): method called.");

		for(int x=0; x<m_buttons.Length; x++) {
			if(m_buttons[x] != null) {
				m_buttons[x].interactable = false;
			}
		}	

		m_panelLoading.SetActive(true);
	}

}
