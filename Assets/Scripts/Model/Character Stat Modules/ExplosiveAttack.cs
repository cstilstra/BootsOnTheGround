using UnityEngine;
using System.Collections;

public class ExplosiveAttack : MonoBehaviour, StatModule {

	[SerializeField] int explosionDMG;
	[SerializeField] int explosionKillRAD;
	[SerializeField] int explosionWoundRAD;
	[SerializeField] int explosionAccurRAD;
	[SerializeField] int explosionDistance;
	[SerializeField] int numExplosives;

	public GameObject explosionIndicator;	

	public void setStat(string statName, int statValue){
		if(statName.Equals("explosionDMG")){
			explosionDMG = statValue;
		}else if (statName.Equals ("explosionKillRAD")){
			explosionKillRAD = statValue;
		}else if (statName.Equals ("explosionWoundRAD")){
			explosionWoundRAD = statValue;
		}else if (statName.Equals ("explosionAccurRAD")){
			explosionAccurRAD = statValue;
		}else if (statName.Equals ("explosionDistance")){
			explosionDistance = statValue;
		}else if (statName.Equals ("numExplosives")){
			numExplosives = statValue;
		}else{
			Debug.Log ("ExplosiveAttack: invalid stat name, unable to assign stat");
		}
	}

	public void useExplosive(){
		numExplosives -= 1;
		if (numExplosives < 0){
			numExplosives = 0;
		}
	}

	public void showExplosion(Vector3 location, int radius){
		GameObject explosion = (GameObject)Instantiate(explosionIndicator, location, transform.rotation);
		sizeExplosionIndicator(explosion, radius);
		Destroy(explosion, 0.75f);
	}

	void sizeExplosionIndicator(GameObject explosion, int radius){
		int diameter = radius * 2;
		explosion.transform.localScale = new Vector3(diameter, diameter, diameter);
	}

	public int getExplosionDistance(){
		return explosionDistance;
	}
	
	public int getExplosionKillRAD(){
		return explosionKillRAD;
	}
	
	public int getExplosionWoundRAD(){
		return explosionWoundRAD;
	}
	
	public int getExplosionDMG(){
		return explosionDMG;
	}
	
	public int getNumExplosives(){
		return numExplosives;
	}
}
