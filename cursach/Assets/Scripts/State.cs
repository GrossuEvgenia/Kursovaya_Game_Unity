using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName="State")]
public class State : ScriptableObject
{
    [SerializeField]string text_on_game;    //получение текста на экране
    [SerializeField]State[] other_text;     //другой текст

    public string GetText()
    {
        return text_on_game;    
    }

    
    public State[] GetOtherText()
    {
        return other_text;   
    }
}
