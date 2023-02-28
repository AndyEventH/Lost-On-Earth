using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{

    [Header("Layer0")]
    [SerializeField] private FadeInFadeOutLayer LAYER0_LVL1;
    [SerializeField] private FadeInFadeOutLayer LAYER0_LVL2;
    [SerializeField] private FadeInFadeOutLayer LAYER0_LVL3;

    [Header("Layer1")]
    [SerializeField] private FadeInFadeOutLayer LAYER1_LVL1;
    [SerializeField] private FadeInFadeOutLayer LAYER1_LVL2;
    [SerializeField] private FadeInFadeOutLayer LAYER1_LVL3;

    [Header("Layer3")]
    [SerializeField] private FadeInFadeOutLayer LAYER3_LVL1;
    [SerializeField] private FadeInFadeOutLayer LAYER3_LVL2;
    [SerializeField] private FadeInFadeOutLayer LAYER3_LVL3;

    [Header("Layer4")]
    [SerializeField] private FadeInFadeOutLayer LAYER4_LVL1;
    [SerializeField] private FadeInFadeOutLayer LAYER4_LVL2;
    [SerializeField] private FadeInFadeOutLayer LAYER4_LVL3;

    [Header("Layer5")]
    [SerializeField] private FadeInFadeOutLayer LAYER5_LVL1;
    [SerializeField] private FadeInFadeOutLayer LAYER5_LVL2;
    [SerializeField] private FadeInFadeOutLayer LAYER5_LVL3;

    [Header("Layer6")]
    [SerializeField] private FadeInFadeOutLayer LAYER6_LVL1;
    [SerializeField] private FadeInFadeOutLayer LAYER6_LVL2;
    [SerializeField] private FadeInFadeOutLayer LAYER6_LVL3;

    public void PassLevel2() // collectiblesAchieved == 1
    {
        Debug.Log("sono arrivato qui");
        //in = true , out = false
        LAYER0_LVL2.ExecuteFadeIN();
        LAYER0_LVL1.ExecuteFadeOUT();

        LAYER1_LVL2.ExecuteFadeIN();
        LAYER1_LVL1.ExecuteFadeOUT();

        LAYER3_LVL2.ExecuteFadeIN();
        LAYER3_LVL1.ExecuteFadeOUT();

        LAYER4_LVL2.ExecuteFadeIN();
        LAYER4_LVL1.ExecuteFadeOUT();

        LAYER5_LVL2.ExecuteFadeIN();
        LAYER5_LVL1.ExecuteFadeOUT();


        LAYER6_LVL2.ExecuteFadeIN();
        LAYER6_LVL1.ExecuteFadeOUT();
    }

    public void PassLevel3() // collectiblesAchieved == 2
    {
        LAYER0_LVL3.ExecuteFadeIN();
        LAYER0_LVL2.ExecuteFadeOUT();

        LAYER1_LVL3.ExecuteFadeIN();
        LAYER1_LVL2.ExecuteFadeOUT();

        LAYER3_LVL3.ExecuteFadeIN();
        LAYER3_LVL2.ExecuteFadeOUT();

        LAYER4_LVL3.ExecuteFadeIN();
        LAYER4_LVL2.ExecuteFadeOUT();

        LAYER5_LVL3.ExecuteFadeIN();
        LAYER5_LVL2.ExecuteFadeOUT();


        LAYER6_LVL3.ExecuteFadeIN();
        LAYER6_LVL2.ExecuteFadeOUT();
    }

}
