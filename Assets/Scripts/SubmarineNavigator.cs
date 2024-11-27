using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SubmarineNavigator : MonoBehaviour
{
    [Header("Effets visuels")]
    [SerializeField] private Material sonarMat;
    [SerializeField] private Texture2D mapTexture; // La carte 2D en noir et blanc représentant les murs et passages
    [SerializeField] private Light warningLight;
    [SerializeField] private ParticleSystem sparks;
    [SerializeField] private ParticleSystem water;

    [Header("XR Controllers")]
    [SerializeField] private ActionBasedController leftHandController;
    [SerializeField] private ActionBasedController rightHandController;

    [Header("Effets extérieurs")]
    [SerializeField] private ScreenShake screenShake;
    [SerializeField] private ControllerShake controllerShake;

    [Header("Vitesse et Trajectoire du sous-marin")]
    public Vector2 centerPoint = new Vector2(0.5f, 0.5f); // Centre de la trajectoire du sous-marin sur le radar
    public float radius = 0f;
    public float speed = 0f;

    [Header("Map / Sonar")]
    [SerializeField] private Vector2 mapScale = new Vector2(10f, 10f); // Ajustez selon les dimensions du radar

    private Vector3 virtualPosition;
    private float angle;
    private bool isBlinking = false;

    void Start()
    {
        sparks.Stop();
        water.Stop();

        if (mapTexture == null)
        {
            Debug.LogError("La texture de la carte n'est pas assignée.");
            return;
        }

        if (warningLight == null)
        {
            Debug.LogError("La lumière n'est pas assignée.");
            return;
        }

        angle = 0;
        virtualPosition = CalculateVirtualPosition();
    }

    void Update()
    {
        // Update angle based on time and speed
        angle += speed * Time.deltaTime;

        // Calculate the new virtual position
        virtualPosition = CalculateVirtualPosition();

        // Check for collision with walls on the map
        if (IsCollidingWithWall(virtualPosition))
        {
            Debug.LogWarning("Collision detected with a wall!");

            // Reverse the direction (180-degree turn)
            speed = -Mathf.Abs(speed); // Reverse and ensure speed is consistent

            // Adjust angle to avoid further collisions
            angle += Mathf.PI; // 180° in radians to flip direction

            // Clamp angle between 0 and 2π to avoid overflow
            angle = Mathf.Repeat(angle, 2 * Mathf.PI);

            // Correct position to avoid moving out of the map
            virtualPosition = CalculateVirtualPosition();

            if (!isBlinking)
            {
                StartCoroutine(BlinkLight());
            }

            if (sparks != null)
            {
                StartCoroutine(ActivateSparks());
            }

            StartCoroutine(screenShake.Shaking());
            StartCoroutine(controllerShake.TriggerShake(leftHandController));
            StartCoroutine(controllerShake.TriggerShake(rightHandController));
        }
    }

    private Vector3 CalculateVirtualPosition()
    {
        float x = centerPoint.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.y + Mathf.Sin(angle) * radius;
        return new Vector3(x, y, transform.position.z);
    }

    public Vector3 GetVirtualPosition()
    {
        return virtualPosition;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void UpdateRadius(float crankRotationValue)
    {
        // update de l'angle à partir de rotation de la valve 
        float scaleFactor = 0.01f; // sensibilité de la rotation
        radius = Mathf.Clamp(crankRotationValue * scaleFactor, -2f, 2f);
    }

    private bool IsCollidingWithWall(Vector3 position)
    {
        // Convert position to texture coordinates
        float localX = (position.x - centerPoint.x) / radius;
        float localY = (position.y - centerPoint.y) / radius;

        float texCoordX = 0.5f + localX / 2f; // Map center at 0.5
        float texCoordY = 0.5f + localY / 2f;

        int texX = Mathf.Clamp(Mathf.FloorToInt(texCoordX * mapTexture.width), 0, mapTexture.width - 1);
        int texY = Mathf.Clamp(Mathf.FloorToInt(texCoordY * mapTexture.height), 0, mapTexture.height - 1);

        if (texX < 0 || texX >= mapTexture.width || texY < 0 || texY >= mapTexture.height)
        {
            // If the position is out of bounds, log a warning and treat it as a collision
            Debug.LogWarning("Position out of map bounds!");
            return true;
        }

        // Get pixel color and check for a wall
        Color pixelColor = mapTexture.GetPixel(texX, texY);
        return pixelColor == Color.black; // Black = wall
    }

    private IEnumerator BlinkLight()
    {
        isBlinking = true;
        float blinkInterval = 0.2f;
        float maxIntensity = 10.0f;
        Color originalColor = warningLight.color;
        Color warningColor = Color.red;
        float originalIntensity = warningLight.intensity;

        for (int i = 0; i < 10; i++)
        {
            warningLight.enabled = !warningLight.enabled;

            if (warningLight.enabled)
            {
                warningLight.color = warningColor;
                warningLight.intensity = maxIntensity;
            }
            else
            {
                warningLight.color = originalColor;
                warningLight.intensity = originalIntensity;
            }

            yield return new WaitForSeconds(blinkInterval);
        }

        warningLight.enabled = true;
        warningLight.color = originalColor;
        warningLight.intensity = originalIntensity;
        isBlinking = false;
    }

    private IEnumerator ActivateSparks()
    {
        if (sparks != null)
        {
            sparks.Play();
            water.Play();
            yield return new WaitForSeconds(1f);
            sparks.Stop();
            water.Stop();
        }
    }
}
