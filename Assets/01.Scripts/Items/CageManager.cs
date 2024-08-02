using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageManager : MonoBehaviour
{
    // �� ���� ����Ʈ
    [SerializeField] List<GameObject> enemies;

    void Start()
    {
        // ����Ʈ�� �� �߰�
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        // ����Ʈ���� ���ŵ� �� ������Ʈ ����
        enemies.RemoveAll(enemy => enemy == null);

        // ����Ʈ�� ��������� -> 00
        if (enemies.Count == 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}