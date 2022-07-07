using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 서바이벌 올라타자 플레이어 정보를 담은 클래스
/// 플레이어 별로 맞은 갯수 등 정보를 담음
/// </summary>
public class GoUpPlayer : MonoBehaviour
{
    [Tooltip("맞은 개수")]
    public int correctNumber = 0;

    // 순위 별 올라가는 높이
    public float[] rankBonuses = new float[4];

    public TextMeshProUGUI nameText;

    /// <summary>
    /// 등수만큼 올라가기
    /// </summary>
    public void GoUp(int rank)
    {
        gameObject.transform.position += Vector3.up * rankBonuses[rank];
        correctNumber += 1;

    }

    public void GoDown()
    {
        // 내려주기
        gameObject.transform.position += Vector3.down;
    }
}
