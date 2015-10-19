﻿using UnityEngine;
using System.Collections;

public class WelcomeHelper : MonoBehaviour
{
  public GameObject proceduralRoomPrefab;
  public GameObject welcomeSignPrefab;

  GameObject proceduralRoom, welcomeSign;

  Material signMaterial;

  // Use this for initialization
  void Start()
  {
    Vector3 roomPosition = new Vector3(0, 1.2f, -8);
    Vector3 signPosition = new Vector3(0, 1.2f, 1);

    proceduralRoom = Instantiate(proceduralRoomPrefab, roomPosition, Quaternion.identity) as GameObject;
    welcomeSign = Instantiate(welcomeSignPrefab, signPosition, Quaternion.identity) as GameObject;

    signMaterial = welcomeSign.GetComponent<MeshRenderer>().material;
  }

  // Update is called once per frame
  void Update()
  {
    if (Utilities.UserClicked())
    {
      StartCoroutine(FadeOutAndDestroy(0));
    }
  }

  IEnumerator FadeOutAndDestroy(float delay)
  {
    yield return new WaitForSeconds(delay);

    iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "easetype", iTween.EaseType.easeOutExpo, "onupdate", "FadeOutUpdateCallback", "time", .5f, "oncomplete", "FadeOutDoneCallback"));
  }

  public void FadeOutUpdateCallback(float progress)
  {
    signMaterial.color = new Color(1, 1, 1, progress);
  }

  public void FadeOutDoneCallback()
  {
    Destroy(welcomeSign);
    StartCoroutine(FadeOutAndDestroy2(0));
  }

  IEnumerator FadeOutAndDestroy2(float delay)
  {
    yield return new WaitForSeconds(delay);

    iTween.ValueTo(gameObject, iTween.Hash("from", 1f, "to", 0f, "easetype", iTween.EaseType.easeOutExpo, "onupdate", "FadeOutUpdateCallback2", "time", 3.5f, "oncomplete", "FadeOutDoneCallback2"));
  }

  public void FadeOutUpdateCallback2(float progress)
  {
    proceduralRoom.GetComponent<ProceduralRoom>().SetColor(new Color(1, 1, 1, progress));
  }

  public void FadeOutDoneCallback2()
  {
    Destroy(proceduralRoom);
  }

}
