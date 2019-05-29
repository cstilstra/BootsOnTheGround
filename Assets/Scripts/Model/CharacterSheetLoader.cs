using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System;

public class CharacterSheetLoader : MonoBehaviour {

	private string filePath;
	private List<string> lineList;
	private StreamReader sReader;
	private int characterLevel;

	public string[] readCharacterSheet(string fileName, int level){
		lineList = new List<string>();
		characterLevel = level;
		filePath = Application.dataPath+"/CharacterSheets/"+fileName+".csh";
		//Debug.Log ("CharacterSheetLoader: filePath = "+filePath);
		openFile (filePath);
		string [] lineArray = lineList.ToArray ();
		return lineArray;
	}

	private void openFile(string filePath){
		sReader = new StreamReader (filePath, Encoding.Default);
		string line;
		do {//line by line
			//read the next line of the file
			line = sReader.ReadLine ();
			if (line != "" && line != null) {
				handleLine (line);						
			}
		} while(line != null);
		sReader.Close ();
	}


	private void handleLine(string lineInput){
		string lineLvlString = lineInput.Substring (3,1);
		int lineLvl = Convert.ToInt32(lineLvlString);
		if (lineLvl == characterLevel){
			string toList = lineInput.Substring (4);
			lineList.Add (toList);
		}else{
			//do nothing
		}
	}
}
