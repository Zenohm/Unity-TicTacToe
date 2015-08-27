using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TicTacToeLogicManager : MonoBehaviour {

	enum Player {PLAYER1,PLAYER2};
	enum GameState {PLAYING,FINISHED}
	Player currentPlayer;
	GameState currentGameState;
	Grid gridManager;
	char[,] virtualGrid = new char[3,3];



	// Use this for initialization
	void Start () {
		currentGameState = GameState.PLAYING;
		currentPlayer = Player.PLAYER1;
		gridManager = FindObjectOfType<Grid> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void click(Vector2i gridPosition)
	{

		if (gridManager.grid [gridPosition.x, gridPosition.y].occupied || currentGameState == GameState.FINISHED) 
		{
			//do nothing
			return;
		}
		switch (currentPlayer) 
		{
		case Player.PLAYER1:
			ObjectFactory.placeX(gridPosition);
			virtualGrid[gridPosition.x,gridPosition.y] = 'X';
			break;
		case Player.PLAYER2:
			ObjectFactory.placeO(gridPosition);
			virtualGrid[gridPosition.x,gridPosition.y] = 'O';
			break;
		default:
			print ("player returned default in LogicManager Click");
			break;
		}
		checkVictory ();
		nextTurn ();

	}

	void nextTurn()
	{
		switch(currentPlayer)
		{
		case Player.PLAYER1:
			currentPlayer = Player.PLAYER2;
			break;
		case Player.PLAYER2:
			currentPlayer = Player.PLAYER1;
			break;
		default:
			currentPlayer = Player.PLAYER1;
			break;
		}
	}

	void checkVictory()
	{
		char playerSymbol;

		if(currentPlayer == Player.PLAYER1)
			playerSymbol = 'X';
		else
			playerSymbol = 'O';

		//check Columns and Rows
		for(int x=0; x < 3; x++)
		{
			if(virtualGrid[x,0] == playerSymbol && virtualGrid[x,1] == playerSymbol && virtualGrid[x,2] == playerSymbol)
				gameOver();
			if(virtualGrid[0,x] == playerSymbol && virtualGrid[1,x] == playerSymbol && virtualGrid[2,x] == playerSymbol)
				gameOver();

		}
		//check Diagnals
		if (virtualGrid [0, 0] == playerSymbol && virtualGrid [1, 1] == playerSymbol && virtualGrid [2, 2] == playerSymbol)
			gameOver ();
		if (virtualGrid [0, 2] == playerSymbol && virtualGrid [1, 1] == playerSymbol && virtualGrid [2, 0] == playerSymbol)
			gameOver ();
	}

	void gameOver()
	{
		print ("Game Over");
		switch(currentPlayer)
		{
		case Player.PLAYER1:
			print ("Player1 Wins");
			currentGameState = GameState.FINISHED;
			break;
		case Player.PLAYER2:
			print("Player2 Wins");
			currentGameState = GameState.FINISHED;
			break;
		default:
			print("Shit's Broke");
			break;
		}
	}
}
