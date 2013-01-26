#define CHEAT

using UnityEngine;
using System.Collections;
using System;



public class CharacterConnection : MonoBehaviour {
	
	[SerializeField] GUIText m_DistanceText;
	[SerializeField] CharacterController m_character;
	[SerializeField] BreathbarController m_heartBar;
	 private bool m_playerDead = false;
	[SerializeField] Color m_StandardColor;
	[SerializeField] Color m_LowColor;
	[SerializeField] Color m_HighColor;
	Color m_currentColor;
	float m_lowTimer=0f;
	
	[SerializeField] float m_HeartHighCooldown = .001f;
	[SerializeField] float m_HeartHighAdd = .05f;
	private float m_heartHightValue = 0;
	
	[SerializeField] AudioClip m_dieClip;
	
	private static CharacterConnection _instance;
	public static CharacterConnection Instance {get {return _instance; }}
	
	[SerializeField]Renderer m_renderer;
	
	public void Awake()
	{
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(m_playerDead)
		{
#if CHEAT 
			m_playerDead = false;
#else 
			return;
#endif			
		}
		if(!animation.isPlaying)
			animation.Play ("run");
		
		float speed = m_heartBar.GetSpeed()*20f;
		if(speed <= 0 && m_heartHightValue < .001f )
			m_lowTimer+= Time.deltaTime;
		else 
			m_lowTimer =0;
		m_character.SimpleMove(new Vector3(speed*Time.deltaTime,-100,0));
		m_DistanceText.text = m_character.transform.position.x.ToString (".0") + " m";
		m_heartBar.gameObject.transform.position = transform.position - new Vector3(-7,-3,10);
		
		// Increase high death risk.
		float relativeSpeed = m_heartBar.GetSpeed() / m_heartBar.maxValue;
		if ( relativeSpeed > .5f ) {
			m_heartHightValue += Time.deltaTime;
			// Cap at 1.
			m_heartHightValue = Mathf.Min( 1, m_heartHightValue );
		} else {
			// Heal the heart.
			m_heartHightValue -= Time.deltaTime;
			// Cap at 0.
			m_heartHightValue = Mathf.Max( 0, m_heartHightValue );
			//m_heartHightValue = 0f;
		}

		if(m_lowTimer > 0)
		{
			if(m_lowTimer >= 1)
				PlayerDeath();
			m_currentColor = Color.Lerp (m_StandardColor, m_LowColor, m_lowTimer);
		}
		else if ( m_heartHightValue > 0 )
		{
			// Set high risk color.
			m_currentColor = Color.Lerp( m_StandardColor, m_HighColor, m_heartHightValue );
			
			if ( m_heartHightValue > .999f ) 
				PlayerDeath();
		}
		else 
			m_currentColor = m_StandardColor;
		m_renderer.material.color = m_currentColor;
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
		PlayerDeath();
	}
	
	public void OnGUI()
	{
		if(m_playerDead)
		{
			if(GUI.Button (new Rect (50,50,200,100),"RESTART"))
				Application.LoadLevel("runner 1");
		}
	}
	
	public void PlayerDeath()
	{
		m_playerDead = true;
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		
		// Play death sound.
		SoundManager.Instance.PlaySound( m_dieClip );
		SoundManager.Instance.StopMusic();
	}
}
