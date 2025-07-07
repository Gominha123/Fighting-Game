using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [HideInInspector] public EnemyReferences enemyReferences;
    private Transform target;

    [Header("General")]
    public Transform shootPoint;
    public Transform vfxPoint;

    public LayerMask layerMask;

    public TrailRenderer trailRenderer;

    public int ammo = 3;
    private int currentAmmo;

    private void Awake()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    private void Start()
    {
        target = enemyReferences.target;
        Reload();
    }

    public void Shoot()
    {
        if (ShouldReload()) return;

        Vector3 direction = GetDirection();
        if (Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask))
        {
            Debug.DrawLine(shootPoint.position, shootPoint.position + direction * 10f, Color.red, 1f);

            TrailRenderer trail = Instantiate(trailRenderer, vfxPoint.position, Quaternion.identity);
            StartCoroutine(SpawnTrail(trail, hit));

            currentAmmo -= 1;
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1f)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = hit.point;

        Destroy(trail.gameObject, trail.time);
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = target.position - shootPoint.position;

        direction.Normalize();
        return direction;
    }

    public bool ShouldReload()
    {
        return currentAmmo <= 0;
    }

    public void Reload()
    {
        currentAmmo = ammo;
    }

}
