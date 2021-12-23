using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class LevelTime: MonoBehaviour
{
	public int level;
	public bool rainbow;

	void Start()
	{
		float time = PlayerPrefs.GetFloat(level + ".best-time", float.NaN);
		int star = PlayerPrefs.GetInt(level + ".star");
		GetComponent<TMP_Text>().text = time.ToString("f2");
		if (star == 1 || (level < 5 && time != float.NaN))
			rainbow = true;
	}

	void FixedUpdate()
	{
		if (rainbow)
		{
			Color.RGBToHSV(GetComponent<TMP_Text>().color, out float h, out float s, out float v);
			GetComponent<TMP_Text>().color = Color.HSVToRGB(h + 0.3f * Time.deltaTime, s, v);
		}
	}
}
