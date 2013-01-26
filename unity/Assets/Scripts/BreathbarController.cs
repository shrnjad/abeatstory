using UnityEngine;
using System.Collections;

public class BreathbarController : MonoBehaviour {
	
	[SerializeField] float m_ReductionPerSecond;
	[SerializeField] float m_IncreasePerClick;
	[SerializeField] float m_MaxValue;
	float m_currentValue;
	[SerializeField] Texture m_barBackgroundTexture;
	[SerializeField] Texture m_barForegroundTexture;	
	[SerializeField] AnimationCurve m_SpeedCurve;
	
	bool started = false;
	
	public float maxValue {
		get { return m_MaxValue; }
	}
	
	// Use this for initialization
	void Start () 
	{
		m_currentValue = m_MaxValue / 2;
	}
	
	public float GetSpeed()
	{
		//return m_SpeedCurve.Evaluate(m_currentValue);
		return m_currentValue;
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if(!started)
		{
			if(Input.GetMouseButtonUp(0))
				started = true;
			return;
		}
		
		
		m_currentValue -= m_ReductionPerSecond * Time.deltaTime;
	
		if(Input.GetMouseButtonUp(0))
			m_currentValue += m_IncreasePerClick;
		
		if(m_currentValue < 0)
			m_currentValue = 0;
		else if (m_currentValue > m_MaxValue)
			m_currentValue = m_MaxValue;
	}
	
	
	public void OnGUI()
	{
		float curveValue = m_SpeedCurve.Evaluate((m_currentValue / m_MaxValue));
		GUI.DrawTexture(new Rect(10,10,300,20),m_barBackgroundTexture);
		//GUI.DrawTexture (new Rect(10+300*curveValue-5,10,10,20),m_barForegroundTexture);
		GUI.DrawTexture (new Rect(10+300*(m_currentValue / m_MaxValue)-5,10,10,20),m_barForegroundTexture);
	}	
}
