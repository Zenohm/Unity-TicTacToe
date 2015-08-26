using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public int gridWidth;
	public int gridHeight;
	public float heightSpacing;
	public float widthSpacing;
	public bool gridColorsEnabled;
	public Color baseColor;
	public Color altColor;
	public GameObject elementToReplicate;
	public GridElement[,] grid;


	// Use this for initialization
	void Start () 
	{
		grid = new GridElement[gridWidth, gridHeight];

		for (int h = 0; h < gridHeight; h++)
		{
			for (int w = 0; w < gridWidth; w++)
			{
				GameObject currentObject = (GameObject)Instantiate(elementToReplicate, new Vector2 (w * widthSpacing - (gridWidth-1) * widthSpacing / 2, h * heightSpacing - heightSpacing * (gridHeight-1) / 2), Quaternion.identity);
				grid[w,h] = currentObject.GetComponent<GridElement>();
				if(grid[w,h] != null)
				{
					grid[w,h].gridIndex = new Vector2i(w,h);
					if (gridColorsEnabled)
					{
						grid[w,h].myRenderer.color = (w + h) % 2 == 0 ? baseColor : altColor;
					}
					grid[w,h].baseColor = grid[w,h].myRenderer.color;
				}
			}
		}
	}
	

}
