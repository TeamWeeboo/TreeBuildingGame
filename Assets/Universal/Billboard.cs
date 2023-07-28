using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CULU
{

    public class Billboard : MonoBehaviour
    {
        [SerializeField] bool reverse = true;
        void Update()
        {
            Vector3 forawrd = Camera.main.transform.position - transform.position;
            if (reverse) forawrd = -forawrd;
            transform.rotation = Quaternion.LookRotation(forawrd, Vector3.up);
        }
    }

}