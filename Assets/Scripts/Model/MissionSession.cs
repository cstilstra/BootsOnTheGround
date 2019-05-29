using UnityEngine;
using System.Collections;

public class MissionSession : MonoBehaviour {
	public GameLoop gameLoop;
    public IGameLoop turnType;

	IGameLoop gameLoopInterface;

	// Use this for initialization
	void Start () {
        //TODO: this needs to be set at runtime
        turnType = GetComponent<FactionTurnGameLoop>();
        //turnType = GetComponent<InitiativeTurnGameLoop>();

        gameLoop.loadGameLoop (turnType);
        gameLoop.doRunLoop ();
	}
}
