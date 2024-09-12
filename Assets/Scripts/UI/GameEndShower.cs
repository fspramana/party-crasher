using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameEndShower : MonoBehaviour
{

    public Text winningText;

    public void PlayerWin(int player)
    {
        winningText.text = string.Format("PLAYER {0} WIN!", player);
    }

}
