using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelCreation : MonoBehaviour {
	
	[SerializeField] Transform m_Player;
	[SerializeField] GameObject m_Stampfer;
	[SerializeField] GameObject m_Ground;
	
	List<GameObject> m_objectList = new List<GameObject>();
	int objectCount = 0;
	float objectDistance = 25;
	Vector3 lastObjectPosition = Vector3.zero;
	
	// Use this for initialization
	void Start () {
		
		m_Ground.renderer.material.color = Color.yellow;
		
		for(int i=0;i<30;i++)
		{
			CreateStampfer();
		}
	}
	
	private void CreateStampfer()
	{
			GameObject go = Instantiate(m_Stampfer) as GameObject;
			go.transform.position = new Vector3(lastObjectPosition.x+Random.Range(5,objectDistance),-4.5f,0);
			objectCount++;
			lastObjectPosition = go.transform.position; 
			Debug.Log ("lastObject position " + go.transform.position.x);
			if(objectDistance > 10)
				objectDistance -= 0.5f;
			m_objectList.Add (go);
			go.GetComponent<Stampfer>().Init ();
	}
	
	// Update is called once per frame
	void Update () {
		if(m_Player.position.x-30 > m_objectList[0].transform.position.x)
		{
			Debug.Log ("delete stampfer at " + m_objectList[0].transform.position.x);
			Destroy(m_objectList[0]);
			m_objectList.RemoveAt(0);
			CreateStampfer();
		}
	}
}
