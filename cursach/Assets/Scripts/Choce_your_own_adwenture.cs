using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class Choce_your_own_adwenture : MonoBehaviour
{
    public Button nextButton;               //кнопка перехода
    public Button saveButton;               //кнопка сохранения
    public Button firstChooseButton;        //кнопка выбора
    public Button secondChooseButton;       //кнопка выбора
    public Button thirdChooseButton;        //кнопка выбора
    public Button exitButton;               //кнопка выхода
    //получение теста с кнопок
    public Text textonButton1;
    public Text textonButton2;
    public Text textonButton3;
    public Text nextButtonText;
    
    public Image labirint;             
    /// </summary>
    public TextAsset intro;
   
    public Text textOn;
    public State startText;             //начальный текс
  //  FileStream file1 ;
    //FileStream file2;
    string check;
    State state;                    //для изменения текста
    int chek=-1;
    // Start is called before the first frame update
    
    //Меню для проверки сохранения
    void menu()
    {
        StreamReader reader = new StreamReader("Assets/Resourses/Save.txt");
        check = reader.ReadToEnd();
        if (check.Length != 0)
        {
           switch(int.Parse(check))
            {
            
                case 0: Introdaction();break;
                case 1:firstAct();break;
                case 2:reactionONfirstinFirstAct();break;
                case 3: reactioninfirstfirst();break;
                case 4:reactionONsecondinFirstAct();break;
               case 5: secondAct();break;
               case 6: reactionFirstinSecondAct();break;
               case 7:reactionFirstFirstinSecondAct();break;
               case 8:reactionSecondtinSecondAct();break;
               case 9:Labirint();break;
               case 10:Labirint_mistake_reaction();break;
               case 11:Labirint_reaction();break;
               case 12:Thirdact();break;
               case 13:Rooms(chek);break;
               case 14:Rools();break;
            }
            
            }
        else next();
            
        reader.Close();
        }
    
    void Start()
    {
        
        Button sb = saveButton.GetComponent<Button>();
        state = startText;
        textOn.text = state.GetText();
        Image img = labirint.GetComponent<Image>();
        img.gameObject.SetActive(false);
        Button btn1 = firstChooseButton.GetComponent<Button>();
        btn1.gameObject.SetActive(false);
        Button btn2 = secondChooseButton.GetComponent<Button>();
        btn2.gameObject.SetActive(false);
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        btn3.gameObject.SetActive(false);
        menu();
        next();
        
    }
    void next()
    {
        Button btn = nextButton.GetComponent<Button>();
        btn.onClick.AddListener(Introdaction);
         
    }
    //-------------------------функции для перехода между уровнями-----------------------------
    void Introdaction()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (btn1.gameObject.activeSelf == true)
        {
            btn1.gameObject.SetActive(false);
        }
        if (btn2.gameObject.activeSelf == true)
        {
            btn2.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        Text tex = nextButtonText.GetComponent<Text>();
        tex.text = "Даьлше";
        int id = 0;
        State[] textNext =state.GetOtherText();
    state = textNext[1];
    textOn.text = state.GetText();
    CloseNextButton();
    firstAct();
    sb.onClick.AddListener(()=>Save(id));
    }
    void CloseNextButton()
    {

        Button btn = nextButton.GetComponent<Button>();
        btn.gameObject.SetActive(false);
    }
    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    void firstAct()
    {
        Button btn = nextButton.GetComponent<Button>();
       
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 1;
        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == true)
        {
            btn.gameObject.SetActive(false);
        }
        if (btn1.gameObject.activeSelf == false)
        {
            btn1.gameObject.SetActive(true);
        }
        if (btn2.gameObject.activeSelf == false)
        {
            btn2.gameObject.SetActive(true);
        }
        
        Text tex = textonButton1.GetComponent<Text>();

        string doing = DoIt(1);
        tex.text = doing;
        btn1.onClick.AddListener(reactionONfirstinFirstAct);
       
        Text tex1 = textonButton2.GetComponent<Text>();
        doing = DoIt(2);
        tex1.text = doing;
        btn2.onClick.AddListener(reactionONsecondinFirstAct);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void reactionONfirstinFirstAct()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 2; 
      string doing;
        State[] textNext = state.GetOtherText();
        state = textNext[2];
        textOn.text = state.GetText();
      
        Text tex = textonButton1.GetComponent<Text>();
        doing = DoIt(3);
        tex.text = doing;
        
        
        btn2.gameObject.SetActive(true);
        Text tex1 = textonButton2.GetComponent<Text>();
        doing = DoIt(4);
        tex1.text = doing;
       
        btn3.gameObject.SetActive(true);
        Text tex3 = textonButton3.GetComponent<Text>();
        doing = DoIt(5);
        tex3.text = doing;
        
        if (btn3.gameObject.activeSelf == false)
        {
            btn3.gameObject.SetActive(true);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == true)
        {
            btn.gameObject.SetActive(false);
        }
        if (btn1.gameObject.activeSelf == false)
        {
            btn1.gameObject.SetActive(true);
        }
        if (btn2.gameObject.activeSelf == false)
        {
            btn2.gameObject.SetActive(true);
        }
       

        btn1.onClick.AddListener(reactioninfirstfirst);
        btn2.onClick.AddListener(reactioninfirstfirst);
        btn3.onClick.AddListener(reactionONsecondinFirstAct);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void reactioninfirstfirst()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 3;
        State[] textNext = state.GetOtherText();
        state = textNext[3];
        textOn.text = state.GetText();
        

        Text tex = nextButtonText.GetComponent<Text>();
        tex.text = "Что бы придумать?";
     

        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        if (btn1.gameObject.activeSelf == true)
        {
            btn1.gameObject.SetActive(false);
        }
        if (btn2.gameObject.activeSelf == true)
        {
            btn2.gameObject.SetActive(false);
        }
        btn.onClick.AddListener(reactionONfirstinFirstAct);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void reactionONsecondinFirstAct()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 4;
        State[] textNext = state.GetOtherText();
        state = textNext[4];
        textOn.text = state.GetText();
       
        btn.gameObject.SetActive(true);
        Text tex = nextButtonText.GetComponent<Text>();
        tex.text = "В путь!";

        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        if (btn1.gameObject.activeSelf == true)
        {
            btn1.gameObject.SetActive(false);
        }
        if (btn2.gameObject.activeSelf == true)
        {
            btn2.gameObject.SetActive(false);
        }
        btn.onClick.AddListener(secondAct);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void secondAct()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 5;
        string doing;
        State[] textNext = state.GetOtherText();
        state = textNext[5];
        textOn.text = state.GetText();
       
        Text tex = textonButton1.GetComponent<Text>();
        doing = DoIt(6);
        tex.text = doing;
        
        
        Text tex1 = textonButton2.GetComponent<Text>();
        doing = DoIt(7);
        tex1.text = doing;
        

        

        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == true)
        {
            btn.gameObject.SetActive(false);
        }
        if (btn1.gameObject.activeSelf == false)
        {
            btn1.gameObject.SetActive(true);
        }
        if (btn2.gameObject.activeSelf == false)
        {
            btn2.gameObject.SetActive(true);
        }
        btn1.onClick.AddListener(reactionFirstinSecondAct);
        btn2.onClick.AddListener(reactionSecondtinSecondAct);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void reactionFirstinSecondAct()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id=6;
        string doing;
        State[] textNext = state.GetOtherText();
        state = textNext[6];
        textOn.text = state.GetText();
        //Button btn1 = firstChooseButton.GetComponent<Button>();
        btn1.gameObject.SetActive(true);
        Text tex = textonButton1.GetComponent<Text>();
        doing = DoIt(8);
        tex.text = doing;
       
     
        
        btn2.gameObject.SetActive(true);
        Text tex1 = textonButton2.GetComponent<Text>();
        doing = DoIt(9);
        tex1.text = doing;


        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == true)
        {
            btn.gameObject.SetActive(false);
        }
        if (btn1.gameObject.activeSelf == false)
        {
            btn1.gameObject.SetActive(true);
        }
        if (btn2.gameObject.activeSelf == false)
        {
            btn2.gameObject.SetActive(true);
        }

        btn1.onClick.AddListener(reactionFirstFirstinSecondAct);
        btn2.onClick.AddListener(reactionSecondtinSecondAct);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
 
    void reactionFirstFirstinSecondAct()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 7;
        
            State[] textNext = state.GetOtherText();
            state = textNext[7];
        
        textOn.text = state.GetText();
        
        Text tex = nextButtonText.GetComponent<Text>();
        tex.text = "В перед!";
        btn.gameObject.SetActive(true);
       

        

        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        if (btn1.gameObject.activeSelf == true)
        {
            btn1.gameObject.SetActive(false);
        }
        if (btn2.gameObject.activeSelf == true)
        {
            btn2.gameObject.SetActive(false);
        }
        sb.onClick.AddListener(() => Save(id));
        btn.onClick.AddListener(reactionSecondtinSecondAct);
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void reactionSecondtinSecondAct()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 8;
        State[] textNext = state.GetOtherText();
        state = textNext[8];
        textOn.text = state.GetText();
        //Button btn = nextButton.GetComponent<Button>();

        Text tex = nextButtonText.GetComponent<Text>();
        tex.text = "Нужно сделать выбор!";
        btn.gameObject.SetActive(true);
        

        
        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        if (btn1.gameObject.activeSelf == true)
        {
            btn1.gameObject.SetActive(false);
        }
        if (btn2.gameObject.activeSelf == true)
        {
            btn2.gameObject.SetActive(false);
        }
        btn.onClick.AddListener(Labirint);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void Labirint()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 9;
        
        if (btn.gameObject.activeSelf == true)
        {
            CloseNextButton();
        }
        string doing;
        
        img.gameObject.SetActive(true);
      
        btn1.gameObject.SetActive(true);
        Text tex = textonButton1.GetComponent<Text>();
        doing = DoIt(10);
        tex.text = doing;
        
        
        btn2.gameObject.SetActive(true);
        doing = DoIt(11);
        Text tex1 = textonButton2.GetComponent<Text>();
        tex1.text = doing;
       
        
        btn3.gameObject.SetActive(true);
        Text tex3 = textonButton3.GetComponent<Text>();
        doing = DoIt(12);
        tex3.text = doing;

        if (btn3.gameObject.activeSelf == false)
        {
            btn3.gameObject.SetActive(true);
        }
        if (img.gameObject.activeSelf == false)
        {
            img.gameObject.SetActive(true);
        }
        if (btn.gameObject.activeSelf == true)
        {
            btn.gameObject.SetActive(false);
        }
        if (btn1.gameObject.activeSelf == false)
        {
            btn1.gameObject.SetActive(true);
        }
        if (btn2.gameObject.activeSelf == false)
        {
            btn2.gameObject.SetActive(true);
        }
        btn1.onClick.AddListener(Labirint_reaction);
        btn2.onClick.AddListener(Labirint_mistake_reaction);
        btn3.onClick.AddListener(Labirint_mistake_reaction);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void Labirint_mistake_reaction()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 10;
      
        img.gameObject.SetActive(false);
        State[] textNext = state.GetOtherText();
        state = textNext[9];
        textOn.text = state.GetText();
        

        Text tex = nextButtonText.GetComponent<Text>();
        tex.text = "Вернуться";
        btn.gameObject.SetActive(true);
       

        
        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        if (btn1.gameObject.activeSelf == true)
        {
            btn1.gameObject.SetActive(false);
        }
        if (btn2.gameObject.activeSelf == true)
        {
            btn2.gameObject.SetActive(false);
        }
        btn.onClick.AddListener(Labirint);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }

    void Labirint_reaction()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 11;
      
        img.gameObject.SetActive(false);
        State[] textNext = state.GetOtherText();
        state = textNext[10];
        textOn.text = state.GetText();
      

        Text tex = nextButtonText.GetComponent<Text>();
        tex.text = "Подземелье";
        btn.gameObject.SetActive(true);
       

      

        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        if (btn1.gameObject.activeSelf == true)
        {
            btn1.gameObject.SetActive(false);
        }
        if (btn2.gameObject.activeSelf == true)
        {
            btn2.gameObject.SetActive(false);
        }
        btn.onClick.AddListener(Thirdact);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void Thirdact()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 12;
        string doing;
      
      
        int [] flag=new int[3]{1,2,3};
        State[] textNext = state.GetOtherText();
        state = textNext[11];
        textOn.text = state.GetText();
      
        btn1.gameObject.SetActive(true);
        Text tex = textonButton1.GetComponent<Text>();
        doing = DoIt(13);
        tex.text = doing;
        
      
        btn2.gameObject.SetActive(true);
        Text tex1 = textonButton2.GetComponent<Text>();
        doing = DoIt(14);
        tex1.text = doing;
        
      
        btn3.gameObject.SetActive(true);
        Text tex3 = textonButton3.GetComponent<Text>();
        doing = DoIt(15);
        tex3.text = doing;
        

        if (btn3.gameObject.activeSelf == false)
        {
            btn3.gameObject.SetActive(true);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == true)
        {
            btn.gameObject.SetActive(false);
        }
        if (btn1.gameObject.activeSelf == false)
        {
            btn1.gameObject.SetActive(true);
        }
        if (btn2.gameObject.activeSelf == false)
        {
            btn2.gameObject.SetActive(true);
        }
        btn1.onClick.AddListener(() => Rooms(flag[0]));
        btn2.onClick.AddListener(() => Rooms(flag[1]));
        btn3.onClick.AddListener(() => Rooms(flag[2]));
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }

    void Rooms(int flag)
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 13;
        string doing;
        chek=flag;
        StreamWriter w = new StreamWriter("Assets/Resourses/forFith.txt");
        if (flag == 1)
        {
            w.Write("2");
            State[] textNext = state.GetOtherText();
            state = textNext[12];
            textOn.text = state.GetText();
        }
        else if (flag == 2)
        {
            w.Write("3");
            State[] textNext = state.GetOtherText();
            state = textNext[13];
            textOn.text = state.GetText();
        }
        else
        {
            w.Write("4");
            State[] textNext = state.GetOtherText();
            state = textNext[14];
            textOn.text = state.GetText();
        }
       

            Text tex = nextButtonText.GetComponent<Text>();
            doing = DoIt(16);
            tex.text = doing;    
            btn.gameObject.SetActive(true);
           


            if (btn3.gameObject.activeSelf == true)
            {
                btn3.gameObject.SetActive(false);
            }
            if (img.gameObject.activeSelf == true)
            {
                img.gameObject.SetActive(false);
            }
            if (btn.gameObject.activeSelf == false)
            {
                btn.gameObject.SetActive(true);
            }
            if (btn1.gameObject.activeSelf == true)
            {
                btn1.gameObject.SetActive(false);
            }
            if (btn2.gameObject.activeSelf == true)
            {
                btn2.gameObject.SetActive(false);
            }
            btn.onClick.AddListener(Rools);
        w.Close();
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    void Rools()
    {
        Button btn = nextButton.GetComponent<Button>();
        Image img = labirint.GetComponent<Image>();
        Button btn1 = firstChooseButton.GetComponent<Button>();
        Button btn2 = secondChooseButton.GetComponent<Button>();
        Button btn3 = thirdChooseButton.GetComponent<Button>();
        Button sb = saveButton.GetComponent<Button>();
        int id = 14;
        string doing;
       
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        StreamReader rol = new StreamReader("Assets/Resourses/Rools.txt");
        string rool = rol.ReadToEnd();
        textOn.text = rool;
       

        Text tex = nextButtonText.GetComponent<Text>();
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        doing = DoIt(17);
        tex.text = doing;
        //tex.text = "Начать";
        if (btn3.gameObject.activeSelf == true)
        {
            btn3.gameObject.SetActive(false);
        }
        if (img.gameObject.activeSelf == true)
        {
            img.gameObject.SetActive(false);
        }
        if (btn.gameObject.activeSelf == false)
        {
            btn.gameObject.SetActive(true);
        }
        if (btn1.gameObject.activeSelf == true)
        {
            btn1.gameObject.SetActive(false);
        }
        if (btn2.gameObject.activeSelf == true)
        {
            btn2.gameObject.SetActive(false);
        }
        btn.onClick.AddListener(OpenMath3);
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
    //---------------------------------
    //начало мини игры
    void OpenMath3()
    {
        SceneManager.LoadScene("Math3");
       
    }
    //функция сохранения
    void Save(int sc)
    {
        StreamWriter w = new StreamWriter("Assets/Resourses/Save.txt");
        w.Write(sc.ToString());
        w.Close();
    }
    //возвращене действия
    string DoIt(int n)
    {
        string str;
        int i = 0;
       
        StreamReader r = new StreamReader("Assets/Resourses/DoIt.txt");
        while ((str = r.ReadLine()) != null)
        {
            i++;
            if (i == n)
            {
                return str;
            }
        }
        r.Close();
        return str;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
