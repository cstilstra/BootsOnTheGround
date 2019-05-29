using UnityEngine;

public class Selected : MonoBehaviour {

	GameObject selected;

	public SelectionUI selectionUI; //linked in inspector

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // takes a GameObject as an argument
    // sets that GameObject as the selected object
	public void setSelected(GameObject incomingSelection){
		selected = incomingSelection; 
		selectionUI.setSelected (new Vector3(selected.transform.position.x, selected.transform.position.y+1, selected.transform.position.z));
		selectionUI.updateSelectionPanel (incomingSelection);
	}

    // returns the selected GameObject
	public GameObject getSelected(){
		return selected;
	}
}
