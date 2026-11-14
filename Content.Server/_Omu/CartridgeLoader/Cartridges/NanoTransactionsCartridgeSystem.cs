
using Content.Server._DV.CartridgeLoader.Cartridges;
using Content.Server.CartridgeLoader;
using Content.Shared._Omu.CartridgeLoader.Cartridges;
using Content.Shared.CartridgeLoader;
using Content.Shared.PDA;
using Robust.Shared.Player;

namespace Content.Server._Omu.CartridgeLoader.Cartridges;

public sealed class NanoTransactionsCartridgeSystem : EntitySystem
{
    [Dependency] private readonly CartridgeLoaderSystem _cartridgeLoader = default!;
    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<NanoTransactionsCartridgeComponent, MapInitEvent>(OnMapInit);
        SubscribeLocalEvent<NanoTransactionsCartridgeComponent, CartridgeUiReadyEvent>(OnUiReady);
    }

    private void OnMapInit(Entity<NanoTransactionsCartridgeComponent> ent, ref MapInitEvent args)
    {
        if (!TryComp<PdaComponent>(ent, out var pda)
            || pda == null
            || pda.ContainedId == null)
            return;

        ent.Comp.ID = GetNetEntity(pda.ContainedId);
        ent.Comp.Balance = 100;

        UpdateAllCartridges();
    }

    private void OnUiReady(Entity<NanoTransactionsCartridgeComponent> ent, ref CartridgeUiReadyEvent args)
    {
        UpdateUI(ent, args.Loader);
    }

    private void UpdateUI(Entity<NanoTransactionsCartridgeComponent> ent, EntityUid loader)
    {
        if (ent.Comp.ID == null || TryComp<MetaDataComponent>(ent, out var selfMeta) || selfMeta == null)
            return;
        List<MetaDataComponent> players = new List<MetaDataComponent>();

        var playerQuery = EntityQueryEnumerator<MetaDataComponent, ActorComponent>();
        while (playerQuery.MoveNext(out var uid, out var playerMeta, out var actor))
        {
            if (GetNetEntity(uid) == ent.Comp.ID.Value)
                continue;
            if (playerMeta.EntityName == null)
                continue;

            players.Add(playerMeta);
        }

        // Send the UI state with balance and history
        var state = new NanoTransactionsUiState(
            Balance: ent.Comp.Balance,
            Players: players,
            Self: selfMeta,
            History: ent.Comp.History ?? []
        );

        _cartridgeLoader.UpdateCartridgeUiState(loader, state);
    }

    private void UpdateAllCartridges()
    {
        var query = EntityQueryEnumerator<NanoTransactionsCartridgeComponent, CartridgeComponent>();
        while (query.MoveNext(out var uid, out var comp, out var cartridge))
        {
            if (cartridge.LoaderUid is not { } loader || comp.ID == null)
                continue;
            UpdateUI((uid, comp), loader);
        }
    }
}
