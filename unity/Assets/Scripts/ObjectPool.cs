using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	static ObjectPool _instance;
	public static ObjectPool Instance {get {return _instance; }}
	
	Dictionary<string,List<GameObject>> m_activePool = new Dictionary<string,List<GameObject>>();
	Dictionary<string,List<GameObject>> m_inactivePool = new Dictionary<string,List<GameObject>>();
	
	public GameObject SetObject(GameObject go, Vector3 position)
	{
		List<GameObject> list;
		
		if(!m_inactivePool.TryGetValue(go.name, out list))
		{
			list = new List<GameObject>();
			m_inactivePool.Add(go.name,list);
		}
		if(list.Count == 0)
			list.Add (Instantiate(go)as GameObject);
		
		Debug.Log ("list count is " + list.Count);
		
		GameObject newObject = list[0];
		newObject.transform.position = position;
		newObject.SetActiveRecursively(true);
		list.RemoveAt(0);
		
		AddToActiveList(go.name,newObject);
		return go;
	}
	
	private void AddToActiveList(string type, GameObject go)
	{
		List<GameObject> list;
		if(!m_activePool.TryGetValue(type,out list))
		{
			list = new List<GameObject>();
			m_activePool.Add (type,list);
		}
		list.Add (go);
	}
	
	// Use this for initialization
	void Awake () {
		_instance = this;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 charPos = CharacterConnection.Instance.transform.position;
		foreach(string type in m_activePool.Keys)
		{
			List<GameObject> list;
			if(!m_activePool.TryGetValue(type,out list))
				Debug.LogError ("error - listshould always be in dictionary");
	
			for(int i=list.Count-1;i>=0;i--)
			{
				if(list[i].transform.position.x +30 < charPos.x)
				{
					GameObject tmp = list[i];
					list.RemoveAt(i);
					tmp.SetActiveRecursively(false);
					List<GameObject> tmpList;
					m_inactivePool.TryGetValue(type,out tmpList);
					tmpList.Add(tmp);
				}
			}
		}
	
	}
}
