
using Robust.Shared.Input.Binding;
using Content.Shared.Input;
using Robust.Shared.Player;
using Content.Shared.Body.Components;
using Robust.Shared.Physics.Components;
using Content.Shared.Atmos;
using Content.Server.Atmos.EntitySystems;
using Robust.Server.Audio;
using Content.Shared.YARtech;
using Content.Server.Popups;
using Robust.Shared.Random;

namespace Content.Server.YARtech;

internal sealed class FartSystem : EntitySystem
{
    [Dependency] private readonly AtmosphereSystem _atmosphere = default!;
    [Dependency] private readonly AudioSystem _audio = default!;
    [Dependency] private readonly PopupSystem _popups = default!;
    [Dependency] private readonly IRobustRandom _random = default!;

    public override void Initialize()
    {
        base.Initialize();
        CommandBinds.Builder.Bind(ContentKeyFunctions.Fart, InputCmdHandler.FromDelegate(PlayerFart)).Register<FartSystem>();
        //SubscribeLocalEvent<FartComponent, FartEvent>(OnFart);
    }


    private void PlayerFart(ICommonSession? session)
    {
        if (session != null && session.AttachedEntity.HasValue)
        {
            var ent = session.AttachedEntity.Value;
            if (TryComp(ent, out FartComponent? fart))
            {
                Fart(new(ent, fart));
            }
        }
    }

    //private void OnFart(Entity<FartComponent> uid, ref FartEvent args)
    //{
    //    Fart(uid);
    //}

    public void Fart(Entity<FartComponent> ent)
    {
        if (!TryComp<PhysicsComponent>(ent.Owner, out var physics) || !HasComp<BodyComponent>(ent.Owner))
            return;

        var tileMix = _atmosphere.GetTileMixture(ent.Owner, excite: true);
        tileMix?.AdjustMoles(Gas.Ammonia, ent.Comp.FartAmount * physics.FixturesMass);

        if (!_random.Prob(ent.Comp.SoundProb))
            return;

        _audio.PlayPvs(ent.Comp.FartSound, ent.Owner);

        if (!_random.Prob(ent.Comp.PopupProb))
            return;

        _popups.PopupEntity(Loc.GetString("fart-" + (int)(_random.NextFloat() * 9 + 1), ("name", ent.Owner)), ent.Owner);


    }


}
//[Serializable]
//[NetSerializable]
//public sealed class FartEvent : CancellableEntityEventArgs
//{
//}
