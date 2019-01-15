using UnityEngine;

[CreateAssetMenu(fileName = "New MindFieldReward", menuName = "MindField/Create MindField Reward")]
public class MindFieldReward : ScriptableObject
{

	[Range(0, 5)]
	public int m_health = 0;
	[Range(0, 5)]
	public int m_mindLight = TheExplorersConfig.MINDLIGHT_MAX;
	[Range(100, 500)]
	public int m_score = 100;

}

