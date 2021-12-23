using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider: MonoBehaviour
{
	[Header("Refs")]
	public Rigidbody2D rb;

	[Header("Body")]
	public float hmmph;

	[Header("Legs")]
	public float speed;
	public float direction => Mathf.Sign(speed);

	void FixedUpdate()
	{
		rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D hit)
	{
		if (hit.rigidbody)
			hit.rigidbody.AddForce(new Vector2(0, hmmph), ForceMode2D.Impulse);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			other.GetComponent<Player>().Lose();
			return;
		}

		speed = -speed;
	}
}
