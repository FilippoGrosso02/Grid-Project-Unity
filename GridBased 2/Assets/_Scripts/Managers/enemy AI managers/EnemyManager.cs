using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float duration = 0;
    public static EnemyManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public async Task EnemyTurn()
    {
        await EnemyMove();

        EndEnemyTurn();

    }

    public void EndEnemyTurn()
    {
        GameManager.Instance.ChangeState(GameState.HeroesTurn);
    }

    public async Task EnemyMove()
    {

        Debug.Log("ran enemymove" + Time.deltaTime);

        List<BaseUnit> enemies = UnitManager.Instance.enemyUnits;

            foreach (BaseUnit enemy in enemies)
            {
           
                

                await EnemyActionManager.Instance.EnemyAction(enemy);

            }
    }

         
}


