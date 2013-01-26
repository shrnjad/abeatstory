using UnityEngine;
using System.Collections;

public class Stampfer : MonoBehaviour {
	
	Rigidbody m_Rigidbody;
	Vector3 startPos;
	float m_stampTime = 3.5f;
	float m_currentStampTime;
	
	public void Init()
	{
		m_Rigidbody = GetComponent<Rigidbody>();
		startPos = transform.position;
		m_currentStampTime = Random.Range (0,m_stampTime);
		this.enabled = true;
	}
	
	public void Update()
	{
		m_currentStampTime -= Time.deltaTime;
		if(m_currentStampTime < 0)
		{
			m_currentStampTime = m_stampTime;
			m_Rigidbody.useGravity = true;
		}
	}
	
	
	public void OnCollisionEnter()
	{
		StartCoroutine (MoveBackToStartposition());
	}
	
	IEnumerator MoveBackToStartposition()
	{
		do
		{
			yield return null;
			transform.position = Vector3.MoveTowards(transform.position,startPos,2f);
		}while(transform.position != startPos);
		m_Rigidbody.useGravity = false;
	}
}
