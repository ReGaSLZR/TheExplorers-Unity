using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller<LevelInstaller>, 
								Instantiator
{
	[Header("Player")]
	[SerializeField]
	private PlayerStatsModel m_playerStatsModel;
	[SerializeField]
	private PlayerGroundModel m_playerGround;
	[SerializeField]
	private PlayerInputModel m_playerInputControls;

	[Header("Audio-related")]
	[SerializeField]
	private BGMModel m_bgmModel;
	[SerializeField]
	private SFXModel m_sfxModel;
	[SerializeField]
	private VolumeModel m_volumeModel;

	[Space]
	[SerializeField]
	private SceneModel m_sceneModel;

	[Space]
	[SerializeField]
	private GamePlay_PanelModel m_gameplaySwitcher;

	[Space]
	[SerializeField]
	private MindLightModel m_mindLightModel;

	[Space]
	[SerializeField]
	private CutsceneModel m_cutsceneModel;

	/* Instantiator ---------------------------------------------------------------------------------------- */

	public void InjectPrefab(GameObject gameObject) {
		Container.InjectGameObject(gameObject);
	}

	public void InjectPrefab(GameObject prefab, GameObject parent) {
		if(prefab != null) {
			InjectPrefab(Instantiate(prefab, parent.transform.position, parent.transform.rotation));	
		}
	}

	/* ---------------------------------------------------------------------------------------- */

    public override void InstallBindings()
    {
		Container.Bind<Instantiator>().FromInstance(this);

		//player stats
		Container.Bind<PlayerStatsModel.Getter>().FromInstance(m_playerStatsModel);
		Container.Bind<PlayerStatsModel.Setter>().FromInstance(m_playerStatsModel);

		//player ground
		Container.Bind<PlayerGroundModel.Getter>().FromInstance(m_playerGround);
		Container.Bind<PlayerGroundModel.Setter>().FromInstance(m_playerGround);

		//player input controls
		Container.Bind<PlayerInputModel>().FromInstance(m_playerInputControls);

		//audio
		Container.Bind<BGMModel.Setter>().FromInstance(m_bgmModel);
		Container.Bind<SFXModel.Setter>().FromInstance(m_sfxModel);
		//volume
		Container.Bind<VolumeModel.Getter>().FromInstance(m_volumeModel);
		Container.Bind<VolumeModel.Setter>().FromInstance(m_volumeModel);

		//scene model
		Container.Bind<SceneModel.Setter>().FromInstance(m_sceneModel);

		//panel switcher
		Container.Bind<GamePlay_PanelModel.Setter>().FromInstance(m_gameplaySwitcher);
		Container.Bind<GamePlay_PanelModel.Freezer>().FromInstance(m_gameplaySwitcher);

		//mindlight model
		Container.Bind<MindLightModel.Setter>().FromInstance(m_mindLightModel);

		//cutscene model
		Container.Bind<CutsceneModel.Setter>().FromInstance(m_cutsceneModel);
		Container.Bind<CutsceneModel.Getter>().FromInstance(m_cutsceneModel);

    }

} //end of class LevelInstaller

public interface Instantiator {
	void InjectPrefab(GameObject gameObject);
	void InjectPrefab(GameObject prefab, GameObject parent);
}