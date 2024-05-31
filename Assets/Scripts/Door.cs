using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public DoorNum doorNum;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseUp()
    {
        if (!GameManager.Instance.selected)
        {
            GameManager.Instance.choose = (int)doorNum;
            if (!GameManager.Instance.second)
            {
                GameManager.Instance.RandomDoorOpen();
            }
            else
            {
                GameManager.Instance.ReChoose();
            }
        }
    }
}
