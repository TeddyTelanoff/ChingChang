using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player: MonoBehaviour
{
	[Header("Refs")]
	public Rigidbody2D rb;

	[Header("Base")]
	public float baseSpeed;
	[HideInInspector]
	public float speed;

	[Header("Dash")]
	public float dashSpeed;
	public float dashDuration;
	public float dashCooldown;
	[HideInInspector]
	public float dashDowntime;
	[HideInInspector]
	public float dashLeft;
	[HideInInspector]
	public bool canDash;

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.LeftShift) && canDash)
		{
			dashDowntime = dashLeft;
			dashLeft = 0;
			canDash = false;
		}
	}

	void FixedUpdate()
	{
		speed = baseSpeed;
		if (Input.GetKey(KeyCode.LeftShift) && canDash)
		{
			speed = dashSpeed;

			dashLeft -= Time.deltaTime;
			if (dashLeft <= 0)
			{
				dashLeft = 0;
				dashDowntime = 0;
				canDash = false;
			}
		}
		else
		{
			dashDowntime += Time.deltaTime;
			if (dashDowntime >= dashCooldown)
			{
				dashDowntime = dashCooldown;
				dashLeft = dashDuration;
				canDash = true;
			}
		}

		float dx = Input.GetAxisRaw("Horizontal") * speed;
		rb.AddForce(new Vector2(dx, 0), ForceMode2D.Impulse);
	}
}
