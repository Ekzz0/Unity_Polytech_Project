using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapHolder : MonoBehaviour
{
    public Vector2Int Size; // Размер ТайлМапа (ограничение, за которым не можем строить)

    private Tilemap map; // сслыка на тайлмап
    private GridCell[,] grid; // таблица, которую будем заполнять в соотв-и с 
    // занятостью тайлмапа

    private void Awake()
    {
        map = GetComponentInChildren<Tilemap>();

        grid = new GridCell[Size.x, Size.y];
        map.size = new Vector3Int(Size.x, Size.y, 0); // размер map как размер таблицы


        Vector3 tilePosition;
        Vector3Int coordinate = new Vector3Int(0, 0, 0);

        for (int x = 0; x < map.size.x; x++)
        {
            for (int y = 0; y < map.size.y; y++)
            {
                coordinate.x = x; // определяем координату в grid
                coordinate.y = y;
                tilePosition = map.CellToWorld(coordinate);
                grid[x, y] = new GridCell(tilePosition.x, tilePosition.y, false);
                // в grid заносим мировые координаты
                // тут создани таблицу и заполлнили ее пустыми клеточками

            }
        }
    }
    
    // Для того, чтобы в дальшейшем менять занятость клеток
    public void SetGridPlaceStatus(GridPlace place, bool isOccupied)
    {
        // place - место, которое планируется занять
        // isOccupied - статус, который нужно присвоить
        // true - занято, false - свободно
        foreach (var cell in place.Place) // пробегаемся по всей таблице и передаем статус
        {
            grid[cell.x, cell.y].IsOccupied = isOccupied;
        }
    }

    public Vector2Int GetGridPosHere(Vector3 mousePos)
    {
        // Для определения по позиции мыши каким координатам соответствует эта позиция на тайлмапе
        Vector3Int cellindex = map.WorldToCell(mousePos);
        return new Vector2Int(cellindex.x, cellindex.y); // показывает какая клетка(клетки целочисленные) находится по данным координатам
    }

    public Vector2 GetGridCellPosition(Vector2Int indecies)
    {
        // indecies - индексы в матрице grid 
        // По индексам определяет координаты, которые в ней лежат
        if (isAreaBounded(indecies.x, indecies.y, Vector2Int.one))
        {// проверяем находится ли эта клетка в пределах нашей таблицы
            // Если занята -> возвращаем эти координаты
            GridCell gridCell = grid[indecies.x, indecies.y];
            return new Vector2(gridCell.centerX, gridCell.centerY);
        }
        // Если нет, то просто возвращаем индексы
        return new Vector2Int(indecies.x, indecies.y);
    }

    public bool isAreaBounded(int x, int y, Vector2Int size)
    {
        // проверяем находится ли эта клетка в пределах нашей таблицы
        bool available = x >= 0 && x <= grid.GetLength(0) - size.x; // пределы длины массива size.x - размер объекта 
        if (available) 
        { // Если помещается по x -> проверяем по y
            return y >= 0 && y <= grid.GetLength(1) - size.y;
        }
        // иначе возвращаем false -> не поместился объект
        return available;
    }

    public bool IsBuildAvailable(Vector2Int gridPose, Preview preview)
    { // Проверяет если превью находится в данной таблице
        bool available = isAreaBounded(gridPose.x, gridPose.y, preview.GetSize());
        if (available && IsPlaceTaken(gridPose.x, gridPose.y, preview.GetSize()))
        {  // Если данное место занято -> false
            available = false;
        }
        // Если свободно -> не выполняем действие и возвращаем available
        return available;
    }

    private bool IsPlaceTaken(int placeX, int placeY, Vector2Int size)
    {// Проверяет занято ли данное место уже в таблице
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if(grid[placeX + x, placeY + y].IsOccupied)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private void OnDrawGizmos()
    { // Рисует сетку из кружочков
        if (grid != null)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for(int y = 0; y < grid.GetLength(1); y++)
                {
                   if (grid[x, y] != null)
                    {
                        Gizmos.color = grid[x, y].IsOccupied ? new Color(1, 0.5f, 0.5f) : new Color(0, 1f, 0.5f);
                        Gizmos.DrawSphere(new Vector3(grid[x, y].centerX, grid[x, y].centerY, 0), 0.1f);
                    }
                }
            }
        }
    }
}
