using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ExplosiveAttackOrder : MonoBehaviour {

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
			//Debug.Log ("ExplosiveAttackOrder: ranged attack order given");
			attacker = selected.getSelected();
			// determine if selected has any actions left
			if(attacker.GetComponent<Character>().getUnusedActions() > 0){
				//determine if attacker has any explosives left
				if(attacker.GetComponent<ExplosiveAttack>().getNumExplosives() > 0){
					selectionUI.showExplosiveAttackRadius(attacker);
					//Debug.Log ("ExplosiveAttackOrder: attacking unit id: " + attacker.GetComponent<Character>().getID ());
					StartCoroutine (waitForExplosiveAttackTargetClick());
				}else{
					alertWindow.showWindow("Unit is out of explosives");
				}
			}else{
				alertWindow.showWindow("Unit is out of moves");
			}
		}
	}
	
	IEnumerator waitForExplosiveAttackTargetClick(){
		while(!Input.GetMouseButtonDown(0)){
			if(Input.GetMouseButtonDown (1)){
				// turn off explosive attack radius indicator
				selectionUI.setExplosiveAttackRadiusIndicator (false);;
				yield break;
			}else{
				yield return null;
			}
		}
		// turn off explosive attack radius indicator
		selectionUI.setExplosiveAttackRadiusIndicator (false);
		// if the pointer is not over a UI element
		if(!EventSystem.current.IsPointerOverGameObject()){
			// figure out where on the terrain the mouse click was
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast (ray, out hit)){
				// determine if mouse click is within range
				Vector3 targetLocation = hit.point;
				// the distance between the selected object's current location and the target's location
				float distance = Vector3.Distance (attacker.transform.position, targetLocation);
				// if the target is too far away
				if(distance > attacker.GetComponent<ExplosiveAttack>().getExplosionDistance()){
					alertWindow.showWindow("Target is too far away");
				}else{
					explosiveAttack (attacker, targetLocation);
				}

			}
		}else{
			// the pointer is over a UI element
			yield break;
		}
	}
	
	void explosiveAttack(GameObject attacker, Vector3 targetPosition){
		// get the attacker's character
		Character tempAttackerCharacterSheet = attacker.GetComponent<Character>();
		ExplosiveAttack attackerExplosiveAttackModule = attacker.GetComponent<ExplosiveAttack>();
		//TODO: move character facing and rotation code out of here and into a characterOrientation class of some sort
		// turn the attacker towards the target
		attacker.transform.LookAt (targetPosition);
		// correct the rotation of the unit
		attacker.transform.Rotate (new Vector3(0,180,0));
		// TODO: play shot animation

		int blastRadius = attackerExplosiveAttackModule.getExplosionWoundRAD();
		// play blast animation
		attackerExplosiveAttackModule.showExplosion(targetPosition, blastRadius);
		// get all units within target blast radius
		Collider[] hitColliders = Physics.OverlapSphere (targetPosition, blastRadius);
		List<Health> charactersHitHealthModules = findCharacters (hitColliders);
		// apply damage to all affected units
		int damage = attackerExplosiveAttackModule.getExplosionDMG();
		for(int i=0; i<charactersHitHealthModules.Count; i++){
			charactersHitHealthModules[i].applyDamage(damage);
		}
		// decrement number of explosives
		attackerExplosiveAttackModule.useExplosive();
		// decrement unused actions
		tempAttackerCharacterSheet.useAction ();
	}

	List<Health> findCharacters(Collider[] hitColliders){
		List<Health> charactersHit = new List<Health>();
		// loop through hitColliders
		for(int i=0; i<hitColliders.Length; i++){
			if(hitColliders[i].gameObject.GetComponent<Health>()){
				charactersHit.Add (hitColliders[i].gameObject.GetComponent<Health>());
			}
		}
		return charactersHit;
	}
}
