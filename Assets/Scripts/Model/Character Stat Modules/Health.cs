using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour, StatModule {

	[SerializeField] int hpMax;
	[SerializeField] int hpCurrent;

    public delegate void ClickAction();
    public static event ClickAction OnDeath;

    public void setStat(string statName, int statValue){
		if(statName.Equals("hpMax")){
			hpMax = statValue;
		}else if (statName.Equals ("hpCurrent")){
			hpCurrent = statValue;
		}else{
			Debug.Log ("Health: invalid stat name, unable to assign stat");
		}
	}

	public void applyDamage(int damage){
		hpCurrent -= damage;
		if (hpCurrent <= 0){
			die();
		}
	}

	void die(){
		gameObject.SetActive (false);

        // raise death event
        OnDeath?.Invoke();
    }

	public void applyHealing(int healing){
		hpCurrent += healing;
		if (hpCurrent > hpMax){
			setHealthToMax ();
		}
	}

	public void setHealthToMax(){
		hpCurrent = hpMax;
	}

	public int getHPCurrent(){
		return hpCurrent;
	}

	public int getHPMax(){
		return hpMax;
	}
}
