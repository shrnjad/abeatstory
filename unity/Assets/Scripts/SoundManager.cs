using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	[SerializeField]AudioSource m_Source;
	
	[SerializeField]AudioClip m_MainTheme;
	[SerializeField]AudioClip m_GameTheme;
	[SerializeField]AudioClip m_DeadTheme;
	
	static SoundManager _instance;
	public static SoundManager Instance {get{return _instance;}}
	
	AudioClip m_nextClip;
	// Use this for initialization
	void Awake () {
		_instance = this;
		SwitchToGameTheme();
	}
	
	public void SwitchToGameTheme()
	{
		m_nextClip = m_GameTheme;	
	}
	
	public void Update()
	{
		if(m_nextClip != null)
		{
			m_Source.volume -= Time.deltaTime*2f;
			if(m_Source.volume <= 0)
			{
				m_Source.volume =0;
				m_Source.clip = m_nextClip;
				m_Source.Play ();
				m_nextClip = null;
			}
		}
		else if(m_Source.volume < 1)
			m_Source.volume += Time.deltaTime*2f;
		
	}
}
