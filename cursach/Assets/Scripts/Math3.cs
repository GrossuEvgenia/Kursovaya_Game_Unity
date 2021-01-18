using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Math3 : MonoBehaviour
{
    public BoardArray boardarray;       //для создание таблицы 14*9

    [Header("UI Elements")]
    public Sprite[] kristals;           //получение изображений кристолов
    public RectTransform gameBoard;     //для возможности изменять визуальный интерфейс
    
    public Button exitButton;           //для возвращения в меню
    [Header("Prefabs")]
    public GameObject kristal;          //для работы с кристалами
    


    public Image bacgroundDoor;         //фон для перехода на сцену
    public Button opendoorButton;      //кнопка для перехода
    int width = 9;          
    int height = 14;
    int sc = 0;                     
    int[] fills;
    Tile[,] board;                      //для работы с тайлом(плитой)
    
    List<Kristal> deletevaluel;                 //для очищения тайлов
    List<Change_tile> flipped;          //для получения списка поменненых кристалов
    List<Kristal> deadKristal;                 //для получения списка найденнных кристалов
  

    System.Random random;

    void Start()
    {
        StartGame();
        Image img = bacgroundDoor.GetComponent<Image>();
        img.gameObject.SetActive(false);
        Button btn = opendoorButton.GetComponent<Button>();
        btn.gameObject.SetActive(false);
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    //Обновление 
    void Update()
    {
        List<Kristal> endUpdate = new List<Kristal>();
        for (int i = 0; i < deletevaluel.Count; i++)
        {
            Kristal kris = deletevaluel[i];
            if (!kris.Update_Tile()) endUpdate.Add(kris);
        }
        for (int i = 0; i < endUpdate.Count; i++)
        {
            Kristal kris = endUpdate[i];
            Change_tile ch = get_Changetile(kris);
            Kristal change_kristal = null;

            int x = (int)kris.index.x;
            fills[x] = Mathf.Clamp(fills[x] - 1, 0, width);

            List<Point> connected = checkFigure(kris.index, true);
            bool wasFlipped = (ch != null);

            if (wasFlipped) //Если произошел переворот тайла, то нужно обновление
            {
                change_kristal = ch.Changing_kristal(kris);
                AddPoints(ref connected, checkFigure(change_kristal.index, true));
            }

            if (connected.Count == 0) //Если нет совпадений
            {
                if (wasFlipped) //Если произошел переворот тайла
                    Change_Tilekristal(kris.index, change_kristal.index, false); //Переворот обратно, так как нет совпадения
            }
            else //Если есть совпаднеие
            {
                foreach (Point pnt in connected) //Удалить совпавшие кристалы
                {
                  
                    Tile tile= Find_position_tile(pnt);
                    Kristal krist = tile.Get_kristal();
                    if (krist!= null)
                    {
                        krist.gameObject.SetActive(false);
                        deadKristal.Add(krist);
                    }
                    tile.Set_kristal(null);
                }

                Kristal_filling();
            }

            flipped.Remove(ch); //избавиться от переворота
            deletevaluel.Remove(kris);
        }
        
    }
    //Падение кристалов при совпадении и обновление доски
    public void Kristal_filling()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = (height - 1); y >= 0; y--) 
            {
                Point p = new Point(x, y);
                Tile tile = Find_position_tile(p);
                int value = Get_position(p);
                if (value != 0) continue;
                //Если место заполнено проверять следующую координату
                    for (int ny = (y - 1); ny >= -1; ny--)
                    {
                        Point next = new Point(x, ny);
                        int nextVal = Get_position(next);
                        if (nextVal == 0)
                            continue;
                        else if   (nextVal != -1)
                        {
                            Tile got = Find_position_tile(next);
                            Kristal kris = got.Get_kristal();

                            //Подготовка к очищению доски 
                            tile.Set_kristal(kris);
                        deletevaluel.Add(kris);

                            //Создание пустого места, для дальнейшего заполнения
                            got.Set_kristal(null);
                        }
                        else//На созданные пустые места добавить созданные кристалы
                        {
                            int newVal = fillTile();
                            Kristal kris;
                           
                            if (deadKristal.Count > 0)
                            {
                         
                            Kristal revived = deadKristal[0];
                            revived.gameObject.SetActive(true);
                            revived.rect.anchoredPosition = Get_position_on_board(new Point(x, -1));
                            kris = revived;

                            deadKristal.RemoveAt(0);

                        }
                            else
                            {
                            
                            GameObject obj = Instantiate(kristal, gameBoard);
                            Kristal k = obj.GetComponent<Kristal>();
                            RectTransform rect = obj.GetComponent<RectTransform>();
                            rect.anchoredPosition = Get_position_on_board(new Point(x, -1));
                            kris = k;
                        }

                            kris.Initial(newVal, p, kristals[newVal - 1]);
                           

                            Tile hole = Find_position_tile(p);
                            hole.Set_kristal(kris);
                            Delete_tile(kris);
                            fills[x]++;
                        }
                        break;
                    }
                
            }
        }
    }
    //Получение поворота, обработка возможности изменения мест кристалов
    Change_tile get_Changetile(Kristal k)
    {
        Change_tile change = null;
        for (int i = 0; i < flipped.Count; i++)
        {
            if (flipped[i].Changing_kristal(k) != null)
            {
                change = flipped[i];
                sc++;
                break;
            }
        }
        if (sc == 10)
        {
            Image img = bacgroundDoor.GetComponent<Image>();
            img.gameObject.SetActive(true);
            Button btn = opendoorButton.GetComponent<Button>();
            btn.gameObject.SetActive(true);
            btn.onClick.AddListener(returntoIF);
            
        }
        return change;
    }
    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    void returntoIF()
    {
        SceneManager.LoadScene("Final");
    }
    
    void StartGame()
    {
        fills = new int[width];
        string seed = forRandom();
        random = new System.Random(seed.GetHashCode());
        deletevaluel = new List<Kristal>();
        flipped = new List<Change_tile>();
        deadKristal= new List<Kristal>();
        //killed = new List<KilledPiece>();

        Inite();
        Definely();
        Create();
    }
    //Обозначение тайнов на доске
    void Inite()
    {
        board = new Tile[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                board[x, y] = new Tile((boardarray.rows[y].row[x]) ? -1 : fillTile(), new Point(x, y));
            }
        }
    }
    //Определение координат на доске
    void Definely()
    {
        List<int> remove;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Point p = new Point(x, y);
                int val = Get_position(p);
                if (val <= 0) continue;

                remove = new List<int>();
                while (checkFigure(p, true).Count > 0)
                {
                    val = Get_position(p);
                    if (!remove.Contains(val))
                        remove.Add(val);
                    Set_position(p, new_value_for_board(ref remove));
                }
            }
        }
    }
    //Заполнение доски
    void Create()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Tile tile = Find_position_tile(new Point(x, y));

                int val = tile.value;
                if (val <= 0) continue;
                GameObject p = Instantiate(kristal, gameBoard);
                Kristal kris = p.GetComponent<Kristal>();
                RectTransform rect = p.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(32 + (64 * x), -32 - (64 * y));
                kris.Initial(val, new Point(x, y), kristals[val - 1]);
                tile.Set_kristal(kris);
            }
        }
    }
    //Очистка кристалов
    public void Delete_tile(Kristal kris)
    {
        kris.Kiled_tile();
        deletevaluel.Add(kris);
    }
    //Поменять местами два кристала
    public void Change_Tilekristal(Point kris_1, Point kris_2, bool main)
    {
        if (Get_position(kris_1) < 0) return;

        Tile tile_1 = Find_position_tile(kris_1);
        Kristal kristal_1 = tile_1.Get_kristal();
        if (Get_position(kris_2) > 0)
        {
            Tile tile_2 = Find_position_tile(kris_2);
            Kristal kristal_2 = tile_2.Get_kristal();
            tile_1.Set_kristal(kristal_2);
            tile_2.Set_kristal(kristal_1);

            if (main)
                flipped.Add(new Change_tile(kristal_1, kristal_2));

            deletevaluel.Add(kristal_1);
            deletevaluel.Add(kristal_2);
        }
        else
            Delete_tile(kristal_1);
    }

  //Проверка фигур
    List<Point> checkFigure(Point p, bool main)
    {
        List<Point> connected = new List<Point>();
        int val = Get_position(p);
        Point[] directions =
        {
            Point.up,
            Point.right,
            Point.down,
            Point.left
        };

        foreach (Point dir in directions) //Проверка на наличие двух или более одинаковых кристалов
        {
            List<Point> line = new List<Point>();

            int same = 0;
            for (int i = 1; i < 3; i++)
            {
                Point check = Point.add(p, Point.increase(dir, i));
                if (Get_position(check) == val)
                {
                    line.Add(check);
                    same++;
                }
            }

            if (same > 1) //Если более одного совпадения
                AddPoints(ref connected, line); //Добавдяем эти точки в найденные
        }

        for (int i = 0; i < 2; i++) //Проверка есть ли связанные с найденными фигуры
        {
            List<Point> line = new List<Point>();

            int same = 0;
            Point[] check = { Point.add(p, directions[i]), Point.add(p, directions[i + 2]) };
            foreach (Point next in check) //Проверка обеих сторон фигуры и добавление в сптсок
            {
                if (Get_position(next) == val)
                {
                    line.Add(next);
                    same++;
                }
            }

            if (same > 1)
                AddPoints(ref connected, line);
        }

        for (int i = 0; i < 4; i++) //Check for a 2x2
        {
            List<Point> square = new List<Point>();

            int same = 0;
            int next = i + 1;
            if (next >= 4)
                next -= 4;

            Point[] check = { Point.add(p, directions[i]), Point.add(p, directions[next]), Point.add(p, Point.add(directions[i], directions[next])) };
            foreach (Point pnt in check) //Проверка всех сторон связанных фигур и добавление в список
            {
                if (Get_position(pnt) == val)
                {
                    square.Add(pnt);
                    same++;
                }
            }

            if (same > 2)
                AddPoints(ref connected, square);
        }

        if (main) //Checks for other matches along the current match
        {
            for (int i = 0; i < connected.Count; i++)
                AddPoints(ref connected, checkFigure(connected[i], false));
        }

     

        return connected;
    }
