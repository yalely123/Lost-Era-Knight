using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObjectThroughScene : MonoBehaviour
{
    public string objectID;   

    private void Start()
    {
        objectID = name + Time.time.ToString();
        string time = Time.time.ToString();
        for (int i = 0; i < Object.FindObjectsOfType<SaveObjectThroughScene>().Length; i++)
        {
            if (Object.FindObjectsOfType<SaveObjectThroughScene>()[i] != this)
            {
                /*
                if (Object.FindObjectsOfType<SaveObjectThroughScene>()[i].objectID == objectID)
                {
                    Destroy(gameObject);
                }
                */
                if (Object.FindObjectsOfType<SaveObjectThroughScene>()[i].name == gameObject.name)
                {
                    Destroy(Object.FindObjectsOfType<SaveObjectThroughScene>()[i].gameObject);
                }

            }
        }
        DontDestroyOnLoad(gameObject);
    }
}
