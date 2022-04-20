using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BaseUnit : MonoBehaviour {
    public string UnitName;
    public Faction Faction;
    public GameObject highlight;
    public LineRenderer path;

    [Space(10)]
    public Tile OccupiedTile;
    
    [HideInInspector] public int currentHealth;

    [Space(10)]
    public int maxHealth;
    public int speed;
    public int attackRange;
    public int damage;

    [Space(10)]
    public bool turnDone = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }
    public void TurnGray()
    {
        highlight.SetActive(false);
        turnDone = true;
    }
    public void ResetAction()
    {
        highlight.SetActive(true);
        turnDone = false;
    }

    public async Task SetDamage(int damage)
    {
        currentHealth -= damage;
        DamageAnimation();

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (currentHealth <= 0)
        {
            RemoveUnit();
        }
    }

    public void RemoveUnit()
    {
        if (Faction == Faction.Hero)
        {
            UnitManager.Instance.playerUnits.Remove(this);
        }
        if (Faction == Faction.Enemy)
        {
            UnitManager.Instance.enemyUnits.Remove(this);
        }
        OccupiedTile.CleanUnit(this);
        this.gameObject.SetActive(false);
    }

    public async Task Attack(BaseUnit target)
    {
        AttackAnimation();
        target.SetDamage(damage);
        
    }

    public void SetStats(ScriptableUnit data)
    {
        maxHealth = data.maxHealth;
        speed = data.speed;
        attackRange = data.attackRange;
        damage = data.damage;
    }

    public async Task AttackAnimation()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x,pos.y, pos.z -0.2f);
        await Task.Delay(System.TimeSpan.FromSeconds(0.1f));
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    public async Task DamageAnimation()
    {
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, pos.z + 0.2f);
        await Task.Delay(System.TimeSpan.FromSeconds(0.1f));
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
    }

    

