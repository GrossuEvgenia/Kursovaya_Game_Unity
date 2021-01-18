using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Kristal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int value;
    public Point index;

    [HideInInspector]
    public Vector2 pos;
    [HideInInspector]
    public RectTransform rect;

    bool rewal;                 //для проверки состояния тайла
    Image image;                //для получения спрайта

    public void Initial(int value, Point pt, Sprite kris)
    {
        image = GetComponent<Image>();
        rect = GetComponent<RectTransform>();

        this.value = value;
        Set_index(pt);
        image.sprite = kris;
    }

    public void Set_index(Point pt)
    {
        index = pt;
        Kiled_tile();
        UpdateName();
    }
    //сброс существующих тайлов
    public void Kiled_tile()
    {
        pos = new Vector2(32 + (64 * index.x), -32 - (64 * index.y));
    }
    //сдвиг от  якорной точки со скоростью 16с
    public void Moving_tile(Vector2 move)
    {
        rect.anchoredPosition += move * Time.deltaTime * 16f;
    }
    //получения новой якорной точки относительно совершонного сдвига
    public void Moving_tile_to(Vector2 move)
    {
        rect.anchoredPosition = Vector2.Lerp(rect.anchoredPosition, move, Time.deltaTime * 16f);
    }
    //обновление поворота тайла
    public bool Update_Tile()
    {
        if (Vector3.Distance(rect.anchoredPosition, pos) > 1)
        {
            Moving_tile_to(pos);
            rewal = true;
          
        }
        else
        {
            rect.anchoredPosition = pos;
            rewal = false;
           
        }
        return rewal;
    }

    void UpdateName()
    {
        transform.name = "Node [" + index.x + ", " + index.y + "]";
    }
//реакция на нажатие на мышку
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Dropped");
        if (rewal) return;

        MoveKristal.exemplar.Move_kristal(this);
    }
    //реакция на отпуcкание мышки
    public void OnPointerUp(PointerEventData eventData)
    {
        MoveKristal.exemplar.Fall_kristal();
    }
}
