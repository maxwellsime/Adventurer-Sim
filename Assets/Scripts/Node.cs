using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public int value;       // 0 = blue, 1 = green, 2 = purple, 3 = red, 4 = yellow
    public Vector2 index;

    public Node(int v, Vector2 i){
        value = v;
        index = i;
    }
}
