using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour, StatModule {

	[SerializeField] int moveRange;

	public void setStat(string statName, int statValue){
		if(statName.Equals("moveRange")){
			moveRange = statValue;
		}else{
			Debug.Log ("Movement: invalid stat name, unable to assign stat");
		}
	}

	public int getMoveRange(){
		return moveRange;
	}
}
