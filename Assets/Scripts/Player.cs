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
		speed = baseSpeed;
		if (Input.GetKey(KeyCode.LeftShift))
			speed = dashSpeed;
		float dx = Input.GetAxisRaw("Horizontal") * speed;
		rb.AddForce(new Vector2(dx, 0), ForceMode2D.Impulse);
	}
}
