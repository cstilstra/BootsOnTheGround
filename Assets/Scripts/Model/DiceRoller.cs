using UnityEngine;
using System.Collections;

public class DiceRoller : MonoBehaviour {

	public bool rollSuccess(int percent){
		int roll = rollDice (100);
		if (percent>= roll){
			return true;
		}else{
			return false;
		}
	}

	public int rollInitiative(int bonus){
		int roll = rollDice (20);
		int initiative = roll + bonus;
		return initiative;
	}

	private int rollDice(int numSides){
		float rollFloat = Random.Range(0F,(float)numSides);
		int rollInt = (int)(rollFloat+1);
		return rollInt;
	}
}
