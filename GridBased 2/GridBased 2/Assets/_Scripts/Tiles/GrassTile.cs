using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassTile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;

    public override void Init(int x, int y) {
        dist = -1;
        var isOffset = (x + y) % 2 == 1;
        renderer.color = isOffset ? _offsetColor : _baseColor;
    }
}
