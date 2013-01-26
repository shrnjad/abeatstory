using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCreation : MonoBehaviour {
	
	[SerializeField] Transform m_Player;
	[SerializeField] GameObject m_Stampfer;
	[SerializeField] GameObject m_Bridge;
	[SerializeField] GameObject[] m_Ground;
	[SerializeField] GameObject[] m_Background;
	
	List<GameObject> m_objectList = new List<GameObject>();
	List<GameObject> m_groundObjectList = new List<GameObject>();
	List<GameObject> m_backgroundObjectList = new List<GameObject>();
	
	
	int objectCount = 0;
	float objectDistance = 25;
	Vector3 nextObjectPosition = new Vector3(15,0,2);
	Vector3 lastGroundPos = Vector3.zero;
	Vector3 lastBackgroundPosition;
	int groundIndex;
	int tilesTillNextPartCheck;
	// Use this for initialization
	void Start () {
		
		groundIndex = 0;
		tilesTillNextPartCheck = 30;
		nextObjectPosition = new Vector3(20,0,0);
		for(int i=0;i<20;i++)
		{
			CreateGround();
			//CreateBackground();
		}
	}
	
	private void CreateBackground()
	{
		GameObject go = Instantiate (m_Background[0]) as GameObject;
		go.transform.position = lastBackgroundPosition;
		lastBackgroundPosition = go.transform.Find ("End").position;
		m_backgroundObjectList.Add (go);
		
		GameObject detailGo;
		int index = Random.Range(0,100)%10+1;
		if(index < m_Background.Length)
		{
			detailGo = GameObject.Instantiate(m_Background[index]) as GameObject;
			detailGo.transform.position = go.transform.position;
			m_backgroundObjectList.Add (detailGo);
		}
	}
	
	private void CreateGround()
	{
		if(tilesTillNextPartCheck <=0)
			CheckNewTileType();
		
		GameObject go = Instantiate(m_Ground[groundIndex]) as GameObject;
		go.transform.position = lastGroundPos;
		m_groundObjectList.Add (go);
		
		
		if(groundIndex == 0)
		{
			GameObject goBack = Instantiate (m_Background[0]) as GameObject;
			goBack.transform.position = lastGroundPos;
			//lastBackgroundPosition = go.transform.Find ("End").position;
			m_backgroundObjectList.Add (goBack);
			
			GameObject detailGo;
			int index = Random.Range(0,100)%10+1;
			if(index < m_Background.Length)
			{
				detailGo = GameObject.Instantiate(m_Background[index]) as GameObject;
				detailGo.transform.position = go.transform.position;
				m_backgroundObjectList.Add (detailGo);
			}
		}
		
		tilesTillNextPartCheck--;
		lastGroundPos = go.transform.Find ("End").position;

		if(lastGroundPos.x > nextObjectPosition.x && groundIndex == 0)
		{
			if(tilesTillNextPartCheck > 4 && Random.Range(0,3) == 0)
				CreateStomperRow(lastGroundPos);
			else 
				CreateStampfer (lastGroundPos);
		}
	}
	
	private void CheckNewTileType()
	{
		if(groundIndex > 0)
			groundIndex = 0;
		else 
			groundIndex = Random.Range (0,3);
		
		
		if(groundIndex == 0)
		{
			tilesTillNextPartCheck = Random.Range (15,30);
		}
		else if(groundIndex <= 2 )
		{
			tilesTillNextPartCheck = Random.Range (2,6);
		}
	}
	
	private void CreateBridge( Vector3 pos ) {
	}
		
	private Stampfer CreateStampfer(Vector3 pos, float stampTime = -1f)
	{
			//GameObject go = ObjectPool.Instance.SetObject(m_Stampfer,pos + new Vector3 (0,0,2));
			GameObject go = Instantiate(m_Stampfer) as GameObject;
			go.transform.position = pos;
			nextObjectPosition = pos + new Vector3(Random.Range(5,objectDistance),0,0);
			objectCount++;
			if(objectDistance > 10)
				objectDistance -= 0.5f;
			m_objectList.Add (go);
			if(stampTime < 0)
				stampTime = UnityEngine.Random.Range (0f,3.5f);
			Stampfer stampfer = go.GetComponent<Stampfer>();
			stampfer.Init (stampTime);
			return stampfer;
	}
	
	private void CreateStomperRow(Vector3 pos)
	{
		float stampTime = UnityEngine.Random.Range (2.5f,5f);
		float stompDelay = Random.Range (0.1f,0.35f);
		int amount = Random.Range (3,Mathf.Min (7,tilesTillNextPartCheck));
		for(int i=0;i<amount;i++)
		{
			CreateStampfer(pos,stampTime).DelayStomp(i*stompDelay);
			pos += new Vector3(2.5f,0,0);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(m_objectList.Count > 0)
		{
			if(m_Player.position.x-30 > m_objectList[0].transform.position.x)
			{
				Destroy(m_objectList[0]);
				m_objectList.RemoveAt(0);
				
			}
		}
		if(m_groundObjectList.Count > 0)
		{
			if(m_Player.position.x-30 > m_groundObjectList[0].transform.position.x)
			{
				Destroy(m_groundObjectList[0]);
				m_groundObjectList.RemoveAt(0);
				CreateGround();
				
			}
		}
		if(m_backgroundObjectList.Count > 0)
		{
			if(m_Player.position.x-30 > m_backgroundObjectList[0].transform.position.x)
			{
				Destroy(m_backgroundObjectList[0]);
				m_backgroundObjectList.RemoveAt(0);
				//CreateBackground();
				
			}
		}
	}
	
}
