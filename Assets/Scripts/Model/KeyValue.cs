using UnityEngine;
using System.Collections;

[System.Serializable]

public class KeyValue : MonoBehaviour {

	public string key;
	public int value;

	public void setKeyValue(string key, int value){
		this.key = key;
		this.value = value;
	}
}
