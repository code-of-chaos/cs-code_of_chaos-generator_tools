// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using System.Diagnostics.CodeAnalysis;

namespace CodeOfChaos.GeneratorTools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// Simple wrapper to make testing not a living hell.
public class SymbolResolver(SemanticModel model) {
    [UsedImplicitly]
    public ISymbol? ResolveSymbol<TSyntaxNode>(TSyntaxNode node) where TSyntaxNode : SyntaxNode => model.GetSymbolInfo(node).Symbol;

    [UsedImplicitly]
    public bool TryResolveSymbol<TSyntaxNode>(TSyntaxNode node, [NotNullWhen(true)] out ISymbol? symbol) where TSyntaxNode : SyntaxNode {
        symbol = ResolveSymbol(node);
        return symbol != null;
    }

    [UsedImplicitly]
    public bool TryResolveSymbol<TSyntaxNode, TSymbol>(TSyntaxNode node, [NotNullWhen(true)] out TSymbol? symbol) where TSyntaxNode : SyntaxNode where TSymbol : class, ISymbol {
        ISymbol? resolved = ResolveSymbol(node);
        symbol = resolved as TSymbol;
        return symbol != null;
    }
}
