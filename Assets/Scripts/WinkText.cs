using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinkText : MonoBehaviour
{
    public float winkSpeed = 1.0f;
    private float _reloadWink = 0.0f;

    private Text _theText;
    private bool _isEnabled = true;

    private void Start()
    {
        _theText = GetComponent<Text>();
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            _reloadWink += Time.deltaTime;
            if (_reloadWink >= winkSpeed)
            {
                _reloadWink = 0.0f;
                _isEnabled = !_isEnabled;
                _theText.enabled = _isEnabled;
            }
        }
    }

}
