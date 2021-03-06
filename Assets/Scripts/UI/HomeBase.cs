﻿using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class HomeBase : MonoBehaviour
{

	[Inject]
	GamePlay_PanelModel.Setter m_panelSwitcher;

	[Inject]
	BGMModel.Setter m_bgmModel;

	[Inject]
	PlayerStatsModel.Getter m_playerStats;

	[SerializeField]
	private AudioClip m_clipHomeBase;

	private void OnEnable()
	{
		this.OnTriggerEnter2DAsObservable()
			.Where(otherCollider2D => TagUtil.IsTagPlayer(otherCollider2D.gameObject.tag))
			.Subscribe(_ => EndLevel())
			.AddTo(this);
	}

	private void EndLevel() {
		m_bgmModel.Play(m_clipHomeBase);
		PlayerPrefsUtil.SaveHighScore(m_playerStats.GetScore().Value);
		PlayerPrefsUtil.SaveLatestLevel();
		m_panelSwitcher.ShowLevelClear();
	}

}

