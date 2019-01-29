using UnityEngine;

public class GameControl : MonoBehaviour
{
    public int countClick = 0;
    public int size = 3;
    public int emptyX, emptyY;
    public GameObject[,] arrayCell;
    public bool WinFlag = false;

    void Start()
    {
        CreateArray();
        StartNewGame();
    }

    void Update()
    {
        if (WinFlag) return;                                            // Закончена ли игра?      
        if (!Input.GetMouseButtonDown(0)) return;                       // Клик    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    // Нажат ли объект?
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (!hit) return;

        int newPosX = 0, newPosY = 0;
        SearchClick(hit.collider.name, ref newPosX, ref newPosY);

        // Нужно ли двигать ячейку
        // Если да, двигаем циклично пустою ячейку до нажатой 
        int tempPos;
        if (newPosY == emptyY)
        {
            if (newPosX == emptyX) return;
            tempPos = emptyX;
            do
            {
                if (tempPos < newPosX)
                    tempPos++;
                else
                    tempPos--;

                SwapCell(tempPos, emptyY);
            } while (tempPos != newPosX);

            countClick++;
            WinFlag = CheckRight();
        }
        else if (newPosX == emptyX)
        {
            if (newPosY == emptyY) return;
            tempPos = emptyY;
            do
            {
                if (tempPos < newPosY)
                    tempPos++;
                else
                    tempPos--;

                SwapCell(emptyX, tempPos);
            } while (tempPos != newPosY);
            countClick++;
            WinFlag = CheckRight();
        }
    }

    // Ищет координты ячейки по имени (в данном случае нажатого элемента)
    void SearchClick(string searchName, ref int searchX, ref int searchY)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (arrayCell[i, j].name != searchName) continue;
                searchX = i;
                searchY = j;
                return;

            }
        }
    }

    // Создает массив с заданным размером и привязывает объекты (пустая ячейка находится в конце).
    void CreateArray()
    {
        arrayCell = new GameObject[size, size];
        int count = 1;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (count == size * size)
                    break;
                arrayCell[i, j] = GameObject.Find(count.ToString());
                count++;
            }
        }
        arrayCell[size - 1, size - 1] = GameObject.Find("0");
        emptyX = size - 1;
        emptyY = size - 1;
    }


    // Проверяет упорядоченность массива.
    bool CheckRight()
    {
        int count = 1;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (arrayCell[i, j].name != count.ToString())
                {
                    if (i == size - 1 && j == size - 1)
                    {
                        Debug.Log("Вы победили!");
                        return true;
                    }
                    Debug.Log("Пазл не собран =(");
                    return false;
                }
                count++;
            }
        }
        return false;
    }

    //Меняет данную ячейку с пустой
    void SwapCell(int moveX, int moveY)
    {
        Vector3 temp;
        GameObject tempObject;

        // Передвигаем ячейки
        temp = arrayCell[emptyX, emptyY].transform.position;
        arrayCell[emptyX, emptyY].transform.position = arrayCell[moveX, moveY].transform.position;
        arrayCell[moveX, moveY].transform.position = temp;

        // Меняем местами объекты во вспомогательной таблице
        tempObject = arrayCell[emptyX, emptyY];
        arrayCell[emptyX, emptyY] = arrayCell[moveX, moveY];
        arrayCell[moveX, moveY] = tempObject;
        emptyX = moveX;
        emptyY = moveY;
    }

    // Перемешивает массив
    void RandomPuzzle(int sizePuzzle)
    {
        for (int i = 0; i < sizePuzzle * 100; i++)
        {
            int K = Random.Range(0, 2);
            if (K == 0)
            {
                SwapCell(Random.Range(0, size), emptyY);
            }
            else
            {
                SwapCell(emptyX, Random.Range(0, size));
            }
        }
    }

    public void StartNewGame()
    {
        WinFlag = false;   
        countClick = 0;
        //RandomPuzzle();
        SwapCell(2, 1);
        SwapCell(2, 0);
        Debug.Log("New Game");
    }
}

