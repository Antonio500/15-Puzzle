using UnityEngine;
using System.Collections;

public class GameControl : MonoBehaviour
{
    public int size;
    public GameObject[] listCell;
    public GameObject[,] arrayCell;
    public int emptyX, emptyY;
    public int randNumber;
    public int countClick = 0;
    public bool WinFlag = false;
    int[,] array;

    void Start()
    {
        CreateArray();
        StartNewGame();
    }

    public void CellClick(string nameCell)
    {
        // Закончена ли игра?      
        if (WinFlag) return;
        // Находимм ячейку
        int newPosX = 0, newPosY = 0;
        GetPosition(int.Parse(nameCell), ref newPosX, ref newPosY);
        // Нужно ли двигать ячейку
        // Если да, двигаем циклично пустою ячейку до нажатой 
        if (newPosY == emptyY)
        {
            int tempPos = emptyX;
            while (tempPos != newPosX)
            {
                if (tempPos < newPosX)
                {
                    tempPos++;
                    GameObject.Find(arrayCell[tempPos, emptyY].name).GetComponent<Animation>().Play("Scroll Top");
                }
                else
                {
                    tempPos--;
                    GameObject.Find(arrayCell[tempPos, emptyY].name).GetComponent<Animation>().Play("Scroll Bot");
                }
                StartCoroutine(SwapCell(0.2f, tempPos, emptyY));
            }
            countClick++;
        }
        else if (newPosX == emptyX)
        {
            int tempPos = emptyY;
            while (tempPos != newPosY)
            {
                if (tempPos < newPosY)
                {
                    tempPos++;
                    GameObject.Find(arrayCell[emptyX, tempPos].name).GetComponent<Animation>().Play("Scroll Left");
                }
                else
                {
                    tempPos--;
                    GameObject.Find(arrayCell[emptyX, tempPos].name).GetComponent<Animation>().Play("Scroll Right");
                }
                StartCoroutine(SwapCell(0.2f, emptyX, tempPos));
            }
            countClick++;
        }

    }
    // Привязывает объекты в массиве
    void CreateArray()
    {
        array = new int[size, size];
        arrayCell = new GameObject[size, size];
        for (int i = 0, count = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++, count++)
            {
                array[i, j] = count + 1;
                arrayCell[i, j] = listCell[count];
            }
        }
        emptyX = size - 1;
        emptyY = size - 1;
    }

    // Проверяет упорядоченность массива.
    bool СheckСorrectness()
    {
        for (int i = 0, count = 1; i < size; i++)
        {
            for (int j = 0; j < size; j++, count++)
            {
                if (int.Parse(arrayCell[i, j].name) != count) { return false; }
            }
        }
        Debug.Log("Вы победили!");
        return true;
    }

    //Меняет данную ячейку с пустой
    IEnumerator SwapCell(float time, int moveX, int moveY)
    {
        yield return new WaitForSeconds(time);
        // Запоминаем позицию пустой ячейки
        Vector2 anchorMinTmp = arrayCell[emptyX, emptyY].GetComponent<RectTransform>().anchorMin,
                anchorMaxTmp = arrayCell[emptyX, emptyY].GetComponent<RectTransform>().anchorMax;
        // Передвигаем ее на место сдвигаемой 
        arrayCell[emptyX, emptyY].GetComponent<RectTransform>().anchorMin = arrayCell[moveX, moveY].GetComponent<RectTransform>().anchorMin;
        arrayCell[emptyX, emptyY].GetComponent<RectTransform>().anchorMax = arrayCell[moveX, moveY].GetComponent<RectTransform>().anchorMax;
        // Ставим сдвигаемeмую на место пустой
        arrayCell[moveX, moveY].GetComponent<RectTransform>().anchorMin = anchorMinTmp;
        arrayCell[moveX, moveY].GetComponent<RectTransform>().anchorMax = anchorMaxTmp;
        arrayCell[moveX, moveY].GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
        arrayCell[moveX, moveY].GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
        // Меняем местами объекты в самой таблице
        GameObject tempObject = arrayCell[emptyX, emptyY];
        arrayCell[emptyX, emptyY] = arrayCell[moveX, moveY];
        arrayCell[moveX, moveY] = tempObject;

        //Меняем местами объекты во всмомогательной таблице
        int tmp = array[emptyX, emptyY];
        array[emptyX, emptyY] = array[moveX, moveY];
        array[moveX, moveY] = tmp;

        // Запоминаем место пустой ячейки
        emptyX = moveX;
        emptyY = moveY;

        WinFlag = СheckСorrectness();
    }

    // Перемешивает массив
    void RandomPuzzle(int moveNumber)
    {
        for (int i = 0; i < moveNumber; i++)
        {
            int newPosX, newPosY;
            int K = Random.Range(0, 2);
            if (K == 0)
            {
                newPosY = emptyY;
                newPosX = Random.Range(0, size);
                int tempPos = emptyX;
                while (tempPos != newPosX)
                {
                    if (tempPos < newPosX) tempPos++;
                    else tempPos--;
                    StartCoroutine(SwapCell(0f, tempPos, emptyY));
                }
            }
            else
            {
                newPosX = emptyX;
                newPosY = Random.Range(0, size);
                int tempPos = emptyY;
                while (tempPos != newPosY)
                {
                    if (tempPos < newPosY) tempPos++;
                    else tempPos--;
                    StartCoroutine(SwapCell(0f, emptyX, tempPos));
                }
            }
        }
    }

    bool GetPosition(int cell, ref int x, ref int y)
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (array[i, j] == cell)
                {
                    x = i;
                    y = j;
                    return true;
                }
            }
        }
        return false;
    }

    public void StartNewGame()
    {
        WinFlag = false;
        countClick = 0;
        RandomPuzzle(randNumber); 
        Debug.Log("New Game " + randNumber);
    }
}

