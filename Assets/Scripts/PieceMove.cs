using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMove : MonoBehaviour
{
    public static PieceMove instance;
    Combat game;
    Piece target;
    Point newIndex;
    Vector2 mouseInput;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        game = GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            Vector2 dir = ((Vector2)Input.mousePosition - mouseInput);
            Vector2 nDir = dir.normalized;
            Vector2 aDir = new Vector2(Mathf.Abs(dir.x), Mathf.Abs(dir.y));

            newIndex = Point.clone(target.index);
            Point add = Point.zero;
            if(dir.magnitude > 32){
                if(aDir.x > aDir.y){
                    add = (new Point((nDir.x > 0) ? 1 : -1, 0));
                }
                else if(aDir.y > aDir.x){
                    add = (new Point(0, (nDir.y > 0) ? 1 : -1));
                }
            }

            newIndex.add(add);
        }
    }
}
