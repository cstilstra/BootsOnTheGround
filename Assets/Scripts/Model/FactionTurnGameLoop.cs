using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FactionTurnGameLoop : MonoBehaviour, IGameLoop {

	public int actionsPerTurn = 2;

	CharacterList characterList;
    List<int>[] charIDsByFaction;

    Selected selected;
	// reference to hold the pop up window
	PopUpWindow alertWindow;
	bool firstTurn = true;
	bool done = false;
	bool endTurn = false;
	int currentFaction = 0;

	public void init () {
		characterList = GameObject.FindGameObjectWithTag ("MissionSession").GetComponent<CharacterList>();
		characterList.init(actionsPerTurn);
        // get a list of all of the characters by faction
        charIDsByFaction = characterList.getIdListsByFaction();

        selected = GameObject.FindGameObjectWithTag ("Interface").GetComponent<Selected>();
		alertWindow = GameObject.FindGameObjectWithTag ("PopUpWindow").GetComponent<PopUpWindow>();

        Health.OnDeath += unitDeath;
	}

	// loop through factions
	public IEnumerator doRunLoop(){		
		// while we haven't been told that the game is finished
		while(!done){
			
			// loop through the array of factions
			for(int i=0; i<charIDsByFaction.Length; i++){
				// if the game has finished since the start of the loop
				if(done){
					// exit
					yield return null;
				// if the game has not finished since the start of the loop
				}else{
					// set which faction has the current turn
					currentFaction = i;
                    // start the single turn method as a coroutine 
                    // (prevents this call from locking up the thread)
					yield return StartCoroutine (doOneTurn (charIDsByFaction[currentFaction]));
				}
			}
            // once we've finished looping through the characters, 
            // we can return (brings us back up to the beginning of the while loop)
			yield return null;
		}	
        	
		Debug.Log ("FactionTurnGameLoop: game finished");
        alertWindow.showWindow("Game finished!");
    }

	// sets all characters in <currentFaction> to be selectable, all others not selectable
	// waits until the "end turn" button is pressed, then finishes and exits
	IEnumerator doOneTurn(List<int> currentFaction){
		if(!firstTurn){
			alertWindow.showWindow("Next player's turn!");
		}
		
		// wait for one frame (the key press event will still be
		// true during this frame, and it should be cleared before we do anything)
		yield return null;
		
		//turn all of the units to be non selectable
		characterList.setAllSelectable(false);
		// loop through each character in the current faction
		// toggle the selected state of each character
		// reset the number of unused actions
		for(int i = 0; i < currentFaction.Count; i++){
			// get the character at the current index in the loop
			GameObject tempCharacter = characterList.getCharacterById(currentFaction[i]);
			// get the selectable object from the current unit
			Selectable tempSelectable = tempCharacter.GetComponent<Selectable>();
			// set selectable to true
			tempSelectable.setSelectable (true);
			// get the character object from the current unit
			Character tempCharacterSheet = tempCharacter.GetComponent<Character>();
			// set the unused actions to match total actions
			tempCharacterSheet.refillActions ();
		}
		// set the first unit in currentFaction to be the currently selected unit
		// unless the first unit is inactive
		for(int i=0; i<currentFaction.Count;i++){
			if(characterList.getCharacterById(currentFaction[i]).activeSelf){
				selected.setSelected (characterList.getCharacterById(currentFaction[i]));
				break;
			}			
		}		
		// loop until endTurn is true
		while(!endTurn){
			yield return null;
		}
		// reset endTurn to false
		resetEndTurn ();
		//Debug.Log ("GameLoop: turn finished");
		firstTurn = false;
	}
		
	public void setEndTurn(bool input){
		endTurn = input;
	}
	
	public void setEndGame(){
		done = true;
	}
	
	void resetEndTurn(){
		setEndTurn (false);
	}

    void unitDeath()
    {
        //Debug.Log("A unit just died!");

        // loop through each of the factions
        foreach(List<int> faction in charIDsByFaction)
        {
            bool liveSoldierFound = false;
            // try to find a unit in this faction that is active
            foreach(int characterId in faction)
            {
                // get the character with the matching ID
                GameObject tempCharacter = characterList.getCharacterById(characterId);
                if (tempCharacter.activeSelf)
                {
                    liveSoldierFound = true;
                    break;
                }
            }
            // if there is no active unit, the game is over;
            if (!liveSoldierFound)
            {
                done = true;
                break;
            }
        }
    }
}
