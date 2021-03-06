using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using StarterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVirtualCamera;
    private StarterAssetsInputs starterAssetsInput;
    private ThirdPersonController thirdPersonController;

    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    [SerializeField] private Transform redEffect;
    [SerializeField] private Transform greenEffect;

    private void Awake()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInput = GetComponent<StarterAssetsInputs>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPoint = Vector3.zero;
        var screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        var ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTarget = null;
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
        {
            debugTransform.position = raycastHit.point;
            mouseWorldPoint = raycastHit.point;
            hitTarget = raycastHit.transform;
        }

        if (starterAssetsInput.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
            Vector3 worldAimTarget = mouseWorldPoint;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
        }


        if (starterAssetsInput.shoot)
        {
            if (hitTarget != null)
            {
                if (hitTarget.GetComponent<BulletTarget>() != null)
                {
                    Instantiate(greenEffect, raycastHit.transform.position, Quaternion.identity);
                    Debug.Log($"Hit target: {hitTarget.name}");
                }
                else
                {
                    Instantiate(redEffect, raycastHit.transform.position, Quaternion.identity);
                    Debug.Log($"Hit target: {hitTarget.name}");
                }


                starterAssetsInput.shoot = false;
            }
        }

    }
}
