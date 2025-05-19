// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
using Microsoft.CodeAnalysis;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace Tests.CodeOfChaos.GeneratorTools.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class INamedTypeSymbolExtensionsTests {
    
    [Test]
    public async Task GetTypeKind_RecordStruct_ReturnsRecordStruct() {
        // Arrange
        var mockSymbol = new Mock<INamedTypeSymbol>();
        mockSymbol.Setup(s => s.IsRecord).Returns(true);
        mockSymbol.Setup(s => s.TypeKind).Returns(TypeKind.Struct);

        // Act
        string result = mockSymbol.Object.GetTypeKind();

        // Assert
        await Assert.That(result).IsEqualTo("record struct");
    }

    [Test]
    public async Task GetTypeKind_RecordClass_ReturnsRecord() {
        // Arrange
        var mockSymbol = new Mock<INamedTypeSymbol>();
        mockSymbol.Setup(s => s.IsRecord).Returns(true);
        mockSymbol.Setup(s => s.TypeKind).Returns(TypeKind.Class);

        // Act
        string result = mockSymbol.Object.GetTypeKind();

        // Assert
        await Assert.That(result).IsEqualTo("record");
    }

    [Test]
    public async Task GetTypeKind_Class_ReturnsClass() {
        // Arrange
        var mockSymbol = new Mock<INamedTypeSymbol>();
        mockSymbol.Setup(s => s.IsRecord).Returns(false);
        mockSymbol.Setup(s => s.TypeKind).Returns(TypeKind.Class);

        // Act
        string result = mockSymbol.Object.GetTypeKind();

        // Assert
        await Assert.That(result).IsEqualTo("class");
    }

    [Test]
    public async Task GetTypeKind_Struct_ReturnsStruct() {
        // Arrange
        var mockSymbol = new Mock<INamedTypeSymbol>();
        mockSymbol.Setup(s => s.IsRecord).Returns(false);
        mockSymbol.Setup(s => s.TypeKind).Returns(TypeKind.Struct);

        // Act
        string result = mockSymbol.Object.GetTypeKind();

        // Assert
        await Assert.That(result).IsEqualTo("struct");
    }

    [Test]
    public async Task GetTypeKind_Interface_ReturnsInterface() {
        // Arrange
        var mockSymbol = new Mock<INamedTypeSymbol>();
        mockSymbol.Setup(s => s.TypeKind).Returns(TypeKind.Interface);

        // Act
        string result = mockSymbol.Object.GetTypeKind();

        // Assert
        await Assert.That(result).IsEqualTo("interface");
    }

    [Test]
    public async Task GetTypeKind_Enum_ReturnsEnum() {
        // Arrange
        var mockSymbol = new Mock<INamedTypeSymbol>();
        mockSymbol.Setup(s => s.TypeKind).Returns(TypeKind.Enum);

        // Act
        string result = mockSymbol.Object.GetTypeKind();

        // Assert
        await Assert.That(result).IsEqualTo("enum");
    }

}
