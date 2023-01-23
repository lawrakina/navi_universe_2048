using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gui : MonoBehaviour
{
    public static Gui Instance;

    [Header("Components")] 
    [SerializeField] private Canvas canvas;

    private void Awake() {
        Instance = this;
    }

    private void OnDestroy() {
        Instance = null;
    }
    
    #region getters

    public Canvas Canvas => canvas;

    #endregion
}
