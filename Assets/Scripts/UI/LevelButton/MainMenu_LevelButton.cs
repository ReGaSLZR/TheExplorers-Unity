using UnityEngine;
using Zenject;

public class MainMenu_LevelButton : LevelButton {

	[Inject]
	MainMenu_PanelModel.Setter m_buttonsHandler;

	private void Awake() {
		CheckIfUnlocked();
		RegisterOnClickListener();
	}

	protected override void OnLevelButtonClick() {
		m_buttonsHandler.DisableAllElements();
	}

}
