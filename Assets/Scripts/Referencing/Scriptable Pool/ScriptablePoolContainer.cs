//# Made by Alex Meesters, Public Domain CC0 1.0 #
//# https://creativecommons.org/publicdomain/zero/1.0/ #

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Referencing.Scriptable_Pool
{
    [CreateAssetMenu(fileName = "Scriptable Pool Container", menuName = "Referencing/Scriptable Pool Container"), DefaultExecutionOrder(-9000)]
    public class ScriptablePoolContainer : ScriptableObject
    {
        [SerializeField, Tooltip("Set this if you want any pooled object to be a child of this prefab.")]
        private GameObject parentPrefab = null;
        private GameObject parentInstance;

        [SerializeField]
        private GameObject prefab = null;

        private List<GameObject> activeObjects;
        private List<GameObject> inactiveObjects;

        [SerializeField, Tooltip("Toggle this if you want to reuse the first active objects when pool exceeds the size limit.")]
        private bool recycleObjects = false;

        [SerializeField, Tooltip("Will start reusing first active object when pool exceeds this size.")]
        private int recyclePoolLimit = 30;

        [SerializeField]
        private bool cacheOnStart = false;

        [SerializeField]
        private int cacheCount;

        public GameObject this[int _index]    // Indexer declaration  
        {
            get
            {
                if (_index >= 0 && _index < activeObjects.Count)
                {
                    return activeObjects[_index];
                }
                else return null;
            }
        }

        public GameObject GetActiveObject(int _index)
        {
            if (_index >= 0 && _index < activeObjects.Count)
            {
                return activeObjects[_index];
            }
            else return null;
        }

        public GameObject GetInactiveObject(int _index)
        {
            if (_index >= 0 && _index < inactiveObjects.Count)
            {
                return activeObjects[_index];
            }
            else return null;
        }

        public int GetUnactiveObjectCount()
        {
            return inactiveObjects.Count;
        }

        public int GetActiveObjectCount()
        {
            return activeObjects.Count;
        }

        public void OnEnable()
        {
            SceneManager.activeSceneChanged += OnSceneSwitch;

            inactiveObjects = new List<GameObject>();
            activeObjects = new List<GameObject>();
        }

        public void OnDisable()
        {
            inactiveObjects.Clear();
            activeObjects.Clear();

            SceneManager.activeSceneChanged -= OnSceneSwitch;
        }

        private void OnSceneSwitch(Scene s, Scene s2)
        {
            ClearNullReferences();

            if (cacheOnStart)
            {
                for (int i = 0; i < cacheCount - activeObjects.Count; i++)
                {
                    GameObject retrieveObject = CreateNewPoolableObject(prefab.transform.position, prefab.transform.rotation);
                    retrieveObject.gameObject.SetActive(false);
                }
            }
        }

        private void ClearNullReferences()
        {
            for (int i = inactiveObjects.Count - 1; i >= 0; i--)
            {
                if (inactiveObjects[i] == null)
                {
                    inactiveObjects.RemoveAt(i);
                }
            }

            for (int i = activeObjects.Count - 1; i >= 0; i--)
            {
                if (activeObjects[i] == null)
                {
                    activeObjects.RemoveAt(i);
                }
            }
        }

        public void Return(Poolable _poolable)
        {
            inactiveObjects.Add(_poolable.gameObject);
            activeObjects.Remove(_poolable.gameObject);
        }

        private GameObject CreateNewPoolableObject(Vector3 _position, Quaternion _rotation)
        {
            GameObject createObject;

            if (parentPrefab == null)
            {
                createObject = GameObject.Instantiate(prefab, _position, _rotation);
            }
            else
            {
                if (parentInstance == null)
                {
                    parentInstance = GameObject.Instantiate(parentPrefab);
                }

                createObject = GameObject.Instantiate(prefab, parentInstance.transform);
                createObject.transform.position = _position;
                createObject.transform.rotation = _rotation;
            }

            Poolable poolableComponent = createObject.AddComponent<Poolable>();
            poolableComponent.SetContainer(this);
            return createObject;
        }

        public GameObject Retrieve(Vector3 _position, Quaternion _rotation)
        {
            // In case we want to reuse(recycle) objects that have been spawned earlier
            if (recycleObjects)
            {
                if (activeObjects.Count >= recyclePoolLimit)
                {
                    GameObject getUsedObject = activeObjects[0];
                    getUsedObject.gameObject.SetActive(false);
                    activeObjects.RemoveAt(0);
                    activeObjects.Add(getUsedObject);
                    getUsedObject.transform.position = _position;
                    getUsedObject.transform.rotation = _rotation;

                    return getUsedObject;
                }
            }

            // If nothing is available, create a new instance of the prefab
            if (inactiveObjects.Count == 0)
            {
                GameObject createObject = CreateNewPoolableObject(_position, _rotation);
                activeObjects.Add(createObject);

                return createObject;
            }
            else
            {
                GameObject getObject = null;

                for (int i = inactiveObjects.Count - 1; i >= 0; i--)
                {
                    if (inactiveObjects[i] != null)
                    {
                        getObject = inactiveObjects[i];
                        break;
                    }
                }

                if (getObject == null)
                {
                    ClearNullReferences();
                    return Retrieve(_position, _rotation);
                }

                inactiveObjects.RemoveAt(inactiveObjects.Count - 1);
                activeObjects.Add(getObject);
                getObject.transform.position = _position;
                getObject.transform.rotation = _rotation;

                return getObject;
            }
        }

        public T Retrieve<T>()
        {
            T getObject = Retrieve().GetComponent<T>();

            return getObject;
        }

        public GameObject Retrieve()
        {
            return Retrieve(prefab.transform.position, prefab.transform.rotation);
        }

        public void Retrieve(GameObject _targetSpawn)
        {
            Retrieve(_targetSpawn.transform.position, _targetSpawn.transform.rotation);
        }
    }
}