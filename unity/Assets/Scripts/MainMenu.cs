using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	[SerializeField] Transform m_MainMenuButtons;
	[SerializeField] Transform m_HighscoreButtons;
	[SerializeField] Transform m_CreditsButtons;
	
	[SerializeField] TextMesh[] m_HighscoreBoardText;
	
	private bool AnimateHighscoreButtons = false;
	Vector3 HighscoreDelta;
	Vector3 MenuDelta;
	Vector3 TargetHighscorePos;
	Vector3 TargetMenuPos;
	float animTime;
	
	Transform oneAnim;
	Transform twoAnim;
	
	public void StartGame()
	{
		SoundManager.Instance.SwitchToGameTheme();

		Application.LoadLevel ("runner 1");
	}
	
	public void LoadHighscores()
	{
		if(AnimateHighscoreButtons)
			return;
		
		oneAnim = m_HighscoreButtons;
		twoAnim = m_MainMenuButtons;
		
		string[] m_HighscoreValues = new string[5];
		for(int i=0;i<5;i++)
		{
			m_HighscoreValues[i] = PlayerPrefs.GetInt ("highscore_values_"+i.ToString (),0).ToString ();
			m_HighscoreBoardText[i].text = (i+1).ToString ()+". " + m_HighscoreValues[i];
		}
			
		

		InitAnimValues();
	}
	
	public void LeaveHighscores()
	{
		if(AnimateHighscoreButtons)
			return;
		
		oneAnim = m_HighscoreButtons;
		twoAnim = m_MainMenuButtons;
		
		InitAnimValues();
	}
	
	public void Exit()
	{
		Application.Quit();
	}
	
	public void ShowCredits()
	{
		if(AnimateHighscoreButtons)
			return;
		
		oneAnim = m_CreditsButtons;
		twoAnim = m_MainMenuButtons;

		InitAnimValues();
	}
	
	public void LeaveCredits()
	{
		if(AnimateHighscoreButtons)
			return;
		
		oneAnim = m_CreditsButtons;
		twoAnim = m_MainMenuButtons;

		InitAnimValues();		
	}
	
	public void Update()
	{
		if(AnimateHighscoreButtons)
		{
			oneAnim.position += HighscoreDelta*Time.deltaTime*2f;
			twoAnim.position += MenuDelta * Time.deltaTime*2f;
			animTime -= Time.deltaTime*2f;
			if(animTime < 0)
			{
				AnimateHighscoreButtons = false;
				twoAnim.position = TargetMenuPos;
				oneAnim.position = TargetHighscorePos;
			}
		}
	}
	
	private void InitAnimValues()
	{
		TargetMenuPos = oneAnim.position;
		TargetHighscorePos = twoAnim.position;
		MenuDelta = TargetMenuPos - twoAnim.position;
		HighscoreDelta = TargetHighscorePos - oneAnim.position;
		animTime = 1f;
		AnimateHighscoreButtons = true;
		
	}

}
