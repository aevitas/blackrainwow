An ObjectManager manages objects in the game World of Warcraft.

# Introduction #

The ObjectManager is relative to the ThreadLocalStorage (TLS). It can be obtained using a static offset, or a pattern method.


# Details #

Obtaining the TLS in C# is rather easy. Shynd has contributed the TLS pattern and masks to the community a while ago. Using them, we can find the Object Manager:

```
CConnection = Memory.ReadUInt(Memory.ReadUInt(ThreadLocalStorage + 0x16));
                    CConnectionOffset = Memory.ReadUInt(ThreadLocalStorage + 0x1C);
                    CurrentManager = Memory.ReadUInt(CConnection + CConnectionOffset);

private static string TLSPattern { get { return "EB 02 33 C0 8B D 00 00 00 00 64 8B 15 00 00 00 00 8B 34 8A 8B D 00 00 00 00 89 81 00 00 00 00"; } }
        private static string TLSMask { get { return "xxxxxx????xxx????xxxxx????xx????"; } }
```

CurrentManager is our pointer to the Object Manager here.
The following enum contains ranges relative to either the TLS, last object, or the CurrentManager:

```
public enum ObjectManager
{
   NextObject = 0x3C,
   FirstObject = 0xAC,
   LocalGUID = 0xC0
}
```

The first object can be found at **CurrentManager + FirstObject**, and every consecutive object at **CurrentObject + NextObject**.

Using a basic WowObject definition for the first object will suffice. Something like:
```
public class WowObject
{
   public uint BaseAddress { get; set; }

   public WowObject(uint baseAddress)
   {
      BaseAddress = baseAddress;
   }
}
```

Although not disclosing any information about the object, we can still use it to make a new object. (**new WowObject(uint baseAddress);**)

Make a list of WowObjects so we have something to work with first:
```
public readonly List<WowObject> Objects = new List<WowObject>();
```

After that, to get all objects in the ObjectManager:
```
var currentObject = new WowObject(Memory.ReadUInt(CurrentManager + (uint)Offsets.ObjectManager.FirstObject));

while (currentObject.BaseAddress != uint.MinValue && currentObject.BaseAddress % 2 == uint.MinValue)
{
   Objects.Add(new WowObject(currentObject.BaseAddress));
}
```

This won't get us anything, but a list of Objects. We won't be able to read any properties from it as it is, nor will we be able to classify the objects as for example; a Player or an Item.

To do that, we can use a switch, or a series of if statements. We'll go with a switch:
```
                    switch (currentObject.Type)
                    {
                        case (int)Offsets.WowObjectType.Unit:
                            Objects.Add(new WowUnit(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.Item:
                            Objects.Add(new WowItem(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.Container:
                            Objects.Add(new WowContainer(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.Corpse:
                            Objects.Add(new WowCorpseObject(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.GameObject:
                            Objects.Add(new WowGameObject(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.DynamicObject:
                            Objects.Add(new WowDynamicObject(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.Player:
                            Objects.Add(new WowPlayer(currentObject.BaseAddress));
                            break;
                    }
```

Basically you repeat the step above this one over and over again, and classify the objects accordingly.