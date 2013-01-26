using UnityEngine;
using System.Collections;

public class CharDeathGui : MonoBehaviour {
	
	[SerializeField] CharacterConnection m_character;
	
	[SerializeField] Texture2D m_backTex;
	[SerializeField] Texture2D m_restartTex;
	[SerializeField] Texture2D m_quitTex;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void OnGUI()
	{
		if ( m_character == null ) return;
		
		if( !m_character.isAlive )
		{
			int backWidth = 400;
			int backHeight = 200;
			int backX =  (int) ((Screen.width - backWidth) * .5f);
			int backY = (int) ((Screen.height - backHeight) * .5f);
			
			GUI.DrawTexture( new Rect(backX, backY, backWidth, backHeight), m_backTex );
			
			int buttonWidth = 40;
			int buttonHeight = 20;
			if(GUI.Button (new Rect (backX + 10, backHeight + 10 + buttonWidth, buttonWidth, buttonHeight), m_restartTex)) {
				Application.LoadLevel("runner 1");
			}
		}
	}

}
