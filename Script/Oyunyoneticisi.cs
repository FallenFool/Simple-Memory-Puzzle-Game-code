
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Oyunyoneticisi : MonoBehaviour {
    [SerializeField]
    private Sprite bgImage;
    public Sprite[] puzzles;
    private bool firstGuess;
    private bool secondGuess;
    private int countGuess;
    private int countCurrentGuess;
    private int countWrongGuess;
    private int firstGuessIndex;
    private int secondGuessIndex;
    private int gameGuess;
    private string firstGuessPuzzle;
    private string secondGuessPuzzle;
    public Text t;


    public List<Sprite> gamePuzzles = new List<Sprite>();


    public List<Button> btns = new List<Button>();
	void Start()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuess = gamePuzzles.Count/2;
  
    }
    void GetButtons()
    {
        GameObject [] objects = GameObject.FindGameObjectsWithTag ("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
           
        }
    }
    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            btn.onClick.AddListener(()=>PickAllPuzzle());
        }
    }
    void AddGamePuzzles()
    {
        int looper = btns.Count;
        int index = 0;
     
        for (int i = 0; i < looper; i++)
        {
            if (index == looper / 2)
            {
                index = 0;
            }
            gamePuzzles.Add(puzzles[index]);
            
            index++;
          
        }
    }
    public void PickAllPuzzle()
    {
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        if (!firstGuess)
        {
            firstGuess = true;

            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            
        }
        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            StartCoroutine(CheckIfThePuzzleMacth());

        }
    }
   IEnumerator CheckIfThePuzzleMacth()
    {
        yield return new WaitForSeconds(1f);
        if (firstGuessPuzzle == secondGuessPuzzle)
        {
            yield return new WaitForSeconds(.5f);
            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            checkIfTheGameFinished();
        }
        else
        {
            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;
            countWrongGuess++;
            if (countWrongGuess == 5)
            {
                Invoke("SceneMang", 2);
                t.text = "Kaybettin";

            }

        }
        yield return new WaitForSeconds(.5f);
        firstGuess = secondGuess = false;
    }
    void checkIfTheGameFinished()
    {
        countCurrentGuess++;
        if (countCurrentGuess==gameGuess)
        {
            t.text = "Kazandın";
            Invoke("SceneMang", 2);

        }
   

    }
    void SceneMang()
    {
        SceneManager.LoadScene("anamenu");
    }
    void Shuffle(List<Sprite> list)
    {
       for(int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomindex = Random.Range(0, list.Count);
            list[i] = list[randomindex];
            list[randomindex] = temp;
        }
        
    }
}
