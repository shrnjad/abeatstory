using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour {
	
	public float FromValue;
	public float ToValue;
	public Light m_Light;
	float current;
	float delta;
	
	// Use this for initialization
	void Start () {
		m_Light.intensity = FromValue;
		current = FromValue;
		delta = ToValue-FromValue;
	}
	
	// Update is called once per frame
	void Update () {
		current += delta*Time.deltaTime*3*Random.Range (0.5f,2f);
		if(current > ToValue && delta > 0)
			delta = -delta;
		else if(current < FromValue && delta < 0)
			delta = -delta;
		
		m_Light.intensity = current;
	
	}
}
