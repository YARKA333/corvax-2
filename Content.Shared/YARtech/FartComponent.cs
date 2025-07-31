using Robust.Shared.Audio;

namespace Content.Shared.YARtech;

[RegisterComponent]
public sealed partial class FartComponent : Component
{
    [DataField]
    public SoundSpecifier? FartSound { get; private set; } = new SoundCollectionSpecifier("Farts");

    [DataField]
    public float FartAmount = 0.01f;

    [DataField]
    public float SoundProb = 0.5f;

    [DataField]
    public float PopupProb = 0.5f;
}
