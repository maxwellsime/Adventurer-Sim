using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    private int _val;       // -1 = enemy, 0 = blue, 1 = green, 2 = purple, 3 = red, 4 = yellow ---- Needs to be 6, water/fire/earth/wind/black/white + enemy tile (maybe make enemy tile a bool)
    public Point index;

    public Node(int v, Point i){
        _val = v;
        index = i;
    }

    public int Val{
        get { return _val; }
        set { _val = value; }
    }
}
