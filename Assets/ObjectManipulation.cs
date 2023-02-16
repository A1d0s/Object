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
            // перемещение объекта с помощью клавиш со стрелками на клавиатуре
            float moveSpeed = 1f;
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            float depth = Input.GetAxis("Depth");
            transform.position += new Vector3(horizontal, vertical, depth) * moveSpeed * Time.deltaTime;

            // вращение объекта с помощью мыши
            float rotationSpeed = 50f;
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            transform.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime, Space.World);
            transform.Rotate(Vector3.right, mouseY * rotationSpeed * Time.deltaTime, Space.World);

            // сброс положения и поворота объекта в исходное состояние с помощью клавиши R
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

    // Получить ссылки на текстовые поля, которые будут отображать информацию об объекте
    public Text objectNameText;
    public Text positionText;
    public Text rotationText;

    // Получить ссылку на кнопку, которая будет выполнять функцию сброса объекта
    public Button resetButton;

    /* void Start()
     {
         // Связать функцию сброса объекта с кнопкой
         resetButton.onClick.AddListener(selectedObject.GetComponent<MyObjectScript>().ResetObject);
     }*/

    public void OnPointerClick(PointerEventData eventData)
    {
        // Если пользователь щелкнул на объекте UI, то обновить информацию об объекте
        if (eventData.pointerPress == gameObject)
        {
            UpdateSelectedObjectInfo();
        }
    }

    void UpdateSelectedObjectInfo()
    {
        // Получить информацию об объекте и отобразить ее в текстовых полях пользовательского интерфейса
        objectNameText.text = selectedObject.name;
        positionText.text = "Position: " + selectedObject.transform.position.ToString();
        rotationText.text = "Rotation: " + selectedObject.transform.rotation.eulerAngles.ToString();
    }
}
