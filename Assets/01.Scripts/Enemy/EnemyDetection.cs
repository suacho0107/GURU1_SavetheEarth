using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // 각 방향별 레이캐스트 설정, 레이가 감지되는 경우 확인
    // path가 열였는지 여부 확인, 결과값 도출
    // 벽 감지: path 닫음, 벽 감지 x: path 열린 상태 유지

    public Transform originPointDown, endPointDown, originPointLeft,
                    endPointLeft, originPointRight, endPointRight,
                    originPointUp, endPointUp;

    public bool wallDetectedDown, wallDetectedLeft, wallDetectedRight, wallDetectUp;

    public static bool pathOpenDown, pathOpenLeft, pathOpenRight, pathOpenUp;
    private int wallLayer = 1 << 10;    // layer 10: wall

    void Update()
    {
        WallDetector();
    }

    void WallDetector()
    {
        Debug.DrawLine(originPointDown.position, endPointDown.position, Color.green);
        Debug.DrawLine(originPointLeft.position, endPointLeft.position, Color.green);
        Debug.DrawLine(originPointRight.position, endPointRight.position, Color.green);
        Debug.DrawLine(originPointUp.position, endPointUp.position, Color.green);

        wallDetectedDown = Physics2D.Linecast(originPointDown.position, endPointDown.position, wallLayer);
        wallDetectedLeft = Physics2D.Linecast(originPointLeft.position, endPointLeft.position, wallLayer);
        wallDetectedRight = Physics2D.Linecast(originPointRight.position, endPointRight.position, wallLayer);
        wallDetectUp = Physics2D.Linecast(originPointUp.position, endPointUp.position, wallLayer);

        CheckResults();
    }

    void CheckResults()
    {
        if (wallDetectedDown == true)
        {
            pathOpenDown = false;
        }
        else
        {
            pathOpenDown = true;
        }

        if (wallDetectedLeft == true)
        {
            pathOpenLeft = false;
        }
        else
        {
            pathOpenLeft = true;
        }

        if (wallDetectedRight == true)
        {
            pathOpenRight = false;
        }
        else
        {
            pathOpenRight = true;
        }

        if (wallDetectUp == true)
        {
            pathOpenUp = false;
        }
        else
        {
            pathOpenUp = true;
        }
    }
}
