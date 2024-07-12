using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    [SerializeField] private int selectedWeaponIndex;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Reference.thePlayer = gameObject;
        selectedWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        Ray rayFromCameraCursor = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        playerPlane.Raycast(rayFromCameraCursor, out float distanceFromCamera);
        Vector3 cursorPosition = rayFromCameraCursor.GetPoint(distanceFromCamera);
        // transform.LookAt(cursorPosition);

        //Face the new position
        Vector3 lookAtPosition = cursorPosition;
        transform.LookAt(lookAtPosition);


        if (weapons.Count > 0 &&  Input.GetMouseButton(0))
        {
            weapons[selectedWeaponIndex].Shot(cursorPosition);
        }

        if (Input.GetMouseButtonDown(1))
        {
            ChangeWeaponIndex(selectedWeaponIndex + 1);
          
        }
    }

    private void ChangeWeaponIndex(int index)
    {
        selectedWeaponIndex = index;
        if (selectedWeaponIndex >= weapons.Count)
        {
            selectedWeaponIndex = 0;
        }

        for(int i = 0; i < weapons.Count; i++)
        {
            if(i==selectedWeaponIndex)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }

        }
    }

    private void Movement()
    {       
        Vector3 inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //rb.AddForce(inputVector * _speed) ;
        rb.velocity = inputVector*_speed;

        Vector3 lookAtPosition = transform.position + inputVector;
        transform.LookAt(lookAtPosition);

        //float maxDistanceToMove = _speed * Time.deltaTime;

        // Vector3 movementVector = inputVector * maxDistanceToMove;
        // Vector3 newPosition = transform.position + movementVector;
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon therWeapon = other.GetComponentInParent<Weapon>();   

        if(therWeapon != null)
        {
            weapons.Add(therWeapon);
            therWeapon.transform.position = transform.position + new Vector3(0.5f,0,0.5f);
            therWeapon.transform.rotation = transform.rotation;
            therWeapon.transform.SetParent(transform);

            ChangeWeaponIndex(weapons.Count - 1);
        }
    }
}
