using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    private Dictionary<KeyCode, bool> inputs = new Dictionary<KeyCode, bool>()
    {
        { KeyCode.A, false},{ KeyCode.D, false},{KeyCode.Space, false}
    };

    private void Update()
    {
        foreach (var key in inputs.Keys.ToList())
        {
            inputs[key] = Input.GetKey(key);
        }
    }

    public bool Get_Keys(KeyCode key)
    {
        return inputs[key];
    }
}
