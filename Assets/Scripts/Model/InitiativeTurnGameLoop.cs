using UnityEngine;
using System.Collections;

public class InitiativeTurnGameLoop : MonoBehaviour, IGameLoop {
	public int actionsPerTurn = 2;

	CharacterList characterList;
	Selected selected;
	// reference to hold the pop up window
	PopUpWindow alertWindow;
	bool firstTurn = true;
	bool done = false;
	bool endTurn = false;

	public void init(){
		characterList = GameObject.FindGameObjectWithTag ("MissionSession").GetComponent<CharacterList>();
		characterList.init(actionsPerTurn);
		selected = GameObject.FindGameObjectWithTag ("Interface").GetComponent<Selected>();
		alertWindow = GameObject.FindGameObjectWithTag ("PopUpWindow").GetComponent<PopUpWindow>();
	}

	public IEnumerator doRunLoop(){

		// get a list of all of the character IDs sorted by initiative roll
		int[] charactersByInitiative = characterList.getIdsByInitiative();

		while (!done){
			// loop through the list of character IDs
			for(int i=0; i<charactersByInitiative.Length; i++){
				// if the game has finished since the start of the loop
				if(done){
					// exit
					yield return null;
				// if the game has not finished since the start of the loop
				}else{
					// start a turn for the current character in the loop
					yield return StartCoroutine (doOneTurn (charactersByInitiative[i]));
				}
			}
			// once we've finished looping through the characters, we can return (brings us back up to the beginning of the while loop)
			yield return null;
		}

		Debug.Log ("InitiativeTurnGameLoop: game finished");
	}

	IEnumerator doOneTurn(int characterID){
		if(!firstTurn){
			alertWindow.showWindow("Next character's turn!");
		}

		// wait for one frame (the key press event will still be
		// true during this frame, and it should be cleared before we do anything)
		yield return null;
		
		//turn all of the units to be non selectable
		characterList.setAllSelectable(false);
		// get the character with the matching ID
		GameObject tempCharacter = characterList.getCharacterById(characterID);
		// get the selectable object from the current unit
		Selectable tempSelectable = tempCharacter.GetComponent<Selectable>();
		// set selectable to true
		tempSelectable.setSelectable (true);
		// get the character object from the current unit
		Character tempCharacterSheet = tempCharacter.GetComponent<Character>();
		// set the unused actions to match total actions
		tempCharacterSheet.refillActions ();
		// set the current character to be the selected character
		selected.setSelected(tempCharacter);

		// loop until endTurn is true
		while(!endTurn){
			yield return null;
		}
		// reset endTurn to false
		setEndTurn (false);
		firstTurn = false;
	}

	public void setEndTurn(bool input){
		endTurn = input;
	}

	public void setEndGame(){
		done = true;
	}
}
