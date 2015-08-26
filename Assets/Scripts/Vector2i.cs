using UnityEngine;
using System.Collections;

public class Vector2i {

	public int x;
	public int y;

	#region initialization	
	public Vector2i()
	{
		x = 0;
		y = 0;
	}

	public Vector2i(int iX, int iY)
	{
		x = iX;
		y = iY;
	}
	#endregion

	#region Math Functions
	public Vector2i add(Vector2i other) {return new Vector2i (x + other.x, y + other.y);}
	public Vector2i subtract(Vector2i other) {return new Vector2i (x - other.x, y - other.y);}
	public Vector2i multiply(Vector2i other) {return new Vector2i (x - other.x, y - other.y);}
	public Vector2i divide(Vector2i other) {return new Vector2i (x - other.x, y - other.y);}
	#endregion

	#region Operators
	public static Vector2i operator +(Vector2i left, Vector2i right) {return left.add (right);}
	public static Vector2i operator -(Vector2i left, Vector2i right) {return left.subtract(right);}
	public static Vector2i operator *(Vector2i left, Vector2i right) {return left.multiply (right);}
	public static Vector2i operator /(Vector2i left, Vector2i right) {return left.divide (right);}
	#endregion
	
}
