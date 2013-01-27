using UnityEngine;
using System.Collections;

public class CharDeathGui : MonoBehaviour {
	
	[SerializeField] CharacterConnection m_character;
	
	[SerializeField] GameObject m_guiGroup;
	[SerializeField] TextMesh m_DisplayText;
	[SerializeField] GameObject RestartButton;
	[SerializeField] GameObject Menu;
	[SerializeField] GameObject StartupMsg;
	
	bool started = false;
	// Use this for initialization
	void Awake () {
		m_guiGroup.SetActive(true);
		RestartButton.SetActive(false);
		Menu.SetActive(false);
		m_DisplayText.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(!started)
		{
			if(Input.GetMouseButtonUp(0))
			{
				started = true;
				m_guiGroup.SetActive( false );
			}
		}
		else if(!m_guiGroup.active && !m_character.isAlive)
		{
			m_guiGroup.SetActiveRecursively( true );
			StartupMsg.SetActive(false);
			m_DisplayText.text = "Game Over\n" + "You achieved " + m_character.transform.position.x.ToString (".0") + " points!!!";
		}
	}	
	
	public void Restart() {
		Application.LoadLevel("Game");
		SoundManager.Instance.SwitchToGameTheme();
	}
	
	public void Quit() {
		Application.LoadLevel("Menu");
		SoundManager.Instance.SwitchToMenuTheme();
	}

}
