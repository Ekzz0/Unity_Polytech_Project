using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Preview : MonoBehaviour
{   // То, что передвигается по карте и с помощью цвета показывать 
    // возможность располагать на карте данный объект

    [SerializeField] private Placable Placable; //тот объект, который мы располагаем

    public Vector2Int Size; // Рамер объекта
    private Vector2Int currentGridPose; // Текущая позиция на карте

    private bool isPlacingAvailable; // Можно ли тут расположить
    private bool isMoving; //Проверка на перемещение

    protected SpriteRenderer MainRenderer; // Для изменения цвета
    private Color green;
    private Color red;

    private void Awake()
    {
        MainRenderer = GetComponentInChildren<SpriteRenderer>(); //?
        green = new Color(0, 1f, .3f, .8f);
        red = new Color(1, .2f, .2f, .8f);
    }

    private void OnMouseDrag()
    {
        //if (EventSystem.current.IsPointerOverGameObject()) { return; } // пока хз что это
        isMoving = true; // Для разрешения передвижения мышью
    }

    private void OnMouseUp()
    {
        isMoving = false;
    }
    public void SetCurrentMousePosition(Vector2 position, Vector2Int GridPose, Func<Boolean> IsBuildAvailable)
    {
        // position - позиция, куда должны переместить превью
        // GridPose - позиция в таблице
        // IsBuildAvailable - функция, которая для того, чтобы понять можно ли строить превью на этой позиции
        // Метод, которые позволяет узнать в каком месте относительно мышки находится превью
        if (isMoving)
        {
            transform.position = position;
            currentGridPose = GridPose; 
            SetBuildAvailable(IsBuildAvailable());

        }
    }

    public void SetSpawnPosition(Vector2Int GridPose)
    {
        currentGridPose = GridPose;
    }

    public Placable InstantiateHere()
    {
        if(isPlacingAvailable) // Проверяет можно ли здесь строить и строит
        {
            Vector2Int size = GetSize(); // Берем размер

            Cell[] placeInGrid = new Cell[size.x * size.y];
            int index = 0;

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    // Заполняем массив клетками из текущей позиции данной превью
                    placeInGrid[index++] = new Cell(currentGridPose.x + x, currentGridPose.y + y);
                }
            }

            Placable placable = InitPlacable(placeInGrid); // Создаем плейсебл
            Destroy(gameObject); // удаляем превью
            return placable;

        }
        return null;
    }

    private Placable InitPlacable(Cell[] placeInGrid)
    {
        // Создаем объект Placable из префаба
        Placable placable = Instantiate(Placable, transform.position, Quaternion.identity);
        //Debug.Log("Создал!!!!");
        // Создаем место, которое но занимает на карте
        placable.GridPlace = new GridPlace(placeInGrid);
        return placable;
    }

    public void SetBuildAvailable(bool available) // Закраска в зависимости от available
    {
        isPlacingAvailable = available;
        MainRenderer.material.color = available ? green : red;
    }

    public bool IsBuildAvailable()
    {
        return isPlacingAvailable;
    }

    public virtual Vector2Int GetSize()
    {
        return Size;
    }
}
