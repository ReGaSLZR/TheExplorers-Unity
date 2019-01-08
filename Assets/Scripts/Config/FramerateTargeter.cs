using UnityEngine;

public class FramerateTargeter : MonoBehaviour {

	private void Awake() {
		Application.targetFrameRate = -1;
	}

}
