using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] InputHandler inputActions;
    [SerializeField] CinemachineFreeLook cinemachineFreeLook;
    public List<ZoomSet> zoomSets;
    private int zoomInput;
    private int zoomSetNumber = 2;
    private int numberOfSets;

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
    }

    private void Update()
    {
        zoomInput = (int)inputActions.cameraZoom;
        if (zoomInput != 0)
        {
            zoomSetNumber -= zoomInput;
            if (zoomSetNumber >= 0 && zoomSetNumber < numberOfSets)
            {
                SetZoomSet(zoomSets[zoomSetNumber]);
            }
            else
            {
                zoomSetNumber += zoomInput;
            }
        }
    }
    private void SetZoomSet(ZoomSet zoomSet)
    {
        cinemachineFreeLook.m_Orbits[0] = new CinemachineFreeLook.Orbit(zoomSet.topRigHeight, zoomSet.topRigRadius);
        cinemachineFreeLook.m_Orbits[1] = new CinemachineFreeLook.Orbit(zoomSet.midRigHeight, zoomSet.midRigRadius);
        cinemachineFreeLook.m_Orbits[2] = new CinemachineFreeLook.Orbit(zoomSet.bottomRigHeight, zoomSet.bottomRigRadius);
    }
}