//Добавление новых коорднат
    void AddPoints(ref List<Point> points, List<Point> add)
    {
        foreach (Point p in add)
        {
            bool doAdd = true;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Equals(p))
                {
                    doAdd = false;
                    break;
                }
            }

            if (doAdd) points.Add(p);
        }
    }
    //получение рагдомного значения для заполнения доски кристалами
    int fillTile()
    {
        int val = 1;
        val = (random.Next(0, 100) / (100 / kristals.Length)) + 1;
        return val;
    }

    //Получение позицции кристала
    int Get_position(Point p)
    {
        if (p.x < 0 || p.x >= width || p.y < 0 || p.y >= height) return -1;
        return board[p.x, p.y].value;
    }

    //задание позиции кристала
    void Set_position(Point p, int v)
    {
        board[p.x, p.y].value = v;
    }
    //Получение координат
    Tile Find_position_tile(Point p)
    {
        return board[p.x, p.y];
    }
  //Получение новых значений для заполнения доски
    int new_value_for_board(ref List<int> remove)
    {
        List<int> available = new List<int>();
        for (int i = 0; i < kristals.Length; i++)
            available.Add(i + 1);
        foreach (int i in remove)
            available.Remove(i);

        if (available.Count <= 0) return 0;
        return available[random.Next(0, available.Count)];
    }
    //Получение рандомного значения для того, чтобы тайлы не имели одинаковые кристалы
    string forRandom()
    {
        string seed = "";
        string acceptableChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdeghijklmnopqrstuvwxyz1234567890!@#$%^&*()";
        for (int i = 0; i < 20; i++)
            seed += acceptableChars[Random.Range(0, acceptableChars.Length)];
        return seed;
    }
    //Возвращение движения кристала в отпреедленнном направлении
    public Vector2 Get_position_on_board(Point p)
    {
        return new Vector2(32 + (64 * p.x), -32 - (64 * p.y));
    }
}

[System.Serializable]
//Класс для работы с отдельными тайлами и их содеримым
public class Tile
{
    public int value; //0 = пустое место, 1 -1kristal, 2 -2kristal, 3 -3kristal, 4 -4kristal, 5 -5kristal, -1 = пустое место
    public Point index;
    Kristal kristalik;

    public Tile(int v, Point i)
    {
        value = v;
        index = i;
    }

    public void Set_kristal(Kristal k)
    {
        kristalik = k;
        value = (kristalik == null) ? 0 : kristalik.value;
        if (kristalik == null) return;
        kristalik.Set_index(index);
    }

    public Kristal Get_kristal()
    {
        return kristalik;
    }
}

[System.Serializable]
//Класс для обмена содержимым двух тайлов, то есть для смены кристалов
public class Change_tile
{
    public Kristal kris1;
    public Kristal kris2;

    public Change_tile(Kristal kris1, Kristal kris2)
    {
        this.kris1 = kris1; 
        this.kris2 = kris2;
    }

    public Kristal Changing_kristal(Kristal k)
    {
        if (k == kris1)
            return kris2;
        else if (k == kris1)
            return kris2;
        else
            return null;
    }
}