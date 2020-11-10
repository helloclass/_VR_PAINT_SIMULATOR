using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveBtn : MonoBehaviour
{
    GameManager GM;
    GameObject target;
    public bool isClicked;
    
    void Start()
    {
        isClicked = false;
        GetComponent<Button>().onClick.AddListener(SaveButtonClicked);
    }

    void SaveButtonClicked()
    {
        isClicked = true;
    }
}
