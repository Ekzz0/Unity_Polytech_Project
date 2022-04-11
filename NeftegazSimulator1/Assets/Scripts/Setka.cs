using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setka : MonoBehaviour
{
    /*
    Texture2D texture2D;
    bool camMove;
    public float camSpeed = 50;
    float camSize;
    Vector3 mpClic;
    public int grid_on = 0;
    */
    void Start()
    {
        /*
        texture2D = new Texture2D(1, 1);
        Camera.main.orthographic = true;
        camSize = Camera.main.orthographicSize;
        */
        
    }
    void Update()
    {
        
        /*
        Camera.main.orthographicSize += Input.mouseScrollDelta.y;
        if (Camera.main.orthographicSize > 13) Camera.main.orthographicSize = 13;
        if (!camMove && Input.GetMouseButton(0))
        {
            camMove = true;
            mpClic = Input.mousePosition;
        }
        if (camMove)
        {
            if (Input.GetMouseButtonUp(0)) camMove = false;
            Vector3 d = (Input.mousePosition - mpClic).normalized * Vector3.Distance(mpClic, Input.mousePosition) / camSpeed * Time.deltaTime;
            Camera.main.transform.position += d;
        }
        */

    }
    /*
    private void OnGUI()
    {
        GuiDrawSnap(new Rect(0, 0, 0, 0), 0);
    }
    void GuiDrawSnap(Rect r, float snapSize)
    {
        float step = Screen.height / 2 /( Camera.main.orthographicSize +1);

        float corrX = Camera.main.transform.position.x - (int)Camera.main.transform.position.x;
        float corrY = Camera.main.transform.position.y - (int)Camera.main.transform.position.y;
        

        float x = Screen.width / 2 - corrX * step;

        for (float ix = 0; ix < Screen.width / 2 + step; ix += step)
        {
            //Debug.Log(ix);
            
            GUI.DrawTexture(new Rect(x + ix, 0, 1, Screen.height), texture2D);
            GUI.DrawTexture(new Rect(x - ix, 0, 1, Screen.height), texture2D);
        }
        for (float iy = corrY * step; iy < Screen.height; iy += step)
        {
            
            GUI.DrawTexture(new Rect(0, iy, Screen.width, 1), texture2D);
        }
        Debug.Log("Нарисовал");
     }
    */
}