using UnityEngine;
using System.Collections;

public class CharDeathGui : MonoBehaviour {
	
	[SerializeField] CharacterConnection m_character;
	
	[SerializeField] GameObject m_guiGroup;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_guiGroup.SetActive( !m_character.isAlive );
	}
	
	public void Restart() {
		Application.LoadLevel("runner 1");
	}
	
	public void Quit() {
		Application.LoadLevel("Menu");
	}

}
