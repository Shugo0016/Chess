using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject mark;
    public GameObject selectedPieceToken;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out RaycastHit hitinfo))
            {
                mark.transform.position = hitinfo.point;
                Piece p = hitinfo.collider.GetComponentInParent<Piece>();
                if (p != null)
                {
                    selectedPieceToken.transform.position = p.transform.position;
                }
            } 
        }
    }
}
