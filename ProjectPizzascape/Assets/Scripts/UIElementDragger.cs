using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : EventTrigger
{
    private bool dragVerticaly = false;
    private bool dragging;
    private Vector2 startingPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private bool getEndPointValue = false;
    

    public void Update()
    {
        if (dragging)
        {
            if (dragVerticaly)
            {
                transform.position = new Vector2(Screen.height/2, Input.mousePosition.y);
            }
            else
            {
                transform.position = new Vector2(Input.mousePosition.x, Screen.width/2);
            }
            
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
        
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }
}