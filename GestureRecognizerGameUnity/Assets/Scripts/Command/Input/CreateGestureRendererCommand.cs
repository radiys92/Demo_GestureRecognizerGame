using GCon;
using Model;
using strange.extensions.command.impl;
using UnityEngine;

namespace Logic.Commands
{
    public class CreateGestureRendererCommand : Command
    {
        [Inject]
        public Gesture G { get; private set; }

        [Inject]
        public IGameFlowModel Model { get; private set; }

        private static readonly Color[] Colors = {Color.red, Color.green};

        private static int _colorInd = 0; // dirty hack :)

        public override void Execute()
        {
            Model.LineRenderers.Add(CreateLine());
        }

        private LineRenderer CreateLine()
        {
            var go = new GameObject("~Line");
            var lr = go.AddComponent<LineRenderer>();
            var mat = new Material(new Material(Shader.Find("MonoColor/OpaqueNLitColor")))
            {
                color = Colors[_colorInd%Colors.Length]
            };
            _colorInd++;
            lr.material = mat;
            lr.SetWidth(.1f,.1f);

            lr.SetVertexCount(1);
            var pos = Camera.main.ScreenToWorldPoint(G.StartPoint);
            pos.z = 0;
            lr.SetPosition(0, pos);

            Model.LineRenderers.Add(lr);

            return lr;
        }
    }
}