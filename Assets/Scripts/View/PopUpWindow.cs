using UnityEngine;
using System.Collections;


// TODO : redo this such that it is detected as UI
public class PopUpWindow : MonoBehaviour {
    public int windowWidth = 0;
    public int windowHeight = 0;
    Rect windowRect;
    bool visible = false;
    string windowText = "";
    public GUIStyle centeredStyle;

	// Use this for initialization
	void Start () {
        windowRect = new Rect((Screen.width/2) - (windowWidth/2),(Screen.height/2) - (windowHeight/2),windowWidth,windowHeight);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI(){
        if(visible){
            windowRect = GUI.Window(0, windowRect, makeWindow, "Alert!");
        }
    }

    void makeWindow(int windowID){
        GUI.Label(new Rect(10,20,230,20), windowText, centeredStyle);
        if (GUI.Button(new Rect((windowWidth/2) - 50 ,windowHeight - 25,100,20), "Okay")){
            visible = false;
        }
    }

    public void showWindow(string text){
        windowText = text;
        visible = true;
    }

	public bool getVisible(){
		return visible;
	}
}
