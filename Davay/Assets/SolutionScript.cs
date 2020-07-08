using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SolutionScript : MonoBehaviour {
	
	public GameObject[] textDisplays;
	
	void Awake(){
	
		List<List<int>> sudokuBoard = CalculateScript.board;
		Debug.Log(sudokuBoard[0][0]);
		
		int index = 0;
		foreach(List<int> innerL in sudokuBoard){
			foreach(int num in innerL){
				textDisplays[index++].GetComponent<Text>().text = num.ToString();
			}
		}
	}
	
	
}
