using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour, StatModule {

	[SerializeField] int meleeDMG;
	[SerializeField] int meleeCritDMG;
	[SerializeField] int meleeHitChance;
	[SerializeField] int meleeCritChance;
	[SerializeField] int meleeDistance = 3;

	public void setStat(string statName, int statValue){
		if(statName.Equals("meleeDMG")){
			meleeDMG = statValue;
		}else if (statName.Equals ("meleeCritDMG")){
			meleeCritDMG = statValue;
		}else if (statName.Equals ("meleeHitChance")){
			meleeHitChance = statValue;
		}else if (statName.Equals ("meleeCritChance")){
			meleeCritChance = statValue;
		}else{
			Debug.Log ("MeleeAttack: invalid stat name, unable to assign stat");
		}
	}

	public void playMeleeAnimation(){
		//Do nothing right now
	}

	public int getMeleeDistance(){
		return meleeDistance;
	}

	public int getMeleeDMG(){
		return meleeDMG;
	}

	public int getMeleeCritDMG(){
		return meleeCritDMG;
	}

	public int getMeleeHitChance(){
		return meleeHitChance;
	}

	public int getMeleeCritChance(){
		return meleeCritChance;
	}
}
