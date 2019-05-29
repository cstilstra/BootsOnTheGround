using UnityEngine;
using System.Collections;

public class Healing : MonoBehaviour, StatModule {

	[SerializeField] int amountHealed;
	[SerializeField] int healDistance;

	public void setStat(string statName, int statValue){
		if(statName.Equals("amountHealed")){
			amountHealed = statValue;
		}else if (statName.Equals ("healDistance")){
			healDistance = statValue;
		}else{
			Debug.Log ("Healing: invalid stat name, unable to assign stat");
		}
	}

	public int getHealDistance(){
		return healDistance;
	}

	public int getAmountHealed(){
		return amountHealed;
	}
}
