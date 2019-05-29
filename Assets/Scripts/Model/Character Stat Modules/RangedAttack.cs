using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour, StatModule {

	[SerializeField] int rangeDMG;
	[SerializeField] int rangeCritDMG;
	[SerializeField] int rangeHitChance;
	[SerializeField] int rangeCritChance;
	[SerializeField] int rangeDistance;
	[SerializeField] int roundsPerBurst;

    public ParticleSystem rangedShotParticle;

	public void setStat(string statName, int statValue){
		if(statName.Equals("rangeDMG")){
			setRangeDMG(statValue);
		}else if (statName.Equals ("rangeCritDMG")){
			setRangeCritDMG(statValue);
		}else if (statName.Equals ("rangeHitChance")){
			setRangeHitChance(statValue);
		}else if (statName.Equals ("rangeCritChance")){
			setRangeCritChance(statValue);
		}else if (statName.Equals ("rangeDistance")){
			setRangeDistance(statValue);
		}else if (statName.Equals ("roundsPerBurst")){
			setRoundsPerBurst(statValue);
		}else{
			Debug.Log ("RangedAttack: invalid stat name, unable to assign stat");
		}
	}

	public void playShotAnimation(){
		rangedShotParticle.Emit(1);
	}

	public int getRangeDMG(){
		return rangeDMG;
	}

	public void setRangeDMG(int damage){
		rangeDMG = damage;
	}

	public int getRangeCritDMG(){
		return rangeCritDMG;
	}

	public void setRangeCritDMG(int damage){
		rangeCritDMG = damage;
	}

	public int getRangeHitChance(){
		return rangeHitChance;
	}

	public void setRangeHitChance(int percentChance){
		rangeHitChance = percentChance;
	}

	public int getRangeCritChance(){
		return rangeCritChance;
	}

	public void setRangeCritChance(int percentChance){
		rangeCritChance = percentChance;
	}

	public int getRangeDistance(){
		return rangeDistance;
	}

	public void setRangeDistance(int distance){
		rangeDistance = distance;
	}

	public int getRoundsPerBurst(){
		return roundsPerBurst;
	}

	public void setRoundsPerBurst(int numRounds){
		roundsPerBurst = numRounds;
	}
    
}
