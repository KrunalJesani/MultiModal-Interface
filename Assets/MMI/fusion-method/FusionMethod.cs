using hci.mmi.speech.SpeechRecognitionSystem;
using hci.mmi.gesture.GestureRecognitionSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using TMPro;

public class FusionMethod : MonoBehaviour
{
    [SerializeField]
    private GameObject speechRecognitionSystemGameObject;

    [SerializeField]
    private GameObject gestureRecognitionSystemGameObject;

    [SerializeField]
    private List<GameObject> gameObjectsPrefabs = new List<GameObject>();

    private SpeechRecognitionSystem speechRecognitionSystem;
    private GestureRecognitionSystem gestureRecognitionSystem;

    private bool secondWord = false;
    private bool flagofselect = false;
    private bool colourflag = false;
    private bool thirdword = false;
    private GameObject selectCurObj;
    public TextMeshProUGUI messageText; // Reference to the Text component for displaying messages
    private Queue<string> messageQueue = new Queue<string>(); // Queue to store messages
    Color colorname;

    public GameObject gun;
    public GameObject BulletPrefab;
    public float bulletSpeed = 20f;
    //public float bulletLifetime = 5f;
    
    public GameObject RightControllerPrefab;
   

   
    private bool isGunActive = false;

    private Vector3 newPosition;
    private const string RayCollisionLayer = "Default";
    private RayPicking rayPicking;
   

    public bool isProcessingGesture = false;


    void Start()
    {
        speechRecognitionSystem = speechRecognitionSystemGameObject.GetComponent<SpeechRecognitionSystem>();
        gestureRecognitionSystem = gestureRecognitionSystemGameObject.GetComponent<GestureRecognitionSystem>();
        speechRecognitionSystem.OnRecognized += OnSpeechRecognized;
        speechRecognitionSystem.OnHypothesized += OnSpeechHypothesized;
        gestureRecognitionSystem.OnGestureRecognized += OnGestureRecognized;

        rayPicking = GetComponent<RayPicking>();
        
    }

    void Update()
    {
        
    }


    void OnGestureRecognized(object sender, Gesture gesture)
    {
       Debug.Log("[Fusion Method] Recognized Gesture: " + gesture.name + " with " + gesture.id + " and " + gesture.confidence + " at " + gesture.timestamp);

        /* if (gesture.name == "big")
        {
            MakeObjectBigger(selectCurObj, 1f); // Makes the selected object 1.5 times bigger
        }
        else if (gesture.name == "small")
        {
            MakeObjectSmaller(selectCurObj, 1f);
        }*/
        if (gesture.name == "rotateright")
        {
            RotateObjectRight(selectCurObj, 4f);     
        } 
        else if (gesture.name == "rotateleft")
        {
            RotateObjectLeft(selectCurObj, 4f);      
        }
    }

   /*void MakeObjectBigger(GameObject obj, float scaleFactor)
    {
        Vector3 currentScale = obj.transform.localScale;
        Vector3 newScale = currentScale + new Vector3(scaleFactor, scaleFactor, scaleFactor);
        obj.transform.localScale = newScale;
        
        Debug.Log("Object made bigger.");
        DisplayMessage("Object made bigger.");
    } 
    void MakeObjectSmaller(GameObject obj, float scaleFactor)
    {
        Vector3 currentScale = obj.transform.localScale;
        Vector3 newScale = currentScale - new Vector3(scaleFactor, scaleFactor, scaleFactor);
        obj.transform.localScale = newScale;
        
        Debug.Log("Object made smaller.");
        DisplayMessage("Object made smaller.");
    }*/
    void RotateObjectRight(GameObject obj, float rotationAngle)
    {
        obj.transform.Rotate(Vector3.up, -rotationAngle);
        
        Debug.Log("Object rotated right.");
        //DisplayMessage("Object rotated right.");
    }
    void RotateObjectLeft(GameObject obj, float rotationAngle)
    {
        obj.transform.Rotate(Vector3.up, rotationAngle);
        
        Debug.Log("Object rotated left.");
       //DisplayMessage("Object rotated left.");
    }

