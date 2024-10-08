﻿using Blasphemous.ModdingAPI;
using Blasphemous.ModdingAPI.Files;
using System.Collections.Generic;
using UnityEngine;

namespace Blasphemous.LostDreams.Animation;

public class AnimationStorage
{
    private readonly Dictionary<string, AnimationInfo> _animations = new();

    public AnimationInfo this[string name] => _animations.TryGetValue(name, out var info)
        ? info : throw new System.Exception($"Animation {name} was never loaded");

    public AnimationStorage(FileHandler file)
    {
        string infoPath = "animations.json";
        if (!file.LoadDataAsJson(infoPath, out AnimationImportInfo[] imports))
        {
            ModLog.Error("Failed to load animation list");
            return;
        }

        var options = new SpriteImportOptions()
        {
            Pivot = new Vector2(0.5f, 0)
        };

        foreach (var import in imports)
        {
            if (!file.LoadDataAsFixedSpritesheet(import.FilePath, new Vector2(import.Width, import.Height), out Sprite[] spritesheet, options))
            {
                ModLog.Error($"Failed to load {import.Name} from {import.FilePath}");
                continue;
            }

            _animations.Add(import.Name, new AnimationInfo(import.Name, spritesheet, import.SecondsPerFrame));
        }

        ModLog.Info($"Loaded {_animations.Count} animations");
    }
}
