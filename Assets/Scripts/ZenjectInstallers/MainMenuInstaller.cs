using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller<LevelInstaller>
{
	[SerializeField]
	private BGMModel m_bgmModel;
	[SerializeField]
	private SFXModel m_sfxModel;
	[SerializeField]
	private VolumeModel m_volumeModel;
	[SerializeField]
	private SceneModel m_sceneModel;

	/* ---------------------------------------------------------------------------------------- */

	public override void InstallBindings()
	{
		Container.Bind<BGMModel.Setter>().FromInstance(m_bgmModel);
		Container.Bind<SFXModel.Setter>().FromInstance(m_sfxModel);
		Container.Bind<VolumeModel.Getter>().FromInstance(m_volumeModel);
		Container.Bind<VolumeModel.Setter>().FromInstance(m_volumeModel);

		Container.Bind<SceneModel.Setter>().FromInstance(m_sceneModel);


	}

} //end of class MainMenuInstaller
