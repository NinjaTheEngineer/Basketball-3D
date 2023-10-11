using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace NinjaTools {
    [System.Serializable]
    public class AssetReferenceAudioClip : AssetReferenceT<AudioClip> {
        public AssetReferenceAudioClip(string guid) : base(guid) { }
    }
    public class AddressableInstantiator : NinjaMonoBehaviour {
        [SerializeField] AssetReferenceGameObject assetGameObject; 
        GameObject _instanceReference;

        private void Start() {
            //assetGameObject.InstantiateAsync();
            
            if(Input.GetKeyDown(KeyCode.I)) {
                assetGameObject.LoadAssetAsync().Completed += OnAddressableLoaded;
            } else if(Input.GetKeyDown(KeyCode.J)) {
                assetGameObject.ReleaseInstance(_instanceReference);
            }
        }

        private void OnAddressableLoaded(AsyncOperationHandle<GameObject> handle) {
            var logId = "OnAddressableLoaded";
            if(handle.Status==AsyncOperationStatus.Succeeded) {
                _instanceReference = handle.Result;
                Instantiate(_instanceReference);
            } else {
                loge(logId, "Loading Asset Failed");
            }
        }
    }
}
