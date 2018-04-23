using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class StringStringDictionary : SerializableDictionary<string, string> {}

[Serializable]
public class ObjectColorDictionary : SerializableDictionary<UnityEngine.Object, Color> {}

[Serializable]
public class StringSoundSetDictionary : SerializableDictionary<string, AudioClip> { }

// NOT WORKING
// [Serializable]
// public class StringListAudioClipDictionary : SerializableDictionary<string, List<AudioClip>> {}