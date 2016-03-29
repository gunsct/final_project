using UnityEngine;
using System.Collections;
using System;

public class Shell : IComparable {
	public enum sType { NONE, PATH, BLOCK, CASTLE, SPAWN, MINITOWER, MAINTOWER, CASTLELONG, CASTLECORNER, DOOR };
	public sType type;//셀의 타입

	#region Fields
	public float nodeTotalCost;         //Total cost so far for the node
	public float estimatedCost;         //Estimated cost from this node to the goal node
	public bool bObstacle;              //Does the node is an obstacle or not
	public Shell parent;                 //Parent of the node in the linked list
	public Vector3 position;            //Position of the node
	#endregion

	public Shell()
	{
		this.type = sType.NONE;
		this.estimatedCost = 0.0f;
		this.nodeTotalCost = 1.0f;
		this.bObstacle = false;
		this.parent = null;
	}

	/// <summary>
	//Constructor with adding position to the node creation
	/// </summary>
	public Shell(Vector3 pos)
	{
		this.type = sType.NONE;
		this.estimatedCost = 0.0f;
		this.nodeTotalCost = 1.0f;
		this.bObstacle = false;
		this.parent = null;

		this.position = pos;
	}

	/// <summary>
	//Make the node to be noted as an obstacle
	/// </summary>
	public void MarkAsObstacle()
	{
		this.bObstacle = true;
	}

	public int CompareTo(object obj)
	{
		Shell node = (Shell)obj;
		if (this.estimatedCost < node.estimatedCost)
			return -1;
		if (this.estimatedCost > node.estimatedCost)
			return 1;

		return 0;
	}
}
