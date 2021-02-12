using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using BS_Utils;
using VMCAvatar;

namespace RamenFixer
{
    internal class RamenLogic
    {
        internal static RamenLogic Instance;
        private GameObject _playerGameObject;
        internal Vector3 AvatarScale;

        internal RamenLogic()
        {
            BS_Utils.Utilities.BSEvents.gameSceneLoaded += GameSceneLoaded;
            BS_Utils.Utilities.BSEvents.menuSceneLoaded += MenuSceneLoaded;
        }

        private void GameSceneLoaded()
        {
            _playerGameObject = GameObject.Find("LocalPlayerGameCore/Origin");
        }

        private void MenuSceneLoaded()
        {
            _playerGameObject = null;
        }

        internal void OnAvatarMove(ref Vector3 rootPosition, ref Quaternion rootRotation, ref Vector3[] bonePositions, ref Quaternion[] boneRotations)
        {
            if (_playerGameObject != null)
            {
                if (VMCAvatar.PluginConfig.Instance.trueVR)
                {
                    rootPosition = rootPosition + _playerGameObject.transform.position;
                }
                else if (AvatarScale != null)
                {
                    Vector3 playerPosition = _playerGameObject.transform.position;
                    rootPosition = rootPosition + new Vector3(playerPosition.x * AvatarScale.x, playerPosition.y * AvatarScale.y, playerPosition.z * AvatarScale.z);
                }

                Vector3 relativePosition = _playerGameObject.transform.right + _playerGameObject.transform.up + _playerGameObject.transform.forward;
                rootPosition = new Vector3(rootPosition.x * relativePosition.x, rootPosition.y, rootPosition.z * relativePosition.z); // No need to fix the y position, it's already correct. Why? I have no idea.
                rootRotation = _playerGameObject.transform.rotation * rootRotation;
            }
        }
    }
}
