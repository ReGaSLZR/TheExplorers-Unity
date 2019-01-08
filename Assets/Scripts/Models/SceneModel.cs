using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneModel : MonoBehaviour,
					      SceneModel.Setter
{

	public interface Setter {

		void LoadScene(int index);

		void ReloadCurrentScene();

		void ExitToMainMenu();

	}

	public void LoadScene(int index) {
		LogUtil.PrintInfo(this.GetType(), "LoadScene(): loading scene at index " + index);
		SceneManager.LoadSceneAsync(index);
	}

	public void ReloadCurrentScene() {
		LogUtil.PrintInfo(this.GetType(), "ReloadCurrentScene()");
		LoadScene(SceneUtil.GetSceneIndex_Current());
	}

	public void ExitToMainMenu() {
		LoadScene(TheExplorersConfig.SCENE_MAIN_MENU);
	}

}//end of class SceneModel