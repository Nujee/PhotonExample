using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager : MonoBehaviour
{
    private string apiUrl = "https://jsonplaceholder.typicode.com/posts";

    public enum HttpMethodType
    {
        Get,
        Post,
        Put,
        Delete
    }

    private void Start()
    {
        StartCoroutine(ExecuteRequestsInOrder());
    }

    private IEnumerator ExecuteRequestsInOrder()
    {
        string jsonData;

        Debug.Log("Processing GET-request...");
        yield return StartCoroutine(SendRequest(apiUrl + "/1", HttpMethodType.Get));

        Debug.Log("Processing POST-request...");
        jsonData = "{ \"title\": \"New Post\", \"body\": \"Post body\", \"userId\": 1 }";
        yield return StartCoroutine(SendRequest(apiUrl, HttpMethodType.Post, jsonData));

        Debug.Log("Processing PUT-request...");
        jsonData = "{ \"title\": \"Updated Title\", \"body\": \"Updated Body\", \"userId\": 1 }";
        yield return StartCoroutine(SendRequest(apiUrl + "/1", HttpMethodType.Put, jsonData));

        Debug.Log("Processing DELETE-request...");
        yield return StartCoroutine(SendRequest(apiUrl + "/1", HttpMethodType.Delete));

        Debug.Log("All requests are completed!");
    }

    private IEnumerator SendRequest(string url, HttpMethodType method, string jsonData = null)
    {
        UnityWebRequest request;

        switch (method)
        {
            case HttpMethodType.Get:
                request = UnityWebRequest.Get(url);
                break;
            case HttpMethodType.Delete:
                request = UnityWebRequest.Delete(url);
                break;
            case HttpMethodType.Post:
            case HttpMethodType.Put:
                request = new UnityWebRequest(url, method.ToString().ToUpper());
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData ?? "");
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                break;
            default:
                throw new NotSupportedException($"Method {method} is not supported.");
        }

        using (request)
        {
            if (method == HttpMethodType.Get || method == HttpMethodType.Delete)
            {
                request.downloadHandler = new DownloadHandlerBuffer();
            }

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"{method}-request error: {request.error}");
            }
            else
            {
                string response = request.downloadHandler?.text;
                Debug.Log($"{method}-response: {(string.IsNullOrEmpty(response) ? request.responseCode.ToString() : response)}");
            }
        }
    }
}
