using System.Collections.Generic;
using System.Linq;
using Helpers;
using Model.Api;
using UnityEngine;

namespace Model.Impl
{
    public class GestureTemplatesModel : IGestureTemplatesModel
    {
        private const string TemplatesAssetPath = "GestureTemplates";

        public List<Vector2[]> GestureTemplates { get; private set; }

        public GestureTemplatesModel()
        {
            var templates = Resources.Load<GestureTemplates>(TemplatesAssetPath);
            if (templates == null)
            {
                Debug.LogErrorFormat("Can't load gesture templates from path '{0}'", TemplatesAssetPath);
                GestureTemplates = new List<Vector2[]>();
            }
            else
            {
                GestureTemplates = new List<Vector2[]>(templates.Templates.Select(i => i.points));
            }
        }
    }
}