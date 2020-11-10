using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorSelector : MonoBehaviour
{
    //누르면 켜지는 버튼 또는 이미지
    public GameObject ColorOption;
    public GameObject Camera;
    public Es.InkPainter.Brush brush;

    void Start()
    {
        brush = Camera.GetComponent<Es.InkPainter.Sample.MousePainter>().brush;
        GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    //onClick
    public void OnClickButton()
    {
        ColorOption.SetActive(false);
        if (!ColorOption.active)
        {
            brush.Color = GetComponent<Image>().color;
        }
    }
}
