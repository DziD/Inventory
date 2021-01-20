using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ItemView : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidBody;
    [SerializeField]
    private MeshCollider meshCollider;
    public IItem Item { get; private set; }

    private Plane floorPlane;
    private bool isInteractive = true;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Environment.GetCamera();
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
    public void EnableIntercative(bool isInteractive)
    {
        meshCollider.enabled = isInteractive;
        this.isInteractive = isInteractive;
        rigidBody.isKinematic = !isInteractive;
        meshCollider.isTrigger = !isInteractive;
    }
    public void Initialize(IItem item)
    {
        this.Item = item;
        rigidBody.mass = item.mass;
    }

    private Vector3 offset;
    private float zCoord;

    private void OnMouseDown()
    {
        if (!isInteractive)
        {
            return;
        }

        zCoord = mainCamera.WorldToScreenPoint(transform.position).z;
        rigidBody.isKinematic = true;
        meshCollider.isTrigger = true;
        offset = transform.position - GetMouseWorldPos(zCoord);
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
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
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

        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
