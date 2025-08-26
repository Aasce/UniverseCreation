using System.Collections.Generic;
using UnityEngine;

namespace Asce.Managers.Utils
{
	/// <summary>
	///     Provides utility extension methods for working with <see cref="Transform"/> objects,
	///     including functionality to destroy child objects and specific transforms.
	/// </summary>
    public static class TransformUtils
    {
		/// <summary>
		///     Destroys all direct child <see cref="Transform"/> objects of the specified
		///     <paramref name="transform"/> immediately.
		/// </summary>
		/// <param name="transform">
		///     The parent transform whose child objects should be destroyed.
		/// </param>
		/// <remarks>
		///     This method uses <see cref="GameObject.DestroyImmediate(Object)"/> and should be used
		///     with caution, as it cannot be undone and will execute immediately in both edit and play mode.
		/// </remarks>
        public static void DestroyChildren(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
		
		/// <summary>
		///     Destroys the <see cref="GameObject"/> instances of the specified list of <see cref="Transform"/> objects immediately.
		/// </summary>
		/// <param name="list">
		///     The list of transforms whose game objects should be destroyed.
		///     Null or empty lists are ignored.
		/// </param>
		/// <remarks>
		///     This method uses <see cref="GameObject.DestroyImmediate(Object)"/> and should be used
		///     with caution, as it will execute immediately in both edit and play mode.
		/// </remarks>
        public static void DestroyTransforms(List<Transform> list)
        {
            if (list == null || list.Count == 0) return;

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] != null)
                {
                    GameObject.DestroyImmediate(list[i].gameObject);
                }
            }
        }
    }
}