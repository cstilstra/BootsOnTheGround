using UnityEngine;
using System.Collections;

public class Sight : MonoBehaviour , StatModule {

	[SerializeField] int sightRadius;

	public void setStat(string statName, int statValue){
		if(statName.Equals("sightRadius")){
			setSightRadius(statValue);
		}else{
			Debug.Log ("Sight: invalid stat name, unable to assign stat");
		}
	}

	void setSightRadius(int radius){
		sightRadius = radius;
	}

	public int getSightRadius(){
		return sightRadius;
	}
}
