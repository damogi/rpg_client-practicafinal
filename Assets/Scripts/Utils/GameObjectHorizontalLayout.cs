using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class GameObjectHorizontalLayout: MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> gameObjects;
        [SerializeField]
        private float distance = 6f;

        private void Start ()
        {
            DoLayout ();
        }

        private void OnDrawGizmos ()
		{
			Debug.DrawLine (transform.position, transform.position + Vector3.right * distance, Color.cyan);
		}

        public void Add (GameObject gameObject)
        {
            gameObjects.Add (gameObject);
            DoLayout ();
        }

        public void Remove (GameObject gameObject)
        {
            gameObjects.Remove (gameObject);
            DoLayout ();
        }

        public void RemoveAt (int index)
        {
            gameObjects.RemoveAt (index);
            DoLayout ();
        }

        public void Remove (Func<GameObject, bool> predicate)
        {
            for (int i = 0; i < gameObjects.Count; i++)
                if (predicate (gameObjects[i]))
                {
                    Destroy (gameObjects[i]);
                    gameObjects.RemoveAt (i);
                    i--;
                }
            DoLayout ();
        }

        private void DoLayout ()
        {
            Vector3 position = transform.position;
            Vector3 displacement = Vector3.right * (distance / gameObjects.Count);
            for (int i = 0; i < gameObjects.Count; i++)
            {
                gameObjects[i].transform.position = position;
                position += displacement;
            }
        }
    }
}