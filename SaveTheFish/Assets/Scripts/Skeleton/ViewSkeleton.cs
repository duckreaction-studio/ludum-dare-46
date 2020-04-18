using UnityEngine;

namespace Skeleton
{
    public class ViewSkeleton : MonoBehaviour
    {

        public Transform rootNode;
        public Transform[] childNodes;
        public float greenCubeScale = 0.05f;
        public float blueCubeScale = 0.01f;

        void OnDrawGizmosSelected()
        {
            if (rootNode != null)
            {
                if (childNodes == null || childNodes.Length == 0)
                {
                    //get all joints to draw
                    PopulateChildren();
                }


                foreach (Transform child in childNodes)
                {

                    if (child == rootNode)
                    {
                        //list includes the root, if root then larger, green cube
                        Gizmos.color = Color.green;
                        Gizmos.DrawCube(child.position, Vector3.one * greenCubeScale);
                    }
                    else
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(child.position, child.parent.position);
                        Gizmos.DrawCube(child.position, Vector3.one * blueCubeScale);
                    }
                }

            }
        }

        public void PopulateChildren()
        {
            childNodes = rootNode.GetComponentsInChildren<Transform>();
        }
    }
}