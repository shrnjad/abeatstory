using UnityEngine;
using System.Collections;

public class ParalaxScript : MonoBehaviour {
	
	[SerializeField ]BreathbarController m_BreathController;
	[SerializeField] float m_ScrollMultiplier;
	
	
	// Update is called once per frame
	void Update () 
	{
		float offset = renderer.material.mainTextureOffset.x + (m_BreathController.GetSpeed()*m_ScrollMultiplier);
		if(offset > 1)
			offset -= 1;
		renderer.material.mainTextureOffset = new Vector2(offset,0);
	}
}
