using UnityEditor;
using UnityEngine;

public class SortHierarchy : MonoBehaviour
{
    [MenuItem("Tools/Sort Selected GameObject Children Alphabetically")]
    static void SortSelectedGameObjectChildren()
    {
        GameObject selectedGameObject = Selection.activeGameObject;

        if (selectedGameObject != null)
        {
            SortChildren(selectedGameObject);
        }
        else
        {
            Debug.LogWarning("No GameObject selected. Please select a GameObject to sort its children.");
        }
    }

    static void SortChildren(GameObject parent)
    {
        int childCount = parent.transform.childCount;
        Transform[] children = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = parent.transform.GetChild(i);
        }

        // 按名稱排序
        System.Array.Sort(children, (Transform t1, Transform t2) => string.Compare(t1.name, t2.name));

        // 更新排序
        for (int i = 0; i < childCount; i++)
        {
            children[i].SetSiblingIndex(i);
        }

        Debug.Log($"Sorted {childCount} children of {parent.name} alphabetically.");
    }
}
