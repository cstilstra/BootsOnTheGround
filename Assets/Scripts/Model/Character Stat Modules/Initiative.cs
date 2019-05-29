using UnityEngine;
using System.Collections;

public class Initiative : MonoBehaviour, StatModule {

	[SerializeField] int initiative;
	[SerializeField] int initiativeRoll;

	public void setStat(string statName, int statValue){
		if(statName.Equals("initiative")){
			initiative = statValue;
		}else if (statName.Equals ("initiativeRoll")){
			initiativeRoll = statValue;
		}else{
			Debug.Log ("Initiative: invalid stat name, unable to assign stat");
		}
	}

	public int getInitiative(){
		return initiative;
	}

	public int getInitiativeRoll(){
		return initiativeRoll;
	}

	public void setInitiativeRoll(int newInitiativeRoll){
		initiativeRoll = newInitiativeRoll;
	}

}
