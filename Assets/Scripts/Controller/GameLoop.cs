using UnityEngine;

// Acts as an interface for the MissionSession and the UI buttons to hook into whichever game loop
// system is the one being used
public class GameLoop : MonoBehaviour {

	IGameLoop gameLoop;

	public void loadGameLoop(IGameLoop gameLoop){
		this.gameLoop = gameLoop;
		gameLoop.init();
	}

	public void setEndTurn(bool input){
		gameLoop.setEndTurn(input);
	}

	public void setEndGame(){
		gameLoop.setEndGame ();
	}

	public void doRunLoop(){
		StartCoroutine(gameLoop.doRunLoop ());
	}
}
