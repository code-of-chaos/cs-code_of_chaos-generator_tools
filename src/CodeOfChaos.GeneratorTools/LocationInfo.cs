// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace CodeOfChaos.GeneratorTools;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class LocationInfo {
    public string FilePath { get; private set; } = null!;
    public TextSpan TextSpan { get; private set; }
    public LinePositionSpan LinePositionSpan { get; private set; }
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static LocationInfo? From<T>(T node) where T : SyntaxNode 
        => From(node.GetLocation());
    
    public static LocationInfo? From(Location location) {
        if (location.SourceTree is null) return null;
        
        string filePath = location.SourceTree.FilePath;
        TextSpan span = location.SourceSpan;
        LinePositionSpan lineSpan = location.GetLineSpan().Span;
        
        return new LocationInfo {
            FilePath = filePath,
            TextSpan = span,
            LinePositionSpan = lineSpan,
        };
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Location ToLocation()
        => Location.Create(FilePath, TextSpan, LinePositionSpan);
    
}

