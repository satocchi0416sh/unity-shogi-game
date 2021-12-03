using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int turn = 0; // 0 1

    int posX;
    int posY;

    private int[,] squares = new int[7, 7];
    public GameObject squareParent;

    GameObject[] squareObjs;

    const int EMPTY = 0; // 何もない
    const int PATH = -2; // 道
    const int ENEMY = -3;

    // player 0
    const int OUSYOU_0 = 10; //10
    const int HISYA_0 = 11; //11
    const int KAKUGYOU_0 = 12; //12
    const int KINSYOU_0 = 13; //13
    const int GINSYOU_0 = 14; //14
    const int KEIMA_0 = 15; //15
    const int KYOUSYA_0 = 16; //16
    const int FUHYOU_0 = 17; //17
    // player 1
    const int OUSYOU_1 = 110; //10
    const int HISYA_1 = 111; //11
    const int KAKUGYOU_1 = 112; //12
    const int KINSYOU_1 = 113; //13
    const int GINSYOU_1 = 114; //14
    const int KEIMA_1 = 115; //15
    const int KYOUSYA_1 = 116; //16
    const int FUHYOU_1 = 117; //17

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

        SetPiece(Ousyou, 3, 0, 0); // obj, x, y, player 
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

        SetPiece(Ousyou, 3, 6, 1); // obj, x, y, player 
        SetPiece(Keima, 1, 6, 1);
        SetPiece(Kyousya, 5, 6, 1);
        SetPiece(Hisya, 4, 5, 1);
        SetPiece(Kakugyou, 2, 5, 1);
        SetPiece(Fuhyou, 2, 4, 1);
        SetPiece(Fuhyou, 3, 4, 1);
        SetPiece(Fuhyou, 4, 4, 1);
        SetPiece(Fuhyou, 1, 4, 1);
        SetPiece(Fuhyou, 5, 4, 1);
        SetPiece(Kinsyou, 2, 6, 1);
        SetPiece(Ginsyou, 4, 6, 1);

        Debug2dArray();
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

    // 道を光らせる場所を指定
    void OnPaths(int p, int x, int y)
    {
        // 道オブジェクトを全取得
        foreach (GameObject square in squareObjs)
        {
            // 一旦白に
            square.GetComponent<Renderer>().material.color = Color.white;
        }
        
        // 動かす
        MovePiece(x, y);

        //for文を利用して配列にアクセスする
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (squares[i, j] == PATH || squares[i,j] == ENEMY)
                {
                    squares[i, j] = EMPTY;
                }
            }
        }

        // クリック位置を保存しておく（移動する際に使用）
        posX = x;
        posY = y;


        // switch (p) {  case ??? :  } は if (p == "???") {     }  と同じ意味だよ！

        if (turn == 0)
            switch (p)
            {
                case OUSYOU_0:
                    SetPath(x - 1, y - 1);
                    SetPath(x - 1, y);
                    SetPath(x - 1, y + 1);
                    SetPath(x, y - 1);
                    SetPath(x, y + 1);
                    SetPath(x + 1, y - 1);
                    SetPath(x + 1, y);
                    SetPath(x + 1, y + 1);
                    break;

                case HISYA_0:
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

                case KAKUGYOU_0:
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

                case KINSYOU_0:
                    SetPath(x - 1, y);
                    SetPath(x - 1, y + 1);
                    SetPath(x, y + 1);
                    SetPath(x + 1, y + 1);
                    SetPath(x + 1, y);
                    SetPath(x, y - 1);
                    break;

                case GINSYOU_0:
                    SetPath(x - 1, y - 1);
                    SetPath(x - 1, y + 1);
                    SetPath(x, y + 1);
                    SetPath(x + 1, y - 1);
                    SetPath(x + 1, y + 1);
                    break;

                case KEIMA_0:
                    SetPath(x + 1, y + 2);
                    SetPath(x - 1, y + 2);
                    break;

                case KYOUSYA_0:
                    if (SetPath(x, y + 1) == 1)
                        if (SetPath(x, y + 2) == 1)
                            if (SetPath(x, y + 3) == 1)
                                if (SetPath(x, y + 4) == 1)
                                    if (SetPath(x, y + 5) == 1)
                                        SetPath(x, y + 6);
                    break;

                case FUHYOU_0:
                    SetPath(x, y + 1);
                    break;

                default:
                    break;
            }
        else
        {
            switch (p)
            {

                case OUSYOU_1:
                    SetPath(x - 1, y - 1);
                    SetPath(x - 1, y);
                    SetPath(x - 1, y + 1);
                    SetPath(x, y - 1);
                    SetPath(x, y + 1);
                    SetPath(x + 1, y - 1);
                    SetPath(x + 1, y);
                    SetPath(x + 1, y + 1);
                    break;


                case HISYA_1:
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


                case KAKUGYOU_1:
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


                case KINSYOU_1:
                    SetPath(x - 1, y);
                    SetPath(x - 1, y - 1);
                    SetPath(x, y - 1);
                    SetPath(x + 1, y - 1);
                    SetPath(x + 1, y);
                    SetPath(x, y + 1);
                    break;


                case GINSYOU_1:
                    SetPath(x - 1, y + 1);
                    SetPath(x - 1, y - 1);
                    SetPath(x, y - 1);
                    SetPath(x + 1, y + 1);
                    SetPath(x + 1, y - 1);
                    break;


                case KEIMA_1:
                    SetPath(x + 1, y - 2);
                    SetPath(x - 1, y - 2);
                    break;


                case KYOUSYA_1:
                    if (SetPath(x, y - 1) == 1)
                        if (SetPath(x, y - 2) == 1)
                            if (SetPath(x, y - 3) == 1)
                                if (SetPath(x, y - 4) == 1)
                                    if (SetPath(x, y - 5) == 1)
                                        SetPath(x, y - 6);
                    break;


                case FUHYOU_1:
                    SetPath(x, y - 1);
                    break;

                default:
                    break;
            }
        }
    }

    // Pieceを動かすためのメソッド
    void MovePiece(int x, int y)
    {
        if (squares[x,y] == PATH)
        {
            Debug.Log(posX + "," + posY + "に動いたよ");
            
            for (int i = 0; i < transform.childCount; i++) 
            {
                GameObject piece = transform.GetChild(i).gameObject;

                if (piece.transform.position == new Vector3(posX, posY, 0))
                {
                    // 今いた場所を空白にする
                    squares[posX, posY] = EMPTY;

                    // 移動先に駒を設置
                    SetPiece(piece, x, y, turn);

                    // 今いた場所の駒を削除
                    Destroy(piece);

                    // ターンを変更
                    if (turn == 0)
                        turn = 1;
                    else
                        turn = 0;
                }
            }
        }
        if (squares[x, y] == ENEMY)
        {
            Debug.Log(posX + "," + posY + "に動いたよ");
            GameObject enemy = transform.GetChild(0).gameObject;

            for (int i = 0; i < transform.childCount; i++)
            {
                enemy = transform.GetChild(i).gameObject;
                if (enemy.transform.position == new Vector3(x,y,0))
                {
                    break;
                }
            }

            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject piece = transform.GetChild(i).gameObject;

                if (piece.transform.position == new Vector3(posX, posY, 0))
                {
                    // 今いた場所を空白にする
                    squares[posX, posY] = EMPTY;

                    // 移動先に駒を設置
                    SetPiece(piece, x, y, turn);

                    // 今いた場所の駒を削除
                    Destroy(piece);
                    Destroy(enemy);

                    // ターンを変更
                    if (turn == 0)
                        turn = 1;
                    else
                        turn = 0;
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
        GameObject piece;
        if (player == 0)
        {
            piece = Instantiate(p, new Vector2(x, y), Quaternion.identity, parent);
        }
        else
        {
            piece = Instantiate(p, new Vector2(x, y), Quaternion.identity, parent);
            piece.transform.Rotate(0, 0, 180);
        }

        // (Clone)を付けないようにする
        piece.name = p.name;

        int pInt;

        switch (p.name)
        {
            //条件１
            case "Ousyou":
                if (player == 0)
                    pInt = OUSYOU_0;
                else
                    pInt = OUSYOU_1;
                break;

            case "Hisya":
                if (player == 0)
                    pInt = HISYA_0;
                else
                    pInt = HISYA_1;
                break;

            case "Kakugyou":
                if (player == 0)
                    pInt = KAKUGYOU_0;
                else
                    pInt = KAKUGYOU_1;
                break;

            case "Kinsyou":
                if (player == 0)
                    pInt = KINSYOU_0;
                else
                    pInt = KINSYOU_1;
                break;

            case "Ginsyou":
                if (player == 0)
                    pInt = GINSYOU_0;
                else
                    pInt = GINSYOU_1;
                break;

            case "Keima":
                if (player == 0)
                    pInt = KEIMA_0;
                else
                    pInt = KEIMA_1;
                break;

            case "Kyousya":
                if (player == 0)
                    pInt = KYOUSYA_0;
                else
                    pInt = KYOUSYA_1;
                break;

            case "Fuhyou":
                if (player == 0)
                    pInt = FUHYOU_0;
                else
                    pInt = FUHYOU_1;
                break;

            default:
                pInt = -1;
                break;
        }

                squares[x, y] = pInt;
        Debug.Log("you set " + p.name + " at " + "[" + x + ", " + y + "]");
    }

    // 道を光らせる
    int SetPath(int x, int y)
    {
        // 指定された場所が盤面の中
        if (x <= 6 && x >= 0 &&
            y <= 6 && y >= 0) // かつ空白だった場合
        {
            if (squares[x, y] == EMPTY)
            {
                // 道にする
                squares[x, y] = PATH;
                // 指定したマスの色を赤に
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
            else if (turn == 0 && squares[x,y] >= 110 && squares[x,y] < 118){
                squares[x, y] = ENEMY;
                foreach (GameObject square in squareObjs)
                {
                    if (square.transform.position == new Vector3(x, y, 0))
                    {
                        square.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
                return 0;
            }
            else if (turn == 1 && squares[x, y] >= 10 && squares[x, y] < 18)
            {
                squares[x, y] = ENEMY;
                foreach (GameObject square in squareObjs)
                {
                    if (square.transform.position == new Vector3(x, y, 0))
                    {
                        square.GetComponent<Renderer>().material.color = Color.green;
                    }
                }
                return 0;
            }
        }
        // 置けなかった場合
        return 0;
    }

    // 配列デバッグ用
    void Debug2dArray()
    {
        string print_array = "";
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
                print_array += squares[i, j].ToString() + "\t";
            }
            print_array += "\n";
        }
        Debug.Log(print_array);
    }
}