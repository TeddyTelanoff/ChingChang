using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent: MonoBehaviour
{
	public float power;

	void OnTriggerStay2D(Collider2D other)
	{
		other.GetComponent<Player>().rb.AddForceAtPosition(new Vector2(0, power * Time.deltaTime), transform.position, ForceMode2D.Impulse);
	}
}
