using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class ImmersiveModeDemo : MonoBehaviour
{
#if UNITY_ANDROID
    public GameObject prefab, unityEditorText, noNavigationBarText;

    private bool isImmersive = true;

    void Start()
    {
        // This call is required when you're not using the included manifest.
        // See readme.txt for more information.
        ImmersiveMode.AddCurrentActivity();

        unityEditorText.SetActive(false);

        if (!ImmersiveMode.IsNavigationBarDetected)
        {
            noNavigationBarText.SetActive(true);
        }
        
        float minDistance = 100, maxDistance = 500, count = 500;

        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, Random.onUnitSphere * Random.Range(minDistance, maxDistance), Quaternion.identity);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
        
        if (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Moved)
        {
            Vector3 torque = Vector3.zero;
            torque.x = Input.touches[0].deltaPosition.y * +50 * Time.deltaTime;
            torque.y = Input.touches[0].deltaPosition.x * -50 * Time.deltaTime;
            rigidbody.AddRelativeTorque(torque);
        }
    }

    void OnDestroy()
    {
        ImmersiveMode.Clear();
    }

    void OnGUI()
    {
        if (isImmersive)
        {
            if (GUI.Button(new Rect(50, 50, 150, 75), "Disable"))
            {
                isImmersive = !ImmersiveMode.Clear();
            }
        }
        else
        {
            if (GUI.Button(new Rect(50, 50, 150, 75), "Enable"))
            {
                isImmersive = ImmersiveMode.AddCurrentActivity();
            }
        }

        if (GUI.Button(new Rect(50, 150, 150, 75), "Keyboard"))
        {
            TouchScreenKeyboard.Open("text");
        }
    }
#endif
}
