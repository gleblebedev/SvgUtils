using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace SvgUtils;

public static class AtlasBuilder
{
    private struct AtlasNode
    {
        public AtlasNode(Rect rect)
        {
            Rect = rect;
        }

        public Rect Rect;
    }


    public static IEnumerable<T> RotateItems<T>(this IEnumerable<T> items) where T : IAtlasItem
    {
        foreach (var item in items)
        {
            if (item.Rect.Size.X > item.Rect.Size.Y)
            {
                item.Rect = new Rect(item.Rect.Position, new Vector2(item.Rect.Size.Y, item.Rect.Size.X));
            }

            yield return item;
        }
    }

    private static int GetNextPow2(int v)
    {
        --v;
        v |= v >> 1;
        v |= v >> 2;
        v |= v >> 4;
        v |= v >> 8;
        v |= v >> 16;
        ++v;
        return v;
    }

    public static Rect BuildAtlas<T>(this IEnumerable<T> items) where T : IAtlasItem
    {
        List<T> sortedItems = items.OrderByDescending(_=>_.Rect.Size.Y).ToList();

        var totalArea = 0.0f;
        foreach (var sortedItem in sortedItems)
        {
            totalArea += sortedItem.Rect.Area;
        }

        var side = GetNextPow2((int)Math.Sqrt(totalArea));
        while (!FitItems(new Rect(new Vector2(side,side)), sortedItems))
        {
            side *= 2;
        }

        return new Rect(new Vector2(side, side));
    }

    public static bool BuildAtlas<T>(this IEnumerable<T> items, Rect area) where T : IAtlasItem
    {
        return FitItems(area, items.OrderByDescending(_=>_.Rect.Size.Y));
    }

    private static bool FitItems<T>(Rect rect, IEnumerable<T> sortedItems) where T : IAtlasItem
    {
        var leafs = new LinkedList<AtlasNode>();
        leafs.AddFirst(new AtlasNode(rect));
        foreach (var item in sortedItems)
        {
            if (!FitItem(leafs, item))
            {
                return false;
            }
        }

        return true;
    }

    private static bool FitItem<T>(LinkedList<AtlasNode> leafs, T item) where T : IAtlasItem
    {
        var leaf = leafs.First;
        while (leaf != null)
        {
            var leafRect = leaf.Value.Rect;
            var itemRect = item.Rect;
            if (itemRect.FitsInto(leafRect))
            {
                item.Rect = new Rect(leafRect.Position, itemRect.Size);
                var bottom = new AtlasNode(new Rect(leafRect.Position + new Vector2(0, itemRect.Size.Y), leafRect.Size - new Vector2(0, itemRect.Size.Y)));
                if (bottom.Rect.Area > float.Epsilon)
                    leafs.AddAfter(leaf, bottom);
                var right = new AtlasNode(new Rect(leafRect.Position + new Vector2(itemRect.Size.X, 0), new Vector2(leafRect.Size.X - itemRect.Size.X, itemRect.Size.Y)));
                if (right.Rect.Area > 0)
                    leafs.AddAfter(leaf, right);
                leafs.Remove(leaf);
                return true;
            }
            leaf = leaf.Next;
        }

        return false;
    }
}