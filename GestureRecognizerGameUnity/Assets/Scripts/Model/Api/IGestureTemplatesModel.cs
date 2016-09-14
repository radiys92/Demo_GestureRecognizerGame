using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public interface IGestureTemplatesModel
    {
        List<Vector2[]> GestureTemplates { get; }
    }
}