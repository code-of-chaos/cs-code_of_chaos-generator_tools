// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;

namespace Tests.CodeOfChaos.GeneratorTools.Backports;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class StackBackportsTests {
    [Test]
    public async Task TryPop_EmptyStack_ReturnsFalse_AndOutNull() {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        var stack = new Stack<int>();

        // Act
        bool result = StackBackports.TryPopBackport(stack,out int value);

        // Assert
        await Assert.That(result).IsFalse().Because("TryPop should return false for an empty stack.");
        await Assert.That(value).IsEqualTo(0).Because("Out value should be default for an empty stack.");
    }

    [Test]
    public async Task TryPop_NonEmptyStack_ReturnsTrue_AndOutTopElement() {
        // Arrange
        var stack = new Stack<int>();
        stack.Push(10);
        stack.Push(20);
        stack.Push(30);// Top element

        // Act
        bool result = StackBackports.TryPopBackport(stack,out int value);

        // Assert
        await Assert.That(result).IsTrue().Because("TryPop should return true for a non-empty stack.");
        await Assert.That(value).IsEqualTo(30).Because("Out value should be the top element of the stack.");
        await Assert.That(stack.Count).IsEqualTo(2).Because("Stack count should be decremented by 1 after TryPop.");
    }
    
    [Test]
    public async Task TryPeek_EmptyStack_ReturnsFalse_AndOutNull() {
        // Arrange
        // ReSharper disable once CollectionNeverUpdated.Local
        var stack = new Stack<string>();

        // Act
        bool result = StackBackports.TryPeekBackport(stack, out string? value);

        // Assert
        await Assert.That(result).IsFalse().Because("TryPeek should return false for an empty stack.");
        await Assert.That(value).IsNull().Because("Out value should be null for an empty stack.");
        await Assert.That(stack.Count).IsEqualTo(0).Because("Stack count should be unchanged for an empty stack.");
    }
    
    [Test]
    public async Task TryPeek_NonEmptyStack_ReturnsTrue_AndOutTopElement() {
        // Arrange
        var stack = new Stack<string>();
        stack.Push("first");
        stack.Push("second");
        stack.Push("top"); // Top element

        // Act
        bool result = StackBackports.TryPeekBackport(stack, out string? value);

        // Assert
        await Assert.That(result).IsTrue().Because("TryPeek should return true for a non-empty stack.");
        await Assert.That(value).IsEqualTo("top").Because("Out value should be the top element of the stack.");
        await Assert.That(stack.Count).IsEqualTo(3).Because("Stack count should be unchanged for a non-empty stack.");
    }
    
    [Test]
    public async Task TryPeekWithNullables_AllowsNullValueHandling() {
        // Arrange
        var stack = new Stack<int?>(); // Stack of nullable ints
        stack.Push(null);
        stack.Push(10);

        // Act
        bool result = StackBackports.TryPeekBackport(stack, out int? value);

        // Assert
        await Assert.That(result).IsTrue().Because("TryPeek should return true for a non-empty stack (nullable).");
        await Assert.That(value).IsEqualTo(10).Because("Out value should not be null for a non-empty stack (nullable).");
        await Assert.That(stack.Count).IsEqualTo(2).Because("Stack count should be unchanged for a non-empty stack (nullable).");
    }
    
    [Test]
    public async Task TryPeek_WithMutatingCollectionAfterPeek() {
        // Arrange
        var stack = new Stack<int>();
        stack.Push(42);

        // Act
        bool resultPeek = StackBackports.TryPeekBackport(stack, out int resultPeekValue);
        bool resultPop = StackBackports.TryPopBackport(stack,out int resultPopValue);

        // Assert
        await Assert.That(resultPeek).IsTrue().Because("TryPeek should succeed.");
        await Assert.That(resultPeekValue).IsEqualTo(42).Because("TryPeek should return the top element of the stack.");
        await Assert.That(resultPop).IsTrue().Because("TryPop should succeed after TryPeek.");
        await Assert.That(resultPopValue).IsEqualTo(42).Because("Stack item should be properly popped after peek.");
    }
    
}
