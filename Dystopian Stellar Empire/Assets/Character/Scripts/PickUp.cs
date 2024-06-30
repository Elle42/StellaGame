using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private float pickupRange = 5f; // Range within which objects can be picked up
    [SerializeField] private LayerMask pickupLayer; // LayerMask to filter pickupable objects

    [SerializeField] private Camera playerCamera;
    private GameObject pickedObject;
    private bool isHoldingObject = false;
    [SerializeField] private Transform holdPosition; // Position where the object will be held
    private PlayerMovement _playerController;
    private bool _isCurWeapon;

    void Start()
    {
        _playerController = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (isHoldingObject)
            {
                DropObject();
            }
            else
            {
                TryPickupObject();
            }
        }

        if (isHoldingObject && pickedObject != null)
        {
            HoldObject();
        }
    }

    void TryPickupObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickupRange, pickupLayer))
        {
            if (hit.collider != null)
            {
                PickupObject(hit.collider.gameObject);
            }
        }
    }

    void PickupObject(GameObject obj)
    {
        pickedObject = obj;
        pickedObject.GetComponent<Rigidbody>().isKinematic = true; // Make the object kinematic to disable physics interactions
        isHoldingObject = true;
        pickedObject.transform.SetParent(holdPosition); // Set the hold position as the parent

        WeaponBase? weapon = pickedObject.gameObject.GetComponent<WeaponBase>();
        if (weapon != null)
        {
            weapon.Init(holdPosition);
            _playerController.SetWeapon(weapon);
            _isCurWeapon = true;
        }
    }

    void HoldObject()
    {
        pickedObject.transform.position = holdPosition.position;
        pickedObject.transform.rotation = holdPosition.rotation;
    }

    void DropObject()
    {
        pickedObject.GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics interactions
        pickedObject.transform.SetParent(null); // Remove the parent
        isHoldingObject = false;
        pickedObject = null;

        if(_isCurWeapon)
        {
            _isCurWeapon = false;
            _playerController.SetWeapon(null);
        }
    }
}
