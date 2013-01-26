using UnityEngine;
using System;
using System.Collections;

public class Stampfer : MonoBehaviour {
	
	Rigidbody m_Rigidbody;
	Vector3 startPos;
	float m_stampTime = 3.5f;
	float m_currentStampTime;
	CharacterConnection m_Character;
	[SerializeField]Animation m_Animation;
	
	public void Init()
	{
		//m_Rigidbody = GetComponent<Rigidbody>();
		startPos = transform.position;
		m_currentStampTime = UnityEngine.Random.Range (0,m_stampTime);
		this.enabled = true;
	}
	
	public void Update()
	{
		m_currentStampTime -= Time.deltaTime;
		if(m_currentStampTime < 0)
		{
			m_currentStampTime = m_stampTime;
			//m_Rigidbody.useGravity = true;
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
		//yield return new WaitForSeconds(1.5f);
		if(m_Animation != null)
			m_Animation.Play ("up");
		
		/*do
		{
			yield return null;
			transform.position = Vector3.MoveTowards(transform.position,startPos,2f);
		}while(transform.position != startPos);
		//if(m_Rigidbody != null)
		//	m_Rigidbody.useGravity = false;
		*/
	}
	
	public void OnDestroy()
	{
		StopAllCoroutines();
	}
}
