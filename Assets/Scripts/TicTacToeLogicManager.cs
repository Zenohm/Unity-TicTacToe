using UnityEngine;
using UnityEngine.UI;

public class TicTacToeLogicManager : MonoBehaviour {

    public bool aiEnabled;
    public bool aiMovesFirst;
    public char[,] virtualGrid = new char[3, 3];
    public enum Player {PLAYER1,PLAYER2};
	public enum GameState {PLAYING,FINISHED}
	public static Player currentPlayer;
    public static GameState currentGameState;
	public Text winnerText;

    private AIManager aiManager;
    private Canvas gameOverScreen;
    private Grid gridManager;
    private Random rand;


    // Use this for initialization
    void Start () {
		rand = new Random ();
		currentGameState = GameState.PLAYING;
		if (!aiMovesFirst)
			currentPlayer = Player.PLAYER1;
		else
			currentPlayer = Player.PLAYER2;
		gridManager = FindObjectOfType<Grid> ();
		aiManager = FindObjectOfType<AIManager> ();
		gameOverScreen = FindObjectOfType<Canvas> ();
		gameOverScreen.enabled = false;
		winnerText = GameObject.Find ("WinnerDeclaration").GetComponent<Text>();
	}

	// Update is called once per frame
	void Update () {
		if (aiEnabled && currentPlayer == Player.PLAYER2 && currentGameState == GameState.PLAYING)
			aiManager.aiTurn ();
	}

	public void click(Vector2i gridPosition)
	{

		if (gridManager.grid [gridPosition.x, gridPosition.y].occupied || currentGameState == GameState.FINISHED)
			return;

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

        playerSymbol = (currentPlayer == Player.PLAYER1) ? 'X' : 'O';

		//check Columns and Rows
		for(int x=0; x < 3; x++)
			if(virtualGrid[x, 0] == playerSymbol && virtualGrid[x, 1] == playerSymbol && virtualGrid[x, 2] == playerSymbol
            || virtualGrid[0, x] == playerSymbol && virtualGrid[1, x] == playerSymbol && virtualGrid[2, x] == playerSymbol)
                gameOver(playerSymbol);

		//check Diagonals
		if (virtualGrid [0, 0] == playerSymbol && virtualGrid [1, 1] == playerSymbol && virtualGrid [2, 2] == playerSymbol
        ||  virtualGrid [0, 2] == playerSymbol && virtualGrid [1, 1] == playerSymbol && virtualGrid [2, 0] == playerSymbol)
			gameOver (playerSymbol);

		//check Stalemate
		bool stalemate = true;

        for (int x = 0; x < 3; x++)
            for (int y = 0; y < 3; y++)
                if (virtualGrid[x, y] != 'X' && virtualGrid[x, y] != 'O') {
                    stalemate = false;
                    goto StalemateExit;
                }

        StalemateExit:
        if (stalemate)
			gameOver ('s');
	}

	void gameOver(char playerSymbol)
	{
		if (currentGameState != GameState.FINISHED)
		{
			print ("Game Over");
			switch (playerSymbol) {
			case 'X':
				print ("X Wins");
				winnerText.text = "X wins";
				currentGameState = GameState.FINISHED;
				break;
			case 'O':
				print ("O Wins");
				winnerText.text = "O wins";
				currentGameState = GameState.FINISHED;
				break;
			default:
				print ("No Winner");
				winnerText.text = "Draw";
				currentGameState = GameState.FINISHED;
				break;
			}
			gameOverScreen.enabled = true;
		}
	}


	#region GUI Functions

	public void reset()
	{
		foreach (GameObject currentObject in ObjectFactory.Xlist)
			Destroy (currentObject);
		foreach (GameObject currentObject in ObjectFactory.Olist)
			Destroy (currentObject);
		ObjectFactory.Olist.Clear ();
		ObjectFactory.Xlist.Clear ();
		gridManager.resetGrid ();
		gameOverScreen.enabled = false;
		virtualGrid = new char[3, 3];
		currentGameState = GameState.PLAYING;
		currentPlayer = aiMovesFirst ? Player.PLAYER2 : Player.PLAYER1;
	}

	public void exitApplication()
	{
		Application.Quit ();
	}

	#endregion
}
