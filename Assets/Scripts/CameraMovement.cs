using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField] private Transform followedObject;
    [SerializeField] private bool parallax;
    [Range(0.001f, 10f)] public float parallaxSpeedFast = .2f;
    [Range(0.001f, 10f)] public float parallaxSpeedSlow = .001f;
    [Range(0.001f, 10f)] public float parallaxDistance = 10f;
    
    // Update is called once per frame
    /*void Update()
    {
        
        var ownTransform = transform;
        var followedObjectLocalPosition = followedObject.localPosition;
        
        if (parallax)
        {
            if (Vector2.Distance(new Vector2(ownTransform.localPosition.x, ownTransform.localPosition.y),
                    new Vector2(followedObjectLocalPosition.x, followedObjectLocalPosition.y)) > parallaxAmount)
            {
                    var cameraPosition = transform.localPosition;
                    cameraPosition = Vector3.MoveTowards(cameraPosition, new Vector3(followedObjectLocalPosition.x, followedObjectLocalPosition.y, cameraPosition.z),
                        parallaxSpeed);
                    transform.localPosition = cameraPosition;
            } 
        }
        else
        {
            ownTransform.localPosition = new Vector3(followedObjectLocalPosition.x, followedObjectLocalPosition.y,
            ownTransform.localPosition.z);
        }
        
    }*/

    private void LateUpdate()
    {

        if (parallax)
        {
            var ownPosition = new Vector2(transform.position.x, transform.position.y);
            var position = followedObject.position;
            var playerPosition = new Vector2(position.x, position.y);
            var smoothedPosition = Vector2.Lerp(ownPosition, playerPosition, parallaxSpeedSlow);
            
            // Check if player is too far for min speed.
            if (Vector2.Distance(smoothedPosition, playerPosition) > parallaxDistance)
            {
                smoothedPosition = Vector2.Lerp(transform.position, playerPosition, parallaxSpeedFast);
            }
            
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
        }
        else
        {
            transform.position =
                new Vector3(followedObject.position.x, followedObject.position.y, transform.position.z);
        }
    }
}
