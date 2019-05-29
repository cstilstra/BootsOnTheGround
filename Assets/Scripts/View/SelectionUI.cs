using UnityEngine;
using UnityEngine.UI;

public class SelectionUI : MonoBehaviour {

	public GameObject selectionIndicator;
	public GameObject destinationIndicator;
	public GameObject moveRadiusIndicator;
    public GameObject rangedAttackRadiusIndicator;
	public GameObject meleeAttackRadiusIndicator;
	public GameObject explosiveAttackRadiusIndicator;
	public GameObject healRadiusIndicator;
	GameObject currentlySelected;
	Text selectedTypeLabel;
	Text selectedHPLabel;
	Text movesThisTurnLabel;
	Text selectedExplosivesLabel;
    Vector3 target;

    // Use this for initialization
    void Start () {
		selectedTypeLabel = GameObject.FindGameObjectWithTag ("SelectedTypeLabel").GetComponent<Text>();
		selectedHPLabel = GameObject.FindGameObjectWithTag ("SelectedHPLabel").GetComponent<Text>();
		movesThisTurnLabel = GameObject.FindGameObjectWithTag("MovesThisTurnLabel").GetComponent<Text>();
		selectedExplosivesLabel = GameObject.FindGameObjectWithTag("SelectedExplosivesLabel").GetComponent<Text>();
        selectionIndicator.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		updateSelectionPanel();
	}

    public void setSelected(Vector3 incomingTarget)
    {
        target = incomingTarget;
        moveSelectionIndicator(target);
    }

	public void moveSelectionIndicator(Vector3 position){
		selectionIndicator.transform.position = position;
	}    

    public void updateSelectionPanel(GameObject selected){
		currentlySelected = selected;
		updateSelectionPanel();
	}

	public void updateSelectionPanel(){
        if (currentlySelected != null)
        {
            Character selectedCharacterSheet = currentlySelected.GetComponent<Character>();
            Health selectedHealthSheet = currentlySelected.GetComponent<Health>();
            ExplosiveAttack selectedExplosiveAttackSheet = currentlySelected.GetComponent<ExplosiveAttack>();
            selectedTypeLabel.text = "Selected Character: " + selectedCharacterSheet.typeName;
            selectedHPLabel.text = "HP: " + selectedHealthSheet.getHPCurrent() + "/" + selectedHealthSheet.getHPMax();
            movesThisTurnLabel.text = "Moves: " + selectedCharacterSheet.getUnusedActions() + "/" + selectedCharacterSheet.getTotalActions();
            selectedExplosivesLabel.text = "Explosives: " + selectedExplosiveAttackSheet.getNumExplosives();
        }
    }

	public void moveDestinationIndicator(Vector3 position){
		destinationIndicator.SetActive (true);
		destinationIndicator.transform.position = position;
	}

	public void setDestinationIndicator(bool value){
		destinationIndicator.SetActive (value);
	}

	// MOVEMENT
	public void showMoveRadius(GameObject selected){
        Debug.Log("SelectionUI.showMoveRadius()");        
		moveRadiusIndicator.SetActive (true);
		sizeMoveRadiusIndicator (selected);
		moveRadiusIndicator.transform.position = selected.transform.position;
        Debug.Log("move indicator position: " + 
            moveRadiusIndicator.transform.position.x + "," +
            moveRadiusIndicator.transform.position.y + "," +
            moveRadiusIndicator.transform.position.z
            );
        Debug.Log("selected unit position: " +
            selected.transform.position.x + "," +
            selected.transform.position.y + "," +
            selected.transform.position.z
            );
    }

	public void setMoveRadiusIndicator(bool value){
		moveRadiusIndicator.SetActive (value);
	}

	private void sizeMoveRadiusIndicator(GameObject selected){
		Movement movScript = selected.GetComponent<Movement>();
		int distance = movScript.getMoveRange() * 2;
		moveRadiusIndicator.transform.localScale = new Vector3(distance, distance, distance);
	}

	// RANGED ATTACK
    public void showRangedAttackRadius(GameObject selected){
        rangedAttackRadiusIndicator.SetActive(true);
        sizeRangedAttackRadiusIndicator(selected);
        rangedAttackRadiusIndicator.transform.position = selected.transform.position;
    }

    public void setRangedAttackRadiusIndicator(bool value){
        rangedAttackRadiusIndicator.SetActive (value);
    }

    private void sizeRangedAttackRadiusIndicator(GameObject selected){
        RangedAttack rangeScript = selected.GetComponent<RangedAttack>();
        int distance = rangeScript.getRangeDistance() * 2;
        rangedAttackRadiusIndicator.transform.localScale = new Vector3(distance, distance, distance);
    }

	// MELEE ATTACK
	public void showMeleeAttackRadius(GameObject selected){
		sizeMeleeAttackRadiusIndicator(selected);
		meleeAttackRadiusIndicator.transform.position = selected.transform.position;
		meleeAttackRadiusIndicator.SetActive(true);
	}
	
	public void setMeleeAttackRadiusIndicator(bool value){
		meleeAttackRadiusIndicator.SetActive (value);
	}
	
	private void sizeMeleeAttackRadiusIndicator(GameObject selected){
		MeleeAttack meleeScript = selected.GetComponent<MeleeAttack>();
		int distance = meleeScript.getMeleeDistance() * 2;
		meleeAttackRadiusIndicator.transform.localScale = new Vector3(distance, distance, distance);
	}

	// EXPLOSIVE ATTACK
	public void showExplosiveAttackRadius(GameObject selected){
		sizeExplosiveAttackRadiusIndicator(selected);
		explosiveAttackRadiusIndicator.transform.position = selected.transform.position;
		explosiveAttackRadiusIndicator.SetActive(true);
	}
	
	public void setExplosiveAttackRadiusIndicator(bool value){
		explosiveAttackRadiusIndicator.SetActive (value);
	}
	
	private void sizeExplosiveAttackRadiusIndicator(GameObject selected){
		ExplosiveAttack explosiveScript = selected.GetComponent<ExplosiveAttack>();
		int distance = explosiveScript.getExplosionDistance() * 2;
		explosiveAttackRadiusIndicator.transform.localScale = new Vector3(distance, distance, distance);
	}

	// HEAL
	public void showHealRadius(GameObject selected){
		sizeHealRadiusIndicator(selected);
		healRadiusIndicator.transform.position = selected.transform.position;
		healRadiusIndicator.SetActive(true);
	}
	
	public void setHealRadiusIndicator(bool value){
		healRadiusIndicator.SetActive (value);
	}
	
	private void sizeHealRadiusIndicator(GameObject selected){
		Healing healScript = selected.GetComponent<Healing>();
		int distance = healScript.getHealDistance() * 2;
		healRadiusIndicator.transform.localScale = new Vector3(distance, distance, distance);
	}
}
