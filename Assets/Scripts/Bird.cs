using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float upAccel = 1f;
    float vel = 0;

    InputSystem_Actions action;

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        action = new InputSystem_Actions();
        action.Enable();
    }

    void Update()
    {
        if (action.Player.Attack.IsPressed())
        {
            if (vel < 0)
            {
                vel = 2.5f;
            }
            else
            {
                vel += upAccel * Time.deltaTime;
            }
        }
        else
        {
            vel -= gravity * Time.deltaTime;
        }
        vel = Mathf.Clamp(vel, -6.5f, 5.5f);
        transform.position += Vector3.up * vel * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, vel * 5);

        var col = Physics2D.OverlapCircle(transform.position, 0.14f);
        if (col != null && col.gameObject != gameObject)
        {
            action.Disable();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
