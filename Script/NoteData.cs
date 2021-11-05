using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Notegrade
{
    perfact,
    great,
    miss
}

[System.Serializable]
public struct NoteInfo
{
    public Notegrade notegrade;
}

[CreateAssetMenu()]
public class NoteData : ScriptableObject
{

    public NoteInfo[] noteInfo;
    public string noteName;

}

public class StringValue : System.Attribute
{
    private string _value;

    public StringValue(string value)
    {
        _value = value;
    }

    public string Value
    {
        get { return _value; }
    }
}

public class StringEnum
{
    public static string GetStringValue(Notegrade value)
    {
        string output = null;

        StringValue[] attrs = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StringValue), false) as StringValue[];

        if (attrs.Length > 0)
        {
            output = attrs[0].Value;
        }

        value.GetType();

        return output;
    }

}