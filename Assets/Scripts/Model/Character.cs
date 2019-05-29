using UnityEngine;
using System.Collections;
using System;

public class Character : MonoBehaviour {

	public string typeName;

	//TODO:? Make a generic unit prefab that all of the other units can inherit from


	[SerializeField]private int level;
	[SerializeField]private int faction;
	[SerializeField]private int unusedActions;
	[SerializeField]private int totalActions;
	[SerializeField]private int id;

	// Stat modules
	Health healthModule;
	RangedAttack rangedAttackModule;
	MeleeAttack meleeAttackModule;
	ExplosiveAttack explosiveAttackModule;
	Healing healingModule;
	Initiative initiativeModule;
	Movement movementModule;
	Sight sightModule;

    // Use this for initialization
    void Start () {
		// Get references to all of the attached stat modules
		healthModule = GetComponent<Health>();
		rangedAttackModule = GetComponent<RangedAttack>();
		meleeAttackModule = GetComponent<MeleeAttack>();
		explosiveAttackModule = GetComponent<ExplosiveAttack>();
		healingModule = GetComponent<Healing>();
		initiativeModule = GetComponent<Initiative>();
		movementModule = GetComponent<Movement>();
		sightModule = GetComponent<Sight>();
	}

	public void assignStats(string[] statsArray){;
		// Loop through the entries in statsArray
		for(int i=0;i<statsArray.Length;i++){
			// Get the name of the stat
			string stat = parseStatFromLine (statsArray[i]);
			// Get the value of the stat
			int value = parseValueFromLine(statsArray[i]);
			// Pass in to the method that will set the stat
			routeStatAndValueToModule (stat,value);
		}
	}

	public int getID(){
		return id;
	}

	public void setID(int newID){
		id = newID;
	}

	public int getLevel(){
		return level;
	}

	public void setLevel(int newLevel){
		level = newLevel;
	}

	public int getFaction(){
		return faction;
	}

	public int getUnusedActions(){
		return unusedActions;
	}

	public void useAction(){
		unusedActions --;
	}

	public void refillActions(){
		unusedActions = totalActions;
	}

	public int getTotalActions(){
		return totalActions;
	}

	public void setTotalActions(int numActions){
		totalActions = numActions;
	}

	public Health getHealthModule(){
		return healthModule;
	}

	// Refills how many actions the character has available
	public void resetActions(){
		unusedActions = totalActions;
	}

	// Figures out which stat a line represents
	private string parseStatFromLine(string toParse){
		return toParse.Substring (0, toParse.IndexOf(':'));
	}

	// Figures out the value that is contained in the line
	private int parseValueFromLine(string toParse){
		string valueString = toParse.Substring (toParse.IndexOf(':')+1);
		int value = Convert.ToInt32(valueString);
		return value;
    }

	// Routes stat names and values to the appropriate module
	private void routeStatAndValueToModule(string statInput, int valueInput){
		if(statInput.Equals("hpMax") || statInput.Equals ("hpCurrent")){
			healthModule.setStat(statInput, valueInput);
			// When filling out hpMax
			if(statInput.Equals("hpMax")){
				healthModule.setHealthToMax ();
			}
		}else if(statInput.Equals("rangeDMG") || statInput.Equals ("rangeCritDMG") || statInput.Equals ("rangeHitChance") || statInput.Equals ("rangeCritChance") || statInput.Equals ("rangeDistance") || statInput.Equals ("roundsPerBurst")){
			rangedAttackModule.setStat(statInput, valueInput);
		}else if(statInput.Equals("explosionDMG") || statInput.Equals ("explosionKillRAD") || statInput.Equals ("explosionWoundRAD") || statInput.Equals ("explosionAccurRAD") || statInput.Equals ("explosionDistance") || statInput.Equals ("numExplosives")){
			explosiveAttackModule.setStat(statInput, valueInput);
		}else if(statInput.Equals("meleeDMG") || statInput.Equals ("meleeCritDMG") || statInput.Equals ("meleeHitChance") || statInput.Equals ("meleeCritChance")){
			meleeAttackModule.setStat(statInput, valueInput);
		}else if(statInput.Equals("amountHealed") || statInput.Equals ("healDistance")){
			healingModule.setStat(statInput, valueInput);
		}else if(statInput.Equals("initiative") || statInput.Equals ("initiativeRoll")){
			initiativeModule.setStat(statInput, valueInput);
		}else if(statInput.Equals("moveRange")){
			movementModule.setStat(statInput, valueInput);
		}else if(statInput.Equals("sightRadius")){
			sightModule.setStat(statInput, valueInput);
		}else{
			if(!(statInput.Equals (""))){
				Debug.Log ("Character: "+typeName+" incorrect stat in .csh file: "+statInput);
            }
        }
	}
}
