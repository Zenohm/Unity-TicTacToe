using UnityEngine;
using System.Collections.Generic;

public class AIManager : MonoBehaviour {

	class PossibleMove {
		public Vector2i location;
		public int value;

		public PossibleMove (Vector2i location, int value) {
			this.location = location;
			this.value    = value;
		}
	}

	private TicTacToeLogicManager logicManager;
	private Grid gridManager;
	private List<PossibleMove> possibleMoves = new List<PossibleMove>();

    // Use this for initialization
	void Start () {
		gridManager = FindObjectOfType<Grid> ();
		logicManager = FindObjectOfType<TicTacToeLogicManager> ();
	}

	public void aiTurn() {
		possibleMoves.Clear ();
		for(int y = 0; y < 3; y++)
			for(int x = 0; x < 3; x++)
				if(logicManager.virtualGrid[x,y] == 0)
					calculatePossibleMove(x,y);

		possibleMoves.Sort ((PossibleMove move1, PossibleMove move2) => move1.value.CompareTo (move2.value));
		logicManager.click (possibleMoves[possibleMoves.Count - 1].location);
		print ("AI has chosen " + possibleMoves [possibleMoves.Count - 1].location.x + ", " + possibleMoves [possibleMoves.Count - 1].location.y + " Value: " + possibleMoves [possibleMoves.Count - 1].value );
	}

	private void pickRandom() {
		int x,y;

        do {
            x = (int)Random.Range(0, 3);
            y = (int)Random.Range(0, 3);
        } while (gridManager.grid[x, y].occupied);

		logicManager.click (new Vector2i (x, y));
	}

    private void calculatePossibleMove(int x, int y) {
        int value = 0;
        int center_x = x % 2;
        int center_y = y % 2;

        //DEFENSIVE CALCULATIONS
        //check horizontal
        value += getCheckValue(Mathf.Abs(x + center_x - 2), y, 1 + center_x, y);

        //check vertical
        value += getCheckValue(x, Mathf.Abs(y + center_y - 2), x, 1 + center_y);

		//check diagonals
		if ((x+y)%2 == 0) {
            // the coordinates of the tile behind selected tile and of the tile ahead of selected tile
            int[] adjTile = new int[4];

            value += 0; // corner/center pieces get a small bonus
            adjTile[0] = Mathf.Abs(x + center_x - 2); adjTile[1] = Mathf.Abs(y + center_y - 2);
            adjTile[2] = 1 + center_x;                adjTile[3] = 1 + center_y;
            //check to see if it's in the center
            if (center_x == 1) {
                value += 1 + getCheckValue(adjTile[0], adjTile[1], adjTile[2], adjTile[3]);
                adjTile[0] = 0; adjTile[1] = 2;
                adjTile[2] = 2; adjTile[3] = 0;
			}

            value += getCheckValue(adjTile[0], adjTile[1], adjTile[2], adjTile[3]);
        }
        possibleMoves.Add (new PossibleMove (new Vector2i (x, y), value));
	}

    private int getCheckValue(int x, int y, int x2, int y2) {
        int value = 0;
        char neighborTile1 = logicManager.virtualGrid[x, y];
        char neighborTile2 = logicManager.virtualGrid[x2, y2];

        if (neighborTile1 == neighborTile2 && (neighborTile1 == 'X' || neighborTile1 == 'O') ) {
            value = neighborTile1 == 'O' ? 102 : 12;
        } else if (neighborTile1 == 'X' || neighborTile2 == 'X') {
            value = 2;
        } else if (neighborTile1 == 'O' || neighborTile2 == 'O') {
            value = 1;
        }

        return value;
    }
}
