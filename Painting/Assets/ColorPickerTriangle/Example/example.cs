using UnityEngine;
using System.Collections;

public class example : MonoBehaviour {

    public GameObject ColorPickedPrefab;
    private ColorPickerTriangle CP;
    private bool isPaint = false;
    private GameObject go;
    private Material mat;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    void Update()
    {
        Debug.Log("fe: " + ColorPickedPrefab.name);
        if (isPaint)
        {
            mat.color = CP.TheColor;
        }
    }

    void OnMouseDown()
    {
        if (isPaint)
        {
            StopPaint();
        }
        else
        {
            StartPaint();
        }
    }

    private void StartPaint()
    {
        // 상자 위에 픽커 생성
        go = (GameObject)Instantiate(ColorPickedPrefab, transform.position, Quaternion.identity);
        // 사이즈 조절
        go.transform.localScale = Vector3.one * 1.3f;
        // 나를 봐! (정면 표시)
        go.transform.LookAt(Camera.main.transform);
        // 
        CP = go.GetComponent<ColorPickerTriangle>();
        CP.SetNewColor(mat.color);
        isPaint = true;
    }

    private void StopPaint()
    {
        Destroy(go);
        isPaint = false;
    }
}
