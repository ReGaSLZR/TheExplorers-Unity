using UnityEngine;
using UnityEngine.UI;

public class MainMenu_ButtonsHandler : MonoBehaviour,
									   MainMenu_ButtonsHandler.Setter
{

	public interface Setter {

		void DisableAllButtons();

	}
		
	[SerializeField]
	private Button[] m_buttons;
	
	public void DisableAllButtons() {
		for(int x=0; x<m_buttons.Length; x++) {
			if(m_buttons[x] != null) {
				m_buttons[x].interactable = false;
			}
		}	
	}

}
