using Robust.Shared.Prototypes;
using Content.Shared._Omu.CartridgeLoader.Cartridges;

namespace Content.Server._Omu.CartridgeLoader.Cartridges;

[RegisterComponent, Access(typeof(NanoTransactionsCartridgeSystem))]
public sealed partial class NanoTransactionsCartridgeComponent : Component
{
    [IdDataField]
    public NetEntity? ID { get; set; } = null;

    public List<NanoTransaction>? History;

    public int Balance;
}


