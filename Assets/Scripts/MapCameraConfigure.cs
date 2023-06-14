using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCameraConfigure : MonoBehaviour
{
    public RenderTexture mapTexture;

    private Camera mapCamera;

    private void Awake() 
    {
        mapCamera = GetComponent<Camera>();
        mapCamera.orthographic = true;
        mapCamera.transform.position = new Vector3(0, 900, 0);
        mapCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
        mapCamera.targetTexture = mapTexture;
    }
}
