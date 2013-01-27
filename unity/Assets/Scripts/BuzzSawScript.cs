using UnityEngine;
using System.Collections;

public class BuzzSawScript : MonoBehaviour {
	
	
	float sawCycle;
	float current;
	Collider[] col;
	bool checkToDeactivate = false;
	// Use this for initialization
	void Start () {
		col = (Collider[]) gameObject.GetComponentsInChildren<Collider>() ; 
		foreach(Collider c in  col)
			c.enabled = false;
	}
	
	public void Init(float cycleTime)
	{
		sawCycle = cycleTime;
		if(sawCycle+1 < animation.clip.length)
			sawCycle = animation.clip.length + 1f;
		current = sawCycle;
		checkToDeactivate = true;
	}
	// Update is called once per frame
	void Update () {
		
		if(checkToDeactivate)
		{
			if(!animation.isPlaying)
			{
				checkToDeactivate = false;
				foreach(Collider c in col)
					c.enabled = false;
				animation.Rewind();
			}
		}
		current -= Time.deltaTime;
		if(current < 0)
		{
			animation.Play ();
			current = sawCycle;
			if (col != null)
			{
				foreach(Collider c in  col)
					c.enabled = true;
			}
		}
	}
}
