using UnityEngine;
using System.Collections;

public class GridElement : MonoBehaviour {

	public Vector2i gridIndex;
	public SpriteRenderer myRenderer;
	public Color baseColor;
	public Color highlightColor;
	public bool occupied = false;
	TicTacToeLogicManager gameLogicManager;

	void Awake() 
	{
		myRenderer = GetComponent<SpriteRenderer>();
		gameLogicManager = FindObjectOfType<TicTacToeLogicManager> ();
		baseColor = myRenderer.color;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void OnMouseEnter ()
	{
		if(TicTacToeLogicManager.currentGameState != TicTacToeLogicManager.GameState.FINISHED && !occupied)
			myRenderer.color = highlightColor;
	}

	void OnMouseExit () 
	{
		myRenderer.color = baseColor;
	}

	void OnMouseDown()
	{
		gameLogicManager.click (gridIndex);
		myRenderer.color = baseColor;
	}
}
