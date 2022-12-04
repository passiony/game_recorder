    using UnityEngine;

    public static class RecordUtility
    {
        public static Vector3 ToVector3(this RVector3 rv)
        {
            return new Vector3(rv.x, rv.y, rv.z);
        }
        
        public static RVector3 ToRVector3(this Vector3 v)
        {
            return new RVector3(v.x, v.y, v.z);
        }

    }
