using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour{

    public static CoroutineRunner instance;

    public static CoroutineRunner Instance() {
        if (instance == null) { 
            instance = new CoroutineRunner(); 
        }
        return instance;
    }

}