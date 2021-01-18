using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void increase(int digit)
    {
        x *= digit;
        y *= digit;
    }

    public void add(Point pt)
    {
        x += pt.x;
        y += pt.y;
    }
    //создание вектора
    public Vector2 ToVector()
    {
        return new Vector2(x, y);
    }
    //проверка значения
    public bool Equals(Point pt)
    {
        if (x == pt.x && y == pt.y)
            return true;
        else return false;
    }
    //получения направления поворота по двум направлениям
    public static Point fromVector(Vector2 vec)
    {
        return new Point((int)vec.x, (int)vec.y);
    }

    public static Point fromVector(Vector3 vec)
    {
        return new Point((int)vec.x, (int)vec.y);
    }
    //расширить координату
    public static Point increase(Point pt, int digit)
    {
        return new Point(pt.x * digit, pt.y * digit);
    }

    public static Point add(Point pt1, Point pt2)
    {
        return new Point(pt1.x + pt2.x, pt1.y + pt2.y);
    }
    //запомнить координату данной точки
    public static Point dublicate(Point pt)
    {
        return new Point(pt.x, pt.y);
    }

//получения направления движения тайла
    public static Point zero
    {
        get { return new Point(0, 0); }
    }
    public static Point one
    {
        get { return new Point(1, 1); }
    }
    public static Point up
    {
        get { return new Point(0, 1); }
    }
    public static Point down
    {
        get { return new Point(0, -1); }
    }
    public static Point right
    {
        get { return new Point(1, 0); }
    }
    public static Point left
    {
        get { return new Point(-1, 0); }
    }
}
