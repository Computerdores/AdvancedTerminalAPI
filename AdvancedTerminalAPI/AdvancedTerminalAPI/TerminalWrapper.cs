using System.Collections.Generic;
using Computerdores.AdvancedTerminalAPI.patch;

namespace Computerdores.AdvancedTerminalAPI; 

public class TerminalWrapper {
    private readonly Terminal _terminal;

    private TerminalNode _lastLoadedNode;

    internal bool redirectLoadNewNode;

    public bool TerminalInUse => _terminal.terminalInUse;
    
    private TerminalWrapper(Terminal terminal) {
        _terminal = terminal;
        #region events
        TerminalPatch.OnEnterTerminal += OnEnterTerminal;
        TerminalPatch.PreAwake += OnPreAwake;
        TerminalPatch.PostAwake += OnPostAwake;
        TerminalPatch.PreStart += OnPreStart;
        TerminalPatch.PostStart += OnPostStart;
        TerminalPatch.PreUpdate += OnPreUpdate;
        TerminalPatch.PostUpdate += OnPostUpdate;
        #endregion
    }

    public TerminalNode LoadNode(TerminalNode node) {
        if (node.itemCost != 0 || node.buyRerouteToMoon == -2)
            _terminal.totalCostOfItems = node.itemCost * _terminal.playerDefinedAmount;
        TerminalNode n = node;
        if (node.buyItemIndex != -1 || node.buyRerouteToMoon != -1 && node.buyRerouteToMoon != -2 || node.shipUnlockableID != -1)
            n = VanillaLoad(node, _terminal.LoadNewNodeIfAffordable);
        else if (node.creatureFileID != -1)
            n = VanillaLoad(node, _terminal.AttemptLoadCreatureFileNode);
        else if (node.storyLogFileID != -1)
            n = VanillaLoad(node, _terminal.AttemptLoadStoryLogFileNode);
        else {
            LoadNewNode(node);
        }
        return n;
    }

    private TerminalNode VanillaLoad(TerminalNode node, Consumer<TerminalNode> loadMethod) {
        // prepare for execution
        _lastLoadedNode = null;
        redirectLoadNewNode = true;
        // execute vanilla method
        loadMethod(node);
        // undo prev set
        redirectLoadNewNode = false;
        return _lastLoadedNode;
    }

    internal void LoadNewNode(TerminalNode node) {
        _terminal.RunTerminalEvents(node);
        _terminal.screenText.interactable = true;
        _lastLoadedNode = node;
        if (node.playSyncedClip != -1)
            _terminal.PlayTerminalAudioServerRpc(node.playSyncedClip);
        else if (node.playClip != null)
            _terminal.terminalAudio.PlayOneShot(node.playClip);
        _terminal.LoadTerminalImage(node);
    }
    
    #region events
    // ReSharper disable EventNeverSubscribedTo.Global
    public event Consumer<bool> EnterTerminal; 
    public event SimpleEvent PreAwake;
    public event SimpleEvent PostAwake;
    public event SimpleEvent PreStart;
    public event SimpleEvent PostStart;
    public event SimpleEvent PreUpdate;
    public event SimpleEvent PostUpdate;
    // ReSharper restore EventNeverSubscribedTo.Global

    private void OnEnterTerminal((bool firstTime, Terminal terminal) t) {
        if (t.terminal == _terminal)
            EnterTerminal?.Invoke(t.firstTime);
    }

    private void OnPreAwake(Terminal terminal) {
        if (terminal == _terminal)
            PreAwake?.Invoke();
    }

    private void OnPostAwake(Terminal terminal) {
        if (terminal == _terminal)
            PostAwake?.Invoke();
    }

    private void OnPreStart(Terminal terminal) {
        if (terminal == _terminal)
            PreStart?.Invoke();
    }

    private void OnPostStart(Terminal terminal) {
        if (terminal == _terminal)
            PostStart?.Invoke();
    }

    private void OnPreUpdate(Terminal terminal) {
        if (terminal == _terminal)
            PreUpdate?.Invoke();
    }

    private void OnPostUpdate(Terminal terminal) {
        if (terminal == _terminal)
            PostUpdate?.Invoke();
    }
    #endregion
    
    private static readonly Dictionary<Terminal, TerminalWrapper> WrapperMap = new();
    public static TerminalWrapper Get(Terminal terminal) {
        if (WrapperMap.TryGetValue(terminal, out TerminalWrapper wrapper))
            return wrapper;
        WrapperMap[terminal] = new TerminalWrapper(terminal);
        return WrapperMap[terminal];
    }

    
}