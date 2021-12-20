using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
	public Rigidbody2D rb;
	public float baseSpeed;
	public float dashSpeed;
	[HideInInspector]
	public float speed;

	void FixedUpdate()
	{
		float speed = baseSpeed;
		if (Input.GetKey(KeyCode.LeftShift))
			spd = dashSpeed;
		float dx = Input.GetAxisRaw("Horizontal") * spd;
		rb.AddForce(new Vector2(dx, 0), ForceMode2D.Impulse);
	}
}
