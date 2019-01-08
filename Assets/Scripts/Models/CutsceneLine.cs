using UnityEngine;

[System.Serializable]
public class CutsceneLine {

	public Transform focusedObject;

	[TextArea]
	public string cutsceneText;

	public Texture avatar;

}