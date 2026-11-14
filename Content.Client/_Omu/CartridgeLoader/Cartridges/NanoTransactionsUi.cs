using Content.Client.UserInterface.Fragments;
using Content.Shared._Omu.CartridgeLoader.Cartridges;
using Robust.Client.UserInterface;

namespace Content.Client._Omu.CartridgeLoader.Cartridges;

public sealed partial class NanoTransactionsUI : UIFragment
{
    private NanoTransactionsUiFragment? _fragment;

    public override Control GetUIFragmentRoot()
    {
        return _fragment!;
    }

    public override void UpdateState(BoundUserInterfaceState state)
    {
        if (state is NanoTransactionsUiState cast)
            _fragment?.UpdateState(cast);
    }

    public override void Setup(BoundUserInterface userInterface, EntityUid? fragmentOwner)
    {
        _fragment = new NanoTransactionsUiFragment();
    }
}
