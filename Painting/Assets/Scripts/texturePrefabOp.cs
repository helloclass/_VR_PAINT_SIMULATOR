using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class texturePrefabOp : MonoBehaviour
{
    public GameObject cam;
    public GameObject selectorPanel;
    public GameObject MainPrefab;

    public Es.InkPainter.Brush brush;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(clickPrefab);
        brush = cam.GetComponent<Es.InkPainter.Sample.MousePainter>().brush;
    }

    // Update is called once per frame
    void clickPrefab()
    {
        string name = GetComponent<Image>().sprite.name;

        Sprite sprite = Resources.Load<Sprite>("Alpha/" + name);

        var croppedTexture = new Texture2D((int)sprite.textureRect.width, (int)sprite.textureRect.height);
        var pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);

        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();

        brush.BrushTexture = croppedTexture;
        MainPrefab.GetComponent<Image>().sprite = Resources.Load<Sprite>("Alpha/" + name);

        selectorPanel.SetActive(false);
    }
}
