using UnityEngine;
using System.Collections;

public class MouseCursor : MonoBehaviour {
	
	[SerializeField] Texture2D m_mouseCursorUp;
	[SerializeField] Texture2D m_mouseCursorDown;
	
	[SerializeField] int m_cursorWidth = 32;
	[SerializeField] int m_cursorHeight = 32;
	
	private Texture2D m_currentCursor = null;

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (transform.gameObject);
		
		Screen.showCursor = false;
		m_currentCursor = m_mouseCursorUp;
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown(0) ) m_currentCursor = m_mouseCursorDown;
		else if ( Input.GetMouseButtonUp(0) ) m_currentCursor = m_mouseCursorUp;
	}
	
	void OnGUI () {
		Vector2 mousePos = new Vector2( Input.mousePosition.x, Screen.height - Input.mousePosition.y );
		Rect mouseRect = new Rect( mousePos.x, mousePos.y, m_cursorWidth, m_cursorHeight );
		GUI.DrawTexture( mouseRect, m_currentCursor );
	}
}
