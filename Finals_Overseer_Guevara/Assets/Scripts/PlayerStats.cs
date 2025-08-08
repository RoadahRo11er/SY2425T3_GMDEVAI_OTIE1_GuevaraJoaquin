using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public bool hasGem = false;

    public void GemObtained()
    {
        hasGem = true;
    }
}
