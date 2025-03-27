using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static CameraZoom;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] InputHandler inputActions;
    [SerializeField] CinemachineFreeLook cinemachineFreeLook;
    public List<ZoomSet> zoomSets;
    private int zoomInput;
    private int zoomSetNumber = 2;
    private int numberOfSets;

    static float t = 0.0f;
    bool isChanging;

    public struct ZoomSet
    {
        public float topRigHeight;
        public float midRigHeight;
        public float bottomRigHeight;
        public float topRigRadius;
        public float midRigRadius;
        public float bottomRigRadius;

        public ZoomSet(float v1, float v2, float v3, float v4, float v5, float v6) : this()
        {
            this.topRigHeight = v1;
            this.midRigHeight = v2;
            this.bottomRigHeight = v3;
            this.topRigRadius = v4;
            this.midRigRadius = v5;
            this.bottomRigRadius = v6;
        }
    }

    private void Start()
    {
        zoomSets = new List<ZoomSet>();
        //ZoomSet zoom1 = new ZoomSet(2.5f, 1.5f, 0.5f, 1f, 2f, 1f);
        //ZoomSet zoom2 = new ZoomSet(3.5f, 1.5f, 0.5f, 2f, 3.5f, 2f);
        //ZoomSet zoom3 = new ZoomSet(4.5f, 1.5f, 0.4f, 4f, 4.5f, 3f);
        //ZoomSet zoom4 = new ZoomSet(5.5f, 1.5f, 0.3f, 4.5f, 5f, 4f);
        //ZoomSet zoom5 = new ZoomSet(6.5f, 1.5f, 0f, 5f, 6.5f, 5);
        ZoomSet zoom1 = new ZoomSet(2.5f, 1.5f, 0.5f, 1f, 2f, 1f);
        ZoomSet zoom2 = new ZoomSet(3.0f, 1.5f, 0.45f, 1.5f, 2.75f, 1.5f);
        ZoomSet zoom3 = new ZoomSet(3.5f, 1.5f, 0.5f, 2f, 3.5f, 2f);
        ZoomSet zoom4 = new ZoomSet(4.0f, 1.5f, 0.45f, 2.5f, 3.75f, 2.25f);
        ZoomSet zoom5 = new ZoomSet(4.25f, 1.5f, 0.425f, 3.5f, 4.25f, 2.75f);
        ZoomSet zoom6 = new ZoomSet(4.5f, 1.5f, 0.4f, 4f, 4.5f, 3f);
        ZoomSet zoom7 = new ZoomSet(4.75f, 1.5f, 0.375f, 4.125f, 4.625f, 3.25f);
        ZoomSet zoom8 = new ZoomSet(5.0f, 1.5f, 0.35f, 4.25f, 4.75f, 3.5f);
        ZoomSet zoom9 = new ZoomSet(5.5f, 1.5f, 0.3f, 4.5f, 5f, 4f);
        ZoomSet zoom10 = new ZoomSet(6.5f, 1.5f, 0f, 5f, 6.5f, 5);

        zoomSets.Add(zoom1);
        zoomSets.Add(zoom2);
        zoomSets.Add(zoom3);
        zoomSets.Add(zoom4);
        zoomSets.Add(zoom5);
        zoomSets.Add(zoom6);
        zoomSets.Add(zoom7);
        zoomSets.Add(zoom8);
        zoomSets.Add(zoom9);
        zoomSets.Add(zoom10);
        numberOfSets = zoomSets.Count;
        SetZoomSet(zoomSets[zoomSetNumber]);
       // PrepareZoomSet();
    }

    private void LateUpdate()
    {
        zoomInput = (int)inputActions.CameraZoomInput;
        if (zoomInput != 0)
        {
            zoomSetNumber -= zoomInput;
            if (zoomSetNumber >= 0 && zoomSetNumber < numberOfSets)
            {
                //SetZoomSet(zoomSets[zoomSetNumber]);
                isChanging = true;
                t = 0.0f;
            }
            else
            {
                zoomSetNumber += zoomInput;
            }
        }

        if (isChanging)
        {
            t += 0.5f * Time.deltaTime;
            LerpValues(zoomSets[zoomSetNumber]);
        }
    }

    /// <summary>
    /// Changes values smoothly for a smoother zoom in and out
    /// </summary>
    /// <param name="zoomSet"> target values</param>
    private void LerpValues(ZoomSet zoomSet)
    {
        float o0h, o0r;
        float o1h, o1r;
        float o2h, o2r;

        o0h = Mathf.Lerp(cinemachineFreeLook.m_Orbits[0].m_Height, zoomSet.topRigHeight,t);
        o0r = Mathf.Lerp(cinemachineFreeLook.m_Orbits[0].m_Radius, zoomSet.topRigRadius, t);
        o1h = Mathf.Lerp(cinemachineFreeLook.m_Orbits[1].m_Height, zoomSet.midRigHeight, t);
        o1r = Mathf.Lerp(cinemachineFreeLook.m_Orbits[1].m_Radius, zoomSet.midRigRadius, t);
        o2h = Mathf.Lerp(cinemachineFreeLook.m_Orbits[2].m_Height, zoomSet.bottomRigHeight, t);
        o2r = Mathf.Lerp(cinemachineFreeLook.m_Orbits[2].m_Radius, zoomSet.bottomRigRadius, t);

        ZoomSet zoomAux = new(o0h, o1h, o2h, o0r, o1r, o2r);
        SetZoomSet(zoomAux);

        if(o2r == zoomSet.bottomRigRadius)
        {
            isChanging = false;
        }
    }

    /// <summary>
    /// Sets initial values for top, mid and bottom rig and height for lerping function
    /// </summary>
    //private void PrepareZoomSet()
    //{
    //    o0h = cinemachineFreeLook.m_Orbits[0].m_Height;
    //    o0r = cinemachineFreeLook.m_Orbits[0].m_Radius;
    //    o1h = cinemachineFreeLook.m_Orbits[1].m_Height;
    //    o1r = cinemachineFreeLook.m_Orbits[1].m_Radius;
    //    o2h = cinemachineFreeLook.m_Orbits[2].m_Height;
    //    o2r = cinemachineFreeLook.m_Orbits[2].m_Radius;
    //}

    /// <summary>
    /// Sets new TopRig, MidRig and BottomRig Values
    /// </summary>
    /// <param name="zoomSet">target values</param>
    private void SetZoomSet(ZoomSet zoomSet)
    {
        cinemachineFreeLook.m_Orbits[0] = new CinemachineFreeLook.Orbit(zoomSet.topRigHeight, zoomSet.topRigRadius);
        cinemachineFreeLook.m_Orbits[1] = new CinemachineFreeLook.Orbit(zoomSet.midRigHeight, zoomSet.midRigRadius);
        cinemachineFreeLook.m_Orbits[2] = new CinemachineFreeLook.Orbit(zoomSet.bottomRigHeight, zoomSet.bottomRigRadius);
    }
}
