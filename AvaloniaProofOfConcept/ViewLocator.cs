using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace AvaloniaProofOfConcept;

public class ViewLocator : IDataTemplate
{
    public Control Build(object param)
    {
        var typeName = param?.GetType().FullName?.Replace("ViewModel", "View").Replace("ViewModels","Views");
        if (string.IsNullOrEmpty(typeName))
        {
            return new TextBlock { Text = "Not Found: " + typeName };
        }

        var type = Type.GetType(typeName);
        if (type == null)
        {
            return new TextBlock { Text = "Not Found: " + typeName };
        }

        return (Control)Activator.CreateInstance(type);
    }

    public bool Match(object data)
    {
        return true;
    }
}