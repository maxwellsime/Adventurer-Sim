using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public int width = 15;
    public int height = 12;
    public Node[,] board;
    System.Random random;
    [Header("UI")]
    public Sprite[] pieces;
    public RectTransform gameBoard;
    [Header("Prefabs")]
    public GameObject piece;

    // Called at first frame of update.
    private void Start(){
        StartGame();
    }

    private void StartGame(){
        string seed = GetRandomSeed();
        random = new System.Random(seed.GetHashCode());
        
        InitializeBoard();
        VerifyBoard();
        InstantiateBoard();
    }

    // Initialize combat board.
    private void InitializeBoard(){
        board = new Node[width, height];

        for (int y = 0; y < height; y++){
            for (int x = 0; x < width; x++){
                board[x, y] = new Node(RandomVal(), new Vector2Int(x, y));
            }
        }
    }

    // Verify the board does not start with existing matches.
    private void VerifyBoard(){
        List<int> used;

        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                used = new List<int>();
                Vector2Int v = new Vector2Int(x,y);
                int val = GetValueFromVector(v);
                
                while(IsConnected(v, true).Count > 0){
                    val = GetValueFromVector(v);

                    if(!used.Contains(val)){
                        used.Add(val);
                    }
                    
                    SetValueAtVector(v, NewVal(ref used));
                }
            }
        }
    }
    
    // Instantiate the board.
    private void InstantiateBoard(){
        for(int x = 0; x < width; x++){
            for(int y = 0; y < height; y++){
                int val = board[x, y].Val;

                GameObject b = Instantiate(piece, gameBoard);
                Piece v = b.GetComponent<Piece>();

                // Make 0,0 start at the top-left
                RectTransform rect = v.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(40 + (80 * x), -40 - (80 * y));
                v.Init(val, new Vector2Int(x,y), pieces[val]);
            }
        }
    }

    // Generate random value 0 -> pieces.Length.
    private int RandomVal(){
        int val = 0;
        val = (random.Next(0, 100)/ (100/pieces.Length));
        return val;
    }

    /* Checks if the board has any connected matches, if so gets rid of them.
        Vector2Int v = Node position being checked for matches.
        bool main = Used to run the code twice, guaranteeing a clean board. */
    private List<Vector2Int> IsConnected(Vector2Int v, bool main){
        List<Vector2Int> connected = new List<Vector2Int>();
        // List of direction functions, ordered to make looping easier.
        Vector2Int[] directions = { 
            Vector2Int.up, 
            Vector2Int.right, 
            Vector2Int.down, 
            Vector2Int.left 
        }; 
        int val = GetValueFromVector(v);

        // Check for nodes matching val from any direction from v.
        foreach(Vector2Int dir in directions){
            List<Vector2Int> line = new List<Vector2Int>();
            int count = 0;
    
            for(int i = 1; i < 3; i++){
                Vector2Int check = v + (dir * i);
                if(GetValueFromVector(check) == val){
                    line.Add(check);
                    count++;
                }
            }

            // Count = 2 or more, there exists a current match.
            if(count > 1){
                AddVectors(ref connected, line);
            }
        }

        // Check for nodes matching val in adjacent vectors.
        for(int i = 0; i < 2; i++){
            List<Vector2Int> line = new List<Vector2Int>();
            // Array of adjacent vectors, i=0 {up, down}, i=1 {left, right}.
            Vector2Int[] check = {   
                v + directions[i], 
                v + directions[i + 2] 
            }; 
            int count = 0;

            foreach(Vector2Int c in check){
                if(GetValueFromVector(c) == val){
                    line.Add(c);
                    count++;
                }
            }

            if(count > 1){
                AddVectors(ref connected, line);
            }
        }

        // Check for nodes matching val in 2x2
        /*for(int i = 0; i < 4; i++){
            List<Vector2Int> square = new List<Vector2Int>();
            int next = i + 1;
            int count = 0;

            if(next >= 4){
                next -= 4;
            }
            // Array of vectors that create a square.
            Vector2Int[] check = { 
                v + directions[i], 
                v + directions[next], 
                v + (directions[i] + directions[next]) 
            };

            foreach(Vector2Int c in check){
                if(GetValueFromVector(c) == val){
                    square.Add(c);
                    count++;
                }
            }
            
            // Count = 3 or more, a 2x2 of matching values exist.
            if(count > 2){
                AddVectors(ref connected, square);
            }
        }*/

        // Re-check connected for each member added to connected list.
        if(main){
            for(int i = 0; i < connected.Count; i++){
                AddVectors(ref connected, IsConnected(connected[i], false));
            }
        }

        if(connected.Count > 0){
            connected.Add(v);
        }

        return connected;
    }

    /* Add vectors from a given list to the referred connected.
        ref List<Vector2Int> connected = referenced list of currently connected vectors that need to have their values manipulated.
        List<Vector2Int> Add = list of vectors that need to be appended to connected.
    */
    private void AddVectors(ref List<Vector2Int> connected, List<Vector2Int> add){
        foreach(Vector2Int v in add){
            bool unique = true;

            for(int i = 0; i < connected.Count; i++){
                if(connected[i].Equals(v)){
                    unique = false;
                    break;
                }
            }

            // If Vector2Int v is not already inside connected then add it.
            if(unique){
                connected.Add(v);
            }
        }
    }

    // Returns node value at Vector2Int v inside board array.
    private int GetValueFromVector(Vector2Int v){
        if(v.x < 0 || v.x >= width || v.y < 0 || v.y >= height){
            return -1;
        }

        return board[v.x, v.y].Val;
    }

    // Sets node value at Vector2Int v inside board array.
    private void SetValueAtVector(Vector2Int v, int val){
        board[v.x, v.y].Val = val;
    }

    // Get position of piece on the gameboard from vector.
    public Vector2 getPosFromVector(Vector2Int v){
        return new Vector2(40 + (80 *v.x), -40 - (80 * v.y));
    }

    // Returns new node value that isn't inside List used.
    private int NewVal(ref List<int> used){
        List<int> usable = new List<int>();

        if(used.Count == pieces.Length){
            Debug.Log("usable count <= 0");
            return RandomVal();
        }
        for(int i = 0; i < pieces.Length; i++){
            usable.Add(i);
        }
        foreach(int i in used){
            usable.Remove(i);
        }

        return usable[random.Next(0, usable.Count)];
    }

    // Generate seed for improved random.
    private string GetRandomSeed(){
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRTUSVWXYZabcdefghijklmnopqrstuvwxyz1234567890!£$#";

        for (int i = 0; i < 20; i++){   
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        }

        return seed;
    }
}
