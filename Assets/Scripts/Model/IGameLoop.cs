using UnityEngine;
using System.Collections;

public interface IGameLoop  {

	void init();
	IEnumerator doRunLoop();
	void setEndTurn(bool input);
	void setEndGame();

}
