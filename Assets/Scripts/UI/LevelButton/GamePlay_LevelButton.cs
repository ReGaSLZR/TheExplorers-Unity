using UnityEngine;
using Zenject;

public class GamePlay_LevelButton : LevelButton {

	[Inject]
	GamePlay_PanelModel.Setter m_panelModel;

	private void OnEnable() {
		RegisterOnClickListener();
	}

	private void Start() {
		CheckIfUnlocked();
	}

	protected override void OnLevelButtonClick() {
		m_panelModel.ShowLoading();
	}

}
