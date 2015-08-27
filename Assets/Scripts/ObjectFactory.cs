using UnityEngine;
using System.Collections;

public class ObjectFactory : MonoBehaviour {

	public GameObject Xprefab;
	public GameObject Oprefab;
	Grid grid;

	public static ObjectFactory instance;

	// Use this for initialization
	void Start () {
		grid = FindObjectOfType<Grid> ();
		instance = this;
	}

	public static GameObject placeX(Vector2i gridPosition)
	{
		Vector2 gridPointPosition = instance.grid.grid [gridPosition.x, gridPosition.y].transform.position;
		instance.grid.grid [gridPosition.x, gridPosition.y].occupied = true;
		return (GameObject)Instantiate (instance.Xprefab, gridPointPosition, Quaternion.identity);
	}

	public static GameObject placeO(Vector2i gridPosition)
	{
		Vector2 gridPointPosition = instance.grid.grid [gridPosition.x, gridPosition.y].transform.position;
		instance.grid.grid [gridPosition.x, gridPosition.y].occupied = true;
		return (GameObject)Instantiate (instance.Oprefab, gridPointPosition, Quaternion.identity);
	}
}
