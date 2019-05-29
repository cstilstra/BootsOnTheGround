using UnityEngine;
using System.Collections;

public class lightMoveTestScript : MonoBehaviour {

	public float speed = 0.25f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(this.transform.position.x + speed, this.transform.position.y, this.transform.position.z);
		if(this.transform.position.x <= -30 || this.transform.position.x >= -1){
			speed*=-1f;
		}
	}
}
