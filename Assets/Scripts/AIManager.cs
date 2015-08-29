using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIManager : MonoBehaviour {

	class PossibleMove 
	{
		public Vector2i location;
		public int value;

		public PossibleMove (Vector2i iLocation, int iValue)
		{
			location = iLocation;
			value = iValue;
		}

	}

	TicTacToeLogicManager logicManager;
	Grid gridManager;
	List<PossibleMove> possibleMoves = new List<PossibleMove>();


	// Use this for initialization
	void Start () {
		gridManager = FindObjectOfType<Grid> ();
		logicManager = FindObjectOfType<TicTacToeLogicManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void aiTurn()
	{
		possibleMoves.Clear ();
		for(int y = 0; y < 3; y++)
		{
			for(int x = 0; x < 3; x++)
			{
				if(logicManager.virtualGrid[x,y] == 0)
					calculatePossibleMove(x,y);
			}
		}

		possibleMoves.Sort ((PossibleMove move1, PossibleMove move2) => move1.value.CompareTo (move2.value));

		logicManager.click (possibleMoves[possibleMoves.Count - 1].location);
		print ("AI has chosen " + possibleMoves [possibleMoves.Count - 1].location.x + ", " + possibleMoves [possibleMoves.Count - 1].location.y + " Value: " + possibleMoves [possibleMoves.Count - 1].value );
	}

	void pickRandom()
	{
		int x;
		int y;
		while (true) 
		{
			x = (int)Random.Range (0, 3);
			y = (int)Random.Range (0, 3);
			if(!gridManager.grid[x,y].occupied)
				break;
		}
		logicManager.click (new Vector2i (x, y));
	}

	void calculatePossibleMove(int x, int y)
	{
		char neighborTile1; //tile behind selected tile
		char neighborTile2; //tile ahead of selected tile
		int value = 0;

		//DEFENSIVE CALCULATIONS
		//check horizontal
		if(x == 0)
			neighborTile1 = logicManager.virtualGrid[2,y];
		else 
			neighborTile1 = logicManager.virtualGrid[x-1,y];
		if (x == 2)
			neighborTile2 = logicManager.virtualGrid [0, y];
		else
			neighborTile2 = logicManager.virtualGrid [x + 1, y];


		if(neighborTile1 == 'X')
			value += 2;
		if (neighborTile2 == 'X')
			value += 2;
		if(neighborTile1 == 'O' && neighborTile2 != 'X')
			value += 1;
		if(neighborTile2 == 'O' && neighborTile1 != 'X')
			value += 1;
		if (neighborTile1 == 'X' && neighborTile2 == 'X')
			value += 8;

		if (neighborTile1 == 'O' && neighborTile2 == 'O')
			value += 100;

		//check vertical
		if(y == 0)
			neighborTile1 = logicManager.virtualGrid[x, 2];
		else 
			neighborTile1 = logicManager.virtualGrid[x, y - 1];
		if (y == 2)
			neighborTile2 = logicManager.virtualGrid [x, 0];
		else
			neighborTile2 = logicManager.virtualGrid [x, y + 1];
		
		
		if(neighborTile1 == 'X')
			value += 2;
		if (neighborTile2 == 'X')
			value += 2;
		if(neighborTile1 == 'O' && neighborTile2 != 'X')
			value += 1;
		if(neighborTile2 == 'O' && neighborTile1 != 'X')
			value += 1;
		if (neighborTile1 == 'X' && neighborTile2 == 'X')
			value += 8;
		if (neighborTile1 == 'O' && neighborTile2 == 'O')
			value += 100;

		//check diagnals
		if ((x == 0 && y == 0) || (x == 0 && y == 2) || (x == 2 && y == 0) || (x == 2 && y == 2) || (x == 1 && y == 1))
		{
			value += 0; // corner/center pieces get a small bonus

			if(x == 0 && y == 0)
			{
				neighborTile1 = logicManager.virtualGrid[2,2];
				neighborTile2 = logicManager.virtualGrid[1,1];
			}
			if (x == 0 && y == 2)
			{
				neighborTile1 = logicManager.virtualGrid[1,1];
				neighborTile2 = logicManager.virtualGrid[2,0];
			}
			if (x == 2 && y == 0)
			{
				neighborTile1 = logicManager.virtualGrid[1,1];
				neighborTile2 = logicManager.virtualGrid[0,2];
			}
			if (x == 2 && y == 2)
			{
				neighborTile1 = logicManager.virtualGrid[1,1];
				neighborTile2 = logicManager.virtualGrid[0,0];
			}
			if (x == 1 && y == 1)
			{
				value += 1;
				neighborTile1 = logicManager.virtualGrid[0,0];
				neighborTile2 = logicManager.virtualGrid[2,2];
				if(neighborTile1 == 'X')
					value += 2;
				if (neighborTile2 == 'X')
					value += 2;
				if(neighborTile1 == 'O' && neighborTile2 != 'X')
					value += 1;
				if(neighborTile2 == 'O' && neighborTile1 != 'X')
					value += 1;
				if (neighborTile1 == 'X' && neighborTile2 == 'X')
					value += 8;
				if (neighborTile1 == 'O' && neighborTile2 == 'O')
					value += 100;
				neighborTile1 = logicManager.virtualGrid[0,2];
				neighborTile2 = logicManager.virtualGrid[2,0];
			}

			if(neighborTile1 == 'X')
				value += 2;
			if (neighborTile2 == 'X')
				value += 2;
			if(neighborTile1 == 'O' && neighborTile2 != 'X')
				value += 1;
			if(neighborTile2 == 'O' && neighborTile1 != 'X')
				value += 1;
			if (neighborTile1 == 'X' && neighborTile2 == 'X')
				value += 8;
			if (neighborTile1 == 'O' && neighborTile2 == 'O')
				value += 100;
		}

		possibleMoves.Add (new PossibleMove (new Vector2i (x, y), value));
	}

}
