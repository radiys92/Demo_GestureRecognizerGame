using System.Collections.Generic;
using UnityEngine;

namespace Model.Api
{
    public interface IGestureTemplatesModel
    {
        List<Vector2[]> GestureTemplates { get; }
    }
}