using UnityEngine;
using System.Collections;

public class DeathTriggerScript : MonoBehaviour {
	[SerializeField] CharacterConnection m_Character;
	
	public void OnTriggerEnter(Collider col)
	{
		m_Character.PlayerDeath();
	}

}
