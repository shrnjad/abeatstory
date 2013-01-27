using UnityEngine;
using System.Collections;

public class BuzzSawScript : MonoBehaviour {
	
	
	float sawCycle;
	float current;
	
	// Use this for initialization
	void Start () {
	
	}
	
	public void Init(float cycleTime)
	{
		sawCycle = cycleTime;
		if(sawCycle+1 < animation.clip.length)
			sawCycle = animation.clip.length + 1f;
		current = sawCycle;
	}
	// Update is called once per frame
	void Update () {
		current -= Time.deltaTime;
		if(current < 0)
		{
			animation.Play ();
			current = sawCycle;
		}
	}
}
