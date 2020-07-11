using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CalculateScript : MonoBehaviour {

	public TextMeshProUGUI tmpDisplay;
	public GameObject[] inputFields;
	
	public static List<List<int>> board;
	public static List<List<int>> givenNumbers;
	
	// Function that prints out the given 9x9 sudoku grid //////////
	////////////////////////////////////////////////////////////////
	public void printGrid(List<List<int>> grid){
		
		string inputRepresentation = "\n";
		foreach(List<int> innerL in grid){
			foreach(int num in innerL){
				inputRepresentation += num;
			}
			inputRepresentation += "\n";
		}
		Debug.Log(inputRepresentation);
	}
	////////////////////////////////////////////////////////////////
	
	// Function that checks if given number can be placed at the given location /////////
	/////////////////////////////////////////////////////////////////////////////////////
	public bool isPossible(List<List<int>> grid, int x, int y, int numberToTry){
		
		// Check row if there exists the same number ////////////
		/////////////////////////////////////////////////////////
		for(int i=0; i<9; i++){
			if(grid[i][y] == numberToTry){
				return false;
			}
		}
		/////////////////////////////////////////////////////////
		
		// Check column if there exists the same number /////////
		/////////////////////////////////////////////////////////
		for(int i=0; i<9; i++){
			if(grid[x][i] == numberToTry){
				return false;
			}
		}
		/////////////////////////////////////////////////////////
		
		// Check inner square if there exists the same number ///
		/////////////////////////////////////////////////////////
		int x0 = (x/3)*3;
		int y0 = (y/3)*3;
		for(int i=0; i<3; i++){
			for(int j=0; j<3; j++){
				if(grid[x0+i][y0+j] == numberToTry){
					return false;
				}
			}
		}
		/////////////////////////////////////////////////////////
		
		return true;
	}
	/////////////////////////////////////////////////////////////////////////////////////
	
	// Recursive function that solves given sudoku grid with backtracking method /////
	//////////////////////////////////////////////////////////////////////////////////
	public bool play(List<List<int>> grid){
		
		for(int i=0; i<9; i++){
			for(int j=0; j<9; j++){
				if(grid[i][j] == 0){
					
					for(int tryNum=1; tryNum<10; tryNum++){
						if(isPossible(grid, i, j, tryNum)){
							grid[i][j] = tryNum;
							if(play(grid)){
								return true;
							}
							else{
								grid[i][j] = 0;
							}
							
						}
					}
					return false;
				}
			}
		}
		return true;
	}
	//////////////////////////////////////////////////////////////////////////////////
	
	// Function that takes input from user and fills 2D array "numbers" with it //////////
	//////////////////////////////////////////////////////////////////////////////////////
	public List<List<int>> getGrid(){
		
		List<List<int>> numbers = new List<List<int>>();
		List<int> innerList = new List<int>();
		
		int innerIndex = 0;
		foreach(GameObject inpF in inputFields){
			
			if(inpF.GetComponent<Text>().text == ""){
				innerList.Add(0);
			}
			else{
				
				int number = 0;
				if(Int32.TryParse(inpF.GetComponent<Text>().text, out number)){
					innerList.Add(number);
				}
				else{
					innerList.Add(0);
				}
			}
			innerIndex++;
			
			if(innerIndex == 9) {
				
				numbers.Add(innerList);
				innerList = new List<int>();
				innerIndex = 0;
			}
		}
		
		if(innerList.Count > 0){
			numbers.Add(innerList);
		}
		
		return numbers;
	}
	//////////////////////////////////////////////////////////////////////////////////////

	// Function that returns matrix whose elements are 1 if index is given by user and 0 if not. /////
	//////////////////////////////////////////////////////////////////////////////////////////////////
	public List<List<int>> getGivenNumbersIndexes(){
		
		List<List<int>> indices = new List<List<int>>();
		List<int> innerList = new List<int>();
		
		for(int i=0; i<9; i++){
			for(int j=0; j<9; j++){
				innerList.Add(0);
			}
			indices.Add(innerList);
			innerList = new List<int>();
		}

		int ctr = 0;
		foreach(GameObject inpF in inputFields){
			int n;
			if( (inpF.GetComponent<Text>().text != "") && (Int32.TryParse(inpF.GetComponent<Text>().text, out n)) ){

				indices[ctr/9][ctr%9] = 1;
			}
			ctr++;
		}
		return indices;
	}
	//////////////////////////////////////////////////////////////////////////////////////////////////

	// Function that checks if given board is a legit sudoku problem //////////
	///////////////////////////////////////////////////////////////////////////
	public bool isLegit(List<List<int>> grid){

		for(int i=0; i<9; i++){
			for(int j = 0; j<9; j++){
				
				if(grid[i][j] != 0){
					
					int temp = grid[i][j];
					grid[i][j] = 0;
					
					if(!isPossible(grid, i, j, temp)){
						
						grid[i][j] = temp;
						return false;
					}
					else{
						
						grid[i][j] = temp;
					}
				}
			}
		}
		return true;
	}
	///////////////////////////////////////////////////////////////////////////
	
	public void main(){
		
		givenNumbers = getGivenNumbersIndexes();
		board = getGrid();
		if(isLegit(board)){
			play(board);
			SceneManager.LoadScene("SolutionScene");
		}
		else{
			tmpDisplay.GetComponent<TextMeshProUGUI>().text = "Invalid Input!";
		}
	}
	
	public void clearGrid(){
		SceneManager.LoadScene("SudokuScene");
	}
	
}
