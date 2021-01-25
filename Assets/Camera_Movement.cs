using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    Animator _animator;
    public Inventory_Opener mov;
    private static readonly int Down = Animator.StringToHash("Down");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mov.closed && mov.NearInvent)
        {
            if (Input.GetKey(KeyCode.I))
            {
                _animator.SetBool(Down, true);
                Debug.Log("Testa Scende");
            }
        }
        if (mov.open)
        {
            if (Input.GetKey(KeyCode.I))
            {
                _animator.SetBool(Down, false);
                Debug.Log("Testa Sale");
            }
        }
    }
}
