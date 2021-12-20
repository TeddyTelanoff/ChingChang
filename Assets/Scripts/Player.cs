using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
	[Header("Misc")]
	public Rigidbody2D rb;
	public float baseSpeed;
	[HideInInspector]
	public float speed;

	[Header("Dash")]
	public DashBar dashBar;
	[Serializable]
	public struct DashBar
	{
		public Transform transform;
		public Color charge;
		public Color full;
		public Color drain;
	}

	public float dashSpeed;
	public float dashDuration;
	public float dashCooldown;
	public float dashInvulnerability;
	[HideInInspector]
	public float dashDowntime;
	[HideInInspector]
	public float dashLeft;
	[HideInInspector]
	public bool canDash;

	void Awake()
	{
		dashDowntime = dashCooldown;
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.LeftShift) && canDash)
		{
			EndDash();
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
			Dash();

			dashLeft -= Time.deltaTime;
			dashBar.transform.GetComponent<Image>().color = dashBar.drain;
			dashDowntime = dashLeft / dashDuration * dashCooldown;
			if (dashLeft <= 0)
			{
				EndDash();
				dashLeft = 0;
				dashDowntime = 0;
				canDash = false;
			}
		}
		else
		{
			dashDowntime += Time.deltaTime;
			dashBar.transform.GetComponent<Image>().color = dashBar.charge;
			if (dashDowntime >= dashCooldown)
			{
				dashBar.transform.GetComponent<Image>().color = dashBar.full;
				dashDowntime = dashCooldown;
				dashLeft = dashDuration;
				canDash = true;
			}
		}

		UpdateDashBar();

		float dx = Input.GetAxisRaw("Horizontal") * speed;
		rb.AddForce(new Vector2(dx, 0), ForceMode2D.Impulse);
	}

	public void Dash()
	{
		speed = dashSpeed;

		rb.simulated = false;
	}

	public void EndDash()
	{
		rb.simulated = true;
		StartCoroutine(DashInvulnerability());
	}

	IEnumerator DashInvulnerability()
	{
		rb.gravityScale = 0;
		GetComponent<Collider2D>().enabled = false;
		yield return new WaitForSeconds(dashInvulnerability);
		GetComponent<Collider2D>().enabled = true;
		rb.gravityScale = 1;
	}

	public void UpdateDashBar()
	{
		float completeness = dashDowntime / dashCooldown;
		dashBar.transform.localScale = new Vector3(completeness, dashBar.transform.localScale.y, dashBar.transform.localScale.z);
	}
}
