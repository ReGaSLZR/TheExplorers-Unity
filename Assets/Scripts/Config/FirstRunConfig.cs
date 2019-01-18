using UnityEngine;

public class FirstRunConfig : MonoBehaviour {

	private void Awake() {
		PlayerPrefsUtil.ConfigFirstRun();
	}

}
