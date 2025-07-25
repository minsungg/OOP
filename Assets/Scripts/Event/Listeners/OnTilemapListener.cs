using Referencing.Scriptable_Reference;
using UnityEngine;
using UnityEngine.Events;
using World;

namespace Event.Listeners
{
    public class OnTilemapListener : MonoBehaviour
    {
        [SerializeField]
        private ScriptableReference gridManagerReference;

        [SerializeField]
        private string[] tileMapNames;

        [SerializeField]
        private UnityEvent onTileMap;

        [SerializeField]
        private UnityEvent notOnTileMap;

        private GridManager gridManager;

        private void Start()
        {
            CheckIfOnTileMap();
        }

        public void CheckIfOnTileMap()
        {
            if (gridManager == null)
            {
                gridManager = gridManagerReference.Reference.GetComponent<GridManager>();
            }

            if (gridManager != null)
            {
                if (gridManager.ContainsMapsAtLocation(tileMapNames, this.transform.position))
                {
                    onTileMap.Invoke();
                }
                else
                {
                    notOnTileMap.Invoke();
                }
            }
        }
    }
}
