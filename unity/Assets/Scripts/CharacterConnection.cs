using UnityEngine;
using System.Collections;

public class CharacterConnection : MonoBehaviour {
	
	[SerializeField] GUIText m_DistanceText;
	[SerializeField] CharacterController m_character;
	[SerializeField] BreathbarController m_heartBar;
	 private bool m_playerDead = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	
	// Update is called once per frame
	void Update () {
		if(m_playerDead)
			return;
		
		m_character.SimpleMove(new Vector3(m_heartBar.GetSpeed()*20f*Time.deltaTime,0,0));
		m_DistanceText.text = m_character.transform.position.x.ToString (".0") + " m";
		m_heartBar.gameObject.transform.position = transform.position - new Vector3(-20,0,10);
	}
	
	public void OnCollisionEnter (Collision hit)
	{
		if(hit.gameObject.name.Contains("Stampfer"))
		{
			Debug.Log ("char hit by " + hit.gameObject.name);
			m_playerDead = true;
		}
	}
	
	public void OnGUI()
	{
		if(m_playerDead)
		{
			if(GUI.Button (new Rect (50,50,200,100),"RESTART"))
				Application.LoadLevel(0);
		}
	}
}
