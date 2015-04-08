This thread will help you getting started with BlackRain, setting up a sample project, setting up references, and populating the ObjectManager.

# Introduction #

First off, download the latest stable release from the downloads page. If you prefer to include the source code in your project, download it from the SVN using an SVN client such as Tortoise SVN. However, in this tutorial we'll assume you have downloaded the binaries.

This tutorial also assumes you have the following:
  * Visual Studio 2010 (Any edition)
  * Microsoft .NET Framework 4.0
  * At least basic knowledge of C#/C-style coding


# Details #

**Referencing BlackRain**
To use BlackRain's objects and API, we'll need to set up a reference to the binary file, and later on to its namespaces.

In your project, go to _Project -> Add Reference_, and select BlackRainObjects.dll. As BlackRain's DLL references BlackMagic, and fasm\_managed, there is no need to set up a reference to these. You are also able to fully operate BlackRain's Memory connection to World of Warcraft though the API.

Once you have done so, set up a reference to the _Common.Objects_ namespace, as following:
```
using BlackRain.Common.Objects;
```

You want to place this before any namespace declarations in the project and file you wish to use BlackRain in.

Now that we have done this, we are able to use BlackRain completely. BlackRain was designed to be easily accessible, without any class instantiations required.

The following block of code assumes you have a _ListBox_ named **lst\_Objects**, and you want to fetch all **WowObjects**. (All Objects inherit from WowObject, so all objects will be shown.)

We'll initialize the ObjectManager first, and attach it to the first Wow Process we find:

```
readonly Process[] _wowProc = Process.GetProcessesByName("Wow");
ObjectManager.Initialize(_wowProc[0].Id); // Attach to the first Wow process found.
```

Now, we want to ensure all objects are fetched. This can only be done when logged in, and in-game. To do so, we call the Pulse(); method:

```
ObjectManager.Pulse();
```

Now that all objects are properly classified, we can add the meat to this tutorial. This is actually quite simple, and only requires one foreach statement:

```
foreach (WowObject obj in ObjectManager.Objects)
{
     lst_Objects.Items.Add(string.Format("GUID: {0} | Entry: {1} | Type: {2}", obj.GUID, obj.Entry, obj.Type));
}
```

And there we go, all objects are now in lst\_Objects, stating their GUIDs, Entries and Types.

Now extend it, and forge something useful out of it! :)