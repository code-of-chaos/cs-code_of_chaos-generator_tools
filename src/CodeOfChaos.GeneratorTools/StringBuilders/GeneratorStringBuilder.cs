﻿// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeOfChaos.GeneratorTools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
/// <summary>
///     A utility class for building text-based generators, providing methods to construct consistent
///     and formatted output such as namespace declarations, using statements, and indented text bodies.
/// </summary>
public class GeneratorStringBuilder(int paddingChars = 4) {
    /// <summary>
    ///     An instance of <see cref="StringBuilder" /> used internally to construct the string content
    ///     within the <see cref="GeneratorStringBuilder" /> class.
    /// </summary>
    private readonly StringBuilder _builder = new();

    /// <summary>
    ///     Represents the number of characters used for indentation or padding in the generated output.
    ///     This value must be a positive integer, representing spaces per level of indentation. Defaults to 4.
    /// </summary>
    private readonly int _paddingChars = paddingChars > 0 ? paddingChars : 4;

    /// <summary>
    ///     Stores the current level of indentation for the <see cref="GeneratorStringBuilder" />.
    ///     This value determines the number of indentation levels applied when appending formatted outputs.
    /// </summary>
    private int _indent;

    /// <summary>
    ///     Gets or sets the number of indentation levels applied in the generated output.
    /// </summary>
    internal int IndentAmount {
        get => _indent;
        private set => _indent = value <= 0 ? 0 : value;
    }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    /// <summary>
    ///     Executes a provided action and returns the current instance of the `GeneratorStringBuilder`
    ///     to allow for method chaining.
    /// </summary>
    /// <param name="action">
    ///     The action to be executed. This is typically a lambda or delegate that performs operations
    ///     on the internal `StringBuilder` or other components of the `GeneratorStringBuilder`.
    /// </param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    internal GeneratorStringBuilder BuilderAction(Action action) {
        action();
        return this;
    }

    /// <summary>
    ///     Appends a 'using' directive to the current string builder.
    /// </summary>
    /// <param name="using">The name of the namespace to be added as a 'using' directive.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendUsings(string @using) => AppendLine($"using {@using};");

    /// <summary>
    ///     Appends the specified using directives to the string builder.
    /// </summary>
    /// <param name="usings">An array of "using" directives to add to the string builder.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendUsings(params string[] usings) => AppendMultipleUsings(usings);

    /// <summary>
    ///     Appends multiple "using" directives to the string builder. Duplicates are automatically removed.
    /// </summary>
    /// <param name="usings">
    ///     An array of collections containing the "using" directives to be added. Each element in the array is an enumerable
    ///     collection of strings representing namespaces.
    /// </param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendMultipleUsings(params IEnumerable<string>[] usings) => ForEach(
        new HashSet<string>(usings.SelectMany(u => u)),
        itemFormatter: (builder, s) => builder.AppendUsings(s)
    );

    /// <summary>
    ///     Appends an auto-generated comment indicating generated code to the builder.
    /// </summary>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendAutoGenerated() => AppendLineComment("<auto-generated />");

    /// <summary>
    ///     Appends a comment to the internal StringBuilder.
    /// </summary>
    /// <param name="comment">The comment to append.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" /> to allow method chaining.</returns>
    public GeneratorStringBuilder AppendComment(string comment) => Append($" // {comment}");

    /// <summary>
    ///     Appends a single-line comment to the string builder, prefixed by "//".
    /// </summary>
    /// <param name="comment">The content of the comment to be appended.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendLineComment(string comment) => AppendLine($"// {comment}");

    /// <summary>
    ///     Appends a namespace declaration to the string builder.
    /// </summary>
    /// <param name="name">The name of the namespace to append.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendNamespace(string name) => AppendLine($"namespace {name};");

    /// <summary>
    ///     Appends the `#nullable enable` directive to the string builder.
    /// </summary>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendNullableEnable() => AppendLine("#nullable enable");

    #region Append (straight stringbuilder)
    /// <summary>
    ///     Appends a single character to the current string being built by the <see cref="GeneratorStringBuilder" />.
    /// </summary>
    /// <param name="c">The character to append to the string.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder Append(char c) => BuilderAction(() => _builder.Append(c));
    /// <summary>
    ///     Appends the specified text to the current string builder instance.
    /// </summary>
    /// <param name="text">The text to append.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder Append(string text) => BuilderAction(() => _builder.Append(text));
    #endregion

    #region AppendLine methods(stringbuilder + indent)
    /// <summary>
    ///     Appends a line terminator to the end of the current string content.
    /// </summary>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendLine() => BuilderAction(() => _builder.AppendLine());
    
    /// <summary>
    ///     Appends the specified text to the StringBuilder instance, followed by a newline character,
    ///     and applies the current indentation level.
    /// </summary>
    /// <param name="text">The text to be appended.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" /> with the appended text.</returns>
    public GeneratorStringBuilder AppendLine(string text) => BuilderAction(() => _builder
        .Append(IndentString(IndentAmount))
        .AppendLine(text)
    );
    #endregion

    #region Auto Indented methods
    /// <summary>
    ///     Generates an indentation string based on the specified indentation level and predefined padding character count.
    /// </summary>
    /// <param name="amount">The level of indentation, typically representing the number of indentation units.</param>
    /// <returns>A string containing the appropriate number of spaces for the specified indentation level.</returns>
    internal string IndentString(int amount) => new(' ', amount * _paddingChars);

    /// <summary>
    ///     Indents the subsequent appending of text or actions by a specified level.
    /// </summary>
    /// <param name="indentedAction">
    ///     An action that specifies the content to be appended, which is indented to match the current indentation level.
    /// </param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder Indent(Action<GeneratorStringBuilder> indentedAction) => BuilderAction(() => {
        IndentAmount++;
        indentedAction(this);
        IndentAmount--;
    });
    /// <summary>
    ///     Appends a line of text with the current indentation.
    ///     The method adds the given text to the builder, prepended with the calculated indentation spaces.
    /// </summary>
    /// <param name="text">The line of text to append, indented according to the current indentation level.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendLineIndented(string text) => Indent(g => g.AppendLine(text));

    /// <summary>
    ///     Appends the provided text to the body of the string builder with the current indentation applied.
    /// </summary>
    /// <param name="text">The text to be appended to the body.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendBody(string text) => BuilderAction(() => {
        string indent = IndentString(IndentAmount);// Cache the indent string
        int start = 0;
        for (int i = 0; i < text.Length; i++) {
            if (text[i] != '\r' && text[i] != '\n') continue;

            _builder.Append(indent).AppendLine(text.Substring(start, i - start));

            if (text[i] == '\r' && i + 1 < text.Length && text[i + 1] == '\n') { i++; }
            start = i + 1;
        }

        // Append the last line if text does not end with a newline
        if (start < text.Length) {
            _builder.Append(indent).AppendLine(text.Substring(start));
        }
    });

    /// <summary>
    ///     Appends the specified text to the builder, applying the current indentation level.
    /// </summary>
    /// <param name="text">The text to append, which will respect the current indentation level.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder AppendBodyIndented(string text) => Indent(g => g.AppendBody(text));
    #endregion

    #region ForEach
    /// <summary>
    ///     Appends each string from the provided collection as a new line to the internal StringBuilder.
    /// </summary>
    /// <param name="items">The collection of strings to append as lines.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder ForEachAppendLine(IEnumerable<string> items) => ForEach(items, itemFormatter: (g, item) => g.AppendLine(item));

    /// <summary>
    ///     Iterates over a collection of items and appends a formatted string representation of each item as a new line.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="items">The collection of items to format and append.</param>
    /// <param name="itemFormatter">
    ///     A delegate function that takes an individual item and returns its string representation to append.
    /// </param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder ForEachAppendLine<T>(IEnumerable<T> items, Func<T, string> itemFormatter) => ForEach(
        items,
        itemFormatter: (g, item) => g.AppendLine(itemFormatter(item))
    );

    /// <summary>
    ///     Appends each item from the provided collection as a new indented line to the internal StringBuilder instance.
    /// </summary>
    /// Appends each item from the provided collection as a new indented line to the internal StringBuilder instance.
    /// <param name="items">The collection of strings to append as indented lines.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder ForEachAppendLineIndented(IEnumerable<string> items) => ForEach(items, itemFormatter: (g, item) => g.AppendLineIndented(item));

    /// <summary>
    ///     Appends each item in the collection as an indented line using the specified formatting function.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the collection.</typeparam>
    /// <param name="items">The collection of items to be appended.</param>
    /// <param name="itemFormatter">
    ///     A function that takes an item from the collection and returns its formatted string
    ///     representation.
    /// </param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder ForEachAppendLineIndented<T>(IEnumerable<T> items, Func<T, string> itemFormatter) => ForEach(
        items,
        itemFormatter: (g, item) => g.AppendLineIndented(itemFormatter(item))
    );

    /// <summary>
    ///     Appends the content of each item in the provided enumerable to the builder
    ///     by formatting them as body content.
    /// </summary>
    /// <param name="items">The collection of strings to be appended as body content.</param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder ForEachAppendBody(IEnumerable<string> items) => ForEach(items, itemFormatter: (g, item) => g.AppendBody(item));
    
    /// <summary>
    ///     Iterates over the specified enumerable collection and appends the formatted body content for each item
    ///     using the provided item formatter function.
    /// </summary>
    /// <typeparam name="T">The type of the items in the enumerable collection.</typeparam>
    /// <param name="items">The collection of items to iterate over.</param>
    /// <param name="itemFormatter">
    ///     A function that formats each item into a string representation. This formatted value is appended as body content
    ///     using <see cref="AppendBody" />.
    /// </param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder ForEachAppendBody<T>(IEnumerable<T> items, Func<T, string> itemFormatter) => ForEach(
        items,
        itemFormatter: (g, item) => g.AppendBody(itemFormatter(item))
    );

    /// <summary>
    ///     Iterates through a collection of items, applying a specified action for each item.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    /// <param name="items">The collection of items to iterate over.</param>
    /// <param name="itemFormatter">
    ///     An action to perform for each item in the collection. The action takes a
    ///     <see cref="GeneratorStringBuilder" /> instance and an item of type <typeparamref name="T" /> as parameters.
    /// </param>
    /// <returns>The current instance of <see cref="GeneratorStringBuilder" />, allowing for method chaining.</returns>
    public GeneratorStringBuilder ForEach<T>(IEnumerable<T> items, Action<GeneratorStringBuilder, T> itemFormatter) => BuilderAction(() => {
        if (items is ICollection<T> { Count: 0 }) return;// Skip iteration if no items

        foreach (T item in items) itemFormatter(this, item);
    });
    #endregion

    #region ToString & Clear
    /// <summary>
    /// Converts the content of the GeneratorStringBuilder instance to a string representation.
    /// </summary>
    /// <returns>
    ///     A string that represents the current content of the GeneratorStringBuilder instance.
    /// </returns>
    public override string ToString() => _builder.ToString();

    /// <summary>
    /// Returns the current string representation of the builder and clears its content.
    /// </summary>
    /// <return>The string representation of the builder before it was cleared.</return>
    public string ToStringAndClear() {
        string result = ToString();
        Clear();
        return result;
    }

    /// <summary>
    /// </summary>
    /// Clears the internal string builder and resets the indentation amount to zero.
    /// <return>An instance of the current GeneratorStringBuilder class with its state cleared.</return>
    public GeneratorStringBuilder Clear() {
        _builder.Clear();
        IndentAmount = 0;
        return this;
    }
    #endregion
}
