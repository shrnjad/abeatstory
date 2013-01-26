using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour 
{
	[SerializeField]Renderer tex;
	[SerializeField]Texture defaultCol;
	[SerializeField]Texture hoverCol;
	
	[SerializeField] MonoBehaviour m_CallbackScript;
	[SerializeField] string m_CallbackFunction;
	
	[SerializeField] AudioClip m_SFX;
	public void OnMouseEnter()
	{
		tex.material.mainTexture = hoverCol;
	}
	
	public void OnMouseExit()
	{
		tex.material.mainTexture = defaultCol;
	}
	
	public void OnMouseUpAsButton()
	{
		Debug.Log ("call " + m_CallbackFunction);
		SoundManager.Instance.PlaySound(m_SFX);
		m_CallbackScript.SendMessage(m_CallbackFunction);
	}
}
