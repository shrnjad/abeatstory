//#define CHEAT

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
	[SerializeField] AudioClip m_heartBeatClip;
	
	private static CharacterConnection _instance;
	public static CharacterConnection Instance {get {return _instance; }}
	
	[SerializeField]Renderer m_renderer;
	private float lastButtomPress;
	private bool started = false;
	[SerializeField] ParticleSystem m_ParticleSystem;
	[SerializeField] ParticleSystem BloodSplatterParticleSystem;
	
	float lastFrameTime=0;
	
	public bool isAlive {
		get { return !m_playerDead; }
	}
	
	public void Awake()
	{
		_instance = this;
		transform.Translate(0,-1.8f,0);
		m_heartBar.gameObject.transform.position = transform.position - new Vector3(-7,-3,10);
		rigidbody.Sleep();
	}
	
	public float GetDeathTimers()
	{
		if(m_heartHightValue > 0)
			return m_heartHightValue;
		else if(m_lowTimer > 0)
			return - m_lowTimer;
		else return 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(rigidbody)
			Debug.Log ("char pos " + transform.position + "   rigi vel " + rigidbody.velocity);
		else 
			Debug.Log ("char pos " + transform.position);
		
		if(!started)
		{
			if(Input.GetMouseButtonUp(0))
			{
				rigidbody.WakeUp ();
				started = true;
			}
			return;
		}
		
		if(m_playerDead)
		{
			//UpdateDeathAnimation();
#if CHEAT 
			m_playerDead = false;
#else 
			return;
#endif			
		}
		if(Input.GetMouseButtonUp(0) )
		{
			m_ParticleSystem.Play();
			lastButtomPress = Time.time;
			if(animation.clip.name != "shock")
			{
				if(UnityEngine.Random.Range(0,8) == 0)
				{
					animation.clip = animation["shock"].clip;
					animation.CrossFade("shock");
				}
				else if(animation.clip.name != "run")
				{
					animation.clip = animation["run"].clip;
					animation.CrossFade("run");
				}
			}
			
			// Play heaartbeat sound.
			
		}
		else if(Time.time - lastButtomPress > 0.66f)
		{
			if(animation.clip.name != "Idle")
			{
				animation.CrossFade("Idle");
				animation.clip = animation["Idle"].clip;
			}
			else if(!animation.isPlaying)
				animation.Play ("Idle");
		}
		else if(animation.clip.name == "Idle")
		{
			animation.CrossFade("run");
			animation.clip = animation["run"].clip;
			
		}
		else if(!animation.isPlaying)
			animation.Play ("run");

		float speed = m_heartBar.GetSpeed()*20f;
		if(speed <= 0 && m_heartHightValue < .001f )
			m_lowTimer+= Time.deltaTime;
		else 
			m_lowTimer =0;
		m_character.SimpleMove(new Vector3(speed*Time.deltaTime,0,0));
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

	private void UpdateDeathAnimation()
	{
		//animation.Stop ();
		//animation["Die"].length += lastFrameTime;
		animation.clip = animation["Die"].clip;
		
		animation["Die"].time += lastFrameTime;
		animation.Play ();
	}
	
	
	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.name.Contains ("Trap_Bridge"))
			return;
		
		PlayerDeath();
	}
	
	public void PlayerDeath()
	{
		if(m_playerDead)
			return;
		lastFrameTime = Time.deltaTime;
		//Time.timeScale = 0f;
		
		
		m_playerDead = true;
		rigidbody.velocity = Vector3.zero;
		
		Destroy (rigidbody);
		//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		//rigidbody.Sleep ();
		// Play death sound.
		SoundManager.Instance.PlaySound( m_dieClip );
		SoundManager.Instance.StopMusic();
		animation.CrossFade ("Die");
		SaveHighscore((int)transform.position.x);
		BloodSplatterParticleSystem.Play();
		
	}
	
	public void SaveHighscore(int score)
	{
		int[] scoreArray = new int[5];
		int TopPlace = -1;
		for(int i=0;i<5;i++)
		{
			scoreArray[i] = PlayerPrefs.GetInt("highscore_values_"+i.ToString ());
			if(score > scoreArray[i] && TopPlace < 0)
				TopPlace = i;
		}
		
		int tmpPlace = score;
		if(TopPlace >= 0)
		{
			for(int i=0;i<5;i++)
			{
				if(i >= TopPlace)
				{
					int tmp = scoreArray[i];
					scoreArray[i] = tmpPlace;
					tmpPlace = tmp;
					PlayerPrefs.SetInt("highscore_values_"+i.ToString(),scoreArray[i]);
				}
			}
		}
			
	}	
}
