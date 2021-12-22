using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star: MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<Player>().star = this;
			print("star collected");
			gameObject.SetActive(false);
		}
	}
}
