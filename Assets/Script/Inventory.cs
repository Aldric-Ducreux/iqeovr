using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class Inventory : MonoBehaviour
{
    string[] deskPrefab = {
        "SM_Prop_Desklamp_01",
        "SM_Prop_Desklamp_02",
        "SM_Prop_Desklamp_03",
        "SM_Prop_Desklamp_06",
        "SM_Prop_Desklamp_07",
        "SM_Prop_Photo_01",
        "SM_Prop_Photo_02",
        "SM_Prop_Photo_03",
        "SM_Prop_Photo_04",
        "SM_Prop_Photo_05"
    };
    string[] furniturePrefab = {
        "SM_Prop_Chair_01",
        "SM_Prop_Chair_02",
        "SM_Prop_Chair_08",
        "SM_Prop_Chair_10",
        "SM_Prop_Chair_BeanBag_01",
        "SM_Prop_Chair_BeanBag_02",
        "SM_Prop_Chair_BeanBag_03",
        "SM_Prop_Chair_BeanBag_04",
        "SM_Prop_Couch_01",
        "SM_Prop_Couch_02",
        "SM_Prop_Couch_03",
        "SM_Prop_Couch_04",
        "SM_Prop_Couch_06",
        "SM_Prop_Table_01",
        "SM_Prop_Table_02",
        "SM_Prop_Table_03",
        "SM_Prop_Table_Round_01",
        "SM_Prop_Table_Round_02",
    };
    private RaycastHit lastRaycastHit;
    [SerializeField] Camera cam;
    public float range = 1000;
    private string objectChoosen;
    private bool isMenuOpened = false;
    private bool isDesk = true;
    private bool isPage2 = false;
    private GameObject menu;
    private ArrayList toDelete = new ArrayList();

    void populate()
    {
        clear();
        print(isDesk);
        string[] inventory = isDesk ? deskPrefab : furniturePrefab;
        int page2Offset = isPage2 ? 15 : 0;
        for (int i = 0; i < 15 && i < inventory.Length; i++)
        {
            GameObject go = GameObject.Find("i" + i);
            //print("GO: " + go.name);
            if (go != null)
            {
                GameObject prefab = (GameObject)Instantiate(Resources
                    .Load("3DAssets/PolygonOffice/Prefabs/Props/" + (isDesk ? "Desk Props/" : "Furniture/") + inventory[i + page2Offset]));
                RuntimePreviewGenerator.BackgroundColor = new Color(0, 0, 0, 0);
                Texture2D texture = RuntimePreviewGenerator.GenerateModelPreview(prefab.transform);
                toDelete.Add(texture);
                go.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            }
        }
    }

    void clear()
    {
        foreach (Texture2D t in this.toDelete)
        {
            DestroyImmediate(t);
        }
        for (int i = 0; i < 15; i++)
        {
            GameObject go = GameObject.Find("i" + i);

            if (go != null)
            {
                go.GetComponent<Image>().sprite = null;
            }
        }
    }

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
    private void toggleMenuDisplay()
    {
        if (!isMenuOpened)
        {
            menu.transform.position = this.transform.position + transform.forward * 4;
        }
        menu.SetActive(isMenuOpened = !isMenuOpened);
    }
    private void PlaceObjectHere()
    {
        print("3DAssets/PolygonOffice/Prefabs/Props/" + (isDesk ? "Desk Props/" : "Furniture/") + objectChoosen);
        GameObject obj = (GameObject)Instantiate(Resources.Load("3DAssets/PolygonOffice/Prefabs/Props/" + (isDesk ? "Desk Props/" : "Furniture/") + objectChoosen));
        Rigidbody gameObjectsRigidBody = obj.AddComponent<Rigidbody>(); // Add the rigidbody.
        gameObjectsRigidBody.mass = 5;
        obj.transform.position = lastRaycastHit.point + lastRaycastHit.normal * 0.5f;
    }

    public GameObject FindObject(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    private void checkNavButton()
    {
        GameObject goL = FindObject(menu, "LeftBtn");
        GameObject goR = FindObject(menu, "RightBtn");
        print("CHECKNAV");
        print(isDesk);
        if (isDesk)
        {
            if (goL != null && goR != null)
            {
                goL.SetActive(false); goR.SetActive(false);
            }
        }
        else
        {
            if (goL != null && goR != null)
            {
                goL.SetActive(isPage2);
                goR.SetActive(!isPage2);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        menu = (GameObject)Instantiate(Resources.Load("Prefabs/InventoryHUD"));
        populate();
        checkNavButton();
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("FireAndroid") || Input.GetButtonDown("Fire"))
        {
            GameObject go = GetLookedAtObject();

            if (go != null)
            {
                print(go.name);
                if (go.name.Equals("DeskBtn"))
                {

                    isPage2 = false;
                    isDesk = true;
                    populate();
                    print("In DESKBTN");
                }
                else if (go.name.Equals("FurnitureBtn"))
                {

                    isPage2 = false;
                    isDesk = false;
                    populate();
                    print("In FURNITUREBTN");
                }
                else if (go.name.Equals("LeftBtn"))
                {

                    isPage2 = false;
                    populate();
                    print("In LEFTBTN");
                }
                else if (go.name.Equals("RightBtn"))
                {

                    isPage2 = true;
                    populate();
                    print("In RIGHTBTN");
                }
                else
                {
                    if (go.GetComponent<Rigidbody>() == null && go.GetComponent<Image>() != null)
                    {
                        print("In ELSE");
                        print(go.name.Length);
                        int index0fItem = Int32.Parse(go.name.Substring(go.name.Length - 1));
                        objectChoosen = isDesk ? deskPrefab[index0fItem] : furniturePrefab[index0fItem];
                    }
                }
                checkNavButton();

            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("FireAndroid") || Input.GetButtonDown("Fire"))
        {

            if (GetLookedAtObject() != null && GetLookedAtObject().GetComponent<Rigidbody>() == null && GetLookedAtObject().GetComponent<Image>() != null && GetLookedAtObject().name != "DeskBtn" && GetLookedAtObject().name != "FurnitureBtn" && GetLookedAtObject().name != "LeftBtn" && GetLookedAtObject().name != "RightBtn")
            {
                PlaceObjectHere();
            }

        }

        if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("MenuAndroid"))
        {
            toggleMenuDisplay();
        }
    }
}
