
public class GridPlace 
{
    private Cell[] place; // Массив клеточек (класс Cell.cs)
    // Тут хранятся все занятые клеточки в таблице
    // В таблице есть индексы от 0 до inf -> они должны быть целочисленные
    // -> используем Cell

    //А там, где мы работаем напрямую с картой (сценой) -> там исп-ем
    // GridCell
    public Cell[] Place { get => place; set => place = value; }
    // Предоставление к нему доступа через свойства

    public GridPlace(Cell[] place)
    {
        this.place = place;
    }
}
