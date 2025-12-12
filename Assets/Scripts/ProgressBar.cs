using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    private RectTransform frameTransform;
    private RectTransform innerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frameTransform = GetComponent<RectTransform>();
        innerTransform = frameTransform.GetChild(0).GetComponent<RectTransform>();
    }
    public void SetProgress(float progress)
    {
        progress = Mathf.Clamp01(progress);
        innerTransform.sizeDelta = new Vector2(frameTransform.sizeDelta.x * progress, innerTransform.sizeDelta.y);
    }
    // Update is called once per frame
    
}
