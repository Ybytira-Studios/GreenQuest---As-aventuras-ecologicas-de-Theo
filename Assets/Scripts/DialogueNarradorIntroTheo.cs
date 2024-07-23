using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueNarradorIntroTheo : MonoBehaviour
{
    public Sprite profile;
    public string speechTxt;
    public string actorName;

    private DialogueControlIntro dci;

    private void Start(){
        dci = FindObjectOfType<DialogueControlIntro>();
    }

    private void FixedUpdate(){
        dci.Speech(profile, speechTxt, actorName);
    }
}
