using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TankControls : MonoBehaviour
{
    //variables for player input
    public InputAction tankLTreadControl, tankRTreadControl, tankTurretControl;
    private Vector2 tankTurretMovement, tankTreadsMovement;

    //variables to control movement
    public float movementSpeed;
    private float halfSpeed;
    public float rotationSpeed;
    private float halfRSpeed;

    public float turretRotationSpeed;

    //variables for internal movement management
    [SerializeField] GameObject turret;
    [SerializeField] GameObject barrel;

    // Start is called before the first frame update
    void Start()
    {
        halfSpeed = movementSpeed / 2;
        halfRSpeed = rotationSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        tankTurretMovement = tankTurretControl.ReadValue<Vector2>();
        tankTreadsMovement = new Vector2(tankLTreadControl.ReadValue<float>(), tankRTreadControl.ReadValue<float>());
    }

    private void FixedUpdate()
    {
        //Tank movement
        transform.Translate(0, 0, (tankTreadsMovement.x * halfSpeed + tankTreadsMovement.y * halfSpeed) * Time.fixedDeltaTime );
        transform.Rotate(Vector3.up, (tankTreadsMovement.x * halfRSpeed + tankTreadsMovement.y * -halfRSpeed) * Time.fixedDeltaTime);

        //Turret movement
        turret.transform.Rotate(Vector3.up, tankTurretMovement.x * turretRotationSpeed * Time.fixedDeltaTime);
        barrel.transform.Rotate(Vector3.right, tankTurretMovement.y * turretRotationSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        tankLTreadControl.Enable();
        tankRTreadControl.Enable();
        tankTurretControl.Enable();
    }

    private void OnDisable()
    {
        tankLTreadControl.Disable();
        tankRTreadControl.Disable();
        tankTurretControl.Disable();
    }
}
