using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float upAccel = 1f;
    float vel = 0;

    InputSystem_Actions action;
    Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        action = new InputSystem_Actions();
        action.Enable();
    }

    void Update()
    {
        if (!GameManager.Instance.Began) return;

        if (action.Player.Attack.IsPressed() || Input.GetMouseButton(0))
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
            if (vel > 2.75f)
            {
                vel = 2.75f;
            }
            else
            {
                vel -= gravity * Time.deltaTime;
            }
        }
        vel = Mathf.Clamp(vel, -6.5f, 5.5f);
        transform.position += Vector3.up * vel * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, vel * 5);
    }

    private void LateUpdate()
    {
        anim.SetFloat("level", GameManager.Instance.Level);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("scorer"))
        {

        }
        else
        {
            action.Disable();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("scorer"))
        {
            GameManager.Instance.AddScore(1);
        }
        else
        {
            
        }
    }
}
