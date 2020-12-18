using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int gridX;//X Position in the Node Array
    public int gridY;//Y Position in the Node Array

    public bool objectWall;//Tells the program if this node is being obstructed.
    public Vector3 pos;//The world position of the node.

    public Node parent;//For the AStar algoritm, will store what node it previously came from so it cn trace the shortest path.

    public int igCost;//The cost of moving to the next square.
    public int ihCost;//The distance to the goal from this node.

    public int FCost { get { return igCost + ihCost; } }//Quick get function to add G cost and H Cost, and since we'll never need to edit FCost, we dont need a set function.

    public Node(bool a_object, Vector3 a_vPos, int a_gridX, int a_gridY)//Constructor
    {
        objectWall = a_object;//Tells the program if this node is being obstructed.
        pos = a_vPos;//The world position of the node.
        gridX = a_gridX;//X Position in the Node Array
        gridY = a_gridY;//Y Position in the Node Array
    }

}
