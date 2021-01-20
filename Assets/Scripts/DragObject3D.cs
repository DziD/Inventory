using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DragObject3D : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;

    [SerializeField]
    private Rigidbody rigidBody;
    [SerializeField]
    private MeshCollider meshCollider;

    public IItem item = null;

    Plane floorPlane;

    private void Start()
    {
        if(rigidBody == null)
        {
            Debug.LogWarningFormat("Please assign Rigidbody for {0}", gameObject.name);
            rigidBody = GetComponent<Rigidbody>();
        }        

        if(meshCollider == null)
        {
            Debug.LogWarningFormat("Please assign MeshCollider for {0}", gameObject.name);
            meshCollider = GetComponent<MeshCollider>();
        }

        floorPlane = new Plane();
        floorPlane.SetNormalAndPosition(Vector3.up, Vector3.zero);
    }

    private void OnMouseDown()
    {
        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        rigidBody.isKinematic = true;
        meshCollider.isTrigger = true;
        offset = gameObject.transform.position - GetMouseWorldPos(zCoord);
    }

    private void OnMouseUp()
    {
        meshCollider.isTrigger = false;
        rigidBody.isKinematic = false;
    }
    private void OnMouseDrag()
    {        
        Vector3 newPos = GetMouseWorldPos(zCoord) + offset;

        if (newPos.y < 0.2f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            if (floorPlane.Raycast(ray, out float distanceOnRay))
            {
                newPos = ray.GetPoint(distanceOnRay - 0.1f);
            }
        }

        transform.position = newPos;
    }

    private void OnTriggerEnter(Collision collision)
    {
        Debug.LogWarning(collision.gameObject.name);
    }

    private Vector3 GetMouseWorldPos(float zCoord)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.y = Input.mousePosition.y;
        mousePosition.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
