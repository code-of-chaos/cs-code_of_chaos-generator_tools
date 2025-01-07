﻿// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;

namespace CodeOfChaos.GeneratorTools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class GeneratorStringBuilder(int paddingChars = 4) {
    private readonly int _paddingChars = paddingChars > 0 ? paddingChars : 4;
    private readonly StringBuilder _builder = new();
    private int _indent;
    private int IndentAmount {
        get => _indent;
        set => _indent = value <= 0 ? 0 : value;
    }
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private GeneratorStringBuilder BuilderAction(Action action) {
        action();
        return this;
    }
    
    
    public GeneratorStringBuilder AppendUsings(string @using) => AppendLine($"using {@using};");
    public GeneratorStringBuilder AppendUsings(params string[] usings) => BuilderAction(() => {
        foreach (string @using in usings) AppendUsings(@using);
    });
    
    public GeneratorStringBuilder AppendMultipleUsings(params Func<IEnumerable<string>>[] usings) {
       string[] data = new HashSet<string>(usings.SelectMany(u => u())).ToArray(); 
       return AppendUsings(data);
    }
    
    public GeneratorStringBuilder AppendAutoGenerated() => AppendLineComment("<auto-generated />");
    public GeneratorStringBuilder AppendComment(string comment) => Append($" // {comment}");
    public GeneratorStringBuilder AppendLineComment(string comment) => AppendLine($"// {comment}");
    public GeneratorStringBuilder AppendNamespace(string name) => AppendLine($"namespace {name};");
    public GeneratorStringBuilder AppendNullableEnable() => AppendLine("#nullable enable");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Append (straight stringbuilder)
    public GeneratorStringBuilder Append(char c) => BuilderAction(() => _builder.Append(c));
    public GeneratorStringBuilder Append(string text) => BuilderAction(() => _builder.Append(text));
    #endregion    
    
    #region AppendLine methods(stringbuilder + indent)
    public GeneratorStringBuilder AppendLine() => BuilderAction(() => _builder.AppendLine());
    public GeneratorStringBuilder AppendLine(string text) => BuilderAction(() => _builder
        .Append(IndentString(IndentAmount))
        .AppendLine(text)
    );
    #endregion
    #region Auto Indented methods
    private string IndentString(int amount) => new(' ', amount * _paddingChars);

    public GeneratorStringBuilder Indent(Action<GeneratorStringBuilder> indentedAction) => BuilderAction(() => {
        IndentAmount++;
        indentedAction(this);
        IndentAmount--;
    });
    public GeneratorStringBuilder AppendLineIndented(string text) => Indent(g => g.AppendLine(text));
    
    public GeneratorStringBuilder AppendBody(string text) => BuilderAction(() => {
        string[] lines = text.Split(["\r\n", "\n"], StringSplitOptions.None);
        
        foreach (string line in lines) _builder
            .Append(IndentString(IndentAmount))
            .AppendLine(line);
    });
    public GeneratorStringBuilder AppendBodyIndented(string text) => Indent(g => g.AppendBody(text));
    #endregion
    
    #region ToString & Clear
    public override string ToString() => _builder.ToString();

    public string ToStringAndClear() {
        string result = ToString();
        Clear();
        return result;
    }

    public GeneratorStringBuilder Clear() {
        _builder.Clear();
        IndentAmount = 0;
        return this;
    }
    #endregion
}
