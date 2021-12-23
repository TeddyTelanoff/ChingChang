using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
	[Header("Misc")]
	public Rigidbody2D rb;
	public SpeedrunTimer timer;
	public float baseSpeed;
	public float fallScale;
	[HideInInspector]
	public Vector3 startPos, startScale;
	[HideInInspector]
	public Quaternion startRot;
	[HideInInspector]
	public float speed;
	[HideInInspector]
	public float defGravScale;
	[HideInInspector]
	public Star star;

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
	public bool invulnerable;
	[HideInInspector]
	public float dashDowntime;
	[HideInInspector]
	public float dashLeft;
	[HideInInspector]
	public bool canDash;

	void Awake()
	{
		dashDowntime = dashCooldown;
		defGravScale = rb.gravityScale;
	}

	void Start()
	{
		if (!dashBar.transform)
			canDash = false;
		startPos = transform.localPosition;
		startRot = transform.localRotation;
		startScale = transform.localScale;
		timer?.Restart();
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

		if (Input.GetKey(KeyCode.R))
		{
			if (Input.GetKey(KeyCode.Tab))
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			else
				Restart();
		}

		//if (invulnerable)
		//	return;

		//if (Input.GetKeyDown(KeyCode.DownArrow))
		//	rb.gravityScale = fallScale;
		//else if (Input.GetKeyUp(KeyCode.DownArrow))
		//	rb.gravityScale = 1;
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
		else if (dashBar.transform)
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

		if (dashBar.transform)
		UpdateDashBar();

		if (Input.GetKey(KeyCode.DownArrow))
			rb.AddForce(new Vector2(0, -fallScale), ForceMode2D.Impulse);

		float dx = Input.GetAxisRaw("Horizontal") * speed;
		rb.AddForce(new Vector2(dx, 0), ForceMode2D.Impulse);
	}

	public void Win()
	{
		Time.timeScale = 0;
		timer?.End();
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Lose()
	{
		Time.timeScale = 0;
		timer?.EndBad();
		StartCoroutine(Routine());

		IEnumerator Routine()
		{
			for (int i = 0; i < 10; i++)
			{
				transform.localScale *= 1.1f;
				var eulerAngs = transform.localRotation.eulerAngles;
				transform.localRotation = Quaternion.Euler(eulerAngs.x, eulerAngs.y, eulerAngs.z + 6.9f);
				GetComponentInChildren<SpriteRenderer>().color -= new Color(0, 0.04f, 0.04f, 0);
				yield return new WaitForSecondsRealtime(0.05f);
			}
			Restart();
		}
	}

	public void Restart()
	{
		dashDowntime = dashCooldown;
		transform.localPosition = startPos;
		transform.localRotation = startRot;
		transform.localScale = startScale;
		rb.velocity = Vector2.zero;
		rb.angularVelocity = 0;
		star?.gameObject.SetActive(true);

		timer?.Restart();
		GetComponentInChildren<SpriteRenderer>().color = Color.white;
		Time.timeScale = 1;
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

		IEnumerator DashInvulnerability()
		{
			if (invulnerable)
				yield break;

			rb.gravityScale = 0;
			invulnerable = true;
			GetComponent<Collider2D>().enabled = false;
			yield return new WaitForSeconds(dashInvulnerability);
			GetComponent<Collider2D>().enabled = true;
			invulnerable = false;
			rb.gravityScale = defGravScale;
		}
	}

	public void UpdateDashBar()
	{
		float completeness = dashDowntime / dashCooldown;
		dashBar.transform.localScale = new Vector3(completeness, dashBar.transform.localScale.y, dashBar.transform.localScale.z);
	}
}