    void OnSpeechHypothesized(object sender, Word word)
    {
       //Debug.Log("[Fusion Method] Hypothesized Word: " + word.text + " with " + word.confidence + " from " + word.startTime + " to " + word.endTime);
    }

    void OnSpeechRecognized(object sender, Word word)
    {
        Debug.Log("Recognized Word: " + word.text + " with " + word.confidence + " from " + word.startTime + " to " + word.endTime);
        DisplayMessage("Speaked Word: " + word.text);           // Display message on screen space camera

        if (secondWord)
        {
            CreateObjects(word.text);
            secondWord = false;
        }
        else if (flagofselect)
        {
            selectedObjectsfun(word.text);
            flagofselect = false;
        }
        else if (colourflag)
        {
           Color color;
            if (ColorUtility.TryParseHtmlString(word.text, out color))
            {
                ChangeObjectColor(color);
            }
            colourflag = false;
        }
        else if (thirdword)
        {
           CreateColoredObjects(word.text, colorname);
            secondWord = false;
        }
        else if (word.text.ToLower() == "create")
        {
            secondWord = true;
        }
        else if (word.text.ToLower() == "select"||word.text.ToLower() == "grab")
        {
            SelectCurrentObject();
            flagofselect = true;
        }
       else if (word.text.ToLower() == "gun")
        {
            isGunActive = !isGunActive;                // Toggle the state of the gun         
            gun.SetActive(isGunActive);                    // Set the active state of the gun based on the toggle

            RightControllerPrefab.SetActive(!isGunActive);                   // Toggle the active state of the RightControllerPrefab

            if (isGunActive)
            {
                DisplayMessage("Gun Activated"); 
            }
            else
            { 
                DisplayMessage("Gun Deactivated"); 
            }
        }
        else if (word.text.ToLower() == "fire"|| word.text.ToLower() == "Shoot" )
        {   
            if (isGunActive)
            { 
                Shoot(); 
            }
            else
            { 
                DisplayMessage("Please Active Gun"); 
            }
        }
    }     

    void CreateObjects(string word)
    {   
        Color color;
        if(word.ToLower() == "glass")
        {
            Instantiate(gameObjectsPrefabs[0],rayPicking.finalRayCastHit.point, Quaternion.Euler(-90f, 0f, 0f));
            Debug.Log("Glass created");
            DisplayMessage("Glass created");
        }
        else if(word.ToLower() == "bottle" || word.ToLower() == "wine")
        {
            Instantiate(gameObjectsPrefabs[1], rayPicking.finalRayCastHit.point, Quaternion.Euler(-90f, 0f, 0f));
            Debug.Log("Bottle created");
            DisplayMessage("Bottle created");
        }
        else if(ColorUtility.TryParseHtmlString(word, out color))
        {
            colorname = color;
            Debug.Log("Color name:" + colorname);
            thirdword = true;
        }
        else
        {
            Debug.Log("Unknown word for creation: " + word);
        }
    }

    void CreateColoredObjects(string word, Color colorname)
    {   
        switch (word.ToLower())
        {
            case "glass":
                InstantiateColoredObject(gameObjectsPrefabs[0], colorname);
                break;
            case "bottle":
            case "wine":
                InstantiateColoredObject(gameObjectsPrefabs[1], colorname);
                break;
        }
        thirdword = false;
        this.colorname = Color.white; // Reset colorname to default or desired initial color
    }

    void SelectCurrentObject()
    {
        if(rayPicking.pickedObject != null )
        {
            selectCurObj= rayPicking.pickedObject;
        
            Debug.Log("selected object:" + selectCurObj.name);
            Debug.Log("selectedobject positon:" + selectCurObj.transform.position);
            DisplayMessage("Selected Object: " + selectCurObj.name);           // Display message on screen space camera
        }
    }

    void selectedObjectsfun(string word)
    {
        switch (word.ToLower())
        {
            case "delete":
            case "destroy":
                DeletePickedObject();
                break;
            case "move":
            case "transfer":
                MoveObject();
                break;   
            case "colour":
            case "color":
                colourflag = true;
                break;   
            default:
                Debug.Log("Unknown word from Slection :  " + word);
                break;    
        }
    }

