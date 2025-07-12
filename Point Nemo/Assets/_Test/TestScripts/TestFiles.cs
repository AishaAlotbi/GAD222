using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TestFiles : MonoBehaviour
{
    [SerializeField] private TextAsset fileName;

   
    void Start()
    {
        StartCoroutine(Run());
    }

    IEnumerator Run()
    {
        List<string> lines = FileManager.ReadTextAsset(fileName, false);

        foreach(string line in lines)
        {
            Debug.Log(line);
        }
        yield return null;
    }
}
