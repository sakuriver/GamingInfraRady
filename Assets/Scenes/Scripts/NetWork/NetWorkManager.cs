using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Collections.Generic;
using BaseCampResponseData;

class NetWorkManager
{
    static public NetWorkManager instance = new NetWorkManager();

    public CloudServerResponse cloudServerResponse;
    public VersionControlSystemGitRoot versionControlSystemGitRoot;

    public static bool IsLocal = true;

    static public string ApiDomainUrl = "https://secure.sakura.ad.jp/";

    // バージョン管理システム・異世界の森チャレンジ
    public IEnumerator VersionControlSysmtemGetRequest(System.Action<VersionControlSystemGitRoot> callBack)
    {
        string baseUrl = "https://api.github.com/users/sakuriver";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(baseUrl))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            string[] pages = baseUrl.Split('/');
            if (webRequest.isNetworkError)
            {
                var responseText = webRequest.downloadHandler.text;
                Debug.Log(responseText);
            }
            else
            {
                var responseText = webRequest.downloadHandler.text;
                Debug.Log(responseText);
                Debug.Log(responseText.Replace("\r", "").Replace("\n", "").Replace("\rn", ""));
                instance.versionControlSystemGitRoot = JsonUtility.FromJson<VersionControlSystemGitRoot>(webRequest.downloadHandler.text);
                Debug.Log(instance.versionControlSystemGitRoot.login);
                Debug.Log(instance.versionControlSystemGitRoot.node_id);

                callBack(instance.versionControlSystemGitRoot);

            }
        }


        yield return null;
    }

    public IEnumerator CloudServerGetRequest(string baseId, System.Action<CloudServerInfo[]> callBack)
    {
        string baseUrl = "https://secure.sakura.ad.jp/cloud/zone/" + baseId + "/api/cloud/1.1/server/";
        Debug.Log(baseUrl);
        var s = "afb64d85-911b-4406-b1f1-3d3a7f5f3273:mp5BkjeUVJKNQrGiN1c4yVQSjlanXteefC7gxtSrWlGJuogPsW6ZOhcy53fQ3rWz";
        var encoding = Encoding.GetEncoding("ISO-8859-1");
        var inArray = encoding.GetBytes(s);
        var basicAuth = Convert.ToBase64String(inArray);
        var authorization = "Basic " + basicAuth;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(baseUrl))
        {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("AUTHORIZATION", authorization);
            yield return webRequest.SendWebRequest();

            string[] pages = baseUrl.Split('/');
            if (webRequest.isNetworkError)
            {
                var responseText = webRequest.downloadHandler.text;
                Debug.Log(responseText);
            }
            else
            {
                var responseText = webRequest.downloadHandler.text;
                Debug.Log(responseText);
                instance.cloudServerResponse = JsonUtility.FromJson<CloudServerResponse>(webRequest.downloadHandler.text);
                //callBack(instance.quizResponse.response.datas[0]);
                callBack(instance.cloudServerResponse.Servers);

            }
        }
        yield return null;
    }

    public static string GetApiUrl()
    {
        if (NetWorkManager.IsLocal == true)
        {
            return ApiDomainUrl;
        }
        return "https://localhost:5001/api/";
    }

}
