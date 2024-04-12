namespace Computerdores.AdvancedTerminalAPI; 

public delegate T Method<out T, in T2>(T2 input);