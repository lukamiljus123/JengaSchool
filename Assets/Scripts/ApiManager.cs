using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : MonoBehaviour
{
    [Serializable]
    private struct Blocks
    {
        public List<Block> blocks;
    }

    [SerializeField] private Blocks _blocks;
    
    private IEnumerator Start()
    {
        string uri = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            
            string json = webRequest.downloadHandler.text;
            json = "{ \"blocks\":" + json + "}";

            _blocks = JsonUtility.FromJson<Blocks>(json);
        }
    }
}
