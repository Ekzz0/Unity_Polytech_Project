using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGrid : MonoBehaviour
{
    private int gridCoordinate;
    [SerializeField] private Camera mainCamera;
    private int width_max;
    private int height_max;
    private int width_0 = 0;
    private int height_0 = 0;
    
    private float h = 64; // шаг сетки
    private float n_x;
    private float n_y;


    public List<float> grid_x;
    public List<float> grid_y;

    public LineRenderer lineRenderer;

    public Vector2 currentPoint;
    public Vector2 currentPoint_true;
    void Start()
    {
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount= 0;
        lineRenderer.startColor= Color.white;
        lineRenderer.endColor = Color.white;

        width_max = mainCamera.pixelWidth;
        height_max = mainCamera.pixelHeight;
      
        n_x = (width_max - width_0) / h;
        n_y = (height_max - height_0) / h;
        grid_x.Add(0);
        grid_y.Add(0);
       
        for (int i = 1; i < n_x - 1; i++)
        {
            
            grid_x.Add(grid_x[i - 1] + h);
        }
        for (int j = 1; j < n_y - 1; j++)
        {
            grid_y.Add(grid_y[j - 1] + h); 
        }

       
        for (int i = 0; i < n_x-1; i++)
        {
            for (int j = 0; j < n_y-1; j++)
            {
                currentPoint = new Vector2(grid_x[i], grid_y[j]);
                currentPoint_true = mainCamera.ScreenToWorldPoint(currentPoint);
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPoint_true);
                Debug.Log(i);
            }
           
        }

        lineRenderer.positionCount = 0;
        for (int i = 0; i < n_x - 1; i++)
        {
            for (int j = 0; j < n_y - 1; j++)
            {
                currentPoint = new Vector2(grid_y[j], grid_x[i]);
                currentPoint_true = mainCamera.ScreenToWorldPoint(currentPoint);
                lineRenderer.positionCount++;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentPoint_true);
                Debug.Log(i);
            }

        }

    }


    void Update()
    {
        
    }
}
