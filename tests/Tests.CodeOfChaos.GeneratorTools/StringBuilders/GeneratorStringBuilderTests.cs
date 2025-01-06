﻿// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;

namespace Tests.CodeOfChaos.GeneratorTools.StringBuilders;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GeneratorStringBuilderTests {
    [Test]
    public async Task Append_ShouldAppendTextWithoutIndent() {
        // Arrange
        var generator = new GeneratorStringBuilder();

        // Act
        generator.Append("Hello");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo("Hello");
    }

    [Test]
    public async Task AppendLine_ShouldAppendTextWithNewLine() {
        // Arrange
        var generator = new GeneratorStringBuilder();

        // Act
        generator.AppendLine("Hello");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo($"Hello{Environment.NewLine}");
    }

    [Test]
    public async Task IndentAndAppend_ShouldIndentTextBasedOnPadding() {
        // Arrange
        var generator = new GeneratorStringBuilder(2);// Padding of 2 spaces per indent level
        generator.Indent();

        // Act
        generator.Append("Indented Text");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo("  Indented Text");
    }

    [Test]
    public async Task MultipleIndentations_ShouldApplyCorrectIndentLevels() {
        // Arrange
        var generator = new GeneratorStringBuilder(2);// Padding of 2 spaces per indent level
        generator.Indent().Indent();

        // Act
        generator.Append("Double Indented Text");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo("    Double Indented Text");
    }

    [Test]
    public async Task UnIndent_ShouldReduceIndentation() {
        // Arrange
        var generator = new GeneratorStringBuilder(2);
        generator.Indent().Indent();

        // Act
        generator.UnIndent();
        generator.Append("Text");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo("  Text");
    }

    [Test]
    public async Task AppendAutoGenerated_ShouldAddGeneratedComment() {
        // Arrange
        var generator = new GeneratorStringBuilder();

        // Act
        generator.AppendAutoGenerated();

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo($"// <auto-generated />{Environment.NewLine}");
    }

    [Test]
    public async Task AppendUsings_ShouldAddUsingDeclarations() {
        // Arrange
        var generator = new GeneratorStringBuilder();

        // Act
        generator.AppendUsings("System", "System.Text");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo($"using System;{Environment.NewLine}using System.Text;{Environment.NewLine}");
    }

    [Test]
    public async Task Clear_ShouldResetBuilderAndIndentation() {
        // Arrange
        var generator = new GeneratorStringBuilder();
        generator.AppendLine("Hello");
        generator.Indent();

        // Act
        generator.Clear();
        generator.Append("New Start");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo("New Start");
    }

    [Test]
    public async Task ToStringAndClear_ShouldReturnStringAndClear() {
        // Arrange
        var generator = new GeneratorStringBuilder();
        generator.Append("Hello");

        // Act
        string result = generator.ToStringAndClear();

        // Assert
        await Assert.That(result).IsEqualTo("Hello");
        await Assert.That(generator.ToString()).IsEqualTo(string.Empty);
    }

    [Test]
    public async Task IndentLine_ShouldIndentAndAddLine() {
        // Arrange
        var generator = new GeneratorStringBuilder();

        // Act
        generator.IndentLine("Indented Line");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo($"    Indented Line{Environment.NewLine}");
    }

    [Test]
    public async Task UnIndentLine_ShouldReduceIndentAndAddLine() {
        // Arrange
        var generator = new GeneratorStringBuilder();
        generator.Indent().Indent();

        // Act
        generator.UnIndentLine("Unindented Line");

        // Assert
        await Assert.That(generator.ToString()).IsEqualTo($"    Unindented Line{Environment.NewLine}");
    }

    [Test]
    public async Task IndentAction_ShouldIndentAndExecuteAction() {
        // Arrange
        var generator = new GeneratorStringBuilder();
        
        // Act
        generator.Indent(g => g.AppendLine("Indented Text"));
        
        // Assert
        await Assert.That(generator.ToString()).IsEqualTo($"    Indented Text{Environment.NewLine}");
    }

    [Test]
    public async Task IndentAction_ShouldIndentAndExecuteAction_MultipleTimes() {
        // Arrange
        var generator = new GeneratorStringBuilder();
        
        // Act
        generator.Indent(g => g.AppendLine("Indented Text"));
        generator.Indent(g => g
            .AppendLine("Something")
            .Indent(g2 => g2.AppendLine("Else"))
        );
        
        // Assert
        await Assert.That(generator.ToString()).IsEqualTo($"    Indented Text{Environment.NewLine}    Something{Environment.NewLine}        Else{Environment.NewLine}");
    }
}
