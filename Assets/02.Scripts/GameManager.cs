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
        CreateMonsterPool();
        StartCoroutine(CreateMonster());
    }

    void CreateMonsterPool()
    {
        for(int i=0;i<maxPool;i++)
        {
            GameObject _monster = Instantiate<GameObject>(monsterPrefab);
            _monster.name = "Monster_" + i.ToString("00");
            _monster.SetActive(false);

            monsterPool.Add(_monster);
        }
    }

    public bool isGameOver = false;

    IEnumerator CreateMonster()
    {
        while(!isGameOver)
        {
            yield return new WaitForSeconds(createTime);
            Vector3 pos = new Vector3( Random.Range(-23.0f, 23.0f)
                                     , 0.0f
                                     , Random.Range(-23.0f, 23.0f));

            //Pooling 
            foreach (var _monster in monsterPool)
            {
                if (_monster.activeSelf == false)
                {
                    _monster.transform.position = pos;
                    _monster.SetActive(true);
                    break;
                }
            }
        }
    }
}
