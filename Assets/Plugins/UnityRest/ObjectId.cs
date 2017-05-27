using System;

namespace UnityRest
{
    [Serializable]
    public class ObjectId
    {
        public string value;

        public ObjectId (string value)
        {
            this.value = value;
        }

        public static implicit operator string(ObjectId id)
        {
            return id.value;
        }
        
        public static implicit operator ObjectId(string value)
        {
            return new ObjectId (value);
        }
    }
}