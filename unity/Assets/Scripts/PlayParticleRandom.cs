using UnityEngine;
using System.Collections;

public class PlayParticleRandom : MonoBehaviour {
	
	public ParticleSystem particle;
	float delay = -1;
	
	// Update is called once per frame
	void Update () 
	{
		if(delay < 0)
		{
			particle.Play ();
			delay = Random.Range (0f,3f);
		}
		delay -= Time.deltaTime;
	
	}
}
