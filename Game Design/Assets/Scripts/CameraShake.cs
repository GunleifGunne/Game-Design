using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float magnitude, fadingSpeed;
    Vector3 initialCamPos;

    // Start is called before the first frame update
    public IEnumerator shakeTrigger(float duration) {
        initialCamPos = transform.localPosition;

        float elapsedTime = 0.0f;

        while(elapsedTime < duration)
        {
            transform.localPosition = initialCamPos + Random.insideUnitSphere * magnitude;

            duration -= Time.deltaTime * fadingSpeed;

            //elapsedTime += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = initialCamPos;

    }
}
