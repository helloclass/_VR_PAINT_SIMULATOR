using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorPrefabOp : MonoBehaviour
{
    public GameObject cam;
    public GameObject colorPanel;
    public Toggle randomSW;

    public Es.InkPainter.Brush brush;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(clickPrefab);
        brush = cam.GetComponent<Es.InkPainter.Sample.MousePainter>().brush;

        colorPanel.SetActive(false);

        randomSW = transform.GetChild(1).GetComponent<Toggle>();
        randomSW.isOn = false;
    }

    void Update()
    {
        GetComponent<Image>().color = brush.brushColor;

        if (randomSW.isOn)
        {
            brush.Color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }

    // Update is called once per frame
    void clickPrefab()
    {
        bool isopen = colorPanel.active;        

        colorPanel.SetActive(isopen ^ true);
 
    }
}
