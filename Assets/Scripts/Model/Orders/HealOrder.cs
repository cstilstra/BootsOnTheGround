using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class HealOrder : MonoBehaviour {

	// reference to the Selected tracker
	Selected selected;
	// reference to hold the healing character
	GameObject healer;
	// reference to the SelectionUI
	SelectionUI selectionUI;
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
			healer = selected.getSelected();
			// determine if selected has any actions left
			if(healer.GetComponent<Character>().getUnusedActions() > 0){
				selectionUI.showHealRadius(healer);
				//Debug.Log ("AttackOrder: attacking unit id: " + attacker.GetComponent<Character>().getID ());
				StartCoroutine (waitForHealTargetClick());
			}else{
				alertWindow.showWindow("Unit is out of moves");
	        }
		}
    }

	IEnumerator waitForHealTargetClick(){
		while(!Input.GetMouseButtonDown(0)){
			if(Input.GetMouseButtonDown (1)){
				// turn off heal radius indicator
				selectionUI.setHealRadiusIndicator (false);
				yield break;
			}else{
				yield return null;
			}
		}
		// turn off heal radius indicator
		selectionUI.setHealRadiusIndicator (false);
		// if the pointer is not over a UI element
		if(!EventSystem.current.IsPointerOverGameObject()){
			// reference to hold the target
			GameObject target;
			// figure out where on the terrain the mouse click was
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast (ray, out hit)){
				GameObject goHit = hit.transform.gameObject;
				// if we hit a character
				if(goHit.GetComponent<Character>()){
					// the distance between the selected object's current location and the target's location
					float distance = Vector3.Distance (healer.transform.position, goHit.transform.position);
					// if the target is too far away
					if(distance > healer.GetComponent<Healing>().getHealDistance()){
						alertWindow.showWindow("Target is too far away");
	                }else{
	                    target = goHit;
	                    heal (healer, target);
	                }
	            }else{
					alertWindow.showWindow ("Not a valid target");
	            }
	            
	        }
		}else{
			yield break;
		}
    }

	void heal(GameObject healer, GameObject target){
		// get the healer's character
		Character tempHealerCharacterSheet = healer.GetComponent<Character>();
		Healing healerHealModule = healer.GetComponent<Healing>();
		Health targetHealthModule = target.GetComponent<Health>();
		int beforeHealing = targetHealthModule.getHPCurrent();
		//TODO: move character facing and rotation code out of here and into a characterOrientation class of some sort
		// turn the healer towards the target
		// turn the character to face in the direction of movement
		healer.transform.LookAt (target.transform);
		// correct the rotation of the unit
		healer.transform.Rotate (new Vector3(0,180,0));
		//apply healing
		targetHealthModule.applyHealing (healerHealModule.getAmountHealed());
		int afterHealing = targetHealthModule.getHPCurrent();
		alertWindow.showWindow("Target healed for " + (afterHealing - beforeHealing) + " health points");

		/*
		// roll for hit
		if(diceRoller.rollSuccess (healerHealModule.getRangeHitChance ())){
			// roll for crit
			if (diceRoller.rollSuccess (healerHealModule.getRangeCritChance ())){
				// apply damage
				targetHealthModule.applyDamage (healerHealModule.getRangeCritDMG());
				alertWindow.showWindow("Crit! Damage applied: " + healerHealModule.getRangeCritDMG());
				Debug.Log ("AttackOrder: Crit! Damage applied: " + healerHealModule.getRangeCritDMG());
			}else{
				// apply damage
				targetHealthModule.applyDamage (healerHealModule.getRangeDMG());
				alertWindow.showWindow("Damage applied: " + healerHealModule.getRangeDMG());
				Debug.Log ("AttackOrder: damage applied: " + healerHealModule.getRangeDMG());
			}
		}else{
            alertWindow.showWindow("Shot Missed");
            Debug.Log ("AttackOrder: shot missed");
        }
		*/

        // decrement unused actions
        tempHealerCharacterSheet.useAction ();
    }
    
}
