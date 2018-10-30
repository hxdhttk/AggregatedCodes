using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Reflection;

public static class Reflection
{
    public static string InvokeMethod (string typeName)
    {
        if (typeName == null || typeName == "")
        {
            return typeName;
        }

        var type = Type.GetType(typeName);
        if (type == null)
        {
            return null;
        }

        var methodToInvoke = type.GetMethods ().Where (m => !m.IsVirtual).First ();
        return (string) methodToInvoke.Invoke (FormatterServices.GetUninitializedObject(type), null);
    }
}