using UnityEngine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectManipulation : MonoBehaviour
{
    private Color originalColor;
    private bool isSelected = false;

    void Start()
    {
        originalColor = GetComponent<Renderer>().material.color;
    }
    void OnMouseDown()
    {
        if (!isSelected)
        {
            originalColor = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = Color.yellow;
            isSelected = true;
        }
        else
        {
            GetComponent<Renderer>().material.color = originalColor;
            isSelected = false;
        }
    }

    void Update()
    {
        if (isSelected)
        {
            // ����������� ������� � ������� ������ �� ��������� �� ����������
            float moveSpeed = 1f;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float depth = Input.GetAxis("Depth");
            transform.position += new Vector3(horizontal, vertical, depth) * moveSpeed * Time.deltaTime;

            // �������� ������� � ������� ����
            float rotationSpeed = 50f;
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime, Space.World);

            // ����� ��������� � �������� ������� � �������� ��������� � ������� ������� R
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.position = Vector3.zero;
                transform.rotation = Quaternion.identity;
            }
        }
    }
}

public class UIManager : MonoBehaviour, IPointerClickHandler
{
    public GameObject selectedObject;

    // �������� ������ �� ��������� ����, ������� ����� ���������� ���������� �� �������
    public Text objectNameText;
    public Text positionText;
    public Text rotationText;

    // �������� ������ �� ������, ������� ����� ��������� ������� ������ �������
    public Button resetButton;

    /* void Start()
     {
         // ������� ������� ������ ������� � �������
         resetButton.onClick.AddListener(selectedObject.GetComponent<MyObjectScript>().ResetObject);
     }*/

    public void OnPointerClick(PointerEventData eventData)
    {
        // ���� ������������ ������� �� ������� UI, �� �������� ���������� �� �������
        if (eventData.pointerPress == gameObject)
        {
            UpdateSelectedObjectInfo();
        }
    }

    void UpdateSelectedObjectInfo()
    {
        // �������� ���������� �� ������� � ���������� �� � ��������� ����� ����������������� ����������
        objectNameText.text = selectedObject.name;
        positionText.text = "Position: " + selectedObject.transform.position.ToString();
        rotationText.text = "Rotation: " + selectedObject.transform.rotation.eulerAngles.ToString();
    }
}
