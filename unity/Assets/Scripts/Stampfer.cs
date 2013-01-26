using UnityEngine;
using System;
using System.Collections;

public class Stampfer : MonoBehaviour {
	
	Vector3 startPos;
	float m_stampTime = 3.5f;
	float m_currentStampTime;
	CharacterConnection m_Character;
	[SerializeField]Animation m_Animation;
	
	bool m_isUp = true;
	
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
		MoveBackToStartposition();
		
		m_currentStampTime -= Time.deltaTime;
		if(m_currentStampTime < 0 && !m_Animation.isPlaying && m_isUp)
		{
			m_currentStampTime = m_stampTime;
			m_Animation.Play ("down");
			m_isUp = false;
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
	}
	
	void MoveBackToStartposition()
	{
		if(!m_isUp && !m_Animation.isPlaying) {
			m_Animation.Play ("up");
			m_isUp = true;
		}
	}
	
	public void OnDestroy()
	{
		StopAllCoroutines();
	}
}
