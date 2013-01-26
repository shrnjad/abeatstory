using UnityEngine;
using System;
using System.Collections;

public class Stampfer : MonoBehaviour {
	
	Vector3 startPos;
	float m_stampTime = 3.5f;
	float m_currentStampTime;
	CharacterConnection m_Character;
	[SerializeField]Animation m_Animation;
	
	public void Init(float stampOffset)
	{
		m_Animation.Play("up");
		startPos = transform.position;
		m_stampTime = stampOffset;
		m_currentStampTime = stampOffset;
		this.enabled = true;
	}
	
	public void DelayStomp(float delay)
	{
		m_currentStampTime += delay;
	}
	
	public void Update()
	{
		m_currentStampTime -= Time.deltaTime;
		if(m_currentStampTime < 0)
		{
			m_currentStampTime = m_stampTime;
			m_Animation.Play ("down");
			StartCoroutine (MoveBackToStartposition());
		}
	}
	
	
	//public void OnCollisionEnter(Collision collision)
	public void OnTriggerEnter(Collider collision)
	{
		Debug.Log ("coll with " + collision.gameObject.name);
		
		if(collision.gameObject.name == "Player")
		{
			CharacterConnection.Instance.PlayerDeath();
		}
		else
		{
			
		}
		StartCoroutine (MoveBackToStartposition());
		
	}
	
	IEnumerator MoveBackToStartposition()
	{
		do
		{
			yield return null;
		}while(m_Animation.isPlaying);
		
		if(m_Animation != null)
			m_Animation.Play ("up");

	}
	
	public void OnDestroy()
	{
		StopAllCoroutines();
	}
}
