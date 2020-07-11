using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SolutionScript : MonoBehaviour {
	
	public GameObject[] textDisplays;
	
	void Awake(){
	
		List<List<int>> sudokuBoard = CalculateScript.board;
		List<List<int>> numsGiven = CalculateScript.givenNumbers;
		
		fillSudoku(sudokuBoard, numsGiven);
	}
	
	public void fillSudoku(List<List<int>> grid, List<List<int>> givenNums){
		
		int index = 0;
		foreach(List<int> innerL in grid){
			foreach(int num in innerL){
				
				if(givenNums[index/9][index%9] == 1){
					textDisplays[index].GetComponent<Text>().color = Color.red;
				}
				textDisplays[index].GetComponent<Text>().fontSize = 50;
				textDisplays[index++].GetComponent<Text>().text = num.ToString();
			}
		}
	}
	
	public void directToSudokuScene(){
		SceneManager.LoadScene("SudokuScene");
	}
	
	public void quitGame(){
		Application.Quit();
	}
	
}
