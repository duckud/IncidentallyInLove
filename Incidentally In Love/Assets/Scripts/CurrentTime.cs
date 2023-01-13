using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CurrentTime : MonoBehaviour
{
    [SerializeField] TMP_Text time;
    void Update()
    {
        time.text = DateTime.Now.ToString("HH:mm");
    }
}
