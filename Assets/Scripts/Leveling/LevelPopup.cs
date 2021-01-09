using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPopup : MonoBehaviour
{
    public Text Lv;

    public int Level = 1;

    public float DisplayTime;
    float reset;

    // Start is called before the first frame update
    void Start()
    {

        reset = DisplayTime;

    }

    // Update is called once per frame
    void Update()
    {

        DisplayTime -= Time.deltaTime;
        if(DisplayTime < 0)
        {

            GetComponent<CanvasGroup>().alpha = 0;

        }

    }

    public void DisplayPopup()
    {
        Lv.text = Level.ToString();
        DisplayTime = reset;
        GetComponent<CanvasGroup>().alpha = 1;

    }
}
