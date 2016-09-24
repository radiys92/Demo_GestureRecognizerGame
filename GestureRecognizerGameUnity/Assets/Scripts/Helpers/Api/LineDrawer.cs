using System;
using System.Linq;
using UnityEngine;

namespace Helpers.Api
{
    public class LineDrawer : MonoBehaviour,
        ILineDrawer
    {
        public LineRenderer UserFigureLine;
        public Transform UserLineParticle;

        public LineRenderer AiFigureLine;
        public Transform AILineParticle;

        private LineRenderer _lr = null;
        private Transform _particle = null;
        private int _pointsCount = 0;

        public void CreateLine(LineType lineType)
        {
            if (_lr!=null)
                DestroyLine();

            var prefab = (GameObject)null;
            var particlesPrefab = (GameObject)null;

            switch (lineType)
            {
                case LineType.AiLine:
                    prefab = AiFigureLine.gameObject;
                    particlesPrefab = AILineParticle.gameObject;
                    break;
                case LineType.UserLine:
                    prefab = UserFigureLine.gameObject;
                    particlesPrefab = UserLineParticle.gameObject;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lineType), lineType, null);
            }

            var go = GameObject.Instantiate(prefab);
            go.name = "~Line";
            _lr = go.GetComponent<LineRenderer>();
            _lr.SetVertexCount(0);
            _pointsCount = 0;

            if (particlesPrefab != null)
            {
                var pgo = GameObject.Instantiate(particlesPrefab);
                pgo.name = "~LineParticle";
                _particle = pgo.transform;
                pgo.SetActive(false);
            }
        }

        public void UpdateLinePoints(Vector3[] worldPoints)
        {
            if (_lr != null)
            {
                _lr.SetVertexCount(worldPoints.Length);
                _lr.SetPositions(worldPoints);
                _pointsCount = worldPoints.Length;
            }

            if (_particle != null && worldPoints.Length>1)
            {
                _particle.transform.position = worldPoints[worldPoints.Length-1];
                _particle.gameObject.SetActive(true);
            }
        }

        public void DestroyLine()
        {
            if (_lr != null)
            {
                Destroy(_lr.gameObject);
                _lr = null;
                _pointsCount = 0;
            }

            if (_particle != null)
            {
                DeleteParticleWhenFinished(_particle.GetComponent<ParticleSystem>());
                _particle = null;
            }
        }

        private void DeleteParticleWhenFinished(ParticleSystem ps)
        {
            var pp = new ParticleSystem.Particle[ps.maxParticles];
            var count = ps.GetParticles(pp);
            for (int i = 0; i < count; i++)
            {
                pp[i].lifetime = Mathf.Min(.3f, pp[i].lifetime);
            }
            ps.SetParticles(pp, count);
//            var lifetime = 0f;
//            for (var i = 0; i < count; i++)
//            {
//                if (pp[i].lifetime > lifetime)
//                    lifetime = pp[i].lifetime;
//            }
//            Destroy(_particle.gameObject, lifetime);
            Destroy(_particle.gameObject, .3f);
        }

        public void AddPoint(Vector3 worldPoint)
        {
            if (_lr != null)
            {
                _pointsCount ++;
                _lr.SetVertexCount(_pointsCount);
                _lr.SetPosition(_pointsCount - 1, worldPoint);
            }

            if (_particle != null)
            {
                _particle.transform.position = worldPoint;
                _particle.gameObject.SetActive(true);
            }
        }
    }
}