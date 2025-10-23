using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScreenEdgeTips : MonoBehaviour
{

    //UI
    public Text distanceLabel;
    //The rect of the parent object
    public RectTransform directContainer;
    //Width of this image label
    public float prefabWidth;
    //Object A
    public GameObject fromHero;
    //Object B
    public GameObject toHero;

    //cameras
    private Camera mainCamera;
    public Camera uicamera;

    
    private List<Line2D> screenLines;


    private void Update()
    {
        UpdateImp();

        if(toHero)
        transform.up = transform.position - toHero.transform.position;
    }

    private void Start()
    {
        float offsetWidth = prefabWidth / 2; //Deviation
        float originalPoint = 0 + offsetWidth; //Initial point + deviation
        float correctionWidth = Screen.width - offsetWidth;//Corrected screen width         
        float correctionHeight = Screen.height - offsetWidth;//Corrected screen height          
        Vector3 point1 = new Vector3(offsetWidth, offsetWidth, 0);
        Vector3 point2 = new Vector3(offsetWidth, correctionHeight, 0);
        Vector3 point3 = new Vector3(correctionWidth, correctionHeight, 0);
        Vector3 point4 = new Vector3(correctionWidth, offsetWidth, 0);
        this.screenLines = new List<Line2D>();
        this.screenLines.Add(new Line2D(point1, point2));
        this.screenLines.Add(new Line2D(point2, point3));
        this.screenLines.Add(new Line2D(point3, point4));
        this.screenLines.Add(new Line2D(point4, point1));
        this.mainCamera = Camera.main;

        transform.SetParent(directContainer);
        transform.localScale = Vector3.one;
    }

    //Whether the point is inside the screen
    private bool PointIsInScreen(Vector3 pos)
    {
        if (pos.x <= this.screenLines[0].point1.x
            || pos.x >= this.screenLines[1].point2.x
            || pos.y <= this.screenLines[0].point1.y
            || pos.y >= this.screenLines[1].point2.y)
        {
            return false;
        }
        return true;
    }

    //World coordinates converted to screen coordinates
    private Vector2 WorldToScreenPoint(Vector3 pos)
    {
        if (null != this.mainCamera)
        {
            return mainCamera.WorldToScreenPoint(pos);
        }
        return Vector2.zero;
    }

    public void UpdateImp()
    {
        bool isIntersce = false;
        if (fromHero != null)
        {
            Vector2 intersecPos = new Vector2();
            Vector2 fromPos = this.WorldToScreenPoint(fromHero.transform.position);
            Vector2 toPos = Vector2.zero;
            
            if (toHero != null)
            {
                toPos = this.WorldToScreenPoint(toHero.transform.position);
             
            }
            //Object B exists and object A is in the screen
            if (toPos != Vector2.zero && this.PointIsInScreen(fromPos))
            {
                Line2D line = new Line2D(fromPos, toPos);
                foreach (Line2D l in this.screenLines)
                {
                    if (line.Intersection(l, out intersecPos) == Line2D.CROSS)
                    {
                        isIntersce = true;
                        Debug.Log("ÓÐ½»µã ");
                        break;
                    }
                }
                //Intersection point exists
                if (isIntersce)
                {
                    Vector2 finalPos = new Vector2();
                    //Replace intersection points on screen with intersection points on UI
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(directContainer, intersecPos, uicamera, out finalPos);
                    this.GetComponent<RectTransform>().anchoredPosition = finalPos;//Absolute layout of the UI
                }
                //No intersection point, i.e. object B is inside the screen, then the label position is at the top end of object B
                else
                {
                    if (this.PointIsInScreen(toPos))
                    {
                        Vector2 finalPos = new Vector2();
                        RectTransformUtility.ScreenPointToLocalPointInRectangle(directContainer, toPos, uicamera, out finalPos);
                        this.GetComponent<RectTransform>().anchoredPosition = finalPos + new Vector2(0, 50);//Absolute layout of the UI
                    }
                }
                if (this.distanceLabel != null && toHero != null)
                {
                    this.distanceLabel.text = String.Format("{0}m", Mathf.Round((toHero.transform.position - fromHero.transform.position).magnitude));
                }
            }
        }

    }

}
