using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour {
    public string TileName;
    public SpriteRenderer renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject cursorHighlight;
    public GameObject rangeHighlight;
    
    public bool _isWalkable;

    public BaseUnit OccupiedUnit;
    public bool Walkable => _isWalkable && OccupiedUnit == null;
    public bool inRange = false;


    [HideInInspector]public bool isCheck= false;
    public Tile parent = null;
    public int dist = -1;

    public virtual void Init(int x, int y)
    {
      parent = this;

}

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        MenuManager.Instance.ShowTileInfo(this);
        if (inRange == true)
        {
            
            cursorHighlight.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
        cursorHighlight.SetActive(false);
        MenuManager.Instance.ShowTileInfo(null);
        if (inRange == true)
        {
            
            cursorHighlight.SetActive(false);
        }
    }

    // movement logic - needs to be changed!
    void OnMouseDown() {
        ActionManager.Instance.MouseDown(this);
        

    }

    public void SetUnit(BaseUnit unit) {
        if (unit.OccupiedTile != null) unit.OccupiedTile.OccupiedUnit = null;
        unit.transform.position = transform.position;
        OccupiedUnit = unit;
        unit.OccupiedTile = this;
    }

    public void CleanUnit(BaseUnit unit)
    {
        unit.OccupiedTile = null;
        OccupiedUnit = null;
    }
}