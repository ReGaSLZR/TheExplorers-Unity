﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtil {

	//	private static int SPLASH = 0;
	public static int MAIN_MENU = 1;
//	public static int LOADING = 2;

	public static void LoadMainMenu() {
//		SceneManager.LoadSceneAsync(LOADING, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync(MAIN_MENU);
	}

	public static void LoadScene(int index) {
//		SceneManager.LoadSceneAsync(LOADING, LoadSceneMode.Additive);
		SceneManager.LoadSceneAsync(index);
	}

	public static void UnloadScene(int index) {
		SceneManager.UnloadSceneAsync(index);
	}

	public static int GetSceneIndex_Current() {
		return SceneManager.GetActiveScene().buildIndex;
	}

	public static string GetSceneName_Current() {
		return SceneManager.GetActiveScene().name;
	}

}
