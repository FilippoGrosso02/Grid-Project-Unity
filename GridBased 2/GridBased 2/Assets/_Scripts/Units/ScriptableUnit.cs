using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit",menuName = "Scriptable Unit")]
public class ScriptableUnit : ScriptableObject {
    public Faction Faction;
    public BaseUnit UnitPrefab;

    public int maxHealth = 10;
    public int speed;
    public int attackRange;
    public int damage = 3;

}

public enum Faction {
    Hero = 0,
    Enemy = 1
}