using UnityEngine;
using System.Collections;

[System.Serializable]
//макет будущей доски для игры
public class BoardArray
{

    [System.Serializable]
    //создание новых строк на сетке
    public struct newRow
    {
        public bool[] row;
    }

    public Grid grid;
    public newRow[] rows = new newRow[14]; //сетка 7на7
}