using UnityEngine;
using System.Collections;

public class AttackOrder : MonoBehaviour {

	/*

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

	public void rangedAttackOrderButtonClicked(){
		//Debug.Log ("AttackOrder: rangedAttackOrderButtonClicked() was called");
		attacker = selected.getSelected();
        // determine if selected has any actions left
        if(attacker.GetComponent<Character>().getUnusedActions() > 0){
            selectionUI.showRangedAttackRadius(attacker);
    		//Debug.Log ("AttackOrder: attacking unit id: " + attacker.GetComponent<Character>().getID ());
    		StartCoroutine (waitForRangedAttackTargetClick());
        }else{
            alertWindow.showWindow("Unit is out of moves");
        }
	}
	
	IEnumerator waitForRangedAttackTargetClick(){
		while(!Input.GetMouseButtonDown(0)){
			yield return null;
		}
        //turn off ranged attack radius indicator
        selectionUI.setRangedAttackRadiusIndicator (false);
		//Debug.Log ("AttackOrder: mouseButtonDown detected in waitForRangedAttackTargetClick()");
		// reference to hold the target
		GameObject target;
		//figure out where on the terrain the mouse click was
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast (ray, out hit)){
			GameObject goHit = hit.transform.gameObject;
			//if we hit a character
			if(goHit.GetComponent<Character>()){
				//Debug.Log ("AttackOrder: clicked on target character, id: " + goHit.GetComponent<Character>().getID ());
				// the distance between the selected object's current location and the target's location
				float distance = Vector3.Distance (attacker.transform.position, goHit.transform.position);
				//Debug.Log ("AttackOrder: distance of shot: " + distance);
				// if the target is too far away
				if(distance > attacker.GetComponent<RangedAttack>().getRangeDistance()){
					//Debug.Log ("AttackOrder: unit cannot shoot that far!");
					alertWindow.showWindow("Target is too far away");
				}else{
					target = goHit;
					rangedAttack (attacker, target);
				}
			}else{
				Debug.Log ("AttackOrder: not a valid target character");
			}

		}
	}

	void rangedAttack(GameObject attacker, GameObject target){
		// get the attacker's character
		Character tempAttackerCharacterSheet = attacker.GetComponent<Character>();
		RangedAttack attackerRangedAttackModule = attacker.GetComponent<RangedAttack>();
		Health targetHealthModule = target.GetComponent<Health>();
        //TODO: move character facing and rotation code out of here and into a characterOrientation class of some sort
		// turn the attacker towards the target
		// turn the character to face in the direction of movement
		attacker.transform.LookAt (target.transform);
		// correct the rotation of the unit
		attacker.transform.Rotate (new Vector3(0,180,0));
        // play shot animation
        attackerRangedAttackModule.playShotAnimation();
		// roll for hit
		if(diceRoller.rollSuccess (attackerRangedAttackModule.getRangeHitChance ())){
			// roll for crit
			if (diceRoller.rollSuccess (attackerRangedAttackModule.getRangeCritChance ())){
				// apply damage
				targetHealthModule.applyDamage (attackerRangedAttackModule.getRangeCritDMG());
                alertWindow.showWindow("Crit! Damage applied: " + attackerRangedAttackModule.getRangeCritDMG());
				//Debug.Log ("AttackOrder: Crit! Damage applied: " + attackerRangedAttackModule.getRangeCritDMG());
			}else{
				// apply damage
				targetHealthModule.applyDamage (attackerRangedAttackModule.getRangeDMG());
                alertWindow.showWindow("Damage applied: " + attackerRangedAttackModule.getRangeDMG());
				//Debug.Log ("AttackOrder: damage applied: " + attackerRangedAttackModule.getRangeDMG());
			}
		}else{
            alertWindow.showWindow("Shot Missed");
			//Debug.Log ("AttackOrder: shot missed");
		}
		// decrement unused actions
		tempAttackerCharacterSheet.useAction ();
	}
	*/
}
