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
        Debug.Log("Выполняем GET-запрос...");
        yield return StartCoroutine(GetRequest(apiUrl + "/1"));

        Debug.Log("Выполняем POST-запрос...");
        yield return StartCoroutine(PostRequest(apiUrl, "{ \"title\": \"New Post\", \"body\": \"Post body\", \"userId\": 1 }"));

        Debug.Log("Выполняем PUT-запрос...");
        yield return StartCoroutine(PutRequest(apiUrl + "/1", "{ \"title\": \"Updated Title\", \"body\": \"Updated Body\", \"userId\": 1 }"));

        Debug.Log("Выполняем DELETE-запрос...");
        yield return StartCoroutine(DeleteRequest(apiUrl + "/1"));

        Debug.Log("Все запросы выполнены!");
    }

    // GET-запрос (получение данных)
    IEnumerator GetRequest(string url)
    {
        using UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("Ошибка GET-запроса: " + request.error);
        else
            Debug.Log("GET-ответ: " + request.downloadHandler.text);
    }

    // POST-запрос (создание данных)
    IEnumerator PostRequest(string url, string jsonData)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("Ошибка POST-запроса: " + request.error);
        else
            Debug.Log("POST-ответ: " + request.downloadHandler.text);
    }

    // PUT-запрос (обновление данных)
    IEnumerator PutRequest(string url, string jsonData)
    {
        using UnityWebRequest request = new UnityWebRequest(url, "PUT");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("Ошибка PUT-запроса: " + request.error);
        else
            Debug.Log("PUT-ответ: " + request.downloadHandler.text);
    }

    // DELETE-запрос (удаление данных)
    IEnumerator DeleteRequest(string url)
    {
        using UnityWebRequest request = UnityWebRequest.Delete(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            Debug.LogError("Ошибка DELETE-запроса: " + request.error);
        else
            Debug.Log("DELETE успешно выполнен, код ответа: " + request.responseCode);
    }
}
