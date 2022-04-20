using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    public static UnitSpawn Instance;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public void LoadStats( ScriptableUnit data, BaseUnit prefab)
    {
        prefab.maxHealth = data.maxHealth;
        prefab.speed = data.speed;
        prefab.attackRange = data.attackRange;
        prefab.damage = data.damage;

    }
}
