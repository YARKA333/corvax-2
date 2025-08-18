using Robust.Shared.Audio;

namespace Content.Server.YARtech.Halt;

[RegisterComponent]
public sealed partial class HaltComponent : Component
{
    [DataField("color"), ViewVariables(VVAccess.ReadWrite)]
    public string ChatColor { get; private set; } = Color.Red.ToHex();

    [DataField("locale"), ViewVariables(VVAccess.ReadWrite)]
    public string ChatLoc { get; private set; } = "chat-manager-entity-say-hailer-wrap-message";

    [DataField("actionEntity")] public EntityUid? ActionEntity;

    public readonly Dictionary<string, SoundSpecifier> PhraseToSoundMap = new()
    {
        ["halt-phrase"]       = new SoundPathSpecifier("/Audio/White/Halt/halt.ogg"),
        ["bobby-phrase"]      = new SoundPathSpecifier("/Audio/White/Halt/bobby.ogg"),
        ["compliance-phrase"] = new SoundPathSpecifier("/Audio/White/Halt/compliance.ogg"),
        ["justice-phrase"]    = new SoundPathSpecifier("/Audio/White/Halt/justice.ogg"),
        ["running-phrase"]    = new SoundPathSpecifier("/Audio/White/Halt/running.ogg"),
        ["dontmove-phrase"]   = new SoundPathSpecifier("/Audio/White/Halt/dontmove.ogg"),
        ["floor-phrase"]      = new SoundPathSpecifier("/Audio/White/Halt/floor.ogg"),
        ["robocop-phrase"]    = new SoundPathSpecifier("/Audio/White/Halt/robocop.ogg"),
        ["freeze-phrase"]     = new SoundPathSpecifier("/Audio/White/Halt/freeze.ogg"),
        ["imperial-phrase"]   = new SoundPathSpecifier("/Audio/White/Halt/imperial.ogg"),
        ["bash-phrase"]       = new SoundPathSpecifier("/Audio/White/Halt/bash.ogg"),
        ["harry-phrase"]      = new SoundPathSpecifier("/Audio/White/Halt/harry.ogg"),
        ["asshole-phrase"]    = new SoundPathSpecifier("/Audio/White/Halt/asshole.ogg"),
        ["stfu-phrase"]       = new SoundPathSpecifier("/Audio/White/Halt/stfu.ogg"),
        ["shutup-phrase"]     = new SoundPathSpecifier("/Audio/White/Halt/shutup.ogg"),
        ["dredd-phrase"]      = new SoundPathSpecifier("/Audio/White/Halt/dredd.ogg")
    };
}
