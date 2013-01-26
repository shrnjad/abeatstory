using UnityEngine;
using System.Collections;
using System;

public class CharacterConnection : MonoBehaviour {
	
	[SerializeField] GUIText m_DistanceText;
	[SerializeField] CharacterController m_character;
	[SerializeField] BreathbarController m_heartBar;
	 private bool m_playerDead = false;
	
	private static CharacterConnection _instance;
	public static CharacterConnection Instance {get {return _instance; }}
	
	public void Awake()
	{
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_playerDead)
			return;
		
		m_character.SimpleMove(new Vector3(m_heartBar.GetSpeed()*20f*Time.deltaTime,0,0));
		//rigidbody.velocity = new Vector3(m_heartBar.GetSpeed()*20f*Time.deltaTime,-1,0);
		//m_DistanceText.text = m_character.transform.position.x.ToString (".0") + " m";
		m_heartBar.gameObject.transform.position = transform.position - new Vector3(-10,-4,10);
	}
	
/*	public void OnCollisionEnter (Collision hit)
	{
		if(hit.gameObject.name.Contains("Stampfer"))
		{
			Debug.Log ("char hit by " + hit.gameObject.name);
			m_playerDead = true;
		}
	}
*/	
	public void OnTriggerEnter(Collider col)
	{
		m_playerDead = true;
	}
	
	public void OnGUI()
	{
		if(m_playerDead)
		{
			if(GUI.Button (new Rect (50,50,200,100),"RESTART"))
				Application.LoadLevel(0);
		}
	}
	
	public void PlayerDeath()
	{
		m_playerDead = true;
	}
}
