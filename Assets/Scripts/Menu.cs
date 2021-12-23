using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu: MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject levelMenu;

	public void Playz()
	{
		SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("last-lvl", 1));
	}

	public void Play(int lvl)
	{
		SceneManager.LoadSceneAsync(lvl);
	}

	public void Levels()
	{
		mainMenu.SetActive(false);
		levelMenu.SetActive(true);
	}

	public void Back2Menu()
	{
		levelMenu.SetActive(false);
		mainMenu.SetActive(true);
	}
}
