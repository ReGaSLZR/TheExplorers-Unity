using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using TMPro;
using Cinemachine;

public class CutsceneModel : MonoBehaviour,
							 CutsceneModel.Setter,
							 CutsceneModel.Getter
{

	public interface Setter {
		
		void StartCutscene(CutsceneLine[] lines);

	}

	public interface Getter {
		
		ReactiveProperty<bool> GetIsDone();

	}

	[SerializeField]
	private CinemachineVirtualCamera m_focusCam;
	[SerializeField]
	private Transform m_focusDefaultObject;

	[Space]

	[SerializeField]
	private GameObject m_cutscenePanel;
	[SerializeField]
	private Button m_buttonNext;
	[SerializeField]
	private Button m_buttonSkip;
	[SerializeField]
	private CutsceneLineFeeder m_lineFeeder;
	[SerializeField]
	private RawImage m_avatar;

	[Space]

	[SerializeField]
	private PlayableDirector m_playableOpen;
	[SerializeField]
	private PlayableDirector m_playableClose;

	public ReactiveProperty<bool> rIsDone {protected set; get;}
	private CutsceneLine[] m_lines;
	private int m_currentIndex;

	public CutsceneModel() {
		rIsDone = new ReactiveProperty<bool>(true);	
	}

	private void Awake() {
		m_currentIndex = -1;
	}

	private void OnEnable() {
		m_buttonNext.OnPointerClickAsObservable()
			.Subscribe(_ => PlayNextLine())
			.AddTo(this);

		m_buttonSkip.OnPointerClickAsObservable()
			.Subscribe(_ => EndSequence())
			.AddTo(this);
	}

	private void Start() {
		DisableEverything();
	}

	private IEnumerator CorCloseCutscene() {
		yield return ((float)m_playableClose.duration);
		DisableEverything();
	}

	private void DisableEverything() {
		rIsDone.Value = true;

		m_focusCam.gameObject.SetActive(false);

		m_playableOpen.Stop();
		m_playableClose.Stop();

		m_playableOpen.gameObject.SetActive(false);
		m_playableClose.gameObject.SetActive(false);

		m_cutscenePanel.SetActive(false);
		m_currentIndex = -1;
	}

	private void PlayCutsceneAnimation(bool isOpen) {
		PlayableDirector director = isOpen ? m_playableOpen : m_playableClose;

		director.gameObject.SetActive(true);
		director.Play();

		if(!isOpen) {
			m_lineFeeder.StopOngoingFeed();
			StartCoroutine(CorCloseCutscene());
		}
	}

	private void PlayNextLine() {
		m_currentIndex++;

		if(m_currentIndex < m_lines.Length) {
			ChangeDisplay();
		}
		else if((m_currentIndex >= m_lines.Length) || (m_lines[m_currentIndex] == null)) {
			LogUtil.PrintInfo(this.gameObject, this.GetType(), 
				"All CutsceneLines played OR next Line is NULL. Ending sequence.");

			EndSequence();
		}
	}

	private void EndSequence() {
		rIsDone.Value = true;
		PlayCutsceneAnimation(false);
	}

	private void ChangeDisplay() {
		CutsceneLine newLine = m_lines[m_currentIndex];

		m_lineFeeder.FeedLine(newLine.cutsceneText);
		m_avatar.texture = newLine.avatar;

		if(newLine.focusedObject != null) {
			m_focusCam.m_Follow = newLine.focusedObject;
		}
	}

	/* Setter ---------------------------------------------------------------------------------------- */

	public void StartCutscene(CutsceneLine[] lines) {
		if(lines.Length > 0) {
			m_lines = lines;
			m_currentIndex = -1;

			rIsDone.Value = false;

			m_cutscenePanel.SetActive(true);
			PlayCutsceneAnimation(true);

			m_focusCam.m_Follow = m_focusDefaultObject;
			m_focusCam.gameObject.SetActive(true);

			PlayNextLine();
		}
		else {
			LogUtil.PrintWarning(this.gameObject, this.GetType(), "StartCutscene(): Cannot proceed with NO lines.");
		}
	}

	public ReactiveProperty<bool> GetIsDone() {
		return rIsDone;
	}

}
