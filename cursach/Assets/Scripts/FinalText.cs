using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;
public class FinalText : MonoBehaviour
{
    public Text textOn;
    public Text txt1;       // тест на кнопке
    public Text txt2;       //-n
    public State startText;
   
    string check;
    State state;            //для заполнения текста
    FileStream file1;       //работа с файлом
    //кнопки для выбора
    public Button firstChoose;      //кнопка выбора
    public Button secondChoose;     //-n
    public Button saveButton;       //кнопка сохранения
    public Button exitButton;       //кнопка выхода
    int chek;
    
    //меню
    void menu()
    {
        StreamReader reader = new StreamReader("Assets/Resourses/Save.txt");
        check = reader.ReadToEnd();
        if (check.Length != 0)
        {
            switch (int.Parse(check))
            {
                case 15: Final(); break;
                case 16: Fith(0); break;
                case 17:
                    {
                        int num;
                        StreamReader n = new StreamReader("Assets/Resourses/forFith.txt");
                        string numb = n.ReadToEnd();
                        n.Close();
                        num = int.Parse(numb); 
                        TheENd(num);
                    }break;
            }
        }
    }
    void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    void Start()
    {
        file1 = new FileStream("Assets/Resourses/Final.txt", FileMode.Open);
      
        Text tx = textOn.GetComponent<Text>();
        tx.gameObject.SetActive(false);
        state = startText;
        textOn.text = state.GetText();
      

            Final(); 
    }
    //-------------Функции для перехода между уровнями---------------------
       void Final()
    {
        int id = 15;
        StreamReader reader = new StreamReader(file1);
        string tex = reader.ReadToEnd();
        string doing;
        Text tx = textOn.GetComponent<Text>();
        textOn.text = tex;
        reader.Close();
        tx.gameObject.SetActive(true);
        Button btn1 = firstChoose.GetComponent<Button>();
        Text t1 =txt1.GetComponent<Text>();
        doing = DoIt(18);
        t1.text = doing;
        btn1.onClick.AddListener(() => Fith(0));
        
        Button btn2 = secondChoose.GetComponent<Button>();
        Text t2 = txt2.GetComponent<Text>();
        doing = DoIt(19);
        t2.text = doing;
        btn2.onClick.AddListener(() => Fith(1));
        if (btn1.gameObject.activeSelf == false)
        {
            btn1.gameObject.SetActive(true);
        }
        if (btn2.gameObject.activeSelf == false)
        {
            btn2.gameObject.SetActive(true);
        }
        Button sb = saveButton.GetComponent<Button>();
        sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);

    }
       void Fith(int f)
       {
           int id = 16;
           int num=-1;
           string doing;
           if (f == 0)
           {
               StreamReader n = new StreamReader("Assets/Resourses/forFith.txt");
               string  numb = n.ReadToEnd();
               n.Close();
                num = int.Parse(numb);
               string t1;
               string t2;
               StreamReader reder = new StreamReader("Assets/Resourses/Fith.txt");
               t1 = reder.ReadLine();
               int i = 2;
               while ((t2 = reder.ReadLine()) != null)
               {
                   if (i == num)
                   {
                       break;
                   }
                   i++;
               }
               string text = t1 + "\n" + t2;
               textOn.text = text;
               reder.Close();
           }
           else if (f == 1)
           {
               
               StreamReader n = new StreamReader("Assets/Resourses/forFith.txt");
               string numb = n.ReadToEnd();
               n.Close();
               num = int.Parse(numb);
               string t1;
               string t2;
               StreamReader reder = new StreamReader("Assets/Resourses/Fith2.txt");
               t1 = reder.ReadLine();
               int i = 2;
               while ((t2 = reder.ReadLine()) != null)
               {
                   if (i == num)
                   {
                       break;
                   }
                   i++;
               }
               string text = t1 + "\n" + t2;
               textOn.text = text;
               reder.Close();
           }
           Button btn1 = firstChoose.GetComponent<Button>();
           if (btn1.gameObject.activeSelf == false)
           {
             btn1.gameObject.SetActive(true);
           }
            Button btn2 = secondChoose.GetComponent<Button>();
            if (btn2.gameObject.activeSelf == true)
           {
             btn2.gameObject.SetActive(false);
           }
            chek = num;
            Text tb1 = txt1.GetComponent<Text>();
            doing = DoIt(20);
            tb1.text = doing;
          
            btn1.onClick.AddListener(()=>TheENd(num));
            Button sb = saveButton.GetComponent<Button>();
            sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.onClick.AddListener(ReturnToMenu);
    }
       void TheENd(int f)
       {
           string doing;
           int id = 17;
           if (f == 2)
           {
               StreamReader reder = new StreamReader("Assets/Resourses/Final1.txt");
               string tex = reder.ReadToEnd();
               textOn.text = tex;
               reder.Close();
           }
           else if (f == 3)
           {

               StreamReader reder = new StreamReader("Assets/Resourses/Final2.txt");
               string tex = reder.ReadToEnd();
               textOn.text = tex;
               reder.Close();
           }
           else 
           {

               StreamReader reder = new StreamReader("Assets/Resourses/Final3.txt");
               string tex = reder.ReadToEnd();
               textOn.text = tex;
               reder.Close();
           }
           Button btn1 = firstChoose.GetComponent<Button>();
           if (btn1.gameObject.activeSelf == false)
           {
               btn1.gameObject.SetActive(true);
           }
           Button btn2 = secondChoose.GetComponent<Button>();
           if (btn2.gameObject.activeSelf == true)
           {
               btn2.gameObject.SetActive(false);
           }
           Text tb1 = txt1.GetComponent<Text>();
           doing = DoIt(21);
           tb1.text = doing;
           //tb1.text = "Конец";
           btn1.onClick.AddListener(Exit);
           Button sb = saveButton.GetComponent<Button>();
           sb.onClick.AddListener(() => Save(id));
        Button eb = exitButton.GetComponent<Button>();
        eb.gameObject.SetActive(false);
    }
    //----------------------------------------------
       void Exit()
       {
           Application.Quit();
       }
    //функция для сохранения
       void Save(int sc)
       {
           StreamWriter w = new StreamWriter("Assets/Resourses/Save.txt");
           w.Write(sc.ToString());
           w.Close();
       }
    //функция для ыозврата действия
       string DoIt(int n)
       {
           string str;
           int i = 0;
           // file2 = new FileStream("Assets/Resourses/DoIt.txt", FileMode.Open);
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
