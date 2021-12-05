using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using BaseCampResponseData;

public class BaseOperationScene : MonoBehaviour
{

    public List<GameObject> SecondRobos;
    public List<GameObject> ThirdRobos;
    public List<GameObject> OtherForests;

    public float span = 3f;
    private float currentTime = 0f;
    //　トータル制限時間
    private float totalTime;
    //　制限時間（分）
    [SerializeField]
    private int minute;
    //　制限時間（秒）
    [SerializeField]
    private float seconds;
    //　前回Update時の秒数
    private float oldSeconds;
    public Camera camera;

    private bool isToggle;


    float tiltAroundY = 0;
    public GameObject cameraOffset;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NetWorkManager.instance.CloudServerGetRequest("tk1a", SetThirdRoboData));
        StartCoroutine(NetWorkManager.instance.CloudServerGetRequest("tk1b", SetSecondRoboData));
        StartCoroutine(NetWorkManager.instance.VersionControlSysmtemGetRequest(SetOtherWorldForest));
    }

    // Update is called once per frame
    void Update()
    {

        // 舞台の人として、動き回る
        var cameraOffsetForward = CreateCameraOffsetForward(new Vector3(1, 0, 1));
        Vector3 direction = cameraOffsetForward * Input.GetAxis("Vertical") + cameraOffset.transform.right * Input.GetAxis("Horizontal");
        cameraOffset.transform.Translate(direction.x, direction.y, direction.z);

        if (Input.GetKey(KeyCode.Return))
        {
            Debug.Log("RotationStart");
            this.Rotation();
        }

        currentTime += Time.deltaTime;

        if (currentTime > span)
        {
            Debug.LogFormat("{0}秒経過", span);
            Debug.LogFormat("第2新東京検査開始");
            StartCoroutine(NetWorkManager.instance.CloudServerGetRequest("tk1a", SetThirdRoboData));
            Debug.LogFormat("第3新東京検査開始");
            StartCoroutine(NetWorkManager.instance.CloudServerGetRequest("tk1b", SetSecondRoboData));
            // issue 第3新東京と石狩も作る
            StartCoroutine(NetWorkManager.instance.VersionControlSysmtemGetRequest(SetOtherWorldForest));
            currentTime = 0f;
        }
    }

    public void SetThirdRoboData(CloudServerInfo[] servers)
    {
        int i = 0;
        DateTime now = System.DateTime.Now;

       
        foreach (GameObject robo in ThirdRobos)
        {
            robo.SetActive(i < servers.Length);
            i++;
            // issue カッコイイパラメータをなんか入れる
        }
    }

    public void SetSecondRoboData(CloudServerInfo[] servers)
    {
        int i = 0;
        DateTime now = System.DateTime.Now;


        foreach (GameObject robo in SecondRobos)
        {
            robo.SetActive(i < servers.Length);
            i++;
            // issue カッコイイパラメータをなんか入れる
        }
    }
    public void SetOtherWorldForest(VersionControlSystemGitRoot vinfo)
    {
        int i = 0;
        DateTime now = System.DateTime.Now;
        Debug.LogFormat(vinfo.public_repos.ToString());
        foreach (GameObject otherForest in OtherForests)
        {
            Debug.Log(i < vinfo.public_repos);
            otherForest.SetActive(i < vinfo.public_repos);
            i++;
            // issue カッコイイパラメータをなんか入れる
        }
    }
    void Rotation()
    {
        tiltAroundY += 0.01f;

        cameraOffset.transform.Rotate(new Vector3(0, tiltAroundY, 0), tiltAroundY);
    }

    void HeightChange()
    {
        var cameraOffsetForward = CreateCameraOffsetForward(new Vector3(0, 1, 1));
        if (Input.GetKey(KeyCode.X))
        {
            cameraOffsetForward += cameraOffset.transform.up;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            cameraOffsetForward -= cameraOffset.transform.up;
        }
        cameraOffset.transform.Translate(cameraOffsetForward.x, cameraOffsetForward.y, cameraOffsetForward.z);
    }

    private Vector3 CreateCameraOffsetForward(Vector3 directionBase)
    {
        return Vector3.Scale(cameraOffset.transform.forward, directionBase).normalized;
    }

}
