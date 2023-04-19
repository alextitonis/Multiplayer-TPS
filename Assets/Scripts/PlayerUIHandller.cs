using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIHandller : MonoBehaviour
{
    public static PlayerUIHandller getInstance;
    void Awake() { getInstance = this; }

    public Slider healthSlider;
}
