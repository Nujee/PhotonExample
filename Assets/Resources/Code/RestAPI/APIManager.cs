using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    private string apiUrl = "https://jsonplaceholder.typicode.com/posts";

    void Start()
    {
        StartCoroutine(ExecuteRequestsInOrder());
    }

    IEnumerator ExecuteRequestsInOrder()
    {
        Debug.Log("Processing GET-request...");
        yield return StartCoroutine(GetRequest(apiUrl + "/1"));

        Debug.Log("Processing POST-request...");
        yield return StartCoroutine(PostRequest(apiUrl, "{ \"title\": \"New Post\", \"body\": \"Post body\", \"userId\": 1 }"));

        Debug.Log("Processing PUT-request...");
        yield return StartCoroutine(PutRequest(apiUrl + "/1", "{ \"title\": \"Updated Title\", \"body\": \"Updated Body\", \"userId\": 1 }"));

        Debug.Log("Processing DELETE-request...");
        yield return StartCoroutine(DeleteRequest(apiUrl + "/1"));

        Debug.Log("All requests are completed!");
    }

    // GET-request - fetch data
    IEnumerator GetRequest(string url)
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("GET-request error: " + request.error);
        else
            Debug.Log("GET-response: " + request.downloadHandler.text);
    }

    // POST-request - create data
    IEnumerator PostRequest(string url, string jsonData)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("POST-request error: " + request.error);
        else
            Debug.Log("POST-response: " + request.downloadHandler.text);
    }

    // PUT-request - delete data
    IEnumerator PutRequest(string url, string jsonData)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("PUT-request error: " + request.error);
        else
            Debug.Log("PUT-response: " + request.downloadHandler.text);
    }

    // DELETE-request - data deletion
    IEnumerator DeleteRequest(string url)
    {
        using UnityWebRequest request = UnityWebRequest.Delete(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("DELETE-request error: " + request.error);
        else
            Debug.Log("DELETE-request completed successfully, response code: " + request.responseCode);
    }
}
