using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MeleeAttackOrder : MonoBehaviour {

	// reference to the Selected tracker
	Selected selected;
	// reference to the SelectionUI
	SelectionUI selectionUI;
	// reference to hold the attacking character
	GameObject attacker;
	// reference to hold the dice roller
	DiceRoller diceRoller;
	// reference to hold the pop up window
	PopUpWindow alertWindow;
	
	// Use this for initialization
	void Start () {
		selected = GameObject.FindGameObjectWithTag ("Interface").GetComponent<Selected>();
		selectionUI = GameObject.FindGameObjectWithTag ("Interface").GetComponent<SelectionUI>();
		diceRoller = GameObject.FindGameObjectWithTag ("MissionSession").GetComponent<DiceRoller>();
		alertWindow = GameObject.FindGameObjectWithTag ("PopUpWindow").GetComponent<PopUpWindow>();
	}

	public void orderCalled(){
		if(alertWindow.getVisible()){
			//do nothing if the alert window is visible
		}else{
			attacker = selected.getSelected();
			// determine if selected has any actions left
			if(attacker.GetComponent<Character>().getUnusedActions() > 0){
				selectionUI.showMeleeAttackRadius(attacker);
				StartCoroutine (waitForMeleeAttackTargetClick());
			}else{
				alertWindow.showWindow("Unit is out of moves");
			}
		}
	}

	IEnumerator waitForMeleeAttackTargetClick(){
		while(!Input.GetMouseButtonDown(0)){
			if(Input.GetMouseButtonDown (1)){
				// turn off melee attack radius indicator
				selectionUI.setMeleeAttackRadiusIndicator (false);
				yield break;
			}else{
				yield return null;
			}
		}
		// turn off melee attack radius indicator
		selectionUI.setMeleeAttackRadiusIndicator (false);
		// if the pointer is not over a UI element
		if(!EventSystem.current.IsPointerOverGameObject()){
			// reference to hold the target
			GameObject target;
			//figure out where on the terrain the mouse click was
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast (ray, out hit)){
				GameObject goHit = hit.transform.gameObject;
				// if we hit a character
				if(goHit.GetComponent<Character>()){
					// the distance between the selected object's current location and the target's location
					float distance = Vector3.Distance (attacker.transform.position, goHit.transform.position);
					// if the target is too far away
					if(distance > attacker.GetComponent<MeleeAttack>().getMeleeDistance()){
						alertWindow.showWindow("Target is too far away");
					}else{
						target = goHit;
						meleeAttack (attacker, target);
					}
				}else{
					// target is not valid
					alertWindow.showWindow("Not a valid target");
				}				
			}
		}else{
			// if the pointer is over a UI element
			yield break;
		}
	}

	void meleeAttack(GameObject attacker, GameObject target){
		// get the attacker's character
		Character tempAttackerCharacterSheet = attacker.GetComponent<Character>();
		MeleeAttack attackerMeleeAttackModule = attacker.GetComponent<MeleeAttack>();
		Health targetHealthModule = target.GetComponent<Health>();
		//TODO: move character facing and rotation code out of here and into a characterOrientation class of some sort
		// turn the attacker towards the target
		// turn the character to face in the direction of movement
		attacker.transform.LookAt (target.transform);
		// correct the rotation of the unit
		attacker.transform.Rotate (new Vector3(0,180,0));
		// play melee attack animation
		attackerMeleeAttackModule.playMeleeAnimation();
		// roll for hit
		if(diceRoller.rollSuccess (attackerMeleeAttackModule.getMeleeHitChance ())){
			// roll for crit
			if (diceRoller.rollSuccess (attackerMeleeAttackModule.getMeleeCritChance ())){
				// apply damage
				targetHealthModule.applyDamage (attackerMeleeAttackModule.getMeleeCritDMG());
				alertWindow.showWindow("Crit! Damage applied: " + attackerMeleeAttackModule.getMeleeCritDMG());
			}else{
				// apply damage
				targetHealthModule.applyDamage (attackerMeleeAttackModule.getMeleeDMG());
				alertWindow.showWindow("Damage applied: " + attackerMeleeAttackModule.getMeleeDMG());
			}
		}else{
			alertWindow.showWindow("Attack Missed");
		}
		// decrement unused actions
		tempAttackerCharacterSheet.useAction ();
	}
}
