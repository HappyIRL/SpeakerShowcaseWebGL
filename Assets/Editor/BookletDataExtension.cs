using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    [CustomEditor(typeof(BookletContainer))]
    public class BookletDataExtension : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (BookletContainer)target;

            if (GUILayout.Button("Add Page", GUILayout.Height(40)))
            {
                script.IncrementDatas();
            }

        }
    }
}
