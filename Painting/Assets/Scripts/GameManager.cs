using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // colorPicker
    public GameObject ColorPickedPrefab;
    private ColorPickerTriangle CP;
    private GameObject go;

    private Color brushColor;

    private Camera uiCam;
    private GameObject mainCam;

    public GameObject target;

    private bool isColorpicking;

    // Start is called before the first frame update
    GameObject MousePointerIMG;
    Es.InkPainter.Sample.MousePainter mpScript;

    GameObject Slider;

    float brushSize;

    Vector3 mpos;

    void Awake()
    {
        mainCam = GameObject.Find("Main Camera");
        uiCam = GameObject.Find("UICamera").GetComponent<Camera>();
        isColorpicking = false;

        Slider = GameObject.Find("Slider");

        target = GameObject.Find("uwagi");
    }

    void Start()
    {
        ColorPickedPrefab = GameObject.Find("ColorPicker");
        if (!ColorPickedPrefab)
        {
            Debug.Log("Can't Load ColorPickerdPrefab");
        }

        MousePointerIMG = GameObject.Find("MousePointer");
        mpScript = GameObject.Find("Main Camera").GetComponent<Es.InkPainter.Sample.MousePainter>();

        Cursor.visible = false;
        mpScript.brush.brushScale = 0.01f;

        brushColor = mpScript.brush.Color;
        mpScript.brush.setBrushScatter(0.1f);

        // brushSize
        Slider.transform.GetChild(0).GetComponent<Slider>().value = 10f;
        brushSize = 10f;
    }

    void Update()
    {
        brushSize = Slider.transform.GetChild(0).GetComponent<Slider>().value;

        mpScript.brush.brushScale = brushSize * 0.001f;
        mpScript.setDense(Slider.transform.GetChild(2).GetComponent<Slider>().value);
        mpScript.setDist(Slider.transform.GetChild(3).GetComponent<Slider>().value);
        mpScript.brush.setBrushScatter(Slider.transform.GetChild(4).GetComponent<Slider>().value);

        MousePointerIMG.GetComponent<RectTransform>().localScale = new Vector3(brushSize * 0.1f, brushSize * 0.1f);

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartPaint();

            MousePointerIMG.SetActive(false);
            isColorpicking = true;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            CP = go.GetComponent<ColorPickerTriangle>();
            mpScript.brush.Color = CP.TheColor;
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            StopPaint();

            MousePointerIMG.SetActive(true);
            isColorpicking = false;
        }

        mpos = Input.mousePosition;
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButtonDown(0))
            //스포이드 마우스로 변경
            StartCoroutine(ScreenShotAndSpoid());
    }

    void FixedUpdate()
    {
        if (Screen.width * 0.8 < Input.mousePosition.x || isColorpicking)
        {
            Cursor.visible = true;
            mpScript.enabled = false;
        }
        else
        {
            MousePointerIMG.transform.position = Input.mousePosition;
            Cursor.visible = false;
            mpScript.enabled = true;
        }

        // [
        if (Input.GetKey(KeyCode.LeftBracket) && mpScript.brush.brushScale > 0)
        {
            mpScript.brush.brushScale -= 0.00001f;
            // brush Slider
            Slider.transform.GetChild(0).GetComponent<Slider>().value -= 0.1f;
            MousePointerIMG.GetComponent<RectTransform>().localScale = MousePointerIMG.GetComponent<RectTransform>().localScale - new Vector3(0.1f, 0.1f);
        }
        // ]
        else if(Input.GetKey(KeyCode.RightBracket))
        {
            mpScript.brush.brushScale += 0.00001f;
            //brush Slider
            Slider.transform.GetChild(0).GetComponent<Slider>().value += 0.1f;
            MousePointerIMG.GetComponent<RectTransform>().localScale = MousePointerIMG.GetComponent<RectTransform>().localScale + new Vector3(0.1f, 0.1f);
        }

        // 지우개
        if (Input.GetKeyDown(KeyCode.R))
        {
            mpScript.setErase(true);
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            mpScript.setErase(false);
        }
    }

    private void StartPaint()
    {
        // 상자 위에 픽커 생성
        go = (GameObject)Instantiate(ColorPickedPrefab, uiCam.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 3.0f + Vector3.up * 2.0f), Quaternion.identity);
        go.GetComponent<ColorPickerTriangle>().SetNewColor(mpScript.brush.brushColor);
        // 사이즈 조절
        go.transform.localScale = Vector3.one * 0.7f;
        // 나를 봐! (정면 표시)
        go.transform.LookAt(uiCam.transform);
        // 컬러 피킹 하는 동안 뒤에 색칠 안되도록
        mpScript.enabled = false;
    }

    private void StopPaint()
    {
        // 색칠 활성화
        mpScript.enabled = true;
        Destroy(go);
    }

    IEnumerator ScreenShotAndSpoid()
    {
        //스크린샷을 찍어, 그것을 Texture2D로 반환시키고 그곳에서 색을 추출!!
        Texture2D tex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        yield return new WaitForEndOfFrame();
        tex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tex.Apply();

        //추출된 색
        Color color = tex.GetPixel((int)mpos.x, (int)mpos.y);

        mpScript.brush.Color = color;
    }
}
