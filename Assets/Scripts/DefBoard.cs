using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour{
    /*public int width;
    public int height;
    public GameObject tilePrefab;
    private backgroundTile[,] tileArr;
    public GameObject[] slimes;
    public GameObject[,] slimesArr;


    // Start is called before the first frame update
    void Start(){
        tileArr = new backgroundTile[width, height];
        slimesArr = new GameObject[width, height];
        SetUp();
    }

    private void SetUp(){
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                Vector2 tempPos = new Vector2(x, y);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPos, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + x + ", " + y + " )";
                int randSlime = Random.Range(0, slimes.Length);
                GameObject slime = Instantiate(slimes[randSlime], tempPos, Quaternion.identity);
                slime.transform.parent = this.transform;
                slime.name = "( " + x + ", " + y + " )";
                slimesArr[x, y] = slime;
            }   
        }
    }*/
}
