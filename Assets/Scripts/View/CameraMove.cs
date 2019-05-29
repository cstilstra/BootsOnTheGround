// handles third person camera movement controls
// authors: c.s.tilstra, rob martin , unknown

using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Camera-Control/Keyboard")]
public class CameraMove : MonoBehaviour
{
	// Keyboard axes buttons in the same order as Unity
	public enum KeyboardAxis { Horizontal = 0, Vertical = 1, MouseX = 2, MouseScrollWheel = 3, HorizontalRotation = 4, None = 5 }
	
	[System.Serializable]
	// Handles left modifiers keys (Alt, Ctrl, Shift)
	public class Modifiers
	{
		public bool leftAlt;
		public bool leftControl;
		public bool leftShift;
		
		public bool checkModifiers()
		{
			return (!leftAlt ^ Input.GetKey(KeyCode.LeftAlt)) &&
				(!leftControl ^ Input.GetKey(KeyCode.LeftControl)) &&
					(!leftShift ^ Input.GetKey(KeyCode.LeftShift));
		}
	}
	
	[System.Serializable]
	// Handles common parameters for translations and rotations
	public class KeyboardControlConfiguration
	{
		
		public bool activate;
		public KeyboardAxis keyboardAxis;
		public Modifiers modifiers;
		public float sensitivity;
		
		public bool isActivated()
		{
			return activate && keyboardAxis != KeyboardAxis.None && modifiers.checkModifiers();
		}
	}
	
	// Yaw default configuration
	public KeyboardControlConfiguration yaw = new KeyboardControlConfiguration { keyboardAxis = KeyboardAxis.HorizontalRotation, sensitivity = 1F };
	
	// Pitch default configuration
	public KeyboardControlConfiguration pitch = new KeyboardControlConfiguration { keyboardAxis = KeyboardAxis.Vertical, modifiers = new Modifiers { leftAlt = true }, sensitivity = 1F };
	
	// Roll default configuration
	public KeyboardControlConfiguration roll = new KeyboardControlConfiguration { keyboardAxis = KeyboardAxis.Horizontal, modifiers = new Modifiers { leftAlt = true, leftControl = true}, sensitivity = 1F };
	
	// Vertical translation default configuration
	public KeyboardControlConfiguration verticalTranslation = new KeyboardControlConfiguration { keyboardAxis = KeyboardAxis.Vertical, modifiers = new Modifiers { leftControl = true }, sensitivity = 0.5F };
	
	// Horizontal translation default configuration
	public KeyboardControlConfiguration horizontalTranslation = new KeyboardControlConfiguration { keyboardAxis = KeyboardAxis.Horizontal, sensitivity = 0.5F };
	
	// Depth (forward/backward) translation default configuration
	public KeyboardControlConfiguration depthTranslation = new KeyboardControlConfiguration { keyboardAxis = KeyboardAxis.MouseScrollWheel, sensitivity = 0.5F };
	
	// names for keyboard axes
	public string keyboardHorizontalAxisName = "Horizontal";
	public string keyboardVerticalAxisName = "Vertical";
    public string MouseScrollAxisName = "Mouse ScrollWheel";
    public string MouseXAxisName = "MouseX";
	public string HorizontalRotationAxisName = "HorizontalRotation";

    private string[] keyboardAxesNames;

    Vector3 camOffset;
    Vector3 defaultCamOffset = new Vector3(0.0f,14.9f,-25.4f);
    public GameObject camPointOfInterest;
    Vector3 camSmoothDampV;
    new Camera camera;
    public float cursorYPosOffset = 1.85f;
      

    void Start()
    {
        keyboardAxesNames = new string[] { keyboardHorizontalAxisName, keyboardVerticalAxisName, MouseScrollAxisName, MouseXAxisName, HorizontalRotationAxisName };

        camera = GetComponent<Camera>();
        camOffset = defaultCamOffset;

        // move the camera point of interest to the center of the viewport
        Vector3 groundPos = getCursorWorldPosAtViewportPoint(0.5f, 0.5f);
        camPointOfInterest.transform.position = groundPos;
    }

