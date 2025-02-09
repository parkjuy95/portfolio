﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class slowamagic : MonoBehaviour
{
    public float Range;
    public GameObject Target;
    public GameObject skill = null;
    private float counttime;
    public float cooltime;
    public float damage;
    public float slow;
    public static float slowmagicdamage;
    public static float slowmagicslow;

    public GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        slowmagicdamage = damage;
        slowmagicslow = slow;
        InvokeRepeating("UpdataTarget", 0f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        attack();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    void UpdataTarget()
    {
        if (Target == null) //타겟이 널일때 == 비어있을때
        {
            GameObject[] Monsters = GameObject.FindGameObjectsWithTag("monster"); //몬스터스 배열에 태그 몬스터 값을 가진 것들을 넣는다.
            float shortestDistance = Mathf.Infinity; //가장짧은거리
            GameObject nearestMonster = null; //가장가까운몬스터
            foreach (GameObject Monster in Monsters)
            {
                float DistanceToMonsters = Vector3.Distance(transform.position, Monster.transform.position); // 지금 오브젝트거리와 몬스터간의 거리를 넣어줌

                if (DistanceToMonsters < shortestDistance)
                {
                    shortestDistance = DistanceToMonsters;  // 무한의 거리가 몬스터와의 거리로 바뀜
                    nearestMonster = Monster;
                }
            }
            if (nearestMonster != null && shortestDistance <= Range) //몬스터값이 생겼을때 그리고 가장가까이있는 몬스터가 범위보다 작을때
            {
                Target = nearestMonster;

            }
            else
            {
                Target = null;
            }

        }
    }
    void attack()
    {
        counttime += Time.deltaTime;
        if(Target != null && counttime > cooltime)
        {
            counttime = 0.0f;
            test = Instantiate(skill, transform.position, Quaternion.identity, transform);
        }
        if (test == true)
        {
            test.GetComponent<slowspeed>().targetPosition = (Target.transform.position);
            test.GetComponent<slowspeed>().target = (Target);
        }
        else
        {
            Destroy(test);
        }
    }
}

/*void attack()
{
    counttime += Time.deltaTime;
    if (Target != null && counttime > cooltime)
    {
        counttime = 0.0f;
        var aBullet = Instantiate(skill, transform.position, Quaternion.identity, transform);
        aBullet.GetComponent<Lamp>().targetPosition = (Target.transform.position - transform.position).normalized;
        // aBullet.transform.localScale = new Vector3(25f, 25f);
    }

}
*/