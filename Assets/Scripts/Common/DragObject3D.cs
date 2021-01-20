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

    public IItem item { get; private set; }

    Plane floorPlane;

    private bool isInteractive = true;

    public void EnableIntercative(bool isInteractive)
    {
        this.isInteractive = isInteractive;
    }

    public void Initialize(IItem item)
    {
        this.item = item;
        rigidBody.mass = item.mass;
    }

    private void Start()
    {
        if (rigidBody == null)
        {
            Debug.LogWarningFormat("Please assign Rigidbody for {0}", gameObject.name);
            rigidBody = GetComponent<Rigidbody>();
        }

        if (meshCollider == null)
        {
            Debug.LogWarningFormat("Please assign MeshCollider for {0}", gameObject.name);
            meshCollider = GetComponent<MeshCollider>();
        }

        floorPlane = new Plane();
        floorPlane.SetNormalAndPosition(Vector3.up, Vector3.zero);
    }

    private void OnMouseDown()
    {
        if (!isInteractive)
        {
            return;
        }

        zCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        rigidBody.isKinematic = true;
        meshCollider.isTrigger = true;
        offset = gameObject.transform.position - GetMouseWorldPos(zCoord);
    }

    private void OnMouseUp()
    {
        if (!isInteractive)
        {
            return;
        }

        meshCollider.isTrigger = false;
        rigidBody.isKinematic = false;
    }
    private void OnMouseDrag()
    {
        if (!isInteractive)
        {
            return;
        }

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
    
    private Vector3 GetMouseWorldPos(float zCoord)
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.y = Input.mousePosition.y;
        mousePosition.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
