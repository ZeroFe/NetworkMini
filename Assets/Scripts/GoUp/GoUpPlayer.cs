using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// �����̹� �ö�Ÿ�� �÷��̾� ������ ���� Ŭ����
/// �÷��̾� ���� ���� ���� �� ������ ����
/// </summary>
public class GoUpPlayer : MonoBehaviour
{
    [Tooltip("���� ����")]
    public int correctNumber = 0;

    // ���� �� �ö󰡴� ����
    public float[] rankBonuses = new float[4];

    public TextMeshProUGUI nameText;

    /// <summary>
    /// �����ŭ �ö󰡱�
    /// </summary>
    public void GoUp(int rank)
    {
        gameObject.transform.position += Vector3.up * rankBonuses[rank];
        correctNumber += 1;

    }

    public void GoDown()
    {
        // �����ֱ�
        gameObject.transform.position += Vector3.down;
    }
}
