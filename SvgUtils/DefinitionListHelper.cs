using System;
using System.Collections.Generic;
using Svg;

namespace SvgUtils;

public class DefinitionListHelper
{
    private readonly SvgDefinitionList _definitionList;
    private readonly HashSet<string> _ids = new HashSet<string>();
    private int _counter = 1;
    private string _prefix = "def";

    public DefinitionListHelper(SvgDefinitionList definitionList)
    {
        _definitionList = definitionList;
        foreach (var child in definitionList.Children)
        {
            if (!string.IsNullOrWhiteSpace(child.ID))
                _ids.Add(child.ID);
        }
    }

    public Uri Add(SvgElement element)
    {
        return Add(element, _prefix);
    }

    public Uri Add(SvgElement element, string baseId)
    {
        _definitionList.Children.Add(element);
        if (_ids.Add(baseId))
        {
            element.ID = baseId;
            return new Uri($"url(#{baseId})", UriKind.Relative);
        }
        for (;;)
        {
            var id = $"{baseId}{_counter}";
            ++_counter;
            if (_ids.Add(id))
            {
                element.ID = id;
                return new Uri($"url(#{id})", UriKind.Relative);
            }
        }
    }
}