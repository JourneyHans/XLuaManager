using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetScript : MonoBehaviour
{
    public Text title;
    private int tick = 0;

	void Start ()
	{
	    Debug.Log(">>>>>>>>>> Start in C#, tick = " + tick);
	}

    void Update()
    {
        if (++tick % 50 == 0)
        {
            title.text = "Update in C#, tick = " + tick;
        }
    }
}
