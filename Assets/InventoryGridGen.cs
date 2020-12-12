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

    public List<InventoryGridNode> AllTiles = new List<InventoryGridNode>();

    public Transform Holder;

    // Start is called before the first frame update
    void Start()
    {
        float allWidth = tileWidth * Columns;
        float allHeight = tileHeight * Rows;
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(allWidth, allHeight);
        for(int r = 1; r <= Rows; r++)
        {
            for(int c = 1; c <= Columns; c++)
            {

                GameObject tempGO = GameObject.Instantiate(tilePrefab, Holder);
                tempGO.name = c + "," + r;
                RectTransform rtTemp = tempGO.GetComponent<RectTransform>();
                rtTemp.localPosition = new Vector2((tileWidth * (c -1) - allWidth/2), (allHeight / 2 - (tileHeight * (r - 1))) );
                InventoryGridNode IGNScript = tempGO.GetComponent<InventoryGridNode>();
                IGNScript.Column = c;
                IGNScript.Row = r;
                IGNScript.Grid = gameObject;
                AllTiles.Add(IGNScript);
                GetComponent<InventoryMenu>().UIElements.Add(tempGO);

            }


        }

    }

    public InventoryGridNode NodeAtCR(int atC, int atR)
    {

        foreach(InventoryGridNode IGNScript in AllTiles)
        {

            if(IGNScript.Column == atC && IGNScript.Row == atR)
            {

                return IGNScript;
            }

        }
        //Debug.Log("error, invalid C,R requested");
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
