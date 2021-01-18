using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveKristal : MonoBehaviour
{
    public static MoveKristal exemplar; // для начала работы класса
    Math3 game;
    int sc = 0;
    Kristal moved;                      //для проверки смещения тайла
    Point new_index;
    Vector2 mouseStart;                 //для обработки направления в зависимости от нажатия мыши
//начало работы функций
    private void Awake()
    {
        exemplar = this;
    }

    void Start()
    {
        game = GetComponent<Math3>();
    }

    void Update()
    {
        int dir;
        if (moved != null)
        {
            Vector2 diraction = ((Vector2)Input.mousePosition - mouseStart);
            Vector2 normal_diraction = diraction.normalized;
            Vector2 abs_diraction = new Vector2(Mathf.Abs(diraction.x), Mathf.Abs(diraction.y));

            new_index = Point.dublicate(moved.index);
            Point add = Point.zero;
            if (diraction.magnitude > 32) //проверка растояния текущего положения мыши от начального (>32пикселей)
            {
                //выбор направления сдвига в зависимости от положения
                if (abs_diraction.x > abs_diraction.y)
                {
                   
                    if (normal_diraction.x > 0)
                    {
                        dir = 1;
                    }
                    else dir = -1;
                    add = new Point(dir, 0);
      
                }
                else if (abs_diraction.y > abs_diraction.x)
                {
                    if (normal_diraction.y > 0)
                    {
                        dir = -1;
                    }
                    else dir = 1;
                    add = new Point(0, dir);
                   
                }
            }
            new_index.add(add);
            //смещение
            Vector2 pos = game.Get_position_on_board(moved.index);
            if (!new_index.Equals(moved.index))
                pos += Point.increase(new Point(add.x, -add.y), 16).ToVector();
            moved.Moving_tile_to(pos);
        }
    }

    //получение тайла для смещения
    public void Move_kristal(Kristal k)
    {
        if (moved == null) 
        {
            moved = k;
            mouseStart = Input.mousePosition;
        }
        else return;
       
    }
    //смена двух тайлов
    public void Fall_kristal()
    {
        bool flag = false;
        if (moved != null) 
        {
           
            if (!new_index.Equals(moved.index))
                game.Change_Tilekristal(moved.index, new_index, true);
            else
                game.Delete_tile(moved);
            moved = null;

        }

        else return;

    }
  
}