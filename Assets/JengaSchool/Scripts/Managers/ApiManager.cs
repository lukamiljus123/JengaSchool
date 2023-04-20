using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ApiManager : MonoBehaviour
{
    public UnityEvent<Blocks> OnLoaded { get; } = new();

    private IEnumerator Start()
    {
        string uri = "https://ga1vqcu3o1.execute-api.us-east-1.amazonaws.com/Assessment/stack";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();
            
            string json = webRequest.downloadHandler.text;
            json = "{ \"blocks\":" + json + "}";

            var _blocks = JsonUtility.FromJson<Blocks>(json);
            OnLoaded.Invoke(_blocks);
        }
    }
}
