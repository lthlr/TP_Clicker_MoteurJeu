using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    public List<MonsterInfos> Monsters;
    int _currentMonster = 0;
    public Monster monster;
    public GameObject PrefabHitPoint;

    private void Start()
    {
        monster.SetMonster(Monsters[_currentMonster]);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(world, Vector2.zero);

            if (hit.collider != null)
            {
                Monster monster = hit.collider.GetComponent<Monster>();
                monster.Hit(1);

                GameObject go = GameObject.Instantiate(PrefabHitPoint, monster.canvas.transform, false);
                go.transform.localPosition = UnityEngine.Random.insideUnitCircle * 5;
                go.transform.DOLocalMoveY(150, 0.8f);
                go.GetComponent<TMP_Text>().DOFade(0, 0.8f);
                GameObject.Destroy(go, 0.8f);
                

            }

            if (monster.IsAlive() == false)
            {
                NextMonster();
            }
        }
    }

    private void NextMonster()
    {
        _currentMonster++;
        monster.SetMonster(Monsters[_currentMonster]);
    }
}
