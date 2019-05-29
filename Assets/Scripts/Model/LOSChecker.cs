using UnityEngine;
using System.Collections;

public class LOSChecker : MonoBehaviour {

	//TODO: this method expects any game object, but the helper changeLayers expects that it is passed an aim point object that is the child of the game piece 
	public bool checkLOS(GameObject source, GameObject target){
		//Debug.Log ("LOSChecker: checking line of sight");
		GameObject[] objects = {source, target};
		//Debug.Log ("LOSChecker: source position" + source.transform.position.x + ", "+ source.transform.position.y + ", "+ source.transform.position.z + ", ");
		//Debug.Log ("LOSChecker: target position" + target.transform.position.x + ", "+ target.transform.position.y + ", "+ target.transform.position.z + ", ");
		// put objects on the IgnoreRaycast layer
		changeLayers (2,objects);

		RaycastHit hit;
		if(Physics.Linecast (source.transform.position, target.transform.position, out hit)){
			// put objects back to their original layer
			changeLayers (0,objects);
			//Debug.Log ("LOSChecker: line of sight blocked");
			//Debug.Log ("LOSChecker: block position" + hit.transform.position.x + ", "+ hit.transform.position.y + ", "+ hit.transform.position.z + ", ");
			return false;
		}else{
			// put objects back to their original layer
			changeLayers (0,objects);
			//Debug.Log ("LOSChecker: line of sight clear");
			return true;
		}
	}

	void changeLayers(int layer, GameObject[] objects){

		for(int i=0; i<objects.Length;i++){
			// set the parent object (since the points passed to LOS are always aiming points)
			objects[i].transform.parent.gameObject.layer = layer;
		}

	}

}



