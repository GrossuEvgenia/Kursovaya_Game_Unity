using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{

    public Button startButton;      //кнопка начала игры
    public Button nextButton;       //кнопка продолжения игры
    public Button exit;             //кнопка выхода
    public Button roolsButton;      //кнопка правил
    public Text roolsText;          //текст правил
    public Button returnButton;     //кнопка сокрытия текста
    // Start is called before the first frame update
    void Start()
    {
        Button st = startButton.GetComponent<Button>();
        st.onClick.AddListener(newGame);
        Button nt = nextButton.GetComponent<Button>();
        nt.onClick.AddListener(menu);
        Button ex = exit.GetComponent<Button>();
        ex.onClick.AddListener(Exit);
        Button rl = roolsButton.GetComponent<Button>();
        rl.onClick.AddListener(rools);
        Text rt = roolsText.GetComponent<Text>();
        rt.gameObject.SetActive(false);
        Button rb = returnButton.GetComponent<Button>();
        rb.gameObject.SetActive(false);
    }
    //просмотр правил
    void rools()
    {
        Button st = startButton.GetComponent<Button>();
        st.gameObject.SetActive(false);
        Button nt = nextButton.GetComponent<Button>();
        nt.gameObject.SetActive(false); ;
        Button ex = exit.GetComponent<Button>();
        ex.gameObject.SetActive(false);
        Button rl = roolsButton.GetComponent<Button>();
        rl.gameObject.SetActive(false); ;
        Text rt = roolsText.GetComponent<Text>();
        rt.gameObject.SetActive(true);
        Button rb = returnButton.GetComponent<Button>();
        rb.gameObject.SetActive(true);
        rb.onClick.AddListener(returnToMenu);
    }
    //возвращение в меню
    void returnToMenu()
    {
        Button st = startButton.GetComponent<Button>();
        st.gameObject.SetActive(true);
        Button nt = nextButton.GetComponent<Button>();
        nt.gameObject.SetActive(true); ;
        Button ex = exit.GetComponent<Button>();
        ex.gameObject.SetActive(true);
        Button rl = roolsButton.GetComponent<Button>();
        rl.gameObject.SetActive(true); ;
        Text rt = roolsText.GetComponent<Text>();
        rt.gameObject.SetActive(false);
        Button rb = returnButton.GetComponent<Button>();
        rb.gameObject.SetActive(false);
    }
    //начать новую игру
    void newGame()
    {
        FileStream f = new FileStream(@"Assets/Resourses/Save.txt", FileMode.Truncate);
        SceneManager.LoadScene("Interactve_fiction");
        f.Close();
    }
    //меню
    void menu()
    {   string check;
        StreamReader red = new StreamReader("Assets/Resourses/Save.txt");
        check = red.ReadToEnd();
        if (check.Length != 0)
        {
            if (int.Parse(check)<=14)
            {
                SceneManager.LoadScene("Interactve_fiction");
            }
            else SceneManager.LoadScene("Final");
        }
        else SceneManager.LoadScene("Interactve_fiction");
        red.Close();
    }
    void Exit()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
