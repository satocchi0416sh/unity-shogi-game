using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text turnText;
    public GameController gameController;

    private void Update()
    {
        turnText.text = "ターン：プレイヤー" + gameController.turn;
    }
}
