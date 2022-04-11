using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{

    // Номера спрайтов для построения
    private int numDiamond = 0;
    private int numGold = 1;
    private int numOil = 2;

    [SerializeField] GameObject[] objects; // массив спрайтов
    [SerializeField] private Camera mainCamera; // камера для перевода координат локальных в мировые
    private GameObject object_name; // номер рисуемого объекта
    private Vector2 pos;
    private Vector2 mousePosition;

 

    void Update()
    {
        // Рисуем спрайт по клику ПКМ
        if (Input.GetMouseButtonDown(1))
        {
            mousePosition = Input.mousePosition;
            pos = mainCamera.ScreenToWorldPoint(mousePosition);

            
            Instantiate(object_name,pos, Quaternion.identity);
        }
    }

    // Для кнопок: Изменение номера рисуемого объекта
    public void pickDiamondClick()
    {
       object_name = objects[numDiamond];
    }
    public void pickGoldClick()
    {
        object_name = objects[numGold];
    }
    public void pickOilClick()
    {
        object_name = objects[numOil];
    }







}
