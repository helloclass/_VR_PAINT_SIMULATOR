using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textureSelector : MonoBehaviour
{
    //누르면 켜지는 버튼 또는 이미지
    public GameObject TextureOption;
    public GameObject Camera;
    public Es.InkPainter.Brush brush;
    string adaptionBrush;

    void Start()
    {
        TextureOption.SetActive(false);

        brush = Camera.GetComponent<Es.InkPainter.Sample.MousePainter>().brush;
        adaptionBrush = brush.BrushTexture.name;

        GetComponent<Image>().sprite = Resources.Load<Sprite>("Alpha/" + adaptionBrush);
        GetComponent<Button>().onClick.AddListener(OnClickButton);
    }

    //onClick
    public void OnClickButton()
    {
        TextureOption.SetActive(TextureOption.active ^ true);
        if (!TextureOption.active)
        {
            adaptionBrush = brush.BrushTexture.name;

            GetComponent<Image>().sprite = Resources.Load<Sprite>("Alpha/" + adaptionBrush);
        }
    }
}
