using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public List<Placable> placeThings; // Список всех плейсебл, размещенных на карте

    private TileMapHolder grid; // ссылка на TileMapHolder
    private Preview placablePreview; // текущее превью, которое располагаем на карте

    private void Awake()
    { // инициализируем список
        placeThings = new List<Placable>();
    }

    private TileMapHolder GetGrid()
    {
        if (grid == null)
        {
            // Получение компонент TileMapHolder с текущего объекта
            grid = GetComponent<TileMapHolder>();
        }

        return grid;
    }

    // Вспомогательные методы

    private void OccupyCells(GridPlace place)
    {// принимает место, которое нужно занять и передает туда true
        GetGrid().SetGridPlaceStatus(place, true); // т.е статус = Занято
    }

    private void InstantiatePlacable()
    { // Проверяет placablePreview != null и что постройка разрешена
        if (placablePreview != null && placablePreview.IsBuildAvailable())
        {
            // Если выполнено -> размещаем в этом месте плейсебл, который хранится в превью
            Placable placableInstance = placablePreview.InstantiateHere();
            //Debug.Log("Создал!");

            placeThings.Add(placableInstance); // добавляем его в общий список
            OccupyCells(placableInstance.GridPlace); // занимаем соотв-е место в таблице

            //Destroy(placableInstance.gameObject); // уничтожаем превьюшку

            if (placablePreview != null) // обнуляем ссылку на превью, если она не равна нулю
            {
                placablePreview = null;
            }
        }
    }

    public void ShowPlacablePreview(Preview preview)
    {
        // Позволяет показать превью
        if (placablePreview != null)
        {// Если превью уже показывается, то удаляем это превью чтобы показать новое
            Destroy(placablePreview.gameObject);
        }

        var cameraPos = Camera.main.transform.position;
        var instPreviewPos = new Vector2(cameraPos.x, cameraPos.y); // точка респавна для превью

        // размещаем в placablePreview нужное превью
        placablePreview = Instantiate(preview, instPreviewPos, Quaternion.identity);

        // Определяем в каком месте превью появилась на таблице
        Vector2Int gridPos = GetGrid().GetGridPosHere(placablePreview.transform.position);

        // Проверяем свободно ли место
        if (GetGrid().isAreaBounded(gridPos.x, gridPos.y, Vector2Int.one))
        {
            // Если свободно, то сообщаем место превью и если оно свободно, то красим ее в соотв-й цвет
            placablePreview.SetSpawnPosition(gridPos);
            placablePreview.SetBuildAvailable(GetGrid().IsBuildAvailable(gridPos, placablePreview));

        }
        else
        {
            // красим в красный цвет
            placablePreview.SetBuildAvailable(false);
        }
    }

    private void Update()
    {
        if (placablePreview == null)
        {
            // Если превью == null -> ничего не делаем
            return;

        }

        if (Input.GetMouseButton(1))
        {// Если ПКМ -> Отмена постройки
            Destroy(placablePreview.gameObject);
            placablePreview = null;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            // Если Enter -> Создаем плейсебл из нашего превью
            InstantiatePlacable();
        }

        if (Input.GetMouseButton(0))
        {   // Если ЛКМ зажата/нажата
            
            // Берем позицию мыши
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Берем место этой позиции из таблицы
            Vector2Int gridPos = GetGrid().GetGridPosHere(mouse);


            Vector2 cellCenter; // Центр узла
            if (GetGrid().isAreaBounded(gridPos.x, gridPos.y, Vector2Int.one))
            {   // Если место входит в пределы таблицы
                // Фокусируемся на узле
                cellCenter = GetGrid().GetGridCellPosition(gridPos);
            }
            else
            {
                // Если не входит в пределы таблицы
                // То двигаем за мышкой, не фокусируясь на каком-то узле
                cellCenter = mouse;
            }
            // Далее всё для перемещение + метод, которые определяет можно ли здесь строить
            placablePreview.SetCurrentMousePosition(cellCenter, gridPos, () => GetGrid().IsBuildAvailable(gridPos, placablePreview));
        }
    }
}
