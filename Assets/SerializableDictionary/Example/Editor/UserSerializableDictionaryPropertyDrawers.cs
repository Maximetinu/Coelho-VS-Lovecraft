using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(StringStringDictionary))]
[CustomPropertyDrawer(typeof(ObjectColorDictionary))]
[CustomPropertyDrawer(typeof(StringSoundSetDictionary))]
// [CustomPropertyDrawer(typeof(StringListAudioClipDictionary))] NOT WORKING
public class AnySerializableDictionaryPropertyDrawer : SerializableDictionaryPropertyDrawer {}