    void DeletePickedObject()
    {
        if (selectCurObj != null)
        {
            Destroy(selectCurObj);
            Debug.Log("Deleted Picked Object: " + selectCurObj.name);
            DisplayMessage("Object Deleted: "+ selectCurObj.name);           // Display message on screen space camera
        }
        else
        {
            Debug.Log("No object picked to delete.");
            DisplayMessage("No object Selected to delete.");           // Display message on screen space camera
        }
    }

    void MoveObject()
    {
        if(selectCurObj != null)
        {
            Vector3 newPosition = rayPicking.finalRayCastHit.point;
            Debug.Log("ray position:" +newPosition);
            selectCurObj.transform.position = newPosition;
            Debug.Log("final position:" + selectCurObj.transform.position);
            DisplayMessage("Object Moved: "+ selectCurObj.name);           // Display message on screen space camera
        }
        else
        {
            DisplayMessage("No Object Selected");
        }
    }

    void Shoot() 
    {
        if (rayPicking.pickedObject != null) {

            Vector3 targetPoint = rayPicking.pickedObject.transform.position; // Determine the target point based on availability
   
            GameObject newBullet = Instantiate(BulletPrefab, transform.position, Quaternion.identity); // Instantiate a new bullet prefab
    
            Rigidbody bulletRigidbody = newBullet.GetComponent<Rigidbody>(); // Get the bullet's rigidbody component
  
            bulletRigidbody.useGravity = false;  // Disable gravity for the bullet initially
   
            newBullet.SetActive(true);  // Activate the bullet prefab
    
            Vector3 direction = (targetPoint - transform.position).normalized; // Calculate the direction from the bullet to the target point
  
            bulletRigidbody.AddForce(direction * bulletSpeed, ForceMode.Impulse); // Apply a force to the bullet in the calculated direction to move it towards the target point
    
            Debug.Log("New Bullet Initial Position: " + newBullet.transform.position);  // Debug the initial position of the new bullet
     
            StartCoroutine(EnableGravityAfterDelay(newBullet, targetPoint)); // Start a coroutine to check when the bullet reaches the target position

            // Destroy(newBullet, bulletLifetime); // Destroy the bullet after a certain amount of time if needed
        }
        else {
            DisplayMessage("No Object Targeted");
        }
    }

    IEnumerator EnableGravityAfterDelay(GameObject bullet, Vector3 targetPoint) 
    {
        
        while (Vector3.Distance(bullet.transform.position, targetPoint) > 0.5f)  // Wait until the bullet reaches the target position
        { 
            yield return null;
        }

        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>(); // Get the bullet's rigidbody component

        bulletRigidbody.useGravity = true; // Enable gravity for the bullet
    }

    private void DisplayMessage(string message)
    {
        if (messageText != null)
        {
            messageQueue.Enqueue(message); // Enqueue the new message

            const int maxMessages = 3;  // Limit the number of displayed messages
            while (messageQueue.Count > maxMessages)
            {
                messageQueue.Dequeue(); // Remove the oldest message if the queue exceeds the limit
            }

            messageText.text = string.Join("\n", messageQueue.ToArray()); // Update the message text with the contents of the message queue
        }
    }


    void ChangeObjectColor(Color color)
    {
        if (selectCurObj != null)
        {
            
            Renderer renderer = selectCurObj.GetComponent<Renderer>(); // Get the renderer component of the selected object
            
            // Change the color of the object
            if (renderer != null)
            {
                renderer.material.color = color;
                DisplayMessage("Object Color Changed");
            }
            else
            {
                Debug.LogWarning("Selected object doesn't have a renderer component.");
            }
        }
        else
        {
            DisplayMessage("No Object Selected");
        }
    }

    void InstantiateColoredObject(GameObject prefab, Color color)
    {
        GameObject newObject = Instantiate(prefab, rayPicking.finalRayCastHit.point, Quaternion.Euler(-90f, 0f, 0f));
        Renderer renderer = newObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
            Debug.Log("Object created with color: " + color);
            DisplayMessage("Color Object Created ");
        }
        else
        {
            Debug.LogWarning("Instantiated object doesn't have a renderer component.");
        }
    }   
}