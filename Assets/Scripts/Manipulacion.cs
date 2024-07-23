// using UnityEngine;
// using UnityEngine.InputSystem;
// using UnityEngine.XR.Interaction.Toolkit;

// public class CubeFaceRotation : MonoBehaviour
// {
//     public GameObject cube; // El objeto padre que contiene todas las piezas del cubo
//     public InputActionProperty leftHandSelect; // Acción para el botón A
//     public InputActionProperty rightHandSelect; // Acción para el botón B
//     public InputActionProperty leftHandTrigger; // Acción para el trigger de apuntar
//     public InputActionProperty rightHandTrigger; // Acción para el trigger de apuntar
//     public LayerMask faceLayerMask; // La capa asignada a las caras del cubo

//     private Transform selectedFace;

//     void Update()
//     {
//         CheckForFaceSelection();
//         HandleFaceRotation();
//     }

//     void CheckForFaceSelection()
//     {
//         if (leftHandTrigger.action.IsPressed())
//         {
//             RaycastHit hit;
//             if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, faceLayerMask))
//             {
//                 selectedFace = hit.transform;
//             }
//         }
//         else if (rightHandTrigger.action.IsPressed())
//         {
//             RaycastHit hit;
//             if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, faceLayerMask))
//             {
//                 selectedFace = hit.transform;
//             }
//         }
//         else
//         {
//             selectedFace = null;
//         }
//     }

//     void HandleFaceRotation()
//     {
//         if (selectedFace != null)
//         {
//             if (leftHandSelect.action.WasPressedThisFrame())
//             {
//                 RotateFace(selectedFace, false); // Rotación antihoraria
//             }
//             else if (rightHandSelect.action.WasPressedThisFrame())
//             {
//                 RotateFace(selectedFace, true); // Rotación horaria
//             }
//         }
//     }

//     void RotateFace(Transform face, bool clockwise)
//     {
//         Vector3 rotationAxis = Vector3.zero;

//         // Determinar el eje de rotación basado en la dirección de la cara seleccionada
//         if (face.name.Contains("Up"))
//         {
//             rotationAxis = Vector3.up;
//         }
//         else if (face.name.Contains("Down"))
//         {
//             rotationAxis = Vector3.down;
//         }
//         else if (face.name.Contains("Left"))
//         {
//             rotationAxis = Vector3.left;
//         }
//         else if (face.name.Contains("Right"))
//         {
//             rotationAxis = Vector3.right;
//         }
//         else if (face.name.Contains("Front"))
//         {
//             rotationAxis = Vector3.forward;
//         }
//         else if (face.name.Contains("Back"))
//         {
//             rotationAxis = Vector3.back;
//         }

//         float angle = clockwise ? 90f : -90f;

//         // Rotar la cara del cubo
//         face.Rotate(rotationAxis, angle, Space.World);
//     }
// }
using UnityEngine;
using System.Collections.Generic;

public class CubeFaceRotationMouse : MonoBehaviour
{
    public GameObject cube; // El objeto padre que contiene todas las piezas del cubo
    public LayerMask faceLayerMask; // La capa asignada a las caras del cubo
    private Transform selectedFace;

    void Update()
    {
        CheckForFaceSelection();
        HandleFaceRotation();
    }

    void CheckForFaceSelection()
    {
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo del ratón para seleccionar la cara
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, faceLayerMask))
            {
                selectedFace = hit.transform;
            }
        }
    }

    void HandleFaceRotation()
    {
        if (selectedFace != null)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                RotateFace(selectedFace, false); // Rotación antihoraria
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                RotateFace(selectedFace, true); // Rotación horaria
            }
        }
    }

    void RotateFace(Transform face, bool clockwise)
    {
        Vector3 rotationAxis = Vector3.zero;
        string faceLetter = face.name.Substring(0, 1);

        // Crear un objeto vacío para agrupar las piezas de la cara
        GameObject faceGroup = new GameObject("FaceGroup");
        faceGroup.transform.position = Vector3.zero;
        faceGroup.transform.rotation = Quaternion.identity;

        // Determinar las piezas que forman la cara seleccionada
        List<Transform> facePieces = new List<Transform>();
        foreach (Transform piece in cube.transform)
        {
            if (piece.name.StartsWith(faceLetter))
            {
                facePieces.Add(piece);
                piece.SetParent(faceGroup.transform, true);
            }
        }

        // Determinar el eje de rotación basado en la dirección de la cara seleccionada
        if (faceLetter == "U")
        {
            rotationAxis = Vector3.up;
        }
        else if (faceLetter == "D")
        {
            rotationAxis = Vector3.down;
        }
        else if (faceLetter == "L")
        {
            rotationAxis = Vector3.left;
        }
        else if (faceLetter == "R")
        {
            rotationAxis = Vector3.right;
        }
        else if (faceLetter == "F")
        {
            rotationAxis = Vector3.forward;
        }
        else if (faceLetter == "B")
        {
            rotationAxis = Vector3.back;
        }

        float angle = clockwise ? 90f : -90f;

        // Rotar la cara del cubo
        faceGroup.transform.RotateAround(faceGroup.transform.position, rotationAxis, angle);

        // Desagrupar las piezas y devolverlas al objeto principal
        foreach (Transform piece in facePieces)
        {
            piece.SetParent(cube.transform, true);
        }

        // Destruir el objeto temporal
        Destroy(faceGroup);
    }
}
