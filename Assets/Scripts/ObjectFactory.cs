using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectFactory : MonoBehaviour {

	public GameObject Xprefab;
	public GameObject Oprefab;
	public static List<GameObject> Xlist = new List<GameObject>();
	public static List<GameObject> Olist = new List<GameObject>();
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
		GameObject currentObject = (GameObject)Instantiate (instance.Xprefab, gridPointPosition, Quaternion.identity);
		Xlist.Add (currentObject);
		return currentObject;
	}

	public static GameObject placeO(Vector2i gridPosition)
	{
		Vector2 gridPointPosition = instance.grid.grid [gridPosition.x, gridPosition.y].transform.position;
		instance.grid.grid [gridPosition.x, gridPosition.y].occupied = true;
		GameObject currentObject = (GameObject)Instantiate (instance.Oprefab, gridPointPosition, Quaternion.identity);
		Olist.Add (currentObject);
		return currentObject;
	}
}