    void LateUpdate()
    {        
        //	if (pitch.isActivated())//unused
        //	{
        //		float rotationY = Input.GetAxis(keyboardAxesNames[(int)pitch.keyboardAxis]) * pitch.sensitivity;
        //		transform.Rotate(-rotationY, 0 , 0);
        //	}
        //	if (roll.isActivated())//unused
        //	{
        //		float rotationZ = Input.GetAxis(keyboardAxesNames[(int)roll.keyboardAxis]) * roll.sensitivity;
        //		transform.Rotate(0, 0, rotationZ);
        //	}
        //if (verticalTranslation.isActivated())//forward and backward movement (rig)
        //{
        //    float translateY = Input.GetAxis(keyboardAxesNames[(int)verticalTranslation.keyboardAxis]) * verticalTranslation.sensitivity * -1;
        //    if (translateY != 0)
        //    {
        //        camPointOfInterest.transform.Translate(new Vector3(0.0f,-translateY, 0.0f));
        //    }
        //}
        //if (horizontalTranslation.isActivated())//side to side movement (rig)
        //{
        //    float translateX = Input.GetAxis(keyboardAxesNames[(int)horizontalTranslation.keyboardAxis]) * horizontalTranslation.sensitivity * -1;
        //    if(translateX != 0)
        //    {
        //        camPointOfInterest.transform.Translate(new Vector3(translateX, 0.0f, 0.0f));
        //    }
        //}
        if (depthTranslation.isActivated())//zooms in and out (camera)
        {
            float translateDepth = Input.GetAxis(keyboardAxesNames[(int)depthTranslation.keyboardAxis]) * depthTranslation.sensitivity;
            if(translateDepth != 0)
            {
                camera.transform.Translate(0, 0, translateDepth);                
                camOffset = camera.transform.position - camPointOfInterest.transform.position;
            }
        }
        if (yaw.isActivated())//turn left and right (camera and rig)
        {
            float rotation = Input.GetAxis(keyboardAxesNames[(int)yaw.keyboardAxis]) * yaw.sensitivity;
            if(rotation != 0)
            {
                camera.transform.RotateAround(camPointOfInterest.transform.position, Vector3.up, rotation);
                camPointOfInterest.transform.Rotate(new Vector3(0.0f, 0.0f, rotation));
                
                // TODO : figure out how to avoid adjusting the offset here
                camOffset = camera.transform.position - camPointOfInterest.transform.position;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos;

            if (getClickPosIfOnGameSurface(out clickPos))
            {
                float mouseX = Input.mousePosition.x / camera.pixelWidth;
                float mouseY = Input.mousePosition.y / camera.pixelHeight;
                camPointOfInterest.transform.position = clickPos;
            }
        }

        // Move the camera smoothly to the target position
        Vector3 cameraTargetLocation = camPointOfInterest.transform.position + camOffset;
        camera.transform.position = Vector3.SmoothDamp(
            camera.transform.position, cameraTargetLocation, ref camSmoothDampV, 0.5f);

    }

    private Vector3 getCursorWorldPosAtViewportPoint(float vx, float vy)
    {
        Vector3 groundPos = getWorldPosAtViewportPoint(vx, vy);
        groundPos.y += cursorYPosOffset;
        return groundPos;
    }

    private Vector3 getWorldPosAtViewportPoint(float vx, float vy)
    {
        Ray worldRay = camera.ViewportPointToRay(new Vector3(vx, vy, 0));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float distanceToGround;
        groundPlane.Raycast(worldRay, out distanceToGround);
        return worldRay.GetPoint(distanceToGround);
    }
    
    private bool getClickPosIfOnGameSurface(out Vector3 clickPos)
    {
        clickPos = new Vector3(0, 0, 0);

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // figure out where on the terrain the mouse click was
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // the pointer is over an object with a collider
                clickPos = hit.point;
                return true;
            }
            // the pointer is not over the game surface or UI
            return false;
        }
        // the pointer is over a UI element
        return false;
    }
}
