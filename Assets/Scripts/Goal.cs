using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal: MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
			print("level completed");
	}
}
