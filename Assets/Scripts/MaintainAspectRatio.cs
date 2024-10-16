using UnityEngine;

public class MaintainAspectRatio : MonoBehaviour
{
    private float targetAspect = 16.0f / 9.0f;

    void Start()
    {
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;
        Camera camera = Camera.main;

        if (scaleHeight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            camera.rect = rect;
        }
        else
        {
            scaleHeight = 1.0f / scaleHeight;

            Rect rect = camera.rect;

            rect.width = scaleHeight;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleHeight) / 2.0f;
            rect.y = 0;

            camera.rect = rect;
        }
    }
}
