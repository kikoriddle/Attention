using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseControl : MonoBehaviour
{
        // Reference to cursor sprite if you want to change it
        public SpriteRenderer cursorSprite;
        // Optional offset if you need to adjust cursor position
        public Vector2 cursorOffset;

        private static mouseControl instance;

        private void Awake()
        {
            // Ensure only one cursor exists
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Cursor.visible = false;
        }

        private void Update()
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(
                mousePosition.x + cursorOffset.x,
                mousePosition.y + cursorOffset.y,
                0
            );
        }

}
