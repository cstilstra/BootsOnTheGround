using UnityEngine;
using System.Collections.Generic;

[System.Serializable]

public class KeyValueList : MonoBehaviour {

	[SerializeField] List<KeyValue> keyValueList;

	// Use this for initialization
	void Start () {
		keyValueList = new List<KeyValue>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool ContainsKey(string key){
		for(int i = 0;i< keyValueList.Count; i++){
			if(keyValueList[i].key.Equals (key)){
				return true;
			}
		}
		return false;
	}

}
