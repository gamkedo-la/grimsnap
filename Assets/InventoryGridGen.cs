using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGridGen : MonoBehaviour
{

    public GameObject tilePrefab;
    public int Rows = 5;
    public int Columns = 10;

    public int tileWidth =50;
    public int tileHeight = 50;

    // Start is called before the first frame update
    void Start()
    {
        float allWidth = tileWidth * Columns;
        float allHeight = tileHeight * Rows;
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(allWidth, allHeight);
        for(int r = 0; r< Rows; r++)
        {
            for(int c = 0; c<Columns; c++)
            {

                GameObject tempGO = GameObject.Instantiate(tilePrefab, transform);
                tempGO.name = c + "," + r;
                RectTransform rtTemp = tempGO.GetComponent<RectTransform>();
                rtTemp.localPosition = new Vector2(tileWidth * c - allWidth/2, tileHeight * r - allHeight/2);

            }


        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
