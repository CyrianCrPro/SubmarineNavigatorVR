using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineNavigator : MonoBehaviour
{
    [SerializeField] Material sonarMat;
    [SerializeField] Texture2D mapTexture; // La carte 2D en noir et blanc représentant les murs et passages

    public Vector2 centerPoint = new Vector2(0.5f, 0.5f); // Centre de la trajectoire du sous-marin sur le radar
    public float radius = 5.0f; // Rayon de la trajectoire circulaire
    public float angularSpeed = 1.0f; // Vitesse angulaire (en radians par seconde)

    // Échelle de la carte par rapport à la texture
    [SerializeField] private Vector2 mapScale = new Vector2(10f, 10f); // Ajustez selon les dimensions du radar

    private Vector3 virtualPosition;
    private float angle;

    void Start()
    {
        if (mapTexture == null)
        {
            Debug.LogError("La texture de la carte n'est pas assignée.");
            return;
        }

        angle = 0;
        virtualPosition = CalculateVirtualPosition();
    }

    void Update()
    {
        // Mise à jour de l'angle en fonction du temps et de la vitesse angulaire
        angle += angularSpeed * Time.deltaTime;

        // Calcul de la position fictive en suivant une trajectoire circulaire
        virtualPosition = CalculateVirtualPosition();

        // Vérifier la collision avec les murs sur la carte
        if (IsCollidingWithWall(virtualPosition))
        {
            Debug.LogWarning("Collision détectée avec un mur !");
            // Arrêter ou inverser la direction du sous-marin en cas de collision
            angularSpeed = -angularSpeed;
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

    private bool IsCollidingWithWall(Vector3 position)
    {
        // Convertir les coordonnées de position en coordonnées locales dans le référentiel de la texture
        float localX = (position.x - centerPoint.x) / radius;
        float localY = (position.y - centerPoint.y) / radius;

        // Convertir les coordonnées locales en coordonnées de texture (0 à 1)
        float texCoordX = 0.5f + localX / 2f; // Assumer que le centre est à 0.5
        float texCoordY = 0.5f + localY / 2f;

        // Convertir les coordonnées normalisées (0 à 1) en pixels (dimensions de la texture)
        int texX = Mathf.Clamp(Mathf.FloorToInt(texCoordX * mapTexture.width), 0, mapTexture.width - 1);
        int texY = Mathf.Clamp(Mathf.FloorToInt(texCoordY * mapTexture.height), 0, mapTexture.height - 1);

        // Vérifier que les coordonnées sont valides
        if (texX < 0 || texX >= mapTexture.width || texY < 0 || texY >= mapTexture.height)
        {
            Debug.LogWarning("Position hors des limites de la carte !");
            return false;
        }

        // Récupérer la couleur du pixel correspondant
        Color pixelColor = mapTexture.GetPixel(texX, texY);

        // Vérifier si le pixel correspond à un mur (noir) ou un passage (blanc)
        return pixelColor == Color.black; // Noir = mur
    }

}
