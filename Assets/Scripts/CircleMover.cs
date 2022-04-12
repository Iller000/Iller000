using UnityEngine;

public class CircleMover : MonoBehaviour
{
    [SerializeField] float radius = 1;                  // Sugár
    [SerializeField] float speed = 1;                   // Sebesség
    float _phaseInDeg = 0;                              // Fázis szögben

    Vector3 _startPos;                                  // Kezdő pozíció

    void Start()
    {
        _startPos = transform.position;                 // Elmentjük a kezdőpozíciót
    }


    void Update()
    {
        float circumference = 2 * radius * Mathf.PI;    // Kiszámoljuk a körünk kerületét.
        float frequency = speed / circumference;        // Kiszámoljuk a frekvenciáját a mozgásnak.
                                                        // (Egy másodperc alatt hány kört tesz meg?)
        float angularSpeed = 360 * frequency;           // Kiszámoljuk a szögsebességet.
                                                        // (Egy másodperc alatt hány foknyi utat tesz meg?)
        _phaseInDeg += angularSpeed * Time.deltaTime;   // Növeljük a fázist .
                                                        // az utolsó Update óta eltelt idővel arányosan.
        float phaseInRad = _phaseInDeg * Mathf.Deg2Rad; // Átváltás radiánba. (Ezt várja a Sin, Cos függvény.)

        float x = Mathf.Cos(phaseInRad);                // Kiszámoljuk a körpálya komponenseit. (R = 1)
        float y = Mathf.Sin(phaseInRad);
        Vector3 circlePos = new Vector3(x, 0, y);       // Vektort hozunk létre belőlük a vízszintes síkban.
        circlePos *= radius;                            // A körpálya sugarát beállítjuk.
        Vector3 position = circlePos + _startPos;       // Eltoljuk a start pozíciónak megfelően.

        transform.position = position;                  // Beállítjuk a kiszámított pozíciót.
    }
}
