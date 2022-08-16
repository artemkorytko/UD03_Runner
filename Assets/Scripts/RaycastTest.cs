using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private GameObject prefab;
    private GameObject instance;

    void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                instance = Instantiate(prefab);
                instance.transform.position = hit.point;
                instance.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
                //hit.transform.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", Random.ColorHSV());
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, LayerMask.GetMask("Default")))
            {
                Debug.Log("raycast");
                var startPos = Camera.main.ViewportToWorldPoint(Input.mousePosition);
                var target = hit.point;
                _lineRenderer.SetPositions(new Vector3[] {startPos, target});
                // instance.transform.position = hit.point;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                // hit.transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", Random.ColorHSV());
            }
        }
    }
    
    public void OnClick()
    {
        Debug.Log("click");
    }
}