using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    int posX;
    int posY;

    private int[,] squares = new int[7, 7];
    public GameObject squareParent;

    GameObject[] squareObjs;

    // 選択されてる駒
    public GameObject selectedPiece;

    const int EMPTY = 0; // 何もない
    const int PATH = 100; // 道

    const int OusyouNum = 10; //10
    const int HisyaNum = 11; //11
    const int KakugyouNum = 12; //12
    const int KinsyouNum = 13; //13
    const int GinsyouNum = 14; //14
    const int KeimaNum = 15; //15
    const int KyousyaNum = 16; //16
    const int FuhyouNum = 17; //17

    public GameObject Ousyou; //10
    public GameObject Hisya; //11
    public GameObject Kakugyou; //12
    public GameObject Kinsyou; //13
    public GameObject Ginsyou; //14
    public GameObject Keima; //15
    public GameObject Kyousya; //16
    public GameObject Fuhyou; //17

    // ゲーム開始時に一度だけ
    private void Start()
    {
        InitializeArray();

        squareObjs = new GameObject[squareParent.transform.childCount];
        for (int i = 0; i < squareParent.transform.childCount; i++)
        {
            squareObjs[i] = squareParent.transform.GetChild(i).gameObject;
        }

        SetPiece(Ousyou, 3, 0, 0);
        SetPiece(Keima, 1, 0, 0);
        SetPiece(Kyousya, 5, 0, 0);
        SetPiece(Hisya, 2, 1, 0);
        SetPiece(Kakugyou, 4, 1, 0);
        SetPiece(Fuhyou, 2, 2, 0);
        SetPiece(Fuhyou, 3, 2, 0);
        SetPiece(Fuhyou, 4, 2, 0);
        SetPiece(Fuhyou, 1, 2, 0);
        SetPiece(Fuhyou, 5, 2, 0);
        SetPiece(Kinsyou, 4, 0, 0);
        SetPiece(Ginsyou, 2, 0, 0);
    }

    // プレイ中毎フレーム呼ばれる
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // カメラから光を飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit2d)
            {
                //x,yの値を取得
                int x = (int)hit2d.collider.gameObject.transform.position.x;
                int y = (int)hit2d.collider.gameObject.transform.position.y;

                if (squares[x, y] == EMPTY)
                {
                    OnPaths(-1, 0, 0);
                }
                else
                {
                    Debug.Log("駒を選択しました");
                    OnPaths(squares[x,y], x, y);

                    
                }
            }
        }
    }

    void OnPaths(int p, int x, int y)
    {
        foreach (GameObject square in squareObjs)
        {
            square.GetComponent<Renderer>().material.color = Color.white;
        }
        MovePiece(x, y);

        //for文を利用して配列にアクセスする
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (squares[i, j] == PATH)
                {
                    squares[i, j] = EMPTY;
                }
            }
        }

        posX = x;
        posY = y;


        // switch(p){  case ??? :  } は if (p == "???") {     }  と同じ意味だよ！
        switch (p)
        {
            case OusyouNum:
                SetPath(x - 1, y - 1);
                SetPath(x - 1, y);
                SetPath(x - 1, y + 1);
                SetPath(x, y - 1);
                SetPath(x, y + 1);
                SetPath(x + 1, y - 1);
                SetPath(x + 1, y);
                SetPath(x + 1, y + 1);
                break;

            case HisyaNum:
                if (SetPath(x - 1, y) == 1)
                    if (SetPath(x - 2, y) == 1)
                        if (SetPath(x - 3, y) == 1)
                            if (SetPath(x - 4, y) == 1)
                                if (SetPath(x - 5, y) == 1)
                                    SetPath(x - 6, y);
                if (SetPath(x + 1, y) == 1)
                    if (SetPath(x + 2, y) == 1)
                        if (SetPath(x + 3, y) == 1)
                            if (SetPath(x + 4, y) == 1)
                                if (SetPath(x + 5, y) == 1)
                                    SetPath(x + 6, y);
                if (SetPath(x, y + 1) == 1)
                    if (SetPath(x, y + 2) == 1)
                        if (SetPath(x, y + 3) == 1)
                            if (SetPath(x, y + 4) == 1)
                                if (SetPath(x, y + 5) == 1)
                                    SetPath(x, y + 6);
                if (SetPath(x, y - 1) == 1)
                    if (SetPath(x, y - 2) == 1)
                        if (SetPath(x, y - 3) == 1)
                            if (SetPath(x, y - 4) == 1)
                                if (SetPath(x, y - 5) == 1)
                                    SetPath(x, y - 6);
                break;

            case KakugyouNum:
                if (SetPath(x - 1, y - 1) == 1)
                    if (SetPath(x - 2, y - 2) == 1)
                        if (SetPath(x - 3, y - 3) == 1)
                            if (SetPath(x - 4, y - 4) == 1)
                                if (SetPath(x - 5, y - 5) == 1)
                                    SetPath(x - 6, y - 6);
                if (SetPath(x + 1, y + 1) == 1)
                    if (SetPath(x + 2, y + 2) == 1)
                        if (SetPath(x + 3, y + 3) == 1)
                            if (SetPath(x + 4, y + 4) == 1)
                                if (SetPath(x + 5, y + 5) == 1)
                                    SetPath(x + 6, y + 6);
                if (SetPath(x - 1, y + 1) == 1)
                    if (SetPath(x - 2, y + 2) == 1)
                        if (SetPath(x - 3, y + 3) == 1)
                            if (SetPath(x - 4, y + 4) == 1)
                                if (SetPath(x - 5, y + 5) == 1)
                                    SetPath(x - 6, y + 6);
                if (SetPath(x + 1, y - 1) == 1)
                    if (SetPath(x + 2, y - 2) == 1)
                        if (SetPath(x + 3, y - 3) == 1)
                            if (SetPath(x + 4, y - 4) == 1)
                                if (SetPath(x + 5, y - 5) == 1)
                                    SetPath(x + 6, y - 6);
                break;

            case KinsyouNum:
                SetPath(x - 1, y);
                SetPath(x - 1, y + 1);
                SetPath(x, y + 1);
                SetPath(x + 1, y + 1);
                SetPath(x + 1, y);
                SetPath(x, y - 1);
                break;

            case GinsyouNum:
                SetPath(x - 1, y - 1);
                SetPath(x - 1, y + 1);
                SetPath(x, y + 1);
                SetPath(x + 1, y - 1);
                SetPath(x + 1, y + 1);
                break;
            case KeimaNum:
                SetPath(x + 1, y + 2);
                SetPath(x - 1, y + 2);
                break;
            case KyousyaNum:
                if (SetPath(x, y + 1) == 1)
                    if (SetPath(x, y + 2) == 1)
                        if (SetPath(x, y + 3) == 1)
                            if (SetPath(x, y + 4) == 1)
                                if (SetPath(x, y + 5) == 1)
                                    SetPath(x, y + 6);
                break;
            case FuhyouNum:
                SetPath(x, y + 1);
                break;
            default:
                break;
        }
    }

    // Pieceを動かすためのメソッド
    void MovePiece(int x, int y)
    {
        if (squares[x,y] == PATH)
        {
            Debug.Log(posX + "," + posY + "に動いたよ");

            // 駒たちを配列に入れていく => 移動する際に再利用
            
            for (int i = 0; i < transform.childCount; i++) 
            {
                GameObject piece = transform.GetChild(i).gameObject;

                if (piece.transform.position == new Vector3(posX, posY, 0))
                {
                    // 今いた場所を空白にする
                    squares[posX, posY] = EMPTY;
                    // 移動先に駒を設置
                    SetPiece(piece, x, y,0);
                    // 今いた場所の駒を削除
                    Destroy(piece);
                }
            }
        }
    }

    //配列情報を初期化する
    private void InitializeArray()
    {
        //for文を利用して配列にアクセスする
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                //配列を空（値を０）にする
                squares[i, j] = EMPTY;
            }
        }
    }

    // Pieceを置くためのメソッド
    void SetPiece(GameObject p, int x, int y, int player)
    {
        // GameControllerを親にしつつ生成
        Transform parent = transform;
        GameObject piece = Instantiate(p, new Vector2(x, y), Quaternion.identity, parent);
        // (Clone)を付けないようにする
        piece.name = p.name;

        int pInt;

        switch (p.name)
        {
            //条件１
            case "Ousyou":
                pInt = OusyouNum;
                break;
            case "Hisya":
                pInt = HisyaNum;
                break;
            case "Kakugyou":
                pInt = KakugyouNum;
                break;
            case "Kinsyou":
                pInt = KinsyouNum;
                break;
            case "Ginsyou":
                pInt = GinsyouNum;
                break;
            case "Keima":
                pInt = KeimaNum;
                break;
            case "Kyousya":
                pInt = KyousyaNum;
                break;
            case "Fuhyou":
                pInt = FuhyouNum;
                break;
            default:
                pInt = -1;
                break;
        }

                squares[x, y] = pInt;
        Debug.Log("you set " + p.name + " at " + "[" + x + "," + y + "]");
    }

    int SetPath(int x, int y)
    {
        if (x <= 6 && x >= 0 &&
            y <= 6 && y >= 0 &&
            squares[x, y] == EMPTY)
        {
            squares[x, y] = 100;
            foreach (GameObject square in squareObjs)
            {
                if (square.transform.position == new Vector3(x, y, 0))
                {
                    square.GetComponent<Renderer>().material.color = Color.red;
                }
            }
            // 置けた場合
            return 1;
        }
        // 置けなかった場合
        return 0;
    }

    void Debug2dArray()
    {
        string print_array = "";
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
                print_array += squares[i, j].ToString() + ":";
            }
            print_array += "\n";
        }
        Debug.Log(print_array);
    }
}
