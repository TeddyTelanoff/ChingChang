using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamrMusik : MonoBehaviour
{
	public AudioSource tutorial;
	public AudioSource sandbox;
	public AudioSource gamer;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		SceneManager.activeSceneChanged += (curr, next) => {
			if (next.buildIndex == 0)
			{
				sandbox.Stop();
				tutorial.Stop();
				gamer.Stop();
			}

			if (next.buildIndex < 6)
			{
				sandbox.Stop();
				tutorial.Play();
				gamer.Stop();
			}

			if (next.buildIndex < 13)
			{
				sandbox.Stop();
				tutorial.Stop();
				gamer.Play();
			}

			if (next.buildIndex == 13)
			{
				sandbox.Play();
				tutorial.Stop();
				gamer.Stop();
			}
		};
	}

	void Update()
	{
		if (Screen.width / Screen.height != 16 / 9)
		{
			if (Screen.width < Screen.height)
			{
				Screen.SetResolution(Screen.width, Screen.width * 9 / 16, Screen.fullScreen);
			}
			else
			{
				Screen.SetResolution(Screen.height * 16 / 9, Screen.height, Screen.fullScreen);
			}
		}
	}
}
