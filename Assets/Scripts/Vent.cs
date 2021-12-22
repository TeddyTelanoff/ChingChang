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
		//other.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(Mathf.Sin(transform.rotation.eulerAngles.z), Mathf.Cos(transform.rotation.eulerAngles.z)) * Mathf.Lerp(closePower, farPower, Mathf.Abs(other.transform.position.y - transform.position.y) / (GetComponent<BoxCollider2D>().size.y / 2)) * Time.deltaTime, new Vector2(Mathf.Clamp(other.transform.position.x, transform.position.x - transform.lossyScale.x / 2, transform.position.x + transform.lossyScale.x / 2), transform.position.y), ForceMode2D.Impulse);
		other.GetComponent<Rigidbody2D>().AddForce(new Vector2(-Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)) * Mathf.Lerp(closePower, farPower, Mathf.Abs(other.transform.position.y - transform.position.y) / (GetComponent<BoxCollider2D>().size.y / 2)) * Time.deltaTime, ForceMode2D.Impulse);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.DrawLine(transform.position, transform.position + new Vector3(-Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)) * 5);
	}
}
