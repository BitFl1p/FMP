using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scroll : MonoBehaviour
{
    [SerializeField] float minVal, maxVal;
    void Update()
    {
        transform.localPosition -= new Vector3(0, InputSystem.GetDevice<Mouse>().scroll.ReadValue().y,0);
        transform.localPosition = new Vector3(transform.localPosition.x,Mathf.Clamp(transform.localPosition.y, minVal, maxVal) , transform.localPosition.z);
    }
}
