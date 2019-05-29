using UnityEngine;
using System.Collections;

public interface StatModule{

	//TODO: can probably be an abstract class that all other stat modules extend, since they don't seem to need to extend Monobehaviour

	void setStat(string statName, int statValue);

}
