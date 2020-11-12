using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Teleport : MonoBehaviour
{
    private RaycastHit lastRaycastHit;
    public float range = 1000;
    private string objectChoosen;
    [SerializeField] Camera cam;

    private GameObject GetLookedAtObject()
    {
        Vector3 origin = transform.position;
        Vector3 direction = Camera.main.transform.forward;
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        if (Physics.Raycast(ray, out lastRaycastHit, range))
        {
            return lastRaycastHit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    private void TeleportToLooktAt()
    {
        transform.position = new Vector3(lastRaycastHit.point.x + lastRaycastHit.normal.x * 1.5f, 6, lastRaycastHit.point.z + lastRaycastHit.normal.z * 1.5f);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ThirdAndroid") || Input.GetButtonDown("Third") || Input.GetKeyDown(KeyCode.T))
        {
            if (GetLookedAtObject() != null)
            {
                TeleportToLooktAt();
            }
        }

    }
}
