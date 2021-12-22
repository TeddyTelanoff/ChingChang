using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class SpeedrunTimer: MonoBehaviour
{
	public TMP_Text ui;
	public float hueStep;
	[HideInInspector]
	public bool ended;
	[HideInInspector]
	public float start, end;
	public float timeElapsed => ended ? end - start : Time.time - start;

	void Start()
	{
		Restart();
	}

	void FixedUpdate()
	{
		ui.text = $"{timeElapsed.ToString("f2")}";
		Color.RGBToHSV(ui.color, out float h, out float s, out float v);
		ui.color = Color.HSVToRGB(h + hueStep * Time.deltaTime, s, v);
	}

	public void Restart()
	{
		start = Time.time;
		ended = false;
	}

	public void End()
	{
		end = Time.time;
ended = true;
		ui.color = Color.white;
	}
}
