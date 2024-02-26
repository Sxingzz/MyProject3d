using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounAnimation : MonoBehaviour
{

    private float bounceSpeed;
    private float bounceAmplitude;
    private float rotationSpeed;

    private float staringHeight;
    private float timeOffset;

    private void Awake()
    {
        if (DataManager.HasInstance)
        {
            bounceSpeed = DataManager.Instance.DataConfig.bounceSpeed;
            bounceAmplitude = DataManager.Instance.DataConfig.bounceAmplitude;
            rotationSpeed = DataManager.Instance.DataConfig.rotationSpeed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        staringHeight = transform.localPosition.y;
        timeOffset = Random.value * Mathf.PI * 2;
    }

    // Update is called once per frame
    void Update()
    {
        // bounce
        float finalHeight = staringHeight + Mathf.Sin(Time.time * bounceSpeed + timeOffset) * bounceAmplitude;
        var position = transform.localPosition;
        position.y = finalHeight;
        transform.localPosition = position;

        //spin
        Vector3 rotation = transform.localRotation.eulerAngles;
        rotation.y += rotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
