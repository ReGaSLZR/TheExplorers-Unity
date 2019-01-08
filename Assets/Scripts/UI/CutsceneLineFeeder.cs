using System.Collections;
using UnityEngine;
using TMPro;

public class CutsceneLineFeeder : MonoBehaviour {

	[SerializeField] private TextMeshProUGUI m_textToFeed;
	[SerializeField] private float m_letterRevealDuration = 0.025f;

	private void Awake() {
		if(m_textToFeed == null) {
			LogUtil.PrintWarning(gameObject, GetType(), "Unfortunately, TextToFeed is unset.");
			Destroy(this);
		}
			
		m_textToFeed.SetText("");
	}

	public void FeedLine(string line) {
		StopAllCoroutines();

		m_textToFeed.SetText("");
		StartCoroutine(CorFeedLine(line));
	}

	public void StopOngoingFeed() {
		StopAllCoroutines();
	}

	private IEnumerator CorFeedLine(string line) {
		char[] charArray = line.ToCharArray();
		string displayText = "";

		for(int x=0; x<charArray.Length; x++) {
			displayText += charArray[x];
			m_textToFeed.SetText(displayText);

			yield return new WaitForSeconds(m_letterRevealDuration);
		}
	}

}
