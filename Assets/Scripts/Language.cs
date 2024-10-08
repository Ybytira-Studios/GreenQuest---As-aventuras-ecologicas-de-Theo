using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Language : MonoBehaviour
{
private string language = "pt";

    public void setPortuguese(){
        language = "pt";
    }
    public void setEnglish(){
        language = "en";
    }
    public void setEspanish(){
        language = "es";
    }
    public void setFrench(){
        language = "fr";
    }
    public string getLanguage(){
        return language;
    }
}
