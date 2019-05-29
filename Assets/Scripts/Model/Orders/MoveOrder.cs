using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MoveOrder : MonoBehaviour {

    // reference to the Selected tracker
    Selected selected;
    // reference to the SelectionUI
    SelectionUI selectionUI;
    // reference to the currently selected character
    GameObject selectedChar;
    // reference to hold the pop up window
    PopUpWindow alertWindow;

	// Use this for initialization
	void Start () {
        selected = GameObject.FindGameObjectWithTag ("Interface").GetComponent<Selected>();
        selectionUI = GameObject.FindGameObjectWithTag ("Interface").GetComponent<SelectionUI>();
        alertWindow = GameObject.FindGameObjectWithTag ("PopUpWindow").GetComponent<PopUpWindow>();
	}

    public void orderCalled(){
		if(alertWindow.getVisible()){
			//do nothing if the alert window is visible
		}else{
	        //Debug.Log ("MoveOrder: moveOrderButtonClicked() was called");
	        selectedChar = selected.getSelected();
	        // determine if selected has any actions left
	        if(selectedChar.GetComponent<Character>().getUnusedActions() > 0){
	            selectionUI.showMoveRadius(selectedChar);
	            StartCoroutine (waitForMoveOrderClick ());
	        }else{
	            alertWindow.showWindow("Unit is out of moves");
	        }
		}
    }

    IEnumerator waitForMoveOrderClick(){
		while(!Input.GetMouseButtonDown(0)){
			if(Input.GetMouseButtonDown (1)){
				// turn off movement radius indicator
				selectionUI.setMoveRadiusIndicator (false);
				yield break;
			}else{
				yield return null;
			}
		}
        // turn off movement radius indicator
        selectionUI.setMoveRadiusIndicator (false);
		// if the pointer is not over a UI element
		if(!EventSystem.current.IsPointerOverGameObject()){
	        // figure out where on the terrain the mouse click was
	        RaycastHit hit;
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        if(Physics.Raycast (ray, out hit)){
	            // the distance between the selected object's current location and the mouse click
	            float distance = Vector3.Distance (selectedChar.transform.position, hit.point);
	            if(distance > selectedChar.GetComponent<Movement>().getMoveRange()){
					alertWindow.showWindow("Cannot move that far");
	            }else{
	                // TODO: move character facing, rotation, and movement code out of here and into a characterOrientation class of some sort
					// turn the character to face in the direction of movement
					selectedChar.transform.LookAt (hit.point);
					// correct the rotation of the unit
					selectedChar.transform.Rotate (new Vector3(0,180,0));
	                // move the character
	                selectedChar.transform.position = hit.point;
	                selectionUI.moveSelectionIndicator (new Vector3(selectedChar.transform.position.x, selectedChar.transform.position.y+1, selectedChar.transform.position.z));
	                // decrement unused actions
	                selectedChar.GetComponent<Character>().useAction();
	            }
	        }
		}else{
			// the pointer is over a UI element
			yield break;
		}
    }
}
