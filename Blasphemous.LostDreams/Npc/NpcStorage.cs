﻿using Blasphemous.ModdingAPI;
using Blasphemous.ModdingAPI.Files;
using System.Collections.Generic;

namespace Blasphemous.LostDreams.Npc;

public class NpcStorage
{
    private readonly Dictionary<string, NpcInfo> _npcs = new();

    public NpcInfo this[string id] => _npcs.TryGetValue(id, out var info)
        ? info : throw new System.Exception($"Npc {id} was never loaded");

    public NpcStorage(FileHandler file)
    {
        string infoPath = "npcs.json";
        if (!file.LoadDataAsJson(infoPath, out NpcInfo[] imports))
        {
            ModLog.Error("Failed to load npc list");
            return;
        }

        foreach (var import in imports)
        {
            _npcs.Add(import.Id, import);
        }

        ModLog.Info($"Loaded {_npcs.Count} npcs");
    }
}
