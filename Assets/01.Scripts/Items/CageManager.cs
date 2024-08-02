using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageManager : MonoBehaviour
{
    // 적 관리 리스트
    [SerializeField] List<GameObject> enemies;

    void Start()
    {
        // 리스트에 적 추가
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    void Update()
    {
        // 리스트에서 제거된 적 오브젝트 삭제
        enemies.RemoveAll(enemy => enemy == null);

        // 리스트가 비어있으면 -> 00
        if (enemies.Count == 0)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}