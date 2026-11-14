using Robust.Shared.Serialization;

namespace Content.Shared._Omu.CartridgeLoader.Cartridges;

[Serializable, NetSerializable]
public sealed class NanoTransactionsUiState(
    List<NanoTransaction> History,
    List<MetaDataComponent> Players,
    MetaDataComponent Self,
    int Balance
    ) : BoundUserInterfaceState
{
    public readonly List<NanoTransaction> History = History;
    public readonly List<MetaDataComponent> Players = Players;
    public readonly MetaDataComponent Self = Self;
    public readonly int Balance = Balance;
}
public struct NanoTransaction
{
    public readonly string _sender;
    public readonly string _receiver;
    public readonly int _amount;

    public NanoTransaction(string sender, string receiver, int amount)
    {
        _sender = sender;
        _receiver = receiver;
        _amount = amount;
    }
}
