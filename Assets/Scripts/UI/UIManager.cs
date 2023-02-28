using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class UIManager : MonoBehaviour
{
    [SerializeField] public GameObject StartGameCanvas;
    [SerializeField] public GameObject IntroGameCanvas;
    [SerializeField] public GameObject InMenuCanvas;
    [SerializeField] public GameObject EndGameCanvas;
    [SerializeField] public GameObject winText;
    [SerializeField] public GameObject loseText;
    public

    PlayerController CH;
    GameManager GM;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
        CH = FindObjectOfType<PlayerController>();
    }

    //Assets/Text/Battute.txt
    private void Start()
    {
    }


    public void ShowStartMenu()
    {
        StartGameCanvas.SetActive(true);
    }
}