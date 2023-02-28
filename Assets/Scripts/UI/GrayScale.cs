using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrayScale : MonoBehaviour
{
    [SerializeField]private Image img;
    [SerializeField]private Color32 originalColor;
    [SerializeField]private int collectibleNumber;
    private bool isDone;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        originalColor = new Color32(255, 255, 225, 255);
        //img.color = new Color32(255, 255, 225, 100);
    }

    private void Update()
    {
       if(!isDone && GameManager.gameManager.collectiblesAchieved == collectibleNumber )
        {

            img.color = originalColor;
            isDone = true;
        }
    }
}
