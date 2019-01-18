using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPrefsUtil {
	
	private static string KEY_INTBOOL_IS_FIRST_RUN = "KEY_IS_FIRST_RUN";

	private static string KEY_FLOAT_AUDIO_VOLUME_BGM = "KEY_AUDIO_VOLUME_BGM";
	private static string KEY_FLOAT_AUDIO_VOLUME_SFX = "KEY_AUDIO_VOLUME_SFX";

	private static string KEY_STRING_PLAYER_NAME = "KEY_STRING_PLAYER_NAME";

	private static string SCORE_CONCATENATOR = " - ";
	private static int SCORE_LIST_MAX_COUNT = 5;
	private static string KEY_STRING_HIGH_SCORE_1 = "KEY_STRING_HIGH_SCORE_1";
	private static string KEY_STRING_HIGH_SCORE_2 = "KEY_STRING_HIGH_SCORE_2";
	private static string KEY_STRING_HIGH_SCORE_3 = "KEY_STRING_HIGH_SCORE_3";
	private static string KEY_STRING_HIGH_SCORE_4 = "KEY_STRING_HIGH_SCORE_4";
	private static string KEY_STRING_HIGH_SCORE_5 = "KEY_STRING_HIGH_SCORE_5";
	private static string[] KEYS_STRING_HIGH_SCORE = new string[]{
		KEY_STRING_HIGH_SCORE_1, KEY_STRING_HIGH_SCORE_2, KEY_STRING_HIGH_SCORE_3, 
		KEY_STRING_HIGH_SCORE_4, KEY_STRING_HIGH_SCORE_5
	};

	private static string KEY_INT_HIGHEST_LEVEL_CLEARED = "KEY_INT_HIGHEST_LEVEL_CLEARED";

	//NOTE: HIGH_SCORE value is formatted like this: SCORE - PLAYER - LEVEL
	private static string CreateNewScoreInput(int score) {
		return StringifyScore(score) + SCORE_CONCATENATOR 
		+ GetPlayerName() + SCORE_CONCATENATOR + "Level "
			+ GetCurrentSceneIndex().ToString();
	}

	private static int GetCurrentSceneIndex() {
		return SceneManager.GetActiveScene().buildIndex;
	}

	private static List<string> GetScoresRaw() {
		List<string> listScores = new List<string>();

		string[] scores = new string[SCORE_LIST_MAX_COUNT];
		for(int x=0; x<SCORE_LIST_MAX_COUNT; x++) {
			scores[x] = PlayerPrefs.GetString(KEYS_STRING_HIGH_SCORE[x]);
		}
			
		for(int x=0; x<scores.Length; x++) {
			if(StringUtil.IsNonNullNonEmpty(scores[x])) {
				listScores.Add(scores[x]);
			}
		}

		return listScores;
	}

	private static string StringifyScore(int score) {
		string prepend = "";
		int missingZeros = TheExplorersConfig.SCORE_MAX.ToString().Length - score.ToString().Length;

		for(int x=0; x<missingZeros; x++) {
			prepend += "0";
		}

		return (prepend + score.ToString());
	}

	// SETTER --------------------------------------------------------

	public static void ConfigFirstRun() {
		if(!PlayerPrefs.HasKey(KEY_INTBOOL_IS_FIRST_RUN)) {
			LogUtil.PrintWarning("PlayerPrefsUtil: First time run detected. Setting config defaults.");
			AnalyticsUtil.RecordFirstRun();

			PlayerPrefs.SetInt(KEY_INTBOOL_IS_FIRST_RUN, 1);

			//AUDIO
			PlayerPrefs.SetFloat(KEY_FLOAT_AUDIO_VOLUME_BGM, TheExplorersConfig.VOLUME_DEFAULT);
			PlayerPrefs.SetFloat(KEY_FLOAT_AUDIO_VOLUME_SFX, TheExplorersConfig.VOLUME_DEFAULT);

			//SCORES
			PlayerPrefs.SetString(KEY_STRING_HIGH_SCORE_1, "");
			PlayerPrefs.SetString(KEY_STRING_HIGH_SCORE_2, "");
			PlayerPrefs.SetString(KEY_STRING_HIGH_SCORE_3, "");
			PlayerPrefs.SetString(KEY_STRING_HIGH_SCORE_4, "");
			PlayerPrefs.SetString(KEY_STRING_HIGH_SCORE_5, "");

			PlayerPrefs.SetInt(KEY_INT_HIGHEST_LEVEL_CLEARED, 0); 

			PlayerPrefs.Save(); 
		} else {
			LogUtil.PrintInfo("PlayerPrefsUtil: This isn't the 1st time the game is ran. Ignoring config defaults.");
		}
	}

	public static void SaveLatestLevel() {
		int latestLevelCleared = PlayerPrefs.GetInt(KEY_INT_HIGHEST_LEVEL_CLEARED);
		int currentSceneIndex = GetCurrentSceneIndex();

		if(currentSceneIndex > latestLevelCleared) {
			PlayerPrefs.SetInt(KEY_INT_HIGHEST_LEVEL_CLEARED, currentSceneIndex);
			PlayerPrefs.Save();
		}
	}

	public static void SaveVolumeBgm(float volumeBgm) {
		PlayerPrefs.SetFloat(KEY_FLOAT_AUDIO_VOLUME_BGM, volumeBgm);
		PlayerPrefs.Save();
	}

	public static void SaveVolumeSfx(float volumeSfx) {
		PlayerPrefs.SetFloat(KEY_FLOAT_AUDIO_VOLUME_SFX, volumeSfx);
		PlayerPrefs.Save();
	}

	public static void SavePlayerName(string playerName) {
		LogUtil.PrintInfo("PlayerPrefsUtil >> Saving player name: " + playerName);
		PlayerPrefs.SetString(KEY_STRING_PLAYER_NAME, playerName);
		PlayerPrefs.Save();
	}

	public static void SaveHighScore(int score) {
		LogUtil.PrintInfo("PlayerPrefsUtil.SaveHighScore(): attempt with score " + score);

		List<string> scores = GetScoresRaw();
		scores.Add(CreateNewScoreInput(score));
		scores.Sort();
		scores.Reverse();

		for(int x=0; x<scores.Count; x++) {
			bool isTopFive = (x < SCORE_LIST_MAX_COUNT) && (StringUtil.IsNonNullNonEmpty(scores[x]));
			string isSaved = isTopFive ? "SAVED" : "REMOVED";
			LogUtil.PrintInfo("PlayerPrefsUtil.SaveHighScore(): Rank #" + x + " - " + scores[x] + " >> " + isSaved);

			if(isTopFive) {
				PlayerPrefs.SetString(KEYS_STRING_HIGH_SCORE[x], scores[x]);
			}
				
		}

		PlayerPrefs.Save();

	}

	//GETTER --------------------------------------------------------

	public static int GetLatestLevel() {
		return PlayerPrefs.GetInt(KEY_INT_HIGHEST_LEVEL_CLEARED);
	}

	public static float GetVolumeBgm() {
		return PlayerPrefs.GetFloat(KEY_FLOAT_AUDIO_VOLUME_BGM);
	}

	public static float GetVolumeSfx() {
		return PlayerPrefs.GetFloat(KEY_FLOAT_AUDIO_VOLUME_SFX);
	}

	public static string GetPlayerName() {
		return PlayerPrefs.GetString(KEY_STRING_PLAYER_NAME);
	}

	public static string[] GetHighScores() {
		return GetScoresRaw().ToArray();
	}

}
