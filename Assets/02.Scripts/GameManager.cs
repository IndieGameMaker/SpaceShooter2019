using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject monsterPrefab;
    public float createTime = 3.0f;

    public List<GameObject> monsterPool = new List<GameObject>();
    public int maxPool = 10;

    void Start()
    {
        CreateMonster();
    }

    void CreateMonster()
    {
        for(int i=0;i<maxPool;i++)
        {
            GameObject _monster = Instantiate<GameObject>(monsterPrefab);
            _monster.name = "Monster_" + i.ToString("00");
            _monster.SetActive(false);

            monsterPool.Add(_monster);
        }
    }
}
