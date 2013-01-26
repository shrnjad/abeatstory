using UnityEngine;
using System.Collections;

public class HeartMeterArrow : MonoBehaviour {
	
	public GameObject Arrow;
	
	Quaternion m_StartRotation;
	public BreathbarController m_HearthBarScript; 
	public CharacterConnection m_Character;
	// Use this for initialization
	void Awake () {
		m_StartRotation = Arrow.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		//float barStatus = m_HearthBarScript.GetSpeed() / m_HearthBarScript.maxValue;
		//barStatus-= 0.5f;
		//float rotation = barStatus * -160f;
		
		float barStatus = m_Character.GetDeathTimers();
		float rotation = barStatus*-90f;
		
		Arrow.transform.localRotation = m_StartRotation;
		Arrow.transform.Rotate(0,0,rotation);
	
	}
}
