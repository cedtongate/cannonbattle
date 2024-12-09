using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MouseHover : MonoBehaviour
{
    public TextMeshProUGUI label;
    void Start()
    {
        //GetComponent<MeshRenderer>().material.color = Color.black;
    }

    public void SceneSwitch()
    {
        SceneManager.LoadScene("PushkarEdit");
    }
}
