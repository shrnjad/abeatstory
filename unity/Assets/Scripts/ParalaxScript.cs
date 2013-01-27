using UnityEngine;
using System.Collections;

public class ParalaxScript : MonoBehaviour {
	
	[SerializeField ]BreathbarController m_BreathController;
	[SerializeField] float m_ScrollMultiplier;
	[SerializeField] float m_SelfSpeed;
	
	
	
	// Update is called once per frame
	void Update () 
	{
		float offset = renderer.material.mainTextureOffset.x + ((m_BreathController.GetSpeed()*m_ScrollMultiplier)+m_SelfSpeed) * Time.deltaTime;
		if(offset > 1)
			offset -= 1;
		renderer.material.mainTextureOffset = new Vector2(offset,0);
	}
}
