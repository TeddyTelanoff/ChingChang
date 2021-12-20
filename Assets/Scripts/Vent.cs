using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent: MonoBehaviour
{
	[Header("Controls")]
	public float closePower;
	public float farPower;

	void OnTriggerStay2D(Collider2D other)
	{
		//other.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(0, power / (.03125f * Mathf.Abs(other.transform.position.y - transform.position.y) + 1) * Time.deltaTime), new Vector2(Mathf.Clamp(other.transform.position.x, transform.position.x - transform.lossyScale.x / 2, transform.position.x + transform.lossyScale.x / 2), transform.position.y), ForceMode2D.Impulse);
		other.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(0, Mathf.Lerp(closePower, farPower, Mathf.Abs(other.transform.position.y - transform.position.y) / (GetComponent<BoxCollider2D>().size.y / 2)) * Time.deltaTime), new Vector2(Mathf.Clamp(other.transform.position.x, transform.position.x - transform.lossyScale.x / 2, transform.position.x + transform.lossyScale.x / 2), transform.position.y), ForceMode2D.Impulse);
	}
}
