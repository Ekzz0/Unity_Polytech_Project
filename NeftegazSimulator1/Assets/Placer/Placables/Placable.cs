using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placable : MonoBehaviour
{   //class Placables - объект на сцене, который можно размещать на карте
    // т.е на TileMap'e
    private GridPlace place;

    public GridPlace GridPlace { get => place; set=> place = value; }
}
