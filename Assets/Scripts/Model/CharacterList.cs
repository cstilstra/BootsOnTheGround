using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class CharacterList : MonoBehaviour {
	GameObject[] characters;
	CharacterSheetLoader csl;
	DiceRoller diceRoller;

	public int numberOfFactions = 0;

	public void init(int actionsPerTurn){
		csl = GameObject.FindGameObjectWithTag ("MissionSession").GetComponent<CharacterSheetLoader>();
		diceRoller = GameObject.FindGameObjectWithTag ("MissionSession").GetComponent<DiceRoller>();
		getCharacterPrefabs();
		//Debug.Log ("CharacterList: num characters in list: "+characters.Length);
		loadCharacterStats(actionsPerTurn);
		rollCharacterInitiative ();
		sortByID ();
	}

	public GameObject[] getCharacters(){
		return characters;
	}

	//this method requires that characters is sorted by ID
	public GameObject getCharacterById(int id){
		return characters[id];
	}

	void getCharacterPrefabs(){
		characters = GameObject.FindGameObjectsWithTag ("Character");
	}

	void loadCharacterStats(int actionsPerTurn){
		for(int i=0; i<characters.Length;i++){
			Character tempCharacter = characters[i].GetComponent<Character>();
			tempCharacter.setID (i);
			tempCharacter.setTotalActions(actionsPerTurn);
			string tempName = tempCharacter.typeName;
			int tempLevel = tempCharacter.getLevel();
			//Debug.Log ("CharacterList: character at index "+i+" is a "+ tempName);
			string[] characterStats = csl.readCharacterSheet (tempName, tempLevel);
			tempCharacter.assignStats (characterStats);
		}
	}

	void rollCharacterInitiative(){
		for(int i=0; i<characters.Length;i++){
			Character tempCharacter = characters[i].GetComponent<Character>();
			Initiative tempCharInitiative = tempCharacter.GetComponent<Initiative>();
			tempCharInitiative.setInitiativeRoll(diceRoller.rollInitiative (tempCharInitiative.getInitiative()));
		}
	}

	void sortByInitiative(){
		characters = characters.OrderByDescending(x => x.GetComponent<Initiative>().getInitiativeRoll()).ToArray ();
	}

	void sortByID(){
		characters = characters.OrderBy(x => x.GetComponent<Character>().getID()).ToArray ();
	}

	public int[] getIdsByInitiative(){
		sortByInitiative ();
		int[] tempArray = new int[characters.Length];
		for(int i=0; i<characters.Length;i++){
			Character tempCharacter = characters[i].GetComponent<Character>();
			tempArray[i] = tempCharacter.getID ();
		}
		sortByID ();
		return tempArray;
	}

	public List<int>[] getIdListsByFaction(){
		// create an array of lists of ints (each list will represent one faction, each entry will be a character's id)
		List<int>[] factions = new List<int>[numberOfFactions];
		// loop through the array, initializing each list
		for(int i = 0; i < factions.Length; i++){
			factions[i] = new List<int>();
		}
		//Debug.Log ("CharacterList: factions list length: " + factions.Length);
		// loop through the character list, assigning each character id to the proper list
		for(int i = 0; i < characters.Length; i++){
			// get character sheet
			Character tempCharacter = characters[i].GetComponent<Character>();
			//Debug.Log ("CharacterList: character faction: " + tempCharacter.faction);
			// add the character's id to the list in their faction's array index
			factions[tempCharacter.getFaction()].Add (tempCharacter.getID ());
		}
		//Debug.Log ("CharacterList: faction 0 length: " + factions[0].Count);
		//Debug.Log ("CharacterList: faction 1 length: " + factions[1].Count);

		return factions;
	}

	void printInSortedOrder(){
		for(int i=0; i<characters.Length; i++){
			Debug.Log ("CharacterList: "+characters[i].GetComponent<Character>().typeName+" has turn "+(i+1)+" with a roll of " + characters[i].GetComponent<Initiative>().getInitiativeRoll());
		}
	}

	public void setAllSelectable(bool input){
		for(int i=0; i<characters.Length; i++){
			if(characters[i].activeSelf){
				characters[i].GetComponent<Selectable>().setSelectable (input);
			}
		}
	}
}
