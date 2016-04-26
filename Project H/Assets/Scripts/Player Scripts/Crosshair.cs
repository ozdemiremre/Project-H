﻿using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Texture2D crosshair;
    public Rect pos;
    static bool OriginalOn = true;
    public bool CursorLock = false;
    public TextMesh infoText;
    RaycastHit hit;
    public Camera cam;
    public MoneyHoldingScript HoldScript;

    [SerializeField]
    private GameObject official;

    private MeshRenderer FOVRender;
    void Start()
    {
        Component[] renderers = official.GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
            if (renderer.name == "DetectionObject")
                FOVRender = renderer;

        pos = new Rect((Screen.width - crosshair.width) / 2, (Screen.height - crosshair.height) / 2, crosshair.width, crosshair.height);
        infoText.text = null;


    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (CursorLock == true)
            {
                CursorLock = false;
            }
            else if (CursorLock == false)
            {
                CursorLock = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {

            if (FOVRender.enabled && official.activeSelf)
                FOVRender.enabled = false;

            else if (!FOVRender.enabled && official.activeSelf)
                FOVRender.enabled = true;


        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5.0f)) //Show on focus object's info.
        {
            if (hit.collider.tag == "door") //Show door info.
            {
                if (hit.collider.GetComponent<DoorScript>().getState() == true)
                    infoText.text = "Press [E] to Close";

                else
                    infoText.text = "Press [E] to Open";

            }
            else if (hit.collider.tag == "Money") //Show money info.
            {
                if (hit.collider.GetComponent<MoneyScript>() == null)
                    infoText.text = hit.collider.GetComponent<MoneyScript10K>().getMoneyAmount() + "$";

                else
                    infoText.text = hit.collider.GetComponent<MoneyScript>().getMoneyAmount() + "$";
            }

            else
            {
                infoText.text = null;
            }
        }
    }

    /// <summary>
    /// Returns the RaycastHit pointer, what's in front of the player at the last frame.
    /// </summary>
    /// <returns>Hit. Object that was in front of the player at the last frame.</returns>
    public RaycastHit GetHit()
    {
        return hit;
    }

    void OnGUI()
    {
        if (CursorLock == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (CursorLock == false)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (OriginalOn == true)
        {
            GUI.DrawTexture(pos, crosshair);
        }
    }
}
