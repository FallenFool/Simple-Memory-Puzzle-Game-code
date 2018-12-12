using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	
	void Start () {
		
	}

	void Update () {
		
	}
    public void yukle(int a)
    {
        if (a == 0)
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (a == 1)
        {
            Application.Quit();
        }
    }
}
