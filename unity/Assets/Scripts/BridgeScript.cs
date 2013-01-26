using UnityEngine;
using System.Collections;

public class BridgeScript : MonoBehaviour {

	public void OnTriggerEnter(Collider col)
	{
		animation["Take 001"].speed = 2.5f;
		animation.Play();
	}
}
