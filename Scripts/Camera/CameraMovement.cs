using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera cam;

    private Vector3 dragOrigin;

    float zoom;
    float zoomMultiplier = 15f;
    float minZoom = 10f;
    float maxZoom = 55f;
    float velocity = 0f;
    float smoothTime = 0.1f;

    [SerializeField]
    private SpriteRenderer mapRenderer;

    private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    private void Awake()
    {
        mapMinX = mapRenderer.transform.position.x - mapRenderer.bounds.size.x / 2f;
        mapMaxX = mapRenderer.transform.position.x + mapRenderer.bounds.size.x / 2f;

        mapMinY = mapRenderer.transform.position.y - mapRenderer.bounds.size.y / 2f;
        mapMaxY = mapRenderer.transform.position.y + mapRenderer.bounds.size.y / 2f;
    }

    void Update()
    {
        PanCamera();
        ZoomInOut();
    }

    private void Start()
    {
        zoom = cam.orthographicSize;
    }

    private void PanCamera()
    {
        if(Input.GetMouseButtonDown(2) || Input.GetMouseButtonDown(1))  // 2는 마우스 휠 1은 마우스 오른쪽 버튼
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(2) || Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
             
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    private void ZoomInOut()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom); // 주어진 값 zoom을 주어진 범위 minZoom과 maxZoom에서 제한하는 함수
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
        // Math.SmmothDamp는 값을 부드럽게 변화시키기 위해 사용된다. 

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(newX, newY, targetPosition.z);
    }
}
