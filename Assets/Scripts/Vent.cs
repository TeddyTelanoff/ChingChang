using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent: MonoBehaviour
{
	[Header("Refs")]
	public BoxCollider2D collider;

	[Header("Controls")]
	public float closePower;
	public float farPower;

	void OnTriggerStay2D(Collider2D other)
	{
		//other.GetComponent<Player>().rb.AddForceAtPosition(new Vector2(0, power / (.03125f * Mathf.Abs(other.transform.position.y - transform.position.y) + 1) * Time.deltaTime), new Vector2(Mathf.Clamp(other.transform.position.x, transform.position.x - transform.lossyScale.x / 2, transform.position.x + transform.lossyScale.x / 2), transform.position.y), ForceMode2D.Impulse);
		other.GetComponent<Player>().rb.AddForceAtPosition(new Vector2(0, Mathf.Lerp(closePower, farPower, Mathf.Abs(other.transform.position.y - transform.position.y) / (collider.size.y / 2)) * Time.deltaTime), new Vector2(Mathf.Clamp(other.transform.position.x, transform.position.x - transform.lossyScale.x / 2, transform.position.x + transform.lossyScale.x / 2), transform.position.y), ForceMode2D.Impulse);
	}
}
