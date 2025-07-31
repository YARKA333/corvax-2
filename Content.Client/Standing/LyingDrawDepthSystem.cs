using Content.Shared.Standing;
using Content.Shared.Climbing.Components;
using Content.Shared.Climbing.Events;
using Robust.Client.GameObjects;
using DrawDepth = Content.Shared.DrawDepth.DrawDepth;


namespace Content.Client.Standing
{
    public sealed class LyingDrawDepthSystem : EntitySystem
    {
        [Dependency] StandingStateSystem _standing = default!;

        public override void Initialize()
        {
            base.Initialize();

            SubscribeLocalEvent<ClimbingComponent, StartClimbEvent>(OnClimbStart);
            SubscribeLocalEvent<ClimbingComponent, EndClimbEvent>(OnClimbEnd);
            SubscribeLocalEvent<StandingStateComponent, StoodEvent>(OnStood);
            SubscribeLocalEvent<StandingStateComponent, DownedEvent>(OnDowned);
        }

        private void OnClimbStart(EntityUid uid, ClimbingComponent component, ref StartClimbEvent args)
        {
            UpdateDrawDepth(uid);
        }

        private void OnClimbEnd(EntityUid uid, ClimbingComponent component, ref EndClimbEvent args)
        {
            UpdateDrawDepth(uid);
        }

        private void OnStood(Entity<StandingStateComponent> uid, ref StoodEvent args)
        {
            UpdateDrawDepth(uid);
        }

        private void OnDowned(Entity<StandingStateComponent> uid, ref DownedEvent args)
        {
            UpdateDrawDepth(uid);
        }

        private void UpdateDrawDepth(EntityUid uid)
        {
            if (!TryComp(uid, out StandingStateComponent? standingState)
             || !standingState.CanLieDown
             || !TryComp(uid, out SpriteComponent? sprite))
                return;
            var low = _standing.IsDown(uid, standingState);
            if (TryComp(uid, out ClimbingComponent? climbing))
            {
                low = low && !climbing.IsClimbing;
            }
            sprite.DrawDepth = (int)(low ? DrawDepth.SmallMobs : DrawDepth.Mobs);
        }
    }
}
