using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeMe : MonoBehaviour
{
    public Camera cam;
    public GameObject theText;
    int idx;
    public int shakeTrigger;
    bool shaking = false;
    // Start is called before the first frame update
    void Start()
    {
        idx = theText.GetComponent<textScript>().currentMessage;
    }

    // Update is called once per frame
    void Update()
    {
        idx = theText.GetComponent<textScript>().currentMessage;
        if (!shaking && idx == shakeTrigger)
        {
            Shake(30f, 30f);
            Debug.Log("shake");
            shaking = true;
        }
        Debug.Log(idx);
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Transform t = cam.transform;
        Vector3 originalPos = t.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            t.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        t.localPosition = originalPos;
    }
}
