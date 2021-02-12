using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RamenFixer
{
    public class RamenFixerController : MonoBehaviour
    {
        private GameObject _playerGameObject;
        private GameObject _vmcAvatarGameObject;
        private Vector3 _originAvatarPosition;
        private Quaternion _originAvatarRotation;
        public static RamenFixerController Instance { get; private set; }
        
        private void Awake()
        {
            _playerGameObject = GameObject.Find("LocalPlayerGameCore/Origin");
            _vmcAvatarGameObject = GameObject.Find("VRM");
            _originAvatarPosition = _vmcAvatarGameObject.transform.localPosition;
            _originAvatarRotation = _vmcAvatarGameObject.transform.localRotation;
        }

        internal static void LoadRamenFixer()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }
            GameObject ramen = new GameObject("Ramen");
            Instance = ramen.AddComponent<RamenFixerController>();
        }

        internal static void UnloadRamenFixer()
        {
            if (Instance != null)
            {
                Instance.ResetAvatarPosition();
                Destroy(Instance.gameObject);
                Instance = null;
            }
        }

        void ResetAvatarPosition()
        {
            if(_vmcAvatarGameObject != null)
            {
                _vmcAvatarGameObject.transform.localPosition = _originAvatarPosition;
                _vmcAvatarGameObject.transform.localRotation = _originAvatarRotation;
            }
        }

        private void Update()
        {
            
            if(_playerGameObject != null && _vmcAvatarGameObject != null)
            {
                _vmcAvatarGameObject.transform.localPosition = _playerGameObject.transform.localPosition;
                _vmcAvatarGameObject.transform.localRotation = _playerGameObject.transform.localRotation;
            }
        }
    }
}
