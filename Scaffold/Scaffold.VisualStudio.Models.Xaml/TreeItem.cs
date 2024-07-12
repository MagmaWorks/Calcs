﻿using System.Runtime.Serialization;
using System.Windows;
using Microsoft.VisualStudio.Extensibility.UI;
using Scaffold.VisualStudio.Models.Results;
using Scaffold.VisualStudio.Models.Scaffold;

namespace Scaffold.VisualStudio.Models.Xaml;

[DataContract]
public class TreeItem : NotifyPropertyChangedObject
{
    private bool _isExpanded;
    private string _name;

    private TreeItem()
    {
        ChangeTreeItemExpansionCommand = new AsyncCommand((_, _, _) =>
        {
            IsExpanded = !IsExpanded;
            return Task.CompletedTask;
        });
    }

    public TreeItem(CalculationResult result) : this()
    {
        Name = result.IsSuccess ? result.CalculationDetail.Title : result.Failure.Source ?? "Unhandled exception";
        AssemblyQualifiedTypeName = result.AssemblyQualifiedTypeName;
        IsExpanded = false;

        SetLists(result);
    }

    public TreeItem(ErrorDetail error) : this()
    {
        Name = "Run failed";
        Error = error;
        IsExpanded = true;
    }
    
    [DataMember] public string AssemblyQualifiedTypeName { get; set; }
    [DataMember] public ObservableList<CalcValueDetail> Inputs { get; } = [];
    [DataMember] public ObservableList<CalcValueDetail> Outputs { get; } = [];
    [DataMember] public ObservableList<DisplayFormula> Formulae { get; } = [];
    [DataMember] public ErrorDetail Error { get; }
    
    [DataMember] public AsyncCommand ChangeTreeItemExpansionCommand { get; set; }

    [DataMember]
    public bool IsExpanded
    {
        get => _isExpanded;
        set => SetProperty(ref _isExpanded, value);
    }

    [DataMember]
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private void SetLists(CalculationResult result)
    {
        foreach (var input in result.CalculationDetail.Inputs)
            Inputs.Add(input);

        foreach (var output in result.CalculationDetail.Outputs)
            Outputs.Add(output);

        foreach (var formula in result.CalculationDetail.Formulae)
        {
            var newFormula = new DisplayFormula
            {
                Ref = formula.Ref,
                Narrative = formula.Narrative,
                Conclusion = formula.Conclusion,
                Status = formula.Status,
                Image = formula.Image,
                Expressions = []
            };

            if (formula.Expressions != null)
                foreach (var expression in formula.Expressions)
                    newFormula.Expressions.Add(expression);
            
            Formulae.Add(newFormula);
        }
    }

    public void SetExpanderState(bool alwaysExpandCalculations, TreeItem existingTreeItem)
    {
        if (alwaysExpandCalculations)
        {
            IsExpanded = true;
        }
        else
        {
            IsExpanded = existingTreeItem is { IsExpanded: true };
        }
    }
}