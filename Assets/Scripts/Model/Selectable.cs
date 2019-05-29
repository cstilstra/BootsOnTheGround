using UnityEngine;
using System.Collections;

// Controls whether a particular unit is selectable
// Must be attached to a character unit

public class Selectable : MonoBehaviour {

	[SerializeField]private bool selectable;
	private bool initialized = false;

	// reference to the 'selected' object from the main scene
	Selected selected;
	// reference to the 'characterList' object from the main scene
	CharacterList characterList;
	// reference to the character object of the current unit (to get the ID)
	Character character;
	// reference to the object that needs to be passed to 'selected'
	GameObject passToSelected;

	// Use this for initialization
	void Start () {
		setSelectable (false);

		// set the reference for selected
		selected = GameObject.FindGameObjectWithTag ("Interface").GetComponent<Selected>();
		// set the reference for characterList
		characterList = GameObject.FindGameObjectWithTag ("MissionSession").GetComponent<CharacterList>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void init(){
		// set the reference for the character
		character = this.GetComponentInChildren<Character>();
		// set the reference for passToSelected
		passToSelected = characterList.getCharacterById (character.getID ());
	}

	// when clicked
	public void OnMouseDown(){
		if(!initialized){
			init ();
			initialized = true;
		}
		if(selectable){
			//Debug.Log ("Selectable: This character is selectable this turn");
			selected.setSelected (passToSelected);
		}else{
			//Debug.Log ("Selectable: This character is NOT selectable this turn");
		}
	}

	// toggle function
	public void toggleSelectable(){
		selectable = !selectable;
	}

	// setter
	public void setSelectable(bool input){
		selectable = input;
	}

	//getter
	public bool getSelectable(){
		return selectable;
	}
}
