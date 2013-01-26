using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	[SerializeField]AudioSource m_MusicSource;
	[SerializeField]AudioSource m_SoundSource;
	
	[SerializeField]AudioClip m_MainTheme;
	[SerializeField]AudioClip m_GameTheme;
	[SerializeField]AudioClip m_DeadTheme;
	
	static SoundManager _instance;
	public static SoundManager Instance {get{return _instance;}}
	
	AudioClip m_nextClip;
	// Use this for initialization
	void Awake () {
		_instance = this;
		DontDestroyOnLoad(gameObject);
		SwitchToMenuTheme();
		Application.LoadLevel("Menu");
	}
	
	public void SwitchToGameTheme()
	{
		m_nextClip = m_GameTheme;	
	}
	public void SwitchToMenuTheme()
	{
		m_nextClip = m_MainTheme;	
	}
	
	public void Update()
	{
		if(m_nextClip != null)
		{
			m_MusicSource.volume -= Time.deltaTime*2f;
			if(m_MusicSource.volume <= 0)
			{
				m_MusicSource.volume =0;
				m_MusicSource.clip = m_nextClip;
				m_MusicSource.Play ();
				m_nextClip = null;
			}
		}
		else if(m_MusicSource.volume < 1)
			m_MusicSource.volume += Time.deltaTime*2f;
		
	}
	
	public void StopMusic() {
		m_nextClip = null;
		m_MusicSource.Stop();
	}
	
	public void PlaySound( AudioClip clip ) {
		if ( clip == null ) return;
		
		m_SoundSource.PlayOneShot( clip );
	}
	
}
